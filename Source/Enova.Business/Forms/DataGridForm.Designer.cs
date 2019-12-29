namespace Enova.Business.Old.Forms
{
    partial class DataGridForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridForm));
            this.DataGridBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.DataGridBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorEditItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorRefreshItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPrintItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorEmailItem = new System.Windows.Forms.ToolStripButton();
            this.fileOpentoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSavelItem = new System.Windows.Forms.ToolStripButton();
            this.TopSplitContainer = new System.Windows.Forms.SplitContainer();
            this.LeftSplitContainer = new System.Windows.Forms.SplitContainer();
            this.RightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.BottomSplitContainer = new System.Windows.Forms.SplitContainer();
            this.DataGrid = new Enova.Business.Old.Controls.DataGrid();
            this.DataGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.zapiszKonfiguracjęTabeliToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingNavigator)).BeginInit();
            this.DataGridBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).BeginInit();
            this.TopSplitContainer.Panel2.SuspendLayout();
            this.TopSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftSplitContainer)).BeginInit();
            this.LeftSplitContainer.Panel2.SuspendLayout();
            this.LeftSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RightSplitContainer)).BeginInit();
            this.RightSplitContainer.Panel1.SuspendLayout();
            this.RightSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomSplitContainer)).BeginInit();
            this.BottomSplitContainer.Panel1.SuspendLayout();
            this.BottomSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.DataGridContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridBindingNavigator
            // 
            this.DataGridBindingNavigator.AddNewItem = null;
            this.DataGridBindingNavigator.BindingSource = this.DataGridBindingSource;
            this.DataGridBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.DataGridBindingNavigator.DeleteItem = null;
            this.DataGridBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorEditItem,
            this.bindingNavigatorDeleteItem,
            this.bindingNavigatorRefreshItem,
            this.toolStripSeparator1,
            this.bindingNavigatorPrintItem,
            this.bindingNavigatorEmailItem,
            this.fileOpentoolStripButton,
            this.bindingNavigatorSavelItem});
            this.DataGridBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.DataGridBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.DataGridBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.DataGridBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.DataGridBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.DataGridBindingNavigator.Name = "DataGridBindingNavigator";
            this.DataGridBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.DataGridBindingNavigator.Size = new System.Drawing.Size(1351, 25);
            this.DataGridBindingNavigator.TabIndex = 2;
            this.DataGridBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // bindingNavigatorEditItem
            // 
            this.bindingNavigatorEditItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorEditItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorEditItem.Image")));
            this.bindingNavigatorEditItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorEditItem.Name = "bindingNavigatorEditItem";
            this.bindingNavigatorEditItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorEditItem.Text = "Edytuj";
            this.bindingNavigatorEditItem.Click += new System.EventHandler(this.bindingNavigatorEditItem_Click);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // bindingNavigatorRefreshItem
            // 
            this.bindingNavigatorRefreshItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorRefreshItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorRefreshItem.Image")));
            this.bindingNavigatorRefreshItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorRefreshItem.Name = "bindingNavigatorRefreshItem";
            this.bindingNavigatorRefreshItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorRefreshItem.Text = "Odświerz";
            this.bindingNavigatorRefreshItem.Click += new System.EventHandler(this.bindingNavigatorRefreshItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPrintItem
            // 
            this.bindingNavigatorPrintItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorPrintItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorPrintItem.Image")));
            this.bindingNavigatorPrintItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorPrintItem.Name = "bindingNavigatorPrintItem";
            this.bindingNavigatorPrintItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorPrintItem.Text = "Drukuj";
            // 
            // bindingNavigatorEmailItem
            // 
            this.bindingNavigatorEmailItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorEmailItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorEmailItem.Image")));
            this.bindingNavigatorEmailItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorEmailItem.Name = "bindingNavigatorEmailItem";
            this.bindingNavigatorEmailItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorEmailItem.Text = "toolStripButton1";
            // 
            // fileOpentoolStripButton
            // 
            this.fileOpentoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fileOpentoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("fileOpentoolStripButton.Image")));
            this.fileOpentoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileOpentoolStripButton.Name = "fileOpentoolStripButton";
            this.fileOpentoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.fileOpentoolStripButton.Text = "Open file";
            this.fileOpentoolStripButton.Visible = false;
            this.fileOpentoolStripButton.Click += new System.EventHandler(this.fileOpentoolStripButton_Click);
            // 
            // bindingNavigatorSavelItem
            // 
            this.bindingNavigatorSavelItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorSavelItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorSavelItem.Image")));
            this.bindingNavigatorSavelItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorSavelItem.Name = "bindingNavigatorSavelItem";
            this.bindingNavigatorSavelItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorSavelItem.Text = "toolStripButton1";
            // 
            // TopSplitContainer
            // 
            this.TopSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.TopSplitContainer.Name = "TopSplitContainer";
            this.TopSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // TopSplitContainer.Panel2
            // 
            this.TopSplitContainer.Panel2.Controls.Add(this.LeftSplitContainer);
            this.TopSplitContainer.Size = new System.Drawing.Size(1351, 664);
            this.TopSplitContainer.SplitterDistance = 80;
            this.TopSplitContainer.TabIndex = 4;
            this.TopSplitContainer.TabStop = false;
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.LeftSplitContainer.Name = "LeftSplitContainer";
            // 
            // LeftSplitContainer.Panel2
            // 
            this.LeftSplitContainer.Panel2.Controls.Add(this.RightSplitContainer);
            this.LeftSplitContainer.Size = new System.Drawing.Size(1351, 580);
            this.LeftSplitContainer.SplitterDistance = 148;
            this.LeftSplitContainer.TabIndex = 1;
            this.LeftSplitContainer.TabStop = false;
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.RightSplitContainer.Name = "RightSplitContainer";
            // 
            // RightSplitContainer.Panel1
            // 
            this.RightSplitContainer.Panel1.Controls.Add(this.BottomSplitContainer);
            this.RightSplitContainer.Size = new System.Drawing.Size(1199, 580);
            this.RightSplitContainer.SplitterDistance = 1064;
            this.RightSplitContainer.TabIndex = 3;
            this.RightSplitContainer.TabStop = false;
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.BottomSplitContainer.Name = "BottomSplitContainer";
            this.BottomSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // BottomSplitContainer.Panel1
            // 
            this.BottomSplitContainer.Panel1.Controls.Add(this.DataGrid);
            this.BottomSplitContainer.Size = new System.Drawing.Size(1064, 580);
            this.BottomSplitContainer.SplitterDistance = 480;
            this.BottomSplitContainer.TabIndex = 0;
            // 
            // DataGrid
            // 
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.AllowUserToDeleteRows = false;
            this.DataGrid.AutoGenerateColumns = false;
            this.DataGrid.ContextMenuStrip = this.DataGridContextMenuStrip;
            this.DataGrid.DataSource = this.DataGridBindingSource;
            this.DataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGrid.Location = new System.Drawing.Point(0, 0);
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.ParentForm = null;
            this.DataGrid.ReadOnly = true;
            this.DataGrid.RowHeadersVisible = false;
            this.DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGrid.Size = new System.Drawing.Size(1064, 480);
            this.DataGrid.TabIndex = 0;
            this.DataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellContentClick);
            this.DataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellDoubleClick);
            this.DataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGrid_DataError);
            this.DataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGrid_KeyDown);
            // 
            // DataGridContextMenuStrip
            // 
            this.DataGridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zapiszKonfiguracjęTabeliToolStripMenuItem});
            this.DataGridContextMenuStrip.Name = "DataGridContextMenuStrip";
            this.DataGridContextMenuStrip.Size = new System.Drawing.Size(209, 26);
            // 
            // zapiszKonfiguracjęTabeliToolStripMenuItem
            // 
            this.zapiszKonfiguracjęTabeliToolStripMenuItem.Name = "zapiszKonfiguracjęTabeliToolStripMenuItem";
            this.zapiszKonfiguracjęTabeliToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.zapiszKonfiguracjęTabeliToolStripMenuItem.Text = "Zapisz konfigurację tabeli";
            this.zapiszKonfiguracjęTabeliToolStripMenuItem.Click += new System.EventHandler(this.zapiszKonfiguracjęTabeliToolStripMenuItem_Click);
            // 
            // DataGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1351, 689);
            this.Controls.Add(this.TopSplitContainer);
            this.Controls.Add(this.DataGridBindingNavigator);
            this.KeyPreview = true;
            this.Name = "DataGridForm";
            this.Shown += new System.EventHandler(this.DataGridForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingNavigator)).EndInit();
            this.DataGridBindingNavigator.ResumeLayout(false);
            this.DataGridBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).EndInit();
            this.TopSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).EndInit();
            this.TopSplitContainer.ResumeLayout(false);
            this.LeftSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftSplitContainer)).EndInit();
            this.LeftSplitContainer.ResumeLayout(false);
            this.RightSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightSplitContainer)).EndInit();
            this.RightSplitContainer.ResumeLayout(false);
            this.BottomSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BottomSplitContainer)).EndInit();
            this.BottomSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.DataGridContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.BindingNavigator DataGridBindingNavigator;
        public System.Windows.Forms.SplitContainer TopSplitContainer;
        public System.Windows.Forms.BindingSource DataGridBindingSource;
        public System.Windows.Forms.SplitContainer LeftSplitContainer;
        public System.Windows.Forms.SplitContainer RightSplitContainer;
        public System.Windows.Forms.SplitContainer BottomSplitContainer;
        public Enova.Business.Old.Controls.DataGrid DataGrid;
        public System.Windows.Forms.ContextMenuStrip DataGridContextMenuStrip;
        public System.Windows.Forms.ToolStripMenuItem zapiszKonfiguracjęTabeliToolStripMenuItem;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton bindingNavigatorPrintItem;
        public System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        public System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        public System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        public System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        public System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        public System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        public System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        public System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        public System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        public System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        public System.Windows.Forms.ToolStripButton bindingNavigatorEditItem;
        public System.Windows.Forms.ToolStripButton bindingNavigatorRefreshItem;
        public System.Windows.Forms.ToolStripButton bindingNavigatorSavelItem;
        public System.Windows.Forms.ToolStripButton fileOpentoolStripButton;
        public System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        public System.Windows.Forms.ToolStripButton bindingNavigatorEmailItem;
    }
}
