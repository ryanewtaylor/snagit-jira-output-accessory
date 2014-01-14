namespace SnagitJiraOutputAccessory.Views
{
    using System;
    using System.Windows.Forms;
    using SnagitJiraOutputAccessory.Models;

    public partial class PreferencesForm : Form
    {
        public OutputPreferences OutputPreferences { get; private set; }

        public PreferencesForm(OutputPreferences preferences)
        {
            InitializeComponent();

            jiraUrlTxt.Text = preferences.JiraRootUrl;
            usernameTxt.Text = preferences.Username;
            passwordTxt.Text = preferences.Password;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.OutputPreferences = new OutputPreferences
            {
                JiraRootUrl = jiraUrlTxt.Text,
                Username = usernameTxt.Text,
                Password = passwordTxt.Text,
            };

            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.OutputPreferences = null;
            Close();
        }
    }
}
