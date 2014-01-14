namespace SnagitJiraOutputAccessory.Models
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    public class OutputPreferencesRepository
    {
        private readonly string _fileName = "JiraOutputAccessoryPreferences.json";
        private readonly string _dir;

        public OutputPreferencesRepository(string dir)
        {
            _dir = dir;
        }

        public OutputPreferences Read()
        {
            String filePath = Path.Combine(_dir, _fileName);
            if (File.Exists(filePath))
            {
                String settingsAsJson = File.ReadAllText(filePath);
                OutputPreferences settings = JsonConvert.DeserializeObject<OutputPreferences>(settingsAsJson);
                settings.Decrypt();
                return settings;
            }

            return new OutputPreferences();
        }

        public void Write(OutputPreferences outputPreferences)
        {
            if (!Directory.Exists(_dir))
            {
                Directory.CreateDirectory(_dir);
            }

            String filePath = Path.Combine(_dir, _fileName);

            outputPreferences.Encrypt();
            String settingsAsJson = JsonConvert.SerializeObject(outputPreferences, Formatting.Indented);
            File.WriteAllText(filePath, settingsAsJson);
        }
    }
}
