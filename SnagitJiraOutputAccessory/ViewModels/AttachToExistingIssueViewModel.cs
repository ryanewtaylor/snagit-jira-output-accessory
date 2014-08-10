namespace SnagitJiraOutputAccessory.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows;
    using Atlassian.Jira;
    using SnagitJiraOutputAccessory.Commands;
    using SnagitJiraOutputAccessory.Models;
    using SNAGITLib;

    public class AttachToExistingIssueViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private ISnagIt _snagit;
        private Jira _jira;

        public AttachToExistingIssueViewModel(ISnagIt snagit, Jira jira)
        {
            _snagit = snagit;
            _jira = jira;
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
                    ValidateIssueId(_selectedIssue);
                }
            }
        }

        public void ValidateIssueId(string issueId)
        {
            Regex regex = new Regex(@"^([A-Za-z]+-\d+)$");
            
            if (!regex.IsMatch(issueId))
            {
                ICollection<string> validationErrors = new List<string>();
                validationErrors.Add("Issue Id cannot be blank");
                _errors["SelectedIssue"] = validationErrors;
                OnErrorsChanged("SelectedIssue");
            }
            else
            {
                _errors.Remove("SelectedIssue");
                OnErrorsChanged("SelectedIssue");
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

        private System.Windows.Input.ICommand _attachCommand;
        public System.Windows.Input.ICommand AttachCommand
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

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void OnErrorsChanged([CallerMemberName]string propertyName = "")
        {
             if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private Dictionary<string, ICollection<string>> _errors = new Dictionary<string, ICollection<string>>();
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
            {
                return null;
            }
            
            return _errors[propertyName];
        }

        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }
    }
}
