using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnagitJiraOutputAccessory.Commands
{
    public class CommandHandler<T> : System.Windows.Input.ICommand
    {
        private Action<T> _action;
        private readonly Predicate<T> _canExecute;

        public CommandHandler(Action<T> action, Predicate<T> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) 
                return true;

            return _canExecute((parameter == null) ? default(T) : (T)Convert.ChangeType(parameter, typeof(T)));
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null) 
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public void Execute(object parameter)
        {
            _action((parameter == null) ? default(T) : (T)Convert.ChangeType(parameter, typeof(T)));
        }
    }
}
