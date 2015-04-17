namespace SnagitJiraOutputAccessory.ViewModels
{
    using System;
    using System.Windows.Controls;
    using SnagitJiraOutputAccessory.Commands;
    using SnagitJiraOutputAccessory.Models;

    public class OutputOptionsViewModel : ViewModelBase
    {
        private string _jiraUrl = string.Empty;
        private string _username = string.Empty;

        private readonly CommandHandler<PasswordBox> _testCommand;
        private readonly CommandHandler<PasswordBox> _saveCommand;
        private readonly OutputPreferencesRepository _outputOptionsRepo;

        public OutputOptionsViewModel(OutputPreferencesRepository outputOptionsRepo)
        {
            _outputOptionsRepo = outputOptionsRepo;

            _saveCommand = new CommandHandler<PasswordBox>(pbox => SaveOptions(pbox), pbox => CanSave());

            var options = _outputOptionsRepo.Read();
            _jiraUrl = options.JiraRootUrl;
            _username = options.Username;
            
            ValidateUrl(_jiraUrl);
            ValidateUsername(_username);
        }

        private bool CanSave()
        {
            return true;
        }

        private void ValidateUsername(string s)
        {
            if (String.IsNullOrWhiteSpace(s))
            {
                base.AddErrors("Username", "Please specify your username");
            }
            else
            {
                base.RemoveErrors("Username");
            }
        }

        private void ValidateUrl(string s)
        {
            if (!Uri.IsWellFormedUriString(s, UriKind.Absolute))
            {
                base.AddErrors("JiraUrl", "JIRA url must be a valid url");
            }
            else
            {
                base.RemoveErrors("JiraUrl");
            }
        }

        public string JiraUrl
        {
            get
            {
                return _jiraUrl;
            }
            set
            {
                if (_jiraUrl != value)
                {
                    _jiraUrl = value;
                    ValidateUrl(_jiraUrl);
                    this.OnPropertyChanged();
                    _testCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    ValidateUsername(_username);
                    this.OnPropertyChanged();
                }
            }
        }

        public System.Windows.Input.ICommand SaveCommand
        {
            get
            {
                return _saveCommand;
            }
        }

        private void SaveOptions(PasswordBox pbox)
        {
            string pwd = pbox.Password;
            var outputOptions = _outputOptionsRepo.Read();
            outputOptions.JiraRootUrl = JiraUrl;
            outputOptions.Username = Username;
            outputOptions.Password = pbox.Password;
            _outputOptionsRepo.Write(outputOptions);
        }
    }
}
