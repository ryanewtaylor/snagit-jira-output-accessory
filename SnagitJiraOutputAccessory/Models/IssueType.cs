namespace SnagitJiraOutputAccessory.Models
{
    public class IssueType
    {
        private readonly string _id;
        private readonly string _name;
        private readonly string _description;
        private readonly bool _isSubtask;

        public IssueType(string id, string name, string description, bool isSubTask)
        {
            _id = id;
            _name = name;
            _description = description;
            _isSubtask = isSubTask;
        }

        public string Id 
        { 
            get { return _id; } 
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public bool IsSubtask
        {
            get { return _isSubtask; }
        }
    }
}
