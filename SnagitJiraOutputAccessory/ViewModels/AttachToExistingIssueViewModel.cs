namespace SnagitJiraOutputAccessory.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using Atlassian.Jira;
    using SnagitJiraOutputAccessory.Models;
    using SNAGITLib;

    public class AttachToExistingIssueViewModel : INotifyPropertyChanged
    {
        private ISnagIt _snagit;
        private Jira _jira;

        public AttachToExistingIssueViewModel(ISnagIt snagit, Jira jira)
        {
            _snagit = snagit;
            _jira = jira;

            IEnumerable<JiraNamedEntity> myJiraFilters = jira.GetFilters();
            _filters = new List<JiraNamedEntity>(myJiraFilters);

            if (_filters.Count > 0)
            {
                SelectedFilter = _filters[0].Name;
            }
        }

        private void UpdateIsuesList()
        {
            IEnumerable<Issue> myJiraIssues = _jira.GetIssuesFromFilter(_selectedFilter);
            Issues = new List<Issue>(myJiraIssues);

            if (Issues.Count > 0)
            {
                SelectedIssue = Issues[0].Key.Value;
            }
        }

        private IList<JiraNamedEntity> _filters;
        public IList<JiraNamedEntity> Filters
        {
            get { return _filters; }
            set
            {
                _filters = value;
                OnPropertyChanged();
            }
        }

        private IList<Issue> _issues;
        public IList<Issue> Issues
        {
            get { return _issues; }
            set
            {
                _issues = value;
                OnPropertyChanged();
            }
        }

        private string _selectedFilter = "";
        public string SelectedFilter 
        {
            get { return _selectedFilter; }
            set 
            {
                if (!String.Equals(_selectedFilter, value))
                {
                    _selectedFilter = value;
                    UpdateIsuesList();
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedIssue = "";
        public string SelectedIssue
        {
            get { return _selectedIssue; }
            set
            {
                if (!String.Equals(_selectedIssue, value))
                {
                    _selectedIssue = value;
                    OnPropertyChanged();
                }
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
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ICommand _attachCommand;
        public ICommand AttachCommand
        {
            get
            {
                return _attachCommand ?? (_attachCommand = new CommandHandler(() => AttachIssue(), _canExecute));
            }
        }

        private bool _canExecute = true;
        private void AttachIssue()
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
            Issue issue = _jira.GetIssue(SelectedIssue);
            issue.AddAttachment(attachmentInfo);

            string issueUrl = string.Format("{0}browse/{1}", _jira.Url, SelectedIssue);
            Clipboard.SetText(issueUrl);

            string title = string.Format("{0} attached to {1}", newFilename, SelectedIssue);
            SnagitJiraOutputAccessory.Commands.ICommand openWebPageCommand = new SnagitJiraOutputAccessory.Commands.OpenWebPageCommand(issueUrl);
            UploadCompleteNotification notifier = new UploadCompleteNotification();
            notifier.Notify(title, issueUrl, openWebPageCommand);
        }
    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;

        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
