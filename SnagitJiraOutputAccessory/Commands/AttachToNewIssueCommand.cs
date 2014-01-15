namespace SnagitJiraOutputAccessory.Commands
{
    using System.Windows.Forms;
    using SnagitJiraOutputAccessory.Models;
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
            MessageBox.Show("TODO: AttachToNewIssueCommand");
        }
    }
}
