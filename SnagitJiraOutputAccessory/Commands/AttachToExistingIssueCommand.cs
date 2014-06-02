namespace SnagitJiraOutputAccessory.Commands
{
    using Atlassian.Jira;
    using SnagitJiraOutputAccessory.Models;
    using SnagitJiraOutputAccessory.ViewModels;
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
            Jira jira = new Jira(prefs.JiraRootUrl, prefs.Username, prefs.Password);

            AttachToExistingIssueView view = new AttachToExistingIssueView();
            var windowHelper = new System.Windows.Interop.WindowInteropHelper(view);
            windowHelper.Owner = (System.IntPtr)(_snagit.TopLevelHWnd);
            view.DataContext = new AttachToExistingIssueViewModel(_snagit, jira);
            view.ShowDialog();
        }
    }
}
