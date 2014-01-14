namespace SnagitJiraOutputAccessory.Commands
{
    using System.Windows.Forms;
    using SnagitJiraOutputAccessory.Models;

    public class AttachToNewIssueCommand : ICommand
    {
        private OutputPreferencesRepository _outputPreferencesRepo;

        public AttachToNewIssueCommand(OutputPreferencesRepository outputPreferencesRepo)
        {
            _outputPreferencesRepo = outputPreferencesRepo;
        }

        public void Execute()
        {
            MessageBox.Show("TODO: AttachToNewIssueCommand");
        }
    }
}
