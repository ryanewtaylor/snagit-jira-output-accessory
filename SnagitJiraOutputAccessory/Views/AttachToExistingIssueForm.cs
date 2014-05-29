namespace SnagitJiraOutputAccessory.Views
{
    using System.Collections;
    using System.Windows.Forms;
    using Atlassian.Jira;

    public partial class AttachToExistingIssueForm : Form
    {
        private Jira _jira;
        public string IssueKey { get; private set; }

        public AttachToExistingIssueForm(Jira jira)
        {
            InitializeComponent();

            _jira = jira;
            
            filtersBindingSrc.DataSource = jira.GetFilters();
            filtersLst.DisplayMember = "Name";
            filtersLst.ValueMember = "Id";
            filtersLst.DataSource = filtersBindingSrc;
        }

        private void attachBtn_Click(object sender, System.EventArgs e)
        {
            IssueKey = issueKeyTxt.Text;
            Close();
        }

        private void cancelBtn_Click(object sender, System.EventArgs e)
        {
            IssueKey = null;
            Close();
        }
    }
}
