namespace SnagitJiraOutputAccessory.Models
{
    public class OutputPreferences
    {
        public string JiraRootUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LastProjectKey { get; set; }

        public void Encrypt()
        {
            Password = Password.Enrypt();
        }

        public void Decrypt()
        {
            Password = Password.Decrypt();
        }
    }
}
