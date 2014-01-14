namespace SnagitJiraOutputAccessory.Commands
{
    using System.Windows.Forms;
    using SnagitJiraOutputAccessory.Models;

    public class AttachToExistingIssueCommand : ICommand
    {
        private OutputPreferencesRepository _outputPreferencesRepo;

        public AttachToExistingIssueCommand(OutputPreferencesRepository outputPreferencesRepo)
        {
            _outputPreferencesRepo = outputPreferencesRepo;
        }

        public void Execute()
        {
            MessageBox.Show("TODO: AttachToExistingIssueCommand");
        }
    }
}
