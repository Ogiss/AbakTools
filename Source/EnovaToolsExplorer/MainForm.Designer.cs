namespace EnovaToolsExplorer
{
    partial class MainForm
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
            this.loginedUserLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLineText = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.TopPanel)).BeginInit();
            this.TopPanel.Panel2.SuspendLayout();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftPanel)).BeginInit();
            this.LeftPanel.Panel2.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RightPanel)).BeginInit();
            this.RightPanel.Panel1.SuspendLayout();
            this.RightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CenterPanel)).BeginInit();
            this.CenterPanel.Panel1.SuspendLayout();
            this.CenterPanel.SuspendLayout();
            this.StatusLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            // 
            // LeftPanel
            // 
            // 
            // RightPanel
            // 
            // 
            // CenterPanel
            // 
            // 
            // StatusLine
            // 
            this.StatusLine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginedUserLabel,
            this.statusLineText});
            // 
            // loginedUserLabel
            // 
            this.loginedUserLabel.Name = "loginedUserLabel";
            this.loginedUserLabel.Size = new System.Drawing.Size(71, 17);
            this.loginedUserLabel.Text = "Użytkownik:";
            // 
            // statusLineText
            // 
            this.statusLineText.Name = "statusLineText";
            this.statusLineText.Size = new System.Drawing.Size(0, 17);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1095, 532);
            this.Name = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TopPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopPanel)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.LeftPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftPanel)).EndInit();
            this.LeftPanel.ResumeLayout(false);
            this.RightPanel.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightPanel)).EndInit();
            this.RightPanel.ResumeLayout(false);
            this.CenterPanel.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CenterPanel)).EndInit();
            this.CenterPanel.ResumeLayout(false);
            this.StatusLine.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel loginedUserLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusLineText;
    }
}
