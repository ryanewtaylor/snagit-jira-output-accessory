namespace SnagitJiraOutputAccessory.Commands
{
    using System.Windows.Forms;
    using SnagitJiraOutputAccessory.Models;
    using SnagitJiraOutputAccessory.Views;

    public class ConfigureSettingsCommand : ICommand
    {
        public void Execute()
        {
            OutputPreferences prefs = new OutputPreferences
            {
                JiraRootUrl = "https://fortressofsolitude.com/",
                Username = "clarkkent",
                Password = "********"
            };

            PreferencesForm preferencesForm = new PreferencesForm(prefs);
            preferencesForm.StartPosition = FormStartPosition.CenterParent;

            if (preferencesForm.ShowDialog() == DialogResult.OK)
            {
                var newPrefs = preferencesForm.OutputPreferences;
            }
        }
    }
}
