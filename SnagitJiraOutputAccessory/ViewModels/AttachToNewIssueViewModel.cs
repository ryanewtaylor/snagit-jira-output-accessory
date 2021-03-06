﻿namespace SnagitJiraOutputAccessory.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Atlassian.Jira;
    using SnagitJiraOutputAccessory.Commands;
    using SnagitJiraOutputAccessory.Models;

    using SNAGITLib;

    using Clipboard = System.Windows.Clipboard;

    public class AttachToNewIssueViewModel : ViewModelBase
    {
        private ISnagIt _snagit;
        private Jira _jira;
        private OutputPreferencesRepository _preferencesRepo;

        private readonly CommandHandler<string> _attachCommand;

        public AttachToNewIssueViewModel(ISnagIt snagit, Jira jira, OutputPreferencesRepository preferencesRepo)
        {
            _snagit = snagit;
            _jira = jira;
            _filename = DefaultFileName();
            _preferencesRepo = preferencesRepo; // TODO: Maybe consider making a PreferencesWriter/Reader?
            var prefs = _preferencesRepo.Read();
            _rememberProject = !string.IsNullOrWhiteSpace(prefs.LastProjectKey);

            ValidateProject(_selectedProject);
            ValidateIssueType(_selectedIssueType);
            ValidateSummary(_summary);
            ValidateFileName(_filename);
            
            _attachCommand = new CommandHandler<string>(s => AttachIssue(), s => !HasErrors);

            Projects = _jira.GetProjects().ToList();
            SelectedProject = prefs.LastProjectKey ?? Projects.First().Key;

            UpdateIssueTypesForProject(SelectedProject);
        }

        private void UpdateIssueTypesForProject(string projectKey)
        {
            IssueTypes = _jira.GetIssueTypes(projectKey).ToList();
            SelectedIssueType = IssueTypes.First().Id;
        }

        private string DefaultFileName()
        {
            ISnagItDocument2 doc = _snagit.SelectedDocument as ISnagItDocument2;

            string extension = Path.GetExtension(doc.DocumentPath);
            if (extension == ".SNAG")
            {
                string filePath = Path.GetTempFileName();
                return Path.GetFileName(filePath);
            }

            return Path.GetFileName(doc.DocumentPath);
        }

        // http://blog.excastle.com/2010/07/25/mvvm-and-dialogresult-with-no-code-behind/
        // http://blog.excastle.com/2010/07/25/mvvm-and-dialogresult-with-no-code-behind/
        private bool? _dialogResult;
        public bool? DialogResult
        {
            get { return _dialogResult; }
            set
            {
                _dialogResult = value;
                this.OnPropertyChanged();
            }
        }

        private List<Project> _projects;
        public List<Project> Projects
        {
            get { return _projects; }
            private set
            {
                _projects = value;
                this.OnPropertyChanged();
            }
        }

        private List<IssueType> _issueTypes;
        public List<IssueType> IssueTypes
        {
            get { return _issueTypes; }
            private set
            {
                _issueTypes = value;
                this.OnPropertyChanged();
            }
        }

        private string _selectedProject = "";
        public string SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                if (!String.Equals(_selectedProject, value))
                {
                    _selectedProject = value;
                    OnPropertyChanged();
                    ValidateProject(_selectedProject);
                    _attachCommand.RaiseCanExecuteChanged();
                    UpdateIssueTypesForProject(_selectedProject);
                    if (RememberProject) SetProjectKeyToRemember(_selectedProject);
                }
            }
        }

        public void ValidateProject(string project)
        {
            if (String.IsNullOrWhiteSpace(project))
            {
                base.AddErrors("SelectedProject", "Project cannot be blank");
            }
            else
            {
                base.RemoveErrors("SelectedProject");
            }
        }

        private string _selectedIssueType = "";
        public string SelectedIssueType
        {
            get { return _selectedIssueType; }
            set
            {
                if (!String.Equals(_selectedIssueType, value))
                {
                    _selectedIssueType = value;
                    OnPropertyChanged();
                    ValidateIssueType(_selectedIssueType);
                    _attachCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public void ValidateIssueType(string issueType)
        {
            if (String.IsNullOrWhiteSpace(issueType))
            {
                base.AddErrors("SelectedIssueType", "Issue type cannot be blank");
            }
            else
            {
                base.RemoveErrors("SelectedIssueType");
            }
        }

        private string _filename = "";
        public string Filename
        {
            get { return _filename; }
            set
            {
                if (!String.Equals(_filename, value))
                {
                    _filename = value;
                    OnPropertyChanged();
                    ValidateFileName(_filename);
                    _attachCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public void ValidateFileName(string filename)
        {
            if (String.IsNullOrWhiteSpace(filename)
                || filename.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                base.AddErrors("Filename", "Filename has invalid characters.");
            }
            else
            {
                base.RemoveErrors("Filename");
            }
        }

        private string _summary = "";
        public string Summary
        {
            get { return _summary; }
            set
            {
                if (!String.Equals(_summary, value))
                {
                    _summary = value;
                    OnPropertyChanged();
                    ValidateSummary(_summary);
                    _attachCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public void ValidateSummary(string summary)
        {
            if (String.IsNullOrWhiteSpace(summary))
            {
                base.AddErrors("Summary", "Summary cannot be blank");
            }
            else
            {
                base.RemoveErrors("Summary");
            }
        }

        private bool _rememberProject = false;
        public bool RememberProject
        {
            get { return _rememberProject; }
            set
            {
                if (_rememberProject != value)
                {
                    _rememberProject = value;
                    this.OnPropertyChanged();

                    var projectKey = _rememberProject ? SelectedProject : null;
                    SetProjectKeyToRemember(projectKey);
                }
            }
        }

        private void SetProjectKeyToRemember(string projectKey)
        {
            var prefs = _preferencesRepo.Read();
            prefs.LastProjectKey = projectKey;
            _preferencesRepo.Write(prefs);
        }

        public System.Windows.Input.ICommand AttachCommand
        {
            get
            {
                return _attachCommand;
            }
        }

        private void AttachIssue()
        {
            try
            {
                // TODO: The saving image logic should be injected as IImageSaver/Helper/ToByter
                ISnagItImageDocumentSave saveableDoc = _snagit.SelectedDocument as ISnagItImageDocumentSave;
                string tempFileName = Path.ChangeExtension(Path.GetTempFileName(), ".png");
                saveableDoc.SaveToFile(tempFileName, snagImageFileType.siftPNG, null);
                byte[] imageBytes = File.ReadAllBytes(tempFileName);

                string newFilename = String.IsNullOrWhiteSpace(_filename)
                    ? Path.GetFileName(tempFileName)
                    : Path.GetFileNameWithoutExtension(_filename) + ".png";

                UploadAttachmentInfo attachmentInfo = new UploadAttachmentInfo(newFilename, imageBytes);
                Issue issue = _jira.CreateIssue(SelectedProject);
                issue.Summary = Summary;
                issue.Type = SelectedIssueType;
                issue.SaveChanges();
                issue.AddAttachment(attachmentInfo);

                string issueUrl = string.Format("{0}browse/{1}", _jira.Url, issue.Key);
                Clipboard.SetText(issueUrl);

                string title = string.Format("{0} attached to {1}", newFilename, issue.Key);
                SnagitJiraOutputAccessory.Commands.ICommand openWebPageCommand = new SnagitJiraOutputAccessory.Commands.OpenWebPageCommand(issueUrl);
                var notifier = new SnagitJiraOutputAccessory.Models.UploadCompleteNotification();
                notifier.Notify(title, issueUrl, openWebPageCommand);
            }
            catch (Exception ex)
            {
                // TODO: Cleanly notify user of any errors
            }
            finally
            {
                DialogResult = true;
            }
        }
    }
}
