using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnagitJiraOutputAccessory.Commands;

namespace SnagitJiraOutputAccessory
{
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
