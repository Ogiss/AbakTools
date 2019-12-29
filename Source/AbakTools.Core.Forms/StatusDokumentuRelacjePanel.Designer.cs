namespace AbakTools.Core.Forms
{
    partial class StatusDokumentuRelacjePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusDokumentuRelacjePanel));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.nadrzedneTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.nadrzedneBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.nadrzedneBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dodajNadrzednyButton = new System.Windows.Forms.ToolStripButton();
            this.usunNadrzednyButton = new System.Windows.Forms.ToolStripButton();
            this.nadrzedneGridView = new AbakTools.Forms.Controls.GridView();
            this.nadrzednyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.podrzedneTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.podrzedneBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.podrzedneBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorCountItem1 = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem1 = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.dodajPodrzednyButton = new System.Windows.Forms.ToolStripButton();
            this.usunPodrzednyButton = new System.Windows.Forms.ToolStripButton();
            this.podrzedneGridView = new AbakTools.Forms.Controls.GridView();
            this.podrzednyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.nadrzedneTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nadrzedneBindingNavigator)).BeginInit();
            this.nadrzedneBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nadrzedneBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nadrzedneGridView)).BeginInit();
            this.podrzedneTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.podrzedneBindingNavigator)).BeginInit();
            this.podrzedneBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.podrzedneBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.podrzedneGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.nadrzedneTableLayoutPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.podrzedneTableLayoutPanel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(546, 385);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // nadrzedneTableLayoutPanel
            // 
            this.nadrzedneTableLayoutPanel.ColumnCount = 1;
            this.nadrzedneTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.nadrzedneTableLayoutPanel.Controls.Add(this.nadrzedneBindingNavigator, 0, 0);
            this.nadrzedneTableLayoutPanel.Controls.Add(this.nadrzedneGridView, 0, 1);
            this.nadrzedneTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nadrzedneTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.nadrzedneTableLayoutPanel.Name = "nadrzedneTableLayoutPanel";
            this.nadrzedneTableLayoutPanel.RowCount = 2;
            this.nadrzedneTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.nadrzedneTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.nadrzedneTableLayoutPanel.Size = new System.Drawing.Size(540, 186);
            this.nadrzedneTableLayoutPanel.TabIndex = 0;
            // 
            // nadrzedneBindingNavigator
            // 
            this.nadrzedneBindingNavigator.AddNewItem = null;
            this.nadrzedneBindingNavigator.BindingSource = this.nadrzedneBindingSource;
            this.nadrzedneBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.nadrzedneBindingNavigator.DeleteItem = null;
            this.nadrzedneBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.dodajNadrzednyButton,
            this.usunNadrzednyButton});
            this.nadrzedneBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.nadrzedneBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.nadrzedneBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.nadrzedneBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.nadrzedneBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.nadrzedneBindingNavigator.Name = "nadrzedneBindingNavigator";
            this.nadrzedneBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.nadrzedneBindingNavigator.Size = new System.Drawing.Size(540, 25);
            this.nadrzedneBindingNavigator.TabIndex = 0;
            this.nadrzedneBindingNavigator.Text = "bindingNavigator1";
            // 
            // nadrzedneBindingSource
            // 
            this.nadrzedneBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.RelacjaStatDok);
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
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
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
            // dodajNadrzednyButton
            // 
            this.dodajNadrzednyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dodajNadrzednyButton.Image = ((System.Drawing.Image)(resources.GetObject("dodajNadrzednyButton.Image")));
            this.dodajNadrzednyButton.Name = "dodajNadrzednyButton";
            this.dodajNadrzednyButton.RightToLeftAutoMirrorImage = true;
            this.dodajNadrzednyButton.Size = new System.Drawing.Size(23, 22);
            this.dodajNadrzednyButton.Text = "Add new";
            this.dodajNadrzednyButton.Click += new System.EventHandler(this.dodajNadrzednyButton_Click);
            // 
            // usunNadrzednyButton
            // 
            this.usunNadrzednyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.usunNadrzednyButton.Image = ((System.Drawing.Image)(resources.GetObject("usunNadrzednyButton.Image")));
            this.usunNadrzednyButton.Name = "usunNadrzednyButton";
            this.usunNadrzednyButton.RightToLeftAutoMirrorImage = true;
            this.usunNadrzednyButton.Size = new System.Drawing.Size(23, 22);
            this.usunNadrzednyButton.Text = "Delete";
            this.usunNadrzednyButton.Click += new System.EventHandler(this.usunNadrzednyButton_Click);
            // 
            // nadrzedneGridView
            // 
            this.nadrzedneGridView.AllowUserToAddRows = false;
            this.nadrzedneGridView.AllowUserToDeleteRows = false;
            this.nadrzedneGridView.AutoGenerateColumns = false;
            this.nadrzedneGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nadrzednyColumn});
            this.nadrzedneGridView.DataContext = null;
            this.nadrzedneGridView.DataSource = this.nadrzedneBindingSource;
            this.nadrzedneGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nadrzedneGridView.Location = new System.Drawing.Point(3, 28);
            this.nadrzedneGridView.Name = "nadrzedneGridView";
            this.nadrzedneGridView.RowHeadersVisible = false;
            this.nadrzedneGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.nadrzedneGridView.Size = new System.Drawing.Size(534, 155);
            this.nadrzedneGridView.TabIndex = 1;
            this.nadrzedneGridView.VirtualMode = true;
            // 
            // nadrzednyColumn
            // 
            this.nadrzednyColumn.DataPropertyName = "Nadrzedny";
            this.nadrzednyColumn.FillWeight = 300F;
            this.nadrzednyColumn.HeaderText = "Nadrzędny";
            this.nadrzednyColumn.Name = "nadrzednyColumn";
            this.nadrzednyColumn.ReadOnly = true;
            this.nadrzednyColumn.Width = 300;
            // 
            // podrzedneTableLayoutPanel
            // 
            this.podrzedneTableLayoutPanel.ColumnCount = 1;
            this.podrzedneTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.podrzedneTableLayoutPanel.Controls.Add(this.podrzedneBindingNavigator, 0, 0);
            this.podrzedneTableLayoutPanel.Controls.Add(this.podrzedneGridView, 0, 1);
            this.podrzedneTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.podrzedneTableLayoutPanel.Location = new System.Drawing.Point(3, 195);
            this.podrzedneTableLayoutPanel.Name = "podrzedneTableLayoutPanel";
            this.podrzedneTableLayoutPanel.RowCount = 2;
            this.podrzedneTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.podrzedneTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.podrzedneTableLayoutPanel.Size = new System.Drawing.Size(540, 187);
            this.podrzedneTableLayoutPanel.TabIndex = 1;
            // 
            // podrzedneBindingNavigator
            // 
            this.podrzedneBindingNavigator.AddNewItem = null;
            this.podrzedneBindingNavigator.BindingSource = this.podrzedneBindingSource;
            this.podrzedneBindingNavigator.CountItem = this.bindingNavigatorCountItem1;
            this.podrzedneBindingNavigator.DeleteItem = null;
            this.podrzedneBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem1,
            this.bindingNavigatorMovePreviousItem1,
            this.bindingNavigatorSeparator3,
            this.bindingNavigatorPositionItem1,
            this.bindingNavigatorCountItem1,
            this.bindingNavigatorSeparator4,
            this.bindingNavigatorMoveNextItem1,
            this.bindingNavigatorMoveLastItem1,
            this.bindingNavigatorSeparator5,
            this.dodajPodrzednyButton,
            this.usunPodrzednyButton});
            this.podrzedneBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.podrzedneBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem1;
            this.podrzedneBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem1;
            this.podrzedneBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem1;
            this.podrzedneBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem1;
            this.podrzedneBindingNavigator.Name = "podrzedneBindingNavigator";
            this.podrzedneBindingNavigator.PositionItem = this.bindingNavigatorPositionItem1;
            this.podrzedneBindingNavigator.Size = new System.Drawing.Size(540, 25);
            this.podrzedneBindingNavigator.TabIndex = 0;
            this.podrzedneBindingNavigator.Text = "bindingNavigator2";
            // 
            // podrzedneBindingSource
            // 
            this.podrzedneBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.RelacjaStatDok);
            // 
            // bindingNavigatorCountItem1
            // 
            this.bindingNavigatorCountItem1.Name = "bindingNavigatorCountItem1";
            this.bindingNavigatorCountItem1.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem1.Text = "of {0}";
            this.bindingNavigatorCountItem1.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem1
            // 
            this.bindingNavigatorMoveFirstItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem1.Image")));
            this.bindingNavigatorMoveFirstItem1.Name = "bindingNavigatorMoveFirstItem1";
            this.bindingNavigatorMoveFirstItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem1.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem1
            // 
            this.bindingNavigatorMovePreviousItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem1.Image")));
            this.bindingNavigatorMovePreviousItem1.Name = "bindingNavigatorMovePreviousItem1";
            this.bindingNavigatorMovePreviousItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem1.Text = "Move previous";
            // 
            // bindingNavigatorSeparator3
            // 
            this.bindingNavigatorSeparator3.Name = "bindingNavigatorSeparator3";
            this.bindingNavigatorSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem1
            // 
            this.bindingNavigatorPositionItem1.AccessibleName = "Position";
            this.bindingNavigatorPositionItem1.AutoSize = false;
            this.bindingNavigatorPositionItem1.Name = "bindingNavigatorPositionItem1";
            this.bindingNavigatorPositionItem1.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem1.Text = "0";
            this.bindingNavigatorPositionItem1.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator4
            // 
            this.bindingNavigatorSeparator4.Name = "bindingNavigatorSeparator4";
            this.bindingNavigatorSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem1
            // 
            this.bindingNavigatorMoveNextItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem1.Image")));
            this.bindingNavigatorMoveNextItem1.Name = "bindingNavigatorMoveNextItem1";
            this.bindingNavigatorMoveNextItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem1.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem1
            // 
            this.bindingNavigatorMoveLastItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem1.Image")));
            this.bindingNavigatorMoveLastItem1.Name = "bindingNavigatorMoveLastItem1";
            this.bindingNavigatorMoveLastItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem1.Text = "Move last";
            // 
            // bindingNavigatorSeparator5
            // 
            this.bindingNavigatorSeparator5.Name = "bindingNavigatorSeparator5";
            this.bindingNavigatorSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // dodajPodrzednyButton
            // 
            this.dodajPodrzednyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dodajPodrzednyButton.Image = ((System.Drawing.Image)(resources.GetObject("dodajPodrzednyButton.Image")));
            this.dodajPodrzednyButton.Name = "dodajPodrzednyButton";
            this.dodajPodrzednyButton.RightToLeftAutoMirrorImage = true;
            this.dodajPodrzednyButton.Size = new System.Drawing.Size(23, 22);
            this.dodajPodrzednyButton.Text = "Add new";
            this.dodajPodrzednyButton.Click += new System.EventHandler(this.dodajPodrzednyButton_Click);
            // 
            // usunPodrzednyButton
            // 
            this.usunPodrzednyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.usunPodrzednyButton.Image = ((System.Drawing.Image)(resources.GetObject("usunPodrzednyButton.Image")));
            this.usunPodrzednyButton.Name = "usunPodrzednyButton";
            this.usunPodrzednyButton.RightToLeftAutoMirrorImage = true;
            this.usunPodrzednyButton.Size = new System.Drawing.Size(23, 22);
            this.usunPodrzednyButton.Text = "Delete";
            this.usunPodrzednyButton.Click += new System.EventHandler(this.usunPodrzednyButton_Click);
            // 
            // podrzedneGridView
            // 
            this.podrzedneGridView.AllowUserToAddRows = false;
            this.podrzedneGridView.AllowUserToDeleteRows = false;
            this.podrzedneGridView.AutoGenerateColumns = false;
            this.podrzedneGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.podrzednyColumn});
            this.podrzedneGridView.DataContext = null;
            this.podrzedneGridView.DataSource = this.podrzedneBindingSource;
            this.podrzedneGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.podrzedneGridView.Location = new System.Drawing.Point(3, 28);
            this.podrzedneGridView.Name = "podrzedneGridView";
            this.podrzedneGridView.RowHeadersVisible = false;
            this.podrzedneGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.podrzedneGridView.Size = new System.Drawing.Size(534, 156);
            this.podrzedneGridView.TabIndex = 1;
            this.podrzedneGridView.VirtualMode = true;
            // 
            // podrzednyColumn
            // 
            this.podrzednyColumn.DataPropertyName = "Podrzedny";
            this.podrzednyColumn.FillWeight = 300F;
            this.podrzednyColumn.HeaderText = "Podrzędny";
            this.podrzednyColumn.Name = "podrzednyColumn";
            this.podrzednyColumn.ReadOnly = true;
            this.podrzednyColumn.Width = 300;
            // 
            // StatusDokumentuRelacjePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StatusDokumentuRelacjePanel";
            this.Size = new System.Drawing.Size(546, 385);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.nadrzedneTableLayoutPanel.ResumeLayout(false);
            this.nadrzedneTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nadrzedneBindingNavigator)).EndInit();
            this.nadrzedneBindingNavigator.ResumeLayout(false);
            this.nadrzedneBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nadrzedneBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nadrzedneGridView)).EndInit();
            this.podrzedneTableLayoutPanel.ResumeLayout(false);
            this.podrzedneTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.podrzedneBindingNavigator)).EndInit();
            this.podrzedneBindingNavigator.ResumeLayout(false);
            this.podrzedneBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.podrzedneBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.podrzedneGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel nadrzedneTableLayoutPanel;
        private System.Windows.Forms.BindingNavigator nadrzedneBindingNavigator;
        private System.Windows.Forms.ToolStripButton dodajNadrzednyButton;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton usunNadrzednyButton;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.TableLayoutPanel podrzedneTableLayoutPanel;
        private System.Windows.Forms.BindingNavigator podrzedneBindingNavigator;
        private System.Windows.Forms.ToolStripButton dodajPodrzednyButton;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem1;
        private System.Windows.Forms.ToolStripButton usunPodrzednyButton;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem1;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator3;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem1;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator4;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem1;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator5;
        private AbakTools.Forms.Controls.GridView nadrzedneGridView;
        private AbakTools.Forms.Controls.GridView podrzedneGridView;
        private System.Windows.Forms.BindingSource nadrzedneBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn nadrzednyColumn;
        private System.Windows.Forms.BindingSource podrzedneBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn podrzednyColumn;
    }
}
