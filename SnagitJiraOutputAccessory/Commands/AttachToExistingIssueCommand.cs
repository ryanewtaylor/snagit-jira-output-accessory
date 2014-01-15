namespace SnagitJiraOutputAccessory.Commands
{
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Atlassian.Jira;
    using SnagitJiraOutputAccessory.Models;
    using SnagitJiraOutputAccessory.Views;
    using SNAGITLib;

    public class AttachToExistingIssueCommand : ICommand
    {
        private readonly ISnagIt _snagit;
        private OutputPreferencesRepository _outputPreferencesRepo;

        public AttachToExistingIssueCommand(ISnagIt snagit, OutputPreferencesRepository outputPreferencesRepo)
        {
            _snagit = snagit;
            _outputPreferencesRepo = outputPreferencesRepo;
        }

        public void Execute()
        {
            var prefs = _outputPreferencesRepo.Read();

            AttachToExistingIssueForm form = new AttachToExistingIssueForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                string issueKey = form.IssueKey;

                Jira jira = new Jira(prefs.JiraRootUrl, prefs.Username, prefs.Password);
                Issue issue = (from i in jira.Issues
                             where i.Key == issueKey
                             select i).First();

                ISnagItImageDocumentSave saveableDoc = _snagit.SelectedDocument as ISnagItImageDocumentSave;
                string tempFileName = Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".png";
                saveableDoc.SaveToFile(tempFileName, snagImageFileType.siftPNG, null);

                issue.AddAttachment(tempFileName);

                string issueUrl = string.Format("{0}browse/{1}", jira.Url, issueKey);
                Clipboard.SetText(issueUrl);

                NotifyIcon notifyIcon = new NotifyIcon();
                notifyIcon.Visible = true;
                notifyIcon.Icon = SystemIcons.Information;
                notifyIcon.BalloonTipTitle = string.Format("{0} attached to {1}", Path.GetFileName(tempFileName), issueKey);
                notifyIcon.BalloonTipText = issueKey;
                notifyIcon.ShowBalloonTip(1000);
                notifyIcon.BalloonTipClicked += (object sender, System.EventArgs e) =>
                {
                    Process.Start(issueUrl);
                };
            }
        }
    }
}
