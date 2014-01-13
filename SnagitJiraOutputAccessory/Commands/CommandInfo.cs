using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnagitJiraOutputAccessory.Commands
{
    public class CommandInfo
    {
        private readonly string _id;
        private readonly string _name;
        private readonly Type _type;

        public CommandInfo(string id, string name, Type type)
        {
            _id = id;
            _name = name;
            _type = type;
        }

        public string Id { get { return _id; } }
        public string Name { get { return _name; } }
        public Type Type { get { return _type; } }

    }
}
