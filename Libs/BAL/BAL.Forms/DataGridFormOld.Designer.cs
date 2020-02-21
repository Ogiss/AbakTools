namespace BAL.Forms
{
    partial class DataGridFormOld
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridFormOld));
            this.loadRowsWorker = new System.ComponentModel.BackgroundWorker();
            this.LeftPanel = new System.Windows.Forms.SplitContainer();
            this.TopPanel = new System.Windows.Forms.SplitContainer();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.GridNavigator = new BAL.Forms.Controls.GridNavigator();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorEditItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorRefreshItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorGridConfig = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorExtraPanelVisible = new System.Windows.Forms.ToolStripButton();
            this.GridView = new BAL.Forms.Controls.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftPanel)).BeginInit();
            this.LeftPanel.Panel2.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopPanel)).BeginInit();
            this.TopPanel.Panel2.SuspendLayout();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridNavigator)).BeginInit();
            this.GridNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // BindingSource
            // 
            this.BindingSource.CurrentChanged += new System.EventHandler(this.BindingSource_CurrentChanged);
            this.BindingSource.PositionChanged += new System.EventHandler(this.BindingSource_PositionChanged);
            // 
            // loadRowsWorker
            // 
            this.loadRowsWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loadRowsWorker_DoWork);
            // 
            // LeftPanel
            // 
            this.LeftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Panel1Collapsed = true;
            // 
            // LeftPanel.Panel2
            // 
            this.LeftPanel.Panel2.Controls.Add(this.TopPanel);
            this.LeftPanel.Size = new System.Drawing.Size(1195, 623);
            this.LeftPanel.SplitterDistance = 120;
            this.LeftPanel.TabIndex = 0;
            this.LeftPanel.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.LeftPanel_SplitterMoved);
            // 
            // TopPanel
            // 
            this.TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // TopPanel.Panel2
            // 
            this.TopPanel.Panel2.Controls.Add(this.splitContainer);
            this.TopPanel.Size = new System.Drawing.Size(1195, 623);
            this.TopPanel.SplitterDistance = 72;
            this.TopPanel.TabIndex = 0;
            this.TopPanel.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.TopPanel_SplitterMoved);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.GridNavigator);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.GridView);
            this.splitContainer.Size = new System.Drawing.Size(1193, 545);
            this.splitContainer.SplitterDistance = 30;
            this.splitContainer.TabIndex = 2;
            // 
            // GridNavigator
            // 
            this.GridNavigator.AddNewItem = null;
            this.GridNavigator.BindingSource = this.BindingSource;
            this.GridNavigator.CountItem = this.bindingNavigatorCountItem;
            this.GridNavigator.DeleteItem = null;
            this.GridNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.toolStripSeparator1,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.toolStripSeparator2,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.toolStripSeparator3,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorEditItem,
            this.bindingNavigatorDeleteItem,
            this.toolStripSeparator4,
            this.bindingNavigatorRefreshItem,
            this.bindingNavigatorGridConfig,
            this.bindingNavigatorExtraPanelVisible});
            this.GridNavigator.Location = new System.Drawing.Point(0, 0);
            this.GridNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.GridNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.GridNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.GridNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.GridNavigator.Name = "GridNavigator";
            this.GridNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.GridNavigator.Size = new System.Drawing.Size(1193, 25);
            this.GridNavigator.TabIndex = 1;
            this.GridNavigator.Text = "gridNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = global::BAL.Forms.Properties.Resources.MoveFirstItem;
            this.bindingNavigatorMoveFirstItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "toolStripButton1";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = global::BAL.Forms.Properties.Resources.MovePreviousItem;
            this.bindingNavigatorMovePreviousItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(60, 25);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = global::BAL.Forms.Properties.Resources.MoveNextItem;
            this.bindingNavigatorMoveNextItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "toolStripButton1";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = global::BAL.Forms.Properties.Resources.MoveLastItem;
            this.bindingNavigatorMoveLastItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "toolStripButton1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "toolStripButton1";
            // 
            // bindingNavigatorEditItem
            // 
            this.bindingNavigatorEditItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorEditItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorEditItem.Image")));
            this.bindingNavigatorEditItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorEditItem.Name = "bindingNavigatorEditItem";
            this.bindingNavigatorEditItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorEditItem.Text = "Edit";
            this.bindingNavigatorEditItem.ToolTipText = "Edit";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = global::BAL.Forms.Properties.Resources.DeleteItem;
            this.bindingNavigatorDeleteItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "toolStripButton1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorRefreshItem
            // 
            this.bindingNavigatorRefreshItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorRefreshItem.Image = global::BAL.Forms.Properties.Resources.Refresh;
            this.bindingNavigatorRefreshItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorRefreshItem.Name = "bindingNavigatorRefreshItem";
            this.bindingNavigatorRefreshItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorRefreshItem.Text = "toolStripButton1";
            // 
            // bindingNavigatorGridConfig
            // 
            this.bindingNavigatorGridConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorGridConfig.Image = global::BAL.Forms.Properties.Resources.tools_16;
            this.bindingNavigatorGridConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorGridConfig.Name = "bindingNavigatorGridConfig";
            this.bindingNavigatorGridConfig.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorGridConfig.Text = "Konfiguracja widoku";
            // 
            // bindingNavigatorExtraPanelVisible
            // 
            this.bindingNavigatorExtraPanelVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorExtraPanelVisible.Image = global::BAL.Forms.Properties.Resources.LeftView;
            this.bindingNavigatorExtraPanelVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorExtraPanelVisible.Name = "bindingNavigatorExtraPanelVisible";
            this.bindingNavigatorExtraPanelVisible.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorExtraPanelVisible.Text = "Extra panel";
            this.bindingNavigatorExtraPanelVisible.Visible = false;
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            this.GridView.AutoGenerateColumns = false;
            this.GridView.DataSource = this.BindingSource;
            this.GridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.GridView.Location = new System.Drawing.Point(0, 0);
            this.GridView.Name = "GridView";
            this.GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView.Size = new System.Drawing.Size(1193, 511);
            this.GridView.TabIndex = 0;
            this.GridView.VirtualMode = true;
            this.GridView.ColumnHeadersHeightChanged += new System.EventHandler(this.GridView_ColumnHeadersHeightChanged);
            this.GridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridView_CellDoubleClick);
            this.GridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridView_CellMouseUp);
            this.GridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridView_ColumnHeaderMouseClick);
            this.GridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.GridView_ColumnWidthChanged);
            this.GridView.SelectionChanged += new System.EventHandler(this.GridView_SelectionChanged);
            this.GridView.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.GridView_SortCompare);
            this.GridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridView_KeyDown);
            this.GridView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GridView_KeyPress);
            // 
            // DataGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1195, 623);
            this.Controls.Add(this.LeftPanel);
            this.KeyPreview = true;
            this.Name = "DataGridForm";
            this.Text = "DataGridForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DataGridForm_KeyPress);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.DataGridForm_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.LeftPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftPanel)).EndInit();
            this.LeftPanel.ResumeLayout(false);
            this.TopPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopPanel)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridNavigator)).EndInit();
            this.GridNavigator.ResumeLayout(false);
            this.GridNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer LeftPanel;
        protected System.Windows.Forms.SplitContainer TopPanel;
        protected Controls.GridNavigator GridNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.ComponentModel.BackgroundWorker loadRowsWorker;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripButton bindingNavigatorRefreshItem;
        protected System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripButton bindingNavigatorGridConfig;
        private System.Windows.Forms.ToolStripButton bindingNavigatorExtraPanelVisible;
        private System.Windows.Forms.ToolStripButton bindingNavigatorEditItem;
        public Controls.GridView GridView;
    }
}
