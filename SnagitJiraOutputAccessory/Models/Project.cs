namespace SnagitJiraOutputAccessory.Models
{
    using System.Collections.Generic;

    public class Project
    {
        private readonly string _id;
        private readonly string _key;
        private readonly string _name;
        private Dictionary<string, IssueType> _issueTypes;

        public Project(string id, string key, string name)
        {
            _id = id;
            _name = name;
            _key = key;
            _issueTypes = new Dictionary<string, IssueType>();
        }

        public string Id
        {
            get { return _id; }
        }

        public string Key
        {
            get { return _key; }
        }

        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<IssueType> IssueTypes
        {
            get { return _issueTypes.Values; }
        }

        public void AddIssueType(IssueType issueType)
        {
            if (!_issueTypes.ContainsKey(issueType.Id))
            {
                _issueTypes.Add(issueType.Id, issueType);
            }
        }
    }
}
