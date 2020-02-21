namespace BAL.Forms
{
    partial class FormWithPanels
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
            this.TopPanel = new System.Windows.Forms.SplitContainer();
            this.LeftPanel = new System.Windows.Forms.SplitContainer();
            this.RightPanel = new System.Windows.Forms.SplitContainer();
            this.CenterPanel = new System.Windows.Forms.SplitContainer();
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
            this.CenterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TopPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.TopPanel.Location = new System.Drawing.Point(0, 27);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // TopPanel.Panel2
            // 
            this.TopPanel.Panel2.Controls.Add(this.LeftPanel);
            this.TopPanel.Size = new System.Drawing.Size(1095, 480);
            this.TopPanel.SplitterDistance = 87;
            this.TopPanel.TabIndex = 2;
            // 
            // LeftPanel
            // 
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            // 
            // LeftPanel.Panel2
            // 
            this.LeftPanel.Panel2.Controls.Add(this.RightPanel);
            this.LeftPanel.Size = new System.Drawing.Size(1095, 389);
            this.LeftPanel.SplitterDistance = 208;
            this.LeftPanel.TabIndex = 0;
            // 
            // RightPanel
            // 
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightPanel.Location = new System.Drawing.Point(0, 0);
            this.RightPanel.Name = "RightPanel";
            // 
            // RightPanel.Panel1
            // 
            this.RightPanel.Panel1.Controls.Add(this.CenterPanel);
            this.RightPanel.Size = new System.Drawing.Size(883, 389);
            this.RightPanel.SplitterDistance = 707;
            this.RightPanel.TabIndex = 0;
            // 
            // CenterPanel
            // 
            this.CenterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CenterPanel.Location = new System.Drawing.Point(0, 0);
            this.CenterPanel.Name = "CenterPanel";
            this.CenterPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CenterPanel.Size = new System.Drawing.Size(707, 389);
            this.CenterPanel.SplitterDistance = 312;
            this.CenterPanel.TabIndex = 0;
            // 
            // FormWithPanels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1095, 532);
            this.Controls.Add(this.TopPanel);
            this.Name = "FormWithPanels";
            this.Load += new System.EventHandler(this.FormWithPanels_Load);
            this.Controls.SetChildIndex(this.TopPanel, 0);
            this.TopPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopPanel)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.LeftPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftPanel)).EndInit();
            this.LeftPanel.ResumeLayout(false);
            this.RightPanel.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightPanel)).EndInit();
            this.RightPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CenterPanel)).EndInit();
            this.CenterPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.SplitContainer TopPanel;
        protected System.Windows.Forms.SplitContainer LeftPanel;
        protected System.Windows.Forms.SplitContainer RightPanel;
        protected System.Windows.Forms.SplitContainer CenterPanel;

    }
}
