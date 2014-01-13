namespace SnagitJiraOutputAccessory.Commands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public static class CommandRegistry
    {
        private static Dictionary<string, CommandInfo> _registry = new Dictionary<string, CommandInfo>()
        {
            {"AttachToNewIssueCommand", new CommandInfo("AttachToNewIssueCommand", "Attach To New Issue", typeof(AttachToNewIssueCommand))}
            , { "AttachToExistingIssueCommand", new CommandInfo("AttachToExistingIssueCommand", "Attach To Existing Issue", typeof(AttachToExistingIssueCommand))}
            , {"ConfigureSettingsCommand", new CommandInfo("ConfigureSettingsCommand", "Preferences", typeof(ConfigureSettingsCommand))}
        };

        public static CommandInfo GetDefaultCommandInfo()
        {
            return _registry.First().Value;
        }

        public static Type GetCommandType(string commandId)
        {
            return _registry[commandId].Type;
        }

        public static IEnumerable<CommandInfo> GetCommandInfos()
        {
            return _registry.Values.AsEnumerable();
        }
    }
}
