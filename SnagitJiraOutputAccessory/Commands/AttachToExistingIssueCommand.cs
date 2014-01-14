namespace SnagitJiraOutputAccessory.Commands
{
    using System.Windows.Forms;
    using SnagitJiraOutputAccessory.Models;
    using SnagitJiraOutputAccessory.Views;

    public class AttachToExistingIssueCommand : ICommand
    {
        private OutputPreferencesRepository _outputPreferencesRepo;

        public AttachToExistingIssueCommand(OutputPreferencesRepository outputPreferencesRepo)
        {
            _outputPreferencesRepo = outputPreferencesRepo;
        }

        public void Execute()
        {
            var prefs = _outputPreferencesRepo.Read();

            AttachToExistingIssueForm form = new AttachToExistingIssueForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                string issueKey = form.IssueKey;
            }
        }
    }
}
