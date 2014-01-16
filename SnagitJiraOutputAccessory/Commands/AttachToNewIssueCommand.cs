namespace SnagitJiraOutputAccessory.Commands
{
    using System.IO;
    using System.Windows.Forms;
    using Atlassian.Jira;
    using SnagitJiraOutputAccessory.Models;
    using SnagitJiraOutputAccessory.Views;
    using SNAGITLib;

    public class AttachToNewIssueCommand : ICommand
    {
        private readonly ISnagIt _snagit;
        private OutputPreferencesRepository _outputPreferencesRepo;

        public AttachToNewIssueCommand(ISnagIt snagit, OutputPreferencesRepository outputPreferencesRepo)
        {
            _snagit = snagit;
            _outputPreferencesRepo = outputPreferencesRepo;
        }

        public void Execute()
        {
            var prefs = _outputPreferencesRepo.Read();

            AttachToNewIssueForm form = new AttachToNewIssueForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                string projectKey = form.ProjectKey;
                string issueType = form.IssueType;
                string issueSummary = form.IssueSummary;

                Jira jira = new Jira(prefs.JiraRootUrl, prefs.Username, prefs.Password);
                Issue issue = jira.CreateIssue(projectKey);
                issue.Type = issueType;
                issue.Summary = issueSummary;
                issue.SaveChanges();

                string issueKey = issue.Key.Value;

                ISnagItImageDocumentSave saveableDoc = _snagit.SelectedDocument as ISnagItImageDocumentSave;
                string tempFileName = Path.ChangeExtension(Path.GetTempFileName(), ".png");
                saveableDoc.SaveToFile(tempFileName, snagImageFileType.siftPNG, null);

                issue.AddAttachment(tempFileName);

                string issueUrl = string.Format("{0}browse/{1}", jira.Url, issueKey);
                Clipboard.SetText(issueUrl);

                string title = string.Format("{0} attached to {1}", Path.GetFileName(tempFileName), issueKey);
                ICommand openWebPageCommand = new OpenWebPageCommand(issueUrl);
                UploadCompleteNotification notifier = new UploadCompleteNotification();
                notifier.Notify(title, issueUrl, openWebPageCommand);
            }
        }
    }
}
