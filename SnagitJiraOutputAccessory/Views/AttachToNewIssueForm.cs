using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnagitJiraOutputAccessory.Views
{
    public partial class AttachToNewIssueForm : Form
    {
        public string ProjectKey;
        public string IssueType;
        public string IssueSummary;

        public AttachToNewIssueForm()
        {
            InitializeComponent();
        }

        private void attachBtn_Click(object sender, EventArgs e)
        {
            ProjectKey = projectTxt.Text;
            IssueType = issueTypeTxt.Text;
            IssueSummary = summaryTxt.Text;
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            ProjectKey = "";
            IssueType = "";
            IssueSummary = "";
            Close();
        }
    }
}
