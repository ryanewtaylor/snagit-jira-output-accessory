namespace SnagitJiraOutputAccessory.Commands
{
    public class CommandFactory
    {
        public CommandFactory()
        {
        }

        public ICommand CreateCommand(string commandId)
        {
            return (ICommand)System.Activator.CreateInstance(
                CommandRegistry.GetCommandType(commandId));
        }

        public ICommand CreateDefaultCommand()
        {
            return this.CreateCommand(CommandRegistry.GetDefaultCommandInfo().Id);
        }
    }
}
