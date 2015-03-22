namespace SnagitJiraOutputAccessory.ViewModels
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    using Atlassian.Jira;
    using SnagitJiraOutputAccessory.Commands;
    using SnagitJiraOutputAccessory.Models;
    using SNAGITLib;

    using Clipboard = System.Windows.Clipboard;

    public class AttachToExistingIssueViewModel : ViewModelBase
    {
        private ISnagIt _snagit;
        private Jira _jira;

        private readonly CommandHandler<string> _attachCommand;

        public AttachToExistingIssueViewModel(ISnagIt snagit, Jira jira)
        {
            _snagit = snagit;
            _jira = jira;
            _filename = DefaultFileName();
            
            ValidateIssueId(_selectedIssue);
            ValidateFileName(_filename);

            _attachCommand = new CommandHandler<string>(s => AttachIssue(), s => !HasErrors);
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
                    _attachCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public void ValidateIssueId(string issueId)
        {
            Regex regex = new Regex(@"^([A-Za-z]+-\d+)$");
            
            if (!regex.IsMatch(issueId))
            {
                base.AddErrors("SelectedIssue", "Issue key cannot be blank");
            }
            else
            {
                base.RemoveErrors("SelectedIssue");
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

        private string _comment = "";
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (!String.Equals(_comment, value))
                {
                    _comment = value;
                    OnPropertyChanged();
                }
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

            DialogResult = true;
        }
    }
}
