namespace SnagitJiraOutputAccessory.Commands
{
    using Atlassian.Jira;
    using SnagitJiraOutputAccessory.Models;
    using SnagitJiraOutputAccessory.ViewModels;
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
            Jira jira = new Jira(prefs.JiraRootUrl, prefs.Username, prefs.Password);

            AttachToNewIssueView view = new AttachToNewIssueView();
            var windowHelper = new System.Windows.Interop.WindowInteropHelper(view);
            windowHelper.Owner = (System.IntPtr)(_snagit.TopLevelHWnd);
            view.DataContext = new AttachToNewIssueViewModel(_snagit, jira);
            view.ShowDialog();
        }
    }
}
