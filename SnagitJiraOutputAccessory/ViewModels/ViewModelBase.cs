namespace SnagitJiraOutputAccessory.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private Dictionary<string, ICollection<string>> errors = new Dictionary<string, ICollection<string>>();

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get { return this.errors.Count > 0; }
        }

        public void AddErrors(string id, string message)
        {
            // TODO: Check for existing errors and handle accordingly
            ICollection<string> validationErrors = new List<string>();
            validationErrors.Add(message);
            this.errors[id] = validationErrors;
            this.OnErrorsChanged(id);
        }

        public void RemoveErrors(string id)
        {
            // TODO: Check if it exists and remove if it does
            this.errors.Remove(id);
            this.OnErrorsChanged(id);
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !this.errors.ContainsKey(propertyName))
            {
                return null;
            }

            return this.errors[propertyName];
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnErrorsChanged([CallerMemberName]string propertyName = "")
        {
            if (this.ErrorsChanged != null)
            {
                this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }
}
