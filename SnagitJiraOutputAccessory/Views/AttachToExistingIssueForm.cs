namespace SnagitJiraOutputAccessory.Views
{
    using System.Windows.Forms;

    public partial class AttachToExistingIssueForm : Form
    {
        public string IssueKey { get; private set; }

        public AttachToExistingIssueForm()
        {
            InitializeComponent();
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
