namespace AbakTools.Handel.Forms
{
    partial class ReklamacjaOgolnePanel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReklamacjaOgolnePanel));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wgDokSprzButton = new System.Windows.Forms.Button();
            this.rozdzielButton = new System.Windows.Forms.Button();
            this.generujZwrotButton = new System.Windows.Forms.Button();
            this.opisTextBox = new System.Windows.Forms.TextBox();
            this.historiaDokumentuControl = new AbakTools.Core.Controls.HistoriaDokumentuControl();
            this.kontrahentSelect = new AbakTools.CRM.Forms.Controls.KontrahentSelect();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pozycjeReklamacjiGrid = new AbakTools.Handel.Forms.PozycjeReklamacjiGrid();
            this.LpColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.towarNazwaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.korektaColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lloscBrakiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iloscNadwyzkiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cenaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpisColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pozycjeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeReklamacjiGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).BeginInit();
            this.bindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // BindingSource
            // 
            this.BindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Reklamacja);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.wgDokSprzButton);
            this.splitContainer1.Panel1.Controls.Add(this.rozdzielButton);
            this.splitContainer1.Panel1.Controls.Add(this.generujZwrotButton);
            this.splitContainer1.Panel1.Controls.Add(this.opisTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.historiaDokumentuControl);
            this.splitContainer1.Panel1.Controls.Add(this.kontrahentSelect);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pozycjeReklamacjiGrid);
            this.splitContainer1.Panel2.Controls.Add(this.bindingNavigator);
            this.splitContainer1.Size = new System.Drawing.Size(1159, 574);
            this.splitContainer1.SplitterDistance = 128;
            this.splitContainer1.TabIndex = 0;
            // 
            // wgDokSprzButton
            // 
            this.wgDokSprzButton.Location = new System.Drawing.Point(887, 4);
            this.wgDokSprzButton.Name = "wgDokSprzButton";
            this.wgDokSprzButton.Size = new System.Drawing.Size(99, 23);
            this.wgDokSprzButton.TabIndex = 10;
            this.wgDokSprzButton.Text = "Wg dok. sprzed.";
            this.wgDokSprzButton.UseVisualStyleBackColor = true;
            this.wgDokSprzButton.Click += new System.EventHandler(this.wgDokSprzButton_Click);
            // 
            // rozdzielButton
            // 
            this.rozdzielButton.Location = new System.Drawing.Point(887, 62);
            this.rozdzielButton.Name = "rozdzielButton";
            this.rozdzielButton.Size = new System.Drawing.Size(99, 23);
            this.rozdzielButton.TabIndex = 9;
            this.rozdzielButton.Text = "Rozdziel";
            this.rozdzielButton.UseVisualStyleBackColor = true;
            this.rozdzielButton.Visible = false;
            this.rozdzielButton.Click += new System.EventHandler(this.rozdzielButton_Click);
            // 
            // generujZwrotButton
            // 
            this.generujZwrotButton.Location = new System.Drawing.Point(887, 33);
            this.generujZwrotButton.Name = "generujZwrotButton";
            this.generujZwrotButton.Size = new System.Drawing.Size(99, 23);
            this.generujZwrotButton.TabIndex = 8;
            this.generujZwrotButton.Text = "Generuj zwrot";
            this.generujZwrotButton.UseVisualStyleBackColor = true;
            this.generujZwrotButton.Click += new System.EventHandler(this.generujZwrotButton_Click);
            // 
            // opisTextBox
            // 
            this.opisTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Opis", true));
            this.opisTextBox.Location = new System.Drawing.Point(532, 4);
            this.opisTextBox.Multiline = true;
            this.opisTextBox.Name = "opisTextBox";
            this.opisTextBox.Size = new System.Drawing.Size(349, 81);
            this.opisTextBox.TabIndex = 7;
            // 
            // historiaDokumentuControl
            // 
            this.historiaDokumentuControl.DataContext = null;
            this.historiaDokumentuControl.Location = new System.Drawing.Point(26, 54);
            this.historiaDokumentuControl.Name = "historiaDokumentuControl";
            this.historiaDokumentuControl.ReadOnly = false;
            this.historiaDokumentuControl.Size = new System.Drawing.Size(470, 31);
            this.historiaDokumentuControl.TabIndex = 6;
            // 
            // kontrahentSelect
            // 
            this.kontrahentSelect.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.BindingSource, "Kontrahent", true));
            this.kontrahentSelect.DataContext = null;
            this.kontrahentSelect.DisplayMember = null;
            this.kontrahentSelect.Location = new System.Drawing.Point(79, 28);
            this.kontrahentSelect.Name = "kontrahentSelect";
            this.kontrahentSelect.ReadOnly = false;
            this.kontrahentSelect.SelectedItem = null;
            this.kontrahentSelect.Size = new System.Drawing.Size(414, 20);
            this.kontrahentSelect.TabIndex = 5;
            this.kontrahentSelect.ValueMember = null;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kontrahent:";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "DataDodania", true));
            this.textBox2.Location = new System.Drawing.Point(363, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(133, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data dodania:";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Numer", true));
            this.textBox1.Location = new System.Drawing.Point(79, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(186, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numer:";
            // 
            // pozycjeReklamacjiGrid
            // 
            this.pozycjeReklamacjiGrid.AllowUserToAddRows = false;
            this.pozycjeReklamacjiGrid.AllowUserToDeleteRows = false;
            this.pozycjeReklamacjiGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pozycjeReklamacjiGrid.AutoGenerateColumns = false;
            this.pozycjeReklamacjiGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LpColumn,
            this.towarNazwaColumn,
            this.korektaColumn,
            this.lloscBrakiColumn,
            this.iloscNadwyzkiColumn,
            this.cenaColumn,
            this.OpisColumn});
            this.pozycjeReklamacjiGrid.DataSource = this.pozycjeBindingSource;
            this.pozycjeReklamacjiGrid.Location = new System.Drawing.Point(0, 28);
            this.pozycjeReklamacjiGrid.Name = "pozycjeReklamacjiGrid";
            this.pozycjeReklamacjiGrid.ParentForm = null;
            this.pozycjeReklamacjiGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.pozycjeReklamacjiGrid.Size = new System.Drawing.Size(1156, 411);
            this.pozycjeReklamacjiGrid.TabIndex = 1;
            this.pozycjeReklamacjiGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pozycjeReklamacjiGrid_KeyDown);
            // 
            // LpColumn
            // 
            this.LpColumn.DataPropertyName = "Lp";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.LpColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.LpColumn.FillWeight = 40F;
            this.LpColumn.HeaderText = "Lp";
            this.LpColumn.Name = "LpColumn";
            this.LpColumn.ReadOnly = true;
            this.LpColumn.Width = 40;
            // 
            // towarNazwaColumn
            // 
            this.towarNazwaColumn.DataPropertyName = "TowarNazwa";
            this.towarNazwaColumn.FillWeight = 400F;
            this.towarNazwaColumn.HeaderText = "Nazwa";
            this.towarNazwaColumn.Name = "towarNazwaColumn";
            this.towarNazwaColumn.ReadOnly = true;
            this.towarNazwaColumn.Width = 400;
            // 
            // korektaColumn
            // 
            this.korektaColumn.DataPropertyName = "Korekta";
            this.korektaColumn.FillWeight = 60F;
            this.korektaColumn.HeaderText = "Korekta";
            this.korektaColumn.Name = "korektaColumn";
            this.korektaColumn.Width = 60;
            // 
            // lloscBrakiColumn
            // 
            this.lloscBrakiColumn.DataPropertyName = "IloscBraki";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.lloscBrakiColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.lloscBrakiColumn.HeaderText = "Braki";
            this.lloscBrakiColumn.Name = "lloscBrakiColumn";
            // 
            // iloscNadwyzkiColumn
            // 
            this.iloscNadwyzkiColumn.DataPropertyName = "IloscNadwyzki";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.iloscNadwyzkiColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.iloscNadwyzkiColumn.HeaderText = "Nadwyżki";
            this.iloscNadwyzkiColumn.Name = "iloscNadwyzkiColumn";
            // 
            // cenaColumn
            // 
            this.cenaColumn.DataPropertyName = "Cena";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.cenaColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.cenaColumn.HeaderText = "Cena";
            this.cenaColumn.Name = "cenaColumn";
            this.cenaColumn.ReadOnly = true;
            // 
            // OpisColumn
            // 
            this.OpisColumn.DataPropertyName = "Opis";
            this.OpisColumn.FillWeight = 300F;
            this.OpisColumn.HeaderText = "Opis";
            this.OpisColumn.Name = "OpisColumn";
            this.OpisColumn.Width = 300;
            // 
            // pozycjeBindingSource
            // 
            this.pozycjeBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.PozycjaReklamacji);
            // 
            // bindingNavigator
            // 
            this.bindingNavigator.AddNewItem = null;
            this.bindingNavigator.BindingSource = this.BindingSource;
            this.bindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator.DeleteItem = null;
            this.bindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator.Name = "bindingNavigator";
            this.bindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator.Size = new System.Drawing.Size(1159, 25);
            this.bindingNavigator.TabIndex = 0;
            this.bindingNavigator.Text = "bindingNavigator1";
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
            // ReklamacjaOgolnePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ReklamacjaOgolnePanel";
            this.Size = new System.Drawing.Size(1159, 574);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeReklamacjiGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).EndInit();
            this.bindingNavigator.ResumeLayout(false);
            this.bindingNavigator.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private CRM.Forms.Controls.KontrahentSelect kontrahentSelect;
        private System.Windows.Forms.BindingSource pozycjeBindingSource;
        private System.Windows.Forms.BindingNavigator bindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private PozycjeReklamacjiGrid pozycjeReklamacjiGrid;
        private Core.Controls.HistoriaDokumentuControl historiaDokumentuControl;
        private System.Windows.Forms.TextBox opisTextBox;
        private System.Windows.Forms.Button generujZwrotButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn LpColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn towarNazwaColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn korektaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lloscBrakiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iloscNadwyzkiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cenaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpisColumn;
        private System.Windows.Forms.Button rozdzielButton;
        private System.Windows.Forms.Button wgDokSprzButton;

    }
}
