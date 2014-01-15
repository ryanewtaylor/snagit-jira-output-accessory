﻿namespace SnagitJiraOutputAccessory.Commands
{
    using System.Windows.Forms;
    using SnagitJiraOutputAccessory.Models;
    using SnagitJiraOutputAccessory.Views;
    using SNAGITLib;

    public class ConfigureSettingsCommand : ICommand
    {
        private OutputPreferencesRepository _outputPreferencesRepo;

        public ConfigureSettingsCommand(ISnagIt snagit, OutputPreferencesRepository outputPreferencesRepo)
        {
            _outputPreferencesRepo = outputPreferencesRepo;
        }

        public void Execute()
        {
            var prefs = _outputPreferencesRepo.Read();
            PreferencesForm preferencesForm = new PreferencesForm(prefs);
            preferencesForm.StartPosition = FormStartPosition.CenterParent;

            if (preferencesForm.ShowDialog() == DialogResult.OK)
            {
                var newPrefs = preferencesForm.OutputPreferences;
                _outputPreferencesRepo.Write(newPrefs);
            }
        }
    }
}
