namespace BAL.Forms.Controls
{
    partial class GridViewControl
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
            this.verticalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.bottomSplitContainer = new System.Windows.Forms.SplitContainer();
            this.topSplitContainer = new System.Windows.Forms.SplitContainer();
            this.GridNavigator = new BAL.Forms.Controls.GridNavigator();
            this.countItem = new System.Windows.Forms.ToolStripLabel();
            this.moveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.movePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.positionItem = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextItem = new System.Windows.Forms.ToolStripButton();
            this.moveLastItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewItem = new System.Windows.Forms.ToolStripButton();
            this.editItem = new System.Windows.Forms.ToolStripButton();
            this.deleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshItem = new System.Windows.Forms.ToolStripButton();
            this.gridConfigItem = new System.Windows.Forms.ToolStripButton();
            this.extraPanelVisibleItem = new System.Windows.Forms.ToolStripButton();
            this.printItem = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.GridView = new BAL.Forms.Controls.GridView();
            this.BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.loadRowsWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.verticalSplitContainer)).BeginInit();
            this.verticalSplitContainer.Panel2.SuspendLayout();
            this.verticalSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomSplitContainer)).BeginInit();
            this.bottomSplitContainer.Panel1.SuspendLayout();
            this.bottomSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topSplitContainer)).BeginInit();
            this.topSplitContainer.Panel2.SuspendLayout();
            this.topSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridNavigator)).BeginInit();
            this.GridNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // verticalSplitContainer
            // 
            this.verticalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.verticalSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.verticalSplitContainer.Name = "verticalSplitContainer";
            this.verticalSplitContainer.Panel1Collapsed = true;
            // 
            // verticalSplitContainer.Panel2
            // 
            this.verticalSplitContainer.Panel2.Controls.Add(this.bottomSplitContainer);
            this.verticalSplitContainer.Panel2MinSize = 20;
            this.verticalSplitContainer.Size = new System.Drawing.Size(445, 244);
            this.verticalSplitContainer.SplitterDistance = 148;
            this.verticalSplitContainer.SplitterWidth = 2;
            this.verticalSplitContainer.TabIndex = 0;
            this.verticalSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.verticalSplitContainer_SplitterMoved);
            // 
            // bottomSplitContainer
            // 
            this.bottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.bottomSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.bottomSplitContainer.Name = "bottomSplitContainer";
            this.bottomSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // bottomSplitContainer.Panel1
            // 
            this.bottomSplitContainer.Panel1.Controls.Add(this.topSplitContainer);
            this.bottomSplitContainer.Panel2Collapsed = true;
            this.bottomSplitContainer.Size = new System.Drawing.Size(445, 244);
            this.bottomSplitContainer.SplitterDistance = 167;
            this.bottomSplitContainer.TabIndex = 0;
            // 
            // topSplitContainer
            // 
            this.topSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.topSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.topSplitContainer.Name = "topSplitContainer";
            this.topSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.topSplitContainer.Panel1Collapsed = true;
            // 
            // topSplitContainer.Panel2
            // 
            this.topSplitContainer.Panel2.Controls.Add(this.GridNavigator);
            this.topSplitContainer.Panel2.Controls.Add(this.GridView);
            this.topSplitContainer.Size = new System.Drawing.Size(445, 244);
            this.topSplitContainer.SplitterDistance = 35;
            this.topSplitContainer.TabIndex = 2;
            this.topSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.topSplitContainer_SplitterMoved);
            // 
            // GridNavigator
            // 
            this.GridNavigator.AddNewItem = null;
            this.GridNavigator.BindingSource = this.BindingSource;
            this.GridNavigator.CountItem = this.countItem;
            this.GridNavigator.DeleteItem = null;
            this.GridNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveFirstItem,
            this.movePreviousItem,
            this.toolStripSeparator1,
            this.positionItem,
            this.countItem,
            this.toolStripSeparator2,
            this.moveNextItem,
            this.moveLastItem,
            this.toolStripSeparator3,
            this.addNewItem,
            this.editItem,
            this.deleteItem,
            this.toolStripSeparator4,
            this.refreshItem,
            this.gridConfigItem,
            this.extraPanelVisibleItem,
            this.printItem});
            this.GridNavigator.Location = new System.Drawing.Point(0, 0);
            this.GridNavigator.MoveFirstItem = this.moveFirstItem;
            this.GridNavigator.MoveLastItem = this.moveLastItem;
            this.GridNavigator.MoveNextItem = this.moveNextItem;
            this.GridNavigator.MovePreviousItem = this.movePreviousItem;
            this.GridNavigator.Name = "GridNavigator";
            this.GridNavigator.PositionItem = this.positionItem;
            this.GridNavigator.Size = new System.Drawing.Size(445, 25);
            this.GridNavigator.TabIndex = 0;
            this.GridNavigator.Text = "gridNavigator1";
            // 
            // countItem
            // 
            this.countItem.Name = "countItem";
            this.countItem.Size = new System.Drawing.Size(35, 22);
            this.countItem.Text = "of {0}";
            // 
            // moveFirstItem
            // 
            this.moveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstItem.Image = global::BAL.Forms.Properties.Resources.MoveFirstItem;
            this.moveFirstItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstItem.Name = "moveFirstItem";
            this.moveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.moveFirstItem.Text = "Move first";
            // 
            // movePreviousItem
            // 
            this.movePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousItem.Image = global::BAL.Forms.Properties.Resources.MovePreviousItem;
            this.movePreviousItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousItem.Name = "movePreviousItem";
            this.movePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.movePreviousItem.Text = "Move previous";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // positionItem
            // 
            this.positionItem.Name = "positionItem";
            this.positionItem.Size = new System.Drawing.Size(80, 25);
            this.positionItem.Text = "0";
            this.positionItem.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextItem
            // 
            this.moveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextItem.Image = global::BAL.Forms.Properties.Resources.MoveNextItem;
            this.moveNextItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextItem.Name = "moveNextItem";
            this.moveNextItem.Size = new System.Drawing.Size(23, 22);
            this.moveNextItem.Text = "Move next";
            // 
            // moveLastItem
            // 
            this.moveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastItem.Image = global::BAL.Forms.Properties.Resources.MoveLastItem;
            this.moveLastItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastItem.Name = "moveLastItem";
            this.moveLastItem.Size = new System.Drawing.Size(23, 22);
            this.moveLastItem.Text = "Move last";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // addNewItem
            // 
            this.addNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addNewItem.Image = global::BAL.Forms.Properties.Resources.AddNewItem;
            this.addNewItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addNewItem.Name = "addNewItem";
            this.addNewItem.Size = new System.Drawing.Size(23, 22);
            this.addNewItem.Text = "toolStripButton1";
            // 
            // editItem
            // 
            this.editItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editItem.Image = global::BAL.Forms.Properties.Resources.OpenForm;
            this.editItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editItem.Name = "editItem";
            this.editItem.Size = new System.Drawing.Size(23, 22);
            this.editItem.Text = "toolStripButton1";
            // 
            // deleteItem
            // 
            this.deleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteItem.Image = global::BAL.Forms.Properties.Resources.DeleteItem;
            this.deleteItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteItem.Name = "deleteItem";
            this.deleteItem.Size = new System.Drawing.Size(23, 22);
            this.deleteItem.Text = "toolStripButton1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshItem
            // 
            this.refreshItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshItem.Image = global::BAL.Forms.Properties.Resources.Refresh;
            this.refreshItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshItem.Name = "refreshItem";
            this.refreshItem.Size = new System.Drawing.Size(23, 22);
            this.refreshItem.Text = "toolStripButton1";
            // 
            // gridConfigItem
            // 
            this.gridConfigItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.gridConfigItem.Image = global::BAL.Forms.Properties.Resources.tools_16;
            this.gridConfigItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gridConfigItem.Name = "gridConfigItem";
            this.gridConfigItem.Size = new System.Drawing.Size(23, 22);
            this.gridConfigItem.Text = "toolStripButton1";
            // 
            // extraPanelVisibleItem
            // 
            this.extraPanelVisibleItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.extraPanelVisibleItem.Image = global::BAL.Forms.Properties.Resources.LeftView;
            this.extraPanelVisibleItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.extraPanelVisibleItem.Name = "extraPanelVisibleItem";
            this.extraPanelVisibleItem.Size = new System.Drawing.Size(23, 22);
            this.extraPanelVisibleItem.Text = "toolStripButton1";
            // 
            // printItem
            // 
            this.printItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.printItem.Image = global::BAL.Forms.Properties.Resources.Print;
            this.printItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printItem.Name = "printItem";
            this.printItem.Size = new System.Drawing.Size(32, 22);
            this.printItem.Text = "Print";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItem1.Text = "Zrzut formularza";
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            this.GridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridView.AutoGenerateColumns = false;
            this.GridView.DataSource = this.BindingSource;
            this.GridView.Location = new System.Drawing.Point(0, 28);
            this.GridView.Name = "GridView";
            this.GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView.Size = new System.Drawing.Size(442, 213);
            this.GridView.TabIndex = 1;
            this.GridView.VirtualMode = true;
            this.GridView.ColumnHeadersHeightChanged += new System.EventHandler(this.GridView_ColumnHeadersHeightChanged);
            this.GridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.GridView_ColumnWidthChanged);
            this.GridView.SelectionChanged += new System.EventHandler(this.GridView_SelectionChanged);
            // 
            // loadRowsWorker
            // 
            this.loadRowsWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loadRowsWorker_DoWork);
            // 
            // GridViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.verticalSplitContainer);
            this.Name = "GridViewControl";
            this.Size = new System.Drawing.Size(445, 244);
            this.verticalSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.verticalSplitContainer)).EndInit();
            this.verticalSplitContainer.ResumeLayout(false);
            this.bottomSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bottomSplitContainer)).EndInit();
            this.bottomSplitContainer.ResumeLayout(false);
            this.topSplitContainer.Panel2.ResumeLayout(false);
            this.topSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topSplitContainer)).EndInit();
            this.topSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridNavigator)).EndInit();
            this.GridNavigator.ResumeLayout(false);
            this.GridNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer verticalSplitContainer;
        protected GridNavigator GridNavigator;
        protected GridView GridView;
        private System.Windows.Forms.ToolStripButton moveFirstItem;
        private System.Windows.Forms.ToolStripButton movePreviousItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox positionItem;
        private System.Windows.Forms.ToolStripLabel countItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton moveNextItem;
        private System.Windows.Forms.ToolStripButton moveLastItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton addNewItem;
        private System.Windows.Forms.ToolStripButton editItem;
        private System.Windows.Forms.ToolStripButton deleteItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton refreshItem;
        private System.Windows.Forms.ToolStripButton gridConfigItem;
        private System.Windows.Forms.ToolStripButton extraPanelVisibleItem;
        private System.Windows.Forms.ToolStripSplitButton printItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        protected System.Windows.Forms.BindingSource BindingSource;
        private System.Windows.Forms.SplitContainer bottomSplitContainer;
        private System.Windows.Forms.SplitContainer topSplitContainer;
        private System.ComponentModel.BackgroundWorker loadRowsWorker;

    }
}
