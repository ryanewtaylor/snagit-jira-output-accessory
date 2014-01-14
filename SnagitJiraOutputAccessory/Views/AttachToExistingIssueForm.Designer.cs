namespace SnagitJiraOutputAccessory.Views
{
    partial class AttachToExistingIssueForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.issueKeyTxt = new System.Windows.Forms.TextBox();
            this.attachBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Issue Key";
            // 
            // issueKeyTxt
            // 
            this.issueKeyTxt.Location = new System.Drawing.Point(12, 25);
            this.issueKeyTxt.Name = "issueKeyTxt";
            this.issueKeyTxt.Size = new System.Drawing.Size(259, 20);
            this.issueKeyTxt.TabIndex = 1;
            // 
            // attachBtn
            // 
            this.attachBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.attachBtn.Location = new System.Drawing.Point(115, 51);
            this.attachBtn.Name = "attachBtn";
            this.attachBtn.Size = new System.Drawing.Size(75, 23);
            this.attachBtn.TabIndex = 2;
            this.attachBtn.Text = "Attach";
            this.attachBtn.UseVisualStyleBackColor = true;
            this.attachBtn.Click += new System.EventHandler(this.attachBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(196, 51);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // AttachToExistingTicketForm
            // 
            this.AcceptButton = this.attachBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(282, 89);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.attachBtn);
            this.Controls.Add(this.issueKeyTxt);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AttachToExistingTicketForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter an Existing Issue Key";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox issueKeyTxt;
        private System.Windows.Forms.Button attachBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}