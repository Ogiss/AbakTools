namespace BAL.Forms
{
    partial class FormWithTabs
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.AddPage = new System.Windows.Forms.TabPage();
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
            this.TabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Panel1Collapsed = true;
            this.TopPanel.SplitterDistance = 39;
            // 
            // LeftPanel
            // 
            this.LeftPanel.Panel1Collapsed = true;
            this.LeftPanel.Size = new System.Drawing.Size(1095, 480);
            // 
            // RightPanel
            // 
            this.RightPanel.Panel2Collapsed = true;
            this.RightPanel.Size = new System.Drawing.Size(1095, 480);
            this.RightPanel.SplitterDistance = 876;
            // 
            // CenterPanel
            // 
            // 
            // CenterPanel.Panel1
            // 
            this.CenterPanel.Panel1.Controls.Add(this.TabControl);
            this.CenterPanel.Panel2Collapsed = true;
            this.CenterPanel.Size = new System.Drawing.Size(1095, 480);
            this.CenterPanel.SplitterDistance = 350;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.AddPage);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TabControl.ItemSize = new System.Drawing.Size(120, 22);
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.ShowToolTips = true;
            this.TabControl.Size = new System.Drawing.Size(1095, 480);
            this.TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl.TabIndex = 0;
            this.TabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabControl_DrawItem);
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            this.TabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabControl_Selecting);
            this.TabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl_Selected);
            this.TabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabControl_MouseClick);
            // 
            // AddPage
            // 
            this.AddPage.ImageKey = "(none)";
            this.AddPage.Location = new System.Drawing.Point(4, 26);
            this.AddPage.Name = "AddPage";
            this.AddPage.Size = new System.Drawing.Size(1087, 450);
            this.AddPage.TabIndex = 1;
            this.AddPage.Text = "+";
            this.AddPage.UseVisualStyleBackColor = true;
            // 
            // FormWithTabs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1095, 532);
            this.Name = "FormWithTabs";
            this.PanelBottomCollapsed = true;
            this.PanelLeftCollapsed = true;
            this.PanelRightCollapsed = true;
            this.PanelTopCollapsed = true;
            this.Text = "FormWithTabs";
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
            this.TabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage AddPage;
    }
}
