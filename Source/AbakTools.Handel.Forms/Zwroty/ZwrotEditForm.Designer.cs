namespace AbakTools.Zwroty.Forms
{
    partial class ZwrotEditForm
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
            System.Windows.Forms.Label iDLabel;
            System.Windows.Forms.Label dataLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZwrotEditForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.iDTextBox = new System.Windows.Forms.TextBox();
            this.dataDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pozycjeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pozycjeBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.wartoscTextBox = new System.Windows.Forms.TextBox();
            this.pozycjeZwrotuGrid = new AbakTools.Handel.Forms.PozycjeZwrotuGrid();
            this.IdentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IloscPozostaloDoSkorygowaniaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WartoscNettoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Opis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opisTextBox = new System.Windows.Forms.TextBox();
            this.zformularzaButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.statusyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.zmienButton = new System.Windows.Forms.Button();
            this.rozdzielButton = new System.Windows.Forms.Button();
            this.historiaButton = new System.Windows.Forms.Button();
            this.pozycjeNieskorygowaneButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.sezon1ComboBox = new System.Windows.Forms.ComboBox();
            this.sezon2ComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sezon3ComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sezon4ComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.kontrahentEnovaSelect = new Enova.Forms.Controls.KontrahentEnovaSelect();
            iDLabel = new System.Windows.Forms.Label();
            dataLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingNavigator)).BeginInit();
            this.pozycjeBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeZwrotuGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(1228, 705);
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(1147, 705);
            // 
            // DataSourceBinding
            // 
            this.DataSourceBinding.DataSource = typeof(Enova.Business.Old.DB.Web.Zwrot);
            // 
            // iDLabel
            // 
            iDLabel.AutoSize = true;
            iDLabel.Location = new System.Drawing.Point(59, 15);
            iDLabel.Name = "iDLabel";
            iDLabel.Size = new System.Drawing.Size(21, 13);
            iDLabel.TabIndex = 2;
            iDLabel.Text = "ID:";
            // 
            // dataLabel
            // 
            dataLabel.AutoSize = true;
            dataLabel.Location = new System.Drawing.Point(190, 15);
            dataLabel.Name = "dataLabel";
            dataLabel.Size = new System.Drawing.Size(33, 13);
            dataLabel.TabIndex = 4;
            dataLabel.Text = "Data:";
            // 
            // iDTextBox
            // 
            this.iDTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "ID", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "(AUTO)"));
            this.iDTextBox.Location = new System.Drawing.Point(86, 12);
            this.iDTextBox.Name = "iDTextBox";
            this.iDTextBox.Size = new System.Drawing.Size(100, 20);
            this.iDTextBox.TabIndex = 3;
            // 
            // dataDateTimePicker
            // 
            this.dataDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.DataSourceBinding, "DataDodania", true));
            this.dataDateTimePicker.Location = new System.Drawing.Point(229, 11);
            this.dataDateTimePicker.Name = "dataDateTimePicker";
            this.dataDateTimePicker.Size = new System.Drawing.Size(136, 20);
            this.dataDateTimePicker.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(395, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Kontrahent:";
            // 
            // pozycjeBindingSource
            // 
            this.pozycjeBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.PozycjaZwrotu);
            // 
            // pozycjeBindingNavigator
            // 
            this.pozycjeBindingNavigator.AddNewItem = null;
            this.pozycjeBindingNavigator.BindingSource = this.pozycjeBindingSource;
            this.pozycjeBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.pozycjeBindingNavigator.DeleteItem = null;
            this.pozycjeBindingNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.pozycjeBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.pozycjeBindingNavigator.Location = new System.Drawing.Point(13, 100);
            this.pozycjeBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.pozycjeBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.pozycjeBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.pozycjeBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.pozycjeBindingNavigator.Name = "pozycjeBindingNavigator";
            this.pozycjeBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.pozycjeBindingNavigator.Size = new System.Drawing.Size(255, 25);
            this.pozycjeBindingNavigator.TabIndex = 9;
            this.pozycjeBindingNavigator.Text = "bindingNavigator1";
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
            // wartoscTextBox
            // 
            this.wartoscTextBox.BackColor = System.Drawing.Color.White;
            this.wartoscTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "WartoscNetto", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.wartoscTextBox.Location = new System.Drawing.Point(663, 661);
            this.wartoscTextBox.Name = "wartoscTextBox";
            this.wartoscTextBox.ReadOnly = true;
            this.wartoscTextBox.Size = new System.Drawing.Size(100, 20);
            this.wartoscTextBox.TabIndex = 11;
            this.wartoscTextBox.TabStop = false;
            this.wartoscTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pozycjeZwrotuGrid
            // 
            this.pozycjeZwrotuGrid.AllowUserToAddRows = false;
            this.pozycjeZwrotuGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pozycjeZwrotuGrid.AutoGenerateColumns = false;
            this.pozycjeZwrotuGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdentColumn,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.IloscPozostaloDoSkorygowaniaColumn,
            this.dataGridViewTextBoxColumn3,
            this.WartoscNettoColumn,
            this.Opis});
            this.pozycjeZwrotuGrid.DataSource = this.pozycjeBindingSource;
            this.pozycjeZwrotuGrid.Location = new System.Drawing.Point(13, 128);
            this.pozycjeZwrotuGrid.Name = "pozycjeZwrotuGrid";
            this.pozycjeZwrotuGrid.ParentForm = null;
            this.pozycjeZwrotuGrid.RowHeadersVisible = false;
            this.pozycjeZwrotuGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pozycjeZwrotuGrid.Size = new System.Drawing.Size(1290, 526);
            this.pozycjeZwrotuGrid.TabIndex = 10;
            this.pozycjeZwrotuGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.pozycjeZwrotuGrid_CellValueChanged);
            this.pozycjeZwrotuGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pozycjeZwrotuGrid_KeyDown);
            // 
            // IdentColumn
            // 
            this.IdentColumn.DataPropertyName = "Ident";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IdentColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.IdentColumn.FillWeight = 50F;
            this.IdentColumn.HeaderText = "Lp";
            this.IdentColumn.Name = "IdentColumn";
            this.IdentColumn.ReadOnly = true;
            this.IdentColumn.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "TowarNazwa";
            this.dataGridViewTextBoxColumn1.FillWeight = 400F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Towar";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 400;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Ilosc";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn2.HeaderText = "Ilosc";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // IloscPozostaloDoSkorygowaniaColumn
            // 
            this.IloscPozostaloDoSkorygowaniaColumn.DataPropertyName = "IloscPozostaloDoSkorygowania";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.IloscPozostaloDoSkorygowaniaColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.IloscPozostaloDoSkorygowaniaColumn.HeaderText = "Pozostało";
            this.IloscPozostaloDoSkorygowaniaColumn.Name = "IloscPozostaloDoSkorygowaniaColumn";
            this.IloscPozostaloDoSkorygowaniaColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Cena";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn3.HeaderText = "Cena";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // WartoscNettoColumn
            // 
            this.WartoscNettoColumn.DataPropertyName = "WartoscNetto";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.WartoscNettoColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.WartoscNettoColumn.HeaderText = "WartoscNetto";
            this.WartoscNettoColumn.Name = "WartoscNettoColumn";
            this.WartoscNettoColumn.ReadOnly = true;
            // 
            // Opis
            // 
            this.Opis.DataPropertyName = "Opis";
            this.Opis.FillWeight = 250F;
            this.Opis.HeaderText = "Opis";
            this.Opis.Name = "Opis";
            this.Opis.Width = 250;
            // 
            // opisTextBox
            // 
            this.opisTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "Opis", true));
            this.opisTextBox.Location = new System.Drawing.Point(931, 11);
            this.opisTextBox.Multiline = true;
            this.opisTextBox.Name = "opisTextBox";
            this.opisTextBox.Size = new System.Drawing.Size(374, 66);
            this.opisTextBox.TabIndex = 12;
            // 
            // zformularzaButton
            // 
            this.zformularzaButton.Enabled = false;
            this.zformularzaButton.Location = new System.Drawing.Point(278, 102);
            this.zformularzaButton.Name = "zformularzaButton";
            this.zformularzaButton.Size = new System.Drawing.Size(100, 23);
            this.zformularzaButton.TabIndex = 13;
            this.zformularzaButton.Text = "Z formularza";
            this.zformularzaButton.UseVisualStyleBackColor = true;
            this.zformularzaButton.Click += new System.EventHandler(this.zformularzaButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Status:";
            // 
            // statusComboBox
            // 
            this.statusComboBox.DataSource = this.statusyBindingSource;
            this.statusComboBox.DisplayMember = "Nazwa";
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Location = new System.Drawing.Point(242, 39);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(137, 21);
            this.statusComboBox.TabIndex = 15;
            this.statusComboBox.ValueMember = "ID";
            // 
            // statusyBindingSource
            // 
            this.statusyBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.StatusZwrotu);
            // 
            // statusTextBox
            // 
            this.statusTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "OstatniStatusStr", true));
            this.statusTextBox.Location = new System.Drawing.Point(86, 39);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(137, 20);
            this.statusTextBox.TabIndex = 16;
            // 
            // zmienButton
            // 
            this.zmienButton.Location = new System.Drawing.Point(385, 36);
            this.zmienButton.Name = "zmienButton";
            this.zmienButton.Size = new System.Drawing.Size(75, 23);
            this.zmienButton.TabIndex = 17;
            this.zmienButton.Text = "Zmień";
            this.zmienButton.UseVisualStyleBackColor = true;
            this.zmienButton.Click += new System.EventHandler(this.zmienButton_Click);
            // 
            // rozdzielButton
            // 
            this.rozdzielButton.Location = new System.Drawing.Point(397, 102);
            this.rozdzielButton.Name = "rozdzielButton";
            this.rozdzielButton.Size = new System.Drawing.Size(140, 23);
            this.rozdzielButton.TabIndex = 18;
            this.rozdzielButton.Text = "Rozdziel zwrot";
            this.rozdzielButton.UseVisualStyleBackColor = true;
            this.rozdzielButton.Click += new System.EventHandler(this.rozdzielButton_Click);
            // 
            // historiaButton
            // 
            this.historiaButton.Location = new System.Drawing.Point(479, 36);
            this.historiaButton.Name = "historiaButton";
            this.historiaButton.Size = new System.Drawing.Size(114, 23);
            this.historiaButton.TabIndex = 19;
            this.historiaButton.Text = "Historia";
            this.historiaButton.UseVisualStyleBackColor = true;
            this.historiaButton.Click += new System.EventHandler(this.historiaButton_Click);
            // 
            // pozycjeNieskorygowaneButton
            // 
            this.pozycjeNieskorygowaneButton.Location = new System.Drawing.Point(600, 102);
            this.pozycjeNieskorygowaneButton.Name = "pozycjeNieskorygowaneButton";
            this.pozycjeNieskorygowaneButton.Size = new System.Drawing.Size(142, 23);
            this.pozycjeNieskorygowaneButton.TabIndex = 20;
            this.pozycjeNieskorygowaneButton.Text = "Pozycje nieskorygowane";
            this.pozycjeNieskorygowaneButton.UseVisualStyleBackColor = true;
            this.pozycjeNieskorygowaneButton.Click += new System.EventHandler(this.pozycjeNieskorygowaneButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Sezon:";
            // 
            // sezon1ComboBox
            // 
            this.sezon1ComboBox.FormattingEnabled = true;
            this.sezon1ComboBox.Location = new System.Drawing.Point(86, 65);
            this.sezon1ComboBox.Name = "sezon1ComboBox";
            this.sezon1ComboBox.Size = new System.Drawing.Size(157, 21);
            this.sezon1ComboBox.TabIndex = 22;
            this.sezon1ComboBox.SelectionChangeCommitted += new System.EventHandler(this.sezonyComboBox_SelectionChangeCommitted);
            // 
            // sezon2ComboBox
            // 
            this.sezon2ComboBox.FormattingEnabled = true;
            this.sezon2ComboBox.Location = new System.Drawing.Point(313, 65);
            this.sezon2ComboBox.Name = "sezon2ComboBox";
            this.sezon2ComboBox.Size = new System.Drawing.Size(157, 21);
            this.sezon2ComboBox.TabIndex = 24;
            this.sezon2ComboBox.SelectionChangeCommitted += new System.EventHandler(this.sezon2ComboBox_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(258, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Sezon 2:";
            // 
            // sezon3ComboBox
            // 
            this.sezon3ComboBox.FormattingEnabled = true;
            this.sezon3ComboBox.Location = new System.Drawing.Point(541, 65);
            this.sezon3ComboBox.Name = "sezon3ComboBox";
            this.sezon3ComboBox.Size = new System.Drawing.Size(157, 21);
            this.sezon3ComboBox.TabIndex = 26;
            this.sezon3ComboBox.SelectionChangeCommitted += new System.EventHandler(this.sezon3ComboBox_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(486, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Sezon 3:";
            // 
            // sezon4ComboBox
            // 
            this.sezon4ComboBox.FormattingEnabled = true;
            this.sezon4ComboBox.Location = new System.Drawing.Point(768, 65);
            this.sezon4ComboBox.Name = "sezon4ComboBox";
            this.sezon4ComboBox.Size = new System.Drawing.Size(157, 21);
            this.sezon4ComboBox.TabIndex = 28;
            this.sezon4ComboBox.SelectionChangeCommitted += new System.EventHandler(this.sezon4ComboBox_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(713, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Sezon 4:";
            // 
            // kontrahentEnovaSelect
            // 
            this.kontrahentEnovaSelect.DataContext = null;
            this.kontrahentEnovaSelect.DisplayMember = null;
            this.kontrahentEnovaSelect.Location = new System.Drawing.Point(463, 11);
            this.kontrahentEnovaSelect.Name = "kontrahentEnovaSelect";
            this.kontrahentEnovaSelect.ReadOnly = false;
            this.kontrahentEnovaSelect.SelectedItem = null;
            this.kontrahentEnovaSelect.Size = new System.Drawing.Size(462, 20);
            this.kontrahentEnovaSelect.TabIndex = 29;
            this.kontrahentEnovaSelect.ValueMember = null;
            this.kontrahentEnovaSelect.ValueChanged += new System.EventHandler(this.kontrahentEnovaSelect_ValueChanged);
            // 
            // ZwrotEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1315, 740);
            this.Controls.Add(this.kontrahentEnovaSelect);
            this.Controls.Add(this.sezon4ComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sezon3ComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sezon2ComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sezon1ComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pozycjeNieskorygowaneButton);
            this.Controls.Add(this.historiaButton);
            this.Controls.Add(this.rozdzielButton);
            this.Controls.Add(this.zmienButton);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.statusComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.zformularzaButton);
            this.Controls.Add(this.opisTextBox);
            this.Controls.Add(this.wartoscTextBox);
            this.Controls.Add(this.pozycjeZwrotuGrid);
            this.Controls.Add(this.pozycjeBindingNavigator);
            this.Controls.Add(this.label1);
            this.Controls.Add(dataLabel);
            this.Controls.Add(this.dataDateTimePicker);
            this.Controls.Add(iDLabel);
            this.Controls.Add(this.iDTextBox);
            this.Name = "ZwrotEditForm";
            this.Text = "Dokument zwrotu";
            this.Load += new System.EventHandler(this.ZwrotEditForm_Load);
            this.Shown += new System.EventHandler(this.ZwrotEditForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ZwrotEditForm_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ZwrotEditForm_PreviewKeyDown);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.anulujButton, 0);
            this.Controls.SetChildIndex(this.iDTextBox, 0);
            this.Controls.SetChildIndex(iDLabel, 0);
            this.Controls.SetChildIndex(this.dataDateTimePicker, 0);
            this.Controls.SetChildIndex(dataLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pozycjeBindingNavigator, 0);
            this.Controls.SetChildIndex(this.pozycjeZwrotuGrid, 0);
            this.Controls.SetChildIndex(this.wartoscTextBox, 0);
            this.Controls.SetChildIndex(this.opisTextBox, 0);
            this.Controls.SetChildIndex(this.zformularzaButton, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.statusComboBox, 0);
            this.Controls.SetChildIndex(this.statusTextBox, 0);
            this.Controls.SetChildIndex(this.zmienButton, 0);
            this.Controls.SetChildIndex(this.rozdzielButton, 0);
            this.Controls.SetChildIndex(this.historiaButton, 0);
            this.Controls.SetChildIndex(this.pozycjeNieskorygowaneButton, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.sezon1ComboBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.sezon2ComboBox, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.sezon3ComboBox, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.sezon4ComboBox, 0);
            this.Controls.SetChildIndex(this.kontrahentEnovaSelect, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingNavigator)).EndInit();
            this.pozycjeBindingNavigator.ResumeLayout(false);
            this.pozycjeBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeZwrotuGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusyBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox iDTextBox;
        private System.Windows.Forms.DateTimePicker dataDateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingNavigator pozycjeBindingNavigator;
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
        private System.Windows.Forms.BindingSource pozycjeBindingSource;
        private AbakTools.Handel.Forms.PozycjeZwrotuGrid pozycjeZwrotuGrid;
        private System.Windows.Forms.TextBox wartoscTextBox;
        private System.Windows.Forms.TextBox opisTextBox;
        private System.Windows.Forms.Button zformularzaButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.BindingSource statusyBindingSource;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.Button zmienButton;
        private System.Windows.Forms.Button rozdzielButton;
        private System.Windows.Forms.Button historiaButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IloscPozostaloDoSkorygowaniaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn WartoscNettoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Opis;
        private System.Windows.Forms.Button pozycjeNieskorygowaneButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox sezon1ComboBox;
        private System.Windows.Forms.ComboBox sezon2ComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox sezon3ComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox sezon4ComboBox;
        private System.Windows.Forms.Label label6;
        private Enova.Forms.Controls.KontrahentEnovaSelect kontrahentEnovaSelect;
    }
}
