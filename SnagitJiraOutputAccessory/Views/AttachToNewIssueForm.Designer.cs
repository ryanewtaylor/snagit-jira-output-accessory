namespace SnagitJiraOutputAccessory.Views
{
    partial class AttachToNewIssueForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.attachBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.summaryTxt = new System.Windows.Forms.TextBox();
            this.projectLst = new System.Windows.Forms.ComboBox();
            this.projectBindingSrc = new System.Windows.Forms.BindingSource(this.components);
            this.issueTypeBindingSrc = new System.Windows.Forms.BindingSource(this.components);
            this.issueTypeLst = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.projectBindingSrc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.issueTypeBindingSrc)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Issue Type";
            // 
            // attachBtn
            // 
            this.attachBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.attachBtn.Location = new System.Drawing.Point(222, 132);
            this.attachBtn.Name = "attachBtn";
            this.attachBtn.Size = new System.Drawing.Size(75, 23);
            this.attachBtn.TabIndex = 4;
            this.attachBtn.Text = "Attach";
            this.attachBtn.UseVisualStyleBackColor = true;
            this.attachBtn.Click += new System.EventHandler(this.attachBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(303, 132);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Summary";
            // 
            // summaryTxt
            // 
            this.summaryTxt.Location = new System.Drawing.Point(90, 69);
            this.summaryTxt.Multiline = true;
            this.summaryTxt.Name = "summaryTxt";
            this.summaryTxt.Size = new System.Drawing.Size(288, 57);
            this.summaryTxt.TabIndex = 7;
            // 
            // projectLst
            // 
            this.projectLst.FormattingEnabled = true;
            this.projectLst.Location = new System.Drawing.Point(90, 17);
            this.projectLst.Name = "projectLst";
            this.projectLst.Size = new System.Drawing.Size(288, 21);
            this.projectLst.TabIndex = 8;
            this.projectLst.SelectionChangeCommitted += new System.EventHandler(this.projectLst_SelectionChangeCommitted);
            // 
            // issueTypeLst
            // 
            this.issueTypeLst.FormattingEnabled = true;
            this.issueTypeLst.Location = new System.Drawing.Point(90, 45);
            this.issueTypeLst.Name = "issueTypeLst";
            this.issueTypeLst.Size = new System.Drawing.Size(126, 21);
            this.issueTypeLst.TabIndex = 9;
            // 
            // AttachToNewIssueForm
            // 
            this.AcceptButton = this.attachBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(390, 165);
            this.Controls.Add(this.issueTypeLst);
            this.Controls.Add(this.projectLst);
            this.Controls.Add(this.summaryTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.attachBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AttachToNewIssueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Attach To New Issue Form";
            this.Load += new System.EventHandler(this.AttachToNewIssueForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.projectBindingSrc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.issueTypeBindingSrc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button attachBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox summaryTxt;
        private System.Windows.Forms.ComboBox projectLst;
        private System.Windows.Forms.BindingSource projectBindingSrc;
        private System.Windows.Forms.BindingSource issueTypeBindingSrc;
        private System.Windows.Forms.ComboBox issueTypeLst;
    }
}