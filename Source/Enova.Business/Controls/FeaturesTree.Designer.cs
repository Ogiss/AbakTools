namespace Enova.Business.Old.Controls
{
    partial class FeaturesTree
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeaturesTree));
            this.treeView = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newGroupButton = new System.Windows.Forms.ToolStripSplitButton();
            this.jednopoziomowajednowartościowaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jednopoziomowawielowartościowaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.hierarchicznajednowartościowaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hierarhicznawielowartościowaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSubgroupButton = new System.Windows.Forms.ToolStripButton();
            this.deleteGroupButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.AllowDrop = true;
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.FullRowSelect = true;
            this.treeView.HideSelection = false;
            this.treeView.Location = new System.Drawing.Point(0, 30);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(277, 515);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.treeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_DragEnter);
            this.treeView.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
            this.treeView.DragLeave += new System.EventHandler(this.treeView_DragLeave);
            this.treeView.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.treeView_GiveFeedback);
            this.treeView.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.treeView_QueryContinueDrag);
            this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGroupButton,
            this.newSubgroupButton,
            this.deleteGroupButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(277, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newGroupButton
            // 
            this.newGroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newGroupButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jednopoziomowajednowartościowaToolStripMenuItem,
            this.jednopoziomowawielowartościowaToolStripMenuItem,
            this.toolStripSeparator1,
            this.hierarchicznajednowartościowaToolStripMenuItem,
            this.hierarhicznawielowartościowaToolStripMenuItem});
            this.newGroupButton.Image = ((System.Drawing.Image)(resources.GetObject("newGroupButton.Image")));
            this.newGroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newGroupButton.Name = "newGroupButton";
            this.newGroupButton.Size = new System.Drawing.Size(32, 22);
            this.newGroupButton.Text = "Nowa grupa";
            this.newGroupButton.ButtonClick += new System.EventHandler(this.newGroupButton_ButtonClick);
            // 
            // jednopoziomowajednowartościowaToolStripMenuItem
            // 
            this.jednopoziomowajednowartościowaToolStripMenuItem.Name = "jednopoziomowajednowartościowaToolStripMenuItem";
            this.jednopoziomowajednowartościowaToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.jednopoziomowajednowartościowaToolStripMenuItem.Text = "Jednopoziomowa-jednowartościowa";
            this.jednopoziomowajednowartościowaToolStripMenuItem.Click += new System.EventHandler(this.jednopoziomowajednowartościowaToolStripMenuItem_Click);
            // 
            // jednopoziomowawielowartościowaToolStripMenuItem
            // 
            this.jednopoziomowawielowartościowaToolStripMenuItem.Name = "jednopoziomowawielowartościowaToolStripMenuItem";
            this.jednopoziomowawielowartościowaToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.jednopoziomowawielowartościowaToolStripMenuItem.Text = "Jednopoziomowa-wielowartościowa";
            this.jednopoziomowawielowartościowaToolStripMenuItem.Click += new System.EventHandler(this.jednopoziomowawielowartościowaToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(255, 6);
            // 
            // hierarchicznajednowartościowaToolStripMenuItem
            // 
            this.hierarchicznajednowartościowaToolStripMenuItem.Name = "hierarchicznajednowartościowaToolStripMenuItem";
            this.hierarchicznajednowartościowaToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.hierarchicznajednowartościowaToolStripMenuItem.Text = "Hierarchiczna-jednowartościowa";
            this.hierarchicznajednowartościowaToolStripMenuItem.Click += new System.EventHandler(this.hierarchicznajednowartościowaToolStripMenuItem_Click);
            // 
            // hierarhicznawielowartościowaToolStripMenuItem
            // 
            this.hierarhicznawielowartościowaToolStripMenuItem.Name = "hierarhicznawielowartościowaToolStripMenuItem";
            this.hierarhicznawielowartościowaToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.hierarhicznawielowartościowaToolStripMenuItem.Text = "Hierarhiczna-wielowartościowa";
            this.hierarhicznawielowartościowaToolStripMenuItem.Click += new System.EventHandler(this.hierarhicznawielowartościowaToolStripMenuItem_Click);
            // 
            // newSubgroupButton
            // 
            this.newSubgroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newSubgroupButton.Image = ((System.Drawing.Image)(resources.GetObject("newSubgroupButton.Image")));
            this.newSubgroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newSubgroupButton.Name = "newSubgroupButton";
            this.newSubgroupButton.Size = new System.Drawing.Size(23, 22);
            this.newSubgroupButton.Text = "Nowa podgrupa";
            this.newSubgroupButton.Click += new System.EventHandler(this.newSubgroupButton_Click);
            // 
            // deleteGroupButton
            // 
            this.deleteGroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteGroupButton.Enabled = false;
            this.deleteGroupButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteGroupButton.Image")));
            this.deleteGroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteGroupButton.Name = "deleteGroupButton";
            this.deleteGroupButton.Size = new System.Drawing.Size(23, 22);
            this.deleteGroupButton.Text = "Usuń grupę";
            this.deleteGroupButton.Click += new System.EventHandler(this.deleteGroupButton_Click);
            // 
            // FeaturesTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.treeView);
            this.Name = "FeaturesTree";
            this.Size = new System.Drawing.Size(277, 545);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton newGroupButton;
        private System.Windows.Forms.ToolStripMenuItem jednopoziomowajednowartościowaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jednopoziomowawielowartościowaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem hierarchicznajednowartościowaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hierarhicznawielowartościowaToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton deleteGroupButton;
        private System.Windows.Forms.ToolStripButton newSubgroupButton;
    }
}
