using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SnagitJiraOutputAccessory.Models;

namespace SnagitJiraOutputAccessory.Views
{
    public partial class AttachToNewIssueForm : Form
    {
        public string ProjectKey;
        public string IssueType;
        public string IssueSummary;
        public readonly IEnumerable<Project> _projects;

        public AttachToNewIssueForm(IEnumerable<Project> projects)
        {
            InitializeComponent();

            _projects = projects;
        }

        private void attachBtn_Click(object sender, EventArgs e)
        {
            ProjectKey = ((Project)projectLst.SelectedItem).Key;
            IssueType = ((IssueType)issueTypeLst.SelectedItem).Name;
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

        private void AttachToNewIssueForm_Load(object sender, EventArgs e)
        {
            projectBindingSrc.DataSource = _projects;
            projectLst.DisplayMember = "Name";
            projectLst.ValueMember = "Key";
            projectLst.DataSource = projectBindingSrc;

            issueTypeBindingSrc.DataSource = _projects.First().IssueTypes;
            issueTypeLst.DisplayMember = "Name";
            issueTypeLst.ValueMember = "Name";
            issueTypeLst.DataSource = issueTypeBindingSrc;
        }

        private void projectLst_SelectionChangeCommitted(object sender, EventArgs e)
        {
            issueTypeBindingSrc.DataSource = ((Project)projectLst.SelectedItem).IssueTypes;
        }
    }
}
