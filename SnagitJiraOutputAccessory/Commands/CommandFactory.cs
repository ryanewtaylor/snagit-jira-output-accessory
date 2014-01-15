using SnagitJiraOutputAccessory.Models;
using SNAGITLib;
namespace SnagitJiraOutputAccessory.Commands
{
    public class CommandFactory
    {
        private ISnagIt _snagit;
        private SnagItOutputPreferences _componentPreferences;

        public CommandFactory(ISnagIt snagit, SnagItOutputPreferences componentPreferences)
        {
            _snagit = snagit;
            _componentPreferences = componentPreferences;
        }

        public ICommand CreateCommand(string commandId)
        {
            var repo = new OutputPreferencesRepository(_componentPreferences.PreferencesDir);

            return (ICommand)System.Activator.CreateInstance(
                CommandRegistry.GetCommandType(commandId), _snagit, repo);
        }

        public ICommand CreateDefaultCommand()
        {
            return this.CreateCommand(CommandRegistry.GetDefaultCommandInfo().Id);
        }
    }
}
