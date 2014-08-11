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

    public class AttachToExistingIssueViewModel : ViewModelBase
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
                base.AddErrors("SelectedIssueError", "Issue key cannot be blank");
            }
            else
            {
                base.RemoveErrors("SelectedIssueError");
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
    }
}
