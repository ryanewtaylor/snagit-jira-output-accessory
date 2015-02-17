namespace SnagitJiraOutputAccessory.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Atlassian.Jira;
    using SnagitJiraOutputAccessory.Commands;
    using SNAGITLib;

    using Clipboard = System.Windows.Clipboard;

    public class AttachToNewIssueViewModel : ViewModelBase
    {
        private ISnagIt _snagit;
        private Jira _jira;

        private readonly CommandHandler<string> _attachCommand;

        public AttachToNewIssueViewModel(ISnagIt snagit, Jira jira)
        {
            _snagit = snagit;
            _jira = jira;
            _filename = DefaultFileName();

            ValidateProject(_selectedProject);
            ValidateIssueType(_selectedIssueType);
            ValidateSummary(_summary);
            ValidateFileName(_filename);

            _attachCommand = new CommandHandler<string>(s => AttachIssue(), s => !HasErrors);

            Projects = _jira.GetProjects().ToList();

            var defaultProject = _projects[0];
            SelectedProject = defaultProject.Name;
            IssueTypes = _jira.GetIssueTypes(defaultProject.Key).ToList();
            var defaultIssueType = _issueTypes[0];
            SelectedIssueType = defaultIssueType.Name;

            //var issueTypes = _jira.GetIssueTypes();
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
                    _attachCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public void ValidateProject(string project)
        {
            if (String.IsNullOrWhiteSpace(project))
            {
                base.AddErrors("Must select a project", "Project cannot be blank");
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
                    _attachCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public void ValidateIssueType(string issueType)
        {
            if (String.IsNullOrWhiteSpace(issueType))
            {
                base.AddErrors("Must select an issue type", "Issue type cannot be blank");
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
                    _attachCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public void ValidateSummary(string summary)
        {
            if (String.IsNullOrWhiteSpace(summary))
            {
                base.AddErrors("Must enter a summary", "Summary cannot be blank");
            }
            else
            {
                base.RemoveErrors("Summary");
            }
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
            /*
            // TODO: The saving image logic should be injected as IImageSaver/Helper/ToByter
            ISnagItImageDocumentSave saveableDoc = _snagit.SelectedDocument as ISnagItImageDocumentSave;
            string tempFileName = Path.ChangeExtension(Path.GetTempFileName(), ".png");
            saveableDoc.SaveToFile(tempFileName, snagImageFileType.siftPNG, null);
            byte[] imageBytes = File.ReadAllBytes(tempFileName);

            string newFilename = String.IsNullOrWhiteSpace(_filename)
                ? Path.GetFileName(tempFileName)
                : Path.GetFileNameWithoutExtension(_filename) + ".png";

            UploadAttachmentInfo attachmentInfo = new UploadAttachmentInfo(newFilename, imageBytes);
            Issue issue = _jira.GetIssue(SelectedIssue);
            issue.AddAttachment(attachmentInfo);

            if (!String.IsNullOrWhiteSpace(_comment))
            {
                issue.AddComment(_comment);
            }

            string issueUrl = string.Format("{0}browse/{1}", _jira.Url, SelectedIssue);
            Clipboard.SetText(issueUrl);

            string title = string.Format("{0} attached to {1}", newFilename, SelectedIssue);
            SnagitJiraOutputAccessory.Commands.ICommand openWebPageCommand = new SnagitJiraOutputAccessory.Commands.OpenWebPageCommand(issueUrl);
            UploadCompleteNotification notifier = new UploadCompleteNotification();
            notifier.Notify(title, issueUrl, openWebPageCommand);
            */

            DialogResult = true;
        }
    }
}
