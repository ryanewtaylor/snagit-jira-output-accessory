namespace SnagitJiraOutputAccessory
{
    using System.Text;
    using SnagitJiraOutputAccessory.Commands;

    public class SnagitMenuFormatter
    {
        public string Format()
        {
            StringBuilder menu = new StringBuilder();
            menu.Append("<menu>");

            foreach (CommandInfo commandInfo in CommandRegistry.GetCommandInfos())
            {
                menu.AppendFormat("<menuitem id=\"{0}\" label=\"{1}\" />", commandInfo.Id, commandInfo.Name);
            }
            
            menu.Append("</menu>");
            return menu.ToString();
        }
    }
}
