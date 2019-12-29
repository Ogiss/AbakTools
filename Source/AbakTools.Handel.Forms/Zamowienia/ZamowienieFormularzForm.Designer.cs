namespace AbakTools.Zamowienia.Forms
{
    partial class ZamowienieFormularzForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grupyTowaroweBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kontrahenciBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.obliczButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dataDoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dataOdDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.kontrahenciComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.przedstawicieleComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grupyTowaroweComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.nazwaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.infoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StandardowaIloscColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StanMagazynuColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenaNettoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.obrotFVDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.obrotFKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.obrotSumaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sp2Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IloscZamColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpisColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pozycjeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.okButton = new System.Windows.Forms.Button();
            this.anulujButton = new System.Windows.Forms.Button();
            this.wypełnijButton = new System.Windows.Forms.Button();
            this.wyczyscButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpZwrotyDo = new System.Windows.Forms.DateTimePicker();
            this.dtpZwrotyOd = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.obliczZZamowienButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.sp2ToDtp = new System.Windows.Forms.DateTimePicker();
            this.sp2FromDtp = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.sp2ActiveCheckBox = new BAL.Forms.Controls.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.sp2FromOrderCheckBox = new BAL.Forms.Controls.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kontrahenciBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grupyTowaroweBindingSource
            // 
            this.grupyTowaroweBindingSource.DataSource = typeof(Enova.API.Business.FeatureDefinition);
            // 
            // kontrahenciBindingSource
            // 
            this.kontrahenciBindingSource.DataSource = typeof(Enova.API.CRM.Kontrahent);
            // 
            // obliczButton
            // 
            this.obliczButton.Location = new System.Drawing.Point(1024, 5);
            this.obliczButton.Name = "obliczButton";
            this.obliczButton.Size = new System.Drawing.Size(105, 23);
            this.obliczButton.TabIndex = 21;
            this.obliczButton.Text = "Oblicz";
            this.obliczButton.UseVisualStyleBackColor = true;
            this.obliczButton.Click += new System.EventHandler(this.obliczButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(240, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Sprzedaż do:";
            // 
            // dataDoDateTimePicker
            // 
            this.dataDoDateTimePicker.Location = new System.Drawing.Point(315, 41);
            this.dataDoDateTimePicker.Name = "dataDoDateTimePicker";
            this.dataDoDateTimePicker.Size = new System.Drawing.Size(129, 20);
            this.dataDoDateTimePicker.TabIndex = 19;
            // 
            // dataOdDateTimePicker
            // 
            this.dataOdDateTimePicker.Location = new System.Drawing.Point(104, 41);
            this.dataOdDateTimePicker.Name = "dataOdDateTimePicker";
            this.dataOdDateTimePicker.Size = new System.Drawing.Size(128, 20);
            this.dataOdDateTimePicker.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Sprzedaż od:";
            // 
            // kontrahenciComboBox
            // 
            this.kontrahenciComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.kontrahenciComboBox.DataSource = this.kontrahenciBindingSource;
            this.kontrahenciComboBox.DisplayMember = "Kod";
            this.kontrahenciComboBox.FormattingEnabled = true;
            this.kontrahenciComboBox.Location = new System.Drawing.Point(619, 12);
            this.kontrahenciComboBox.Name = "kontrahenciComboBox";
            this.kontrahenciComboBox.Size = new System.Drawing.Size(224, 21);
            this.kontrahenciComboBox.TabIndex = 16;
            this.kontrahenciComboBox.ValueMember = "ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(550, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Kontrahent:";
            // 
            // przedstawicieleComboBox
            // 
            this.przedstawicieleComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.przedstawicieleComboBox.FormattingEnabled = true;
            this.przedstawicieleComboBox.Location = new System.Drawing.Point(407, 12);
            this.przedstawicieleComboBox.MaxDropDownItems = 20;
            this.przedstawicieleComboBox.Name = "przedstawicieleComboBox";
            this.przedstawicieleComboBox.Size = new System.Drawing.Size(136, 21);
            this.przedstawicieleComboBox.TabIndex = 14;
            this.przedstawicieleComboBox.SelectedIndexChanged += new System.EventHandler(this.przedstawicieleComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Przedstawiciel:";
            // 
            // grupyTowaroweComboBox
            // 
            this.grupyTowaroweComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.grupyTowaroweComboBox.DataSource = this.grupyTowaroweBindingSource;
            this.grupyTowaroweComboBox.DisplayMember = "Name";
            this.grupyTowaroweComboBox.FormattingEnabled = true;
            this.grupyTowaroweComboBox.Location = new System.Drawing.Point(105, 12);
            this.grupyTowaroweComboBox.MaxDropDownItems = 20;
            this.grupyTowaroweComboBox.Name = "grupyTowaroweComboBox";
            this.grupyTowaroweComboBox.Size = new System.Drawing.Size(193, 21);
            this.grupyTowaroweComboBox.TabIndex = 12;
            this.grupyTowaroweComboBox.ValueMember = "ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Grupa towarowa:";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nazwaDataGridViewTextBoxColumn,
            this.infoDataGridViewTextBoxColumn,
            this.StandardowaIloscColumn,
            this.StanMagazynuColumn,
            this.CenaNettoColumn,
            this.obrotFVDataGridViewTextBoxColumn,
            this.obrotFKDataGridViewTextBoxColumn,
            this.obrotSumaDataGridViewTextBoxColumn,
            this.sp2Column,
            this.IloscZamColumn,
            this.OpisColumn});
            this.dataGridView.DataSource = this.pozycjeBindingSource;
            this.dataGridView.Location = new System.Drawing.Point(12, 90);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(1301, 550);
            this.dataGridView.TabIndex = 22;
            this.dataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView_RowPrePaint);
            // 
            // nazwaDataGridViewTextBoxColumn
            // 
            this.nazwaDataGridViewTextBoxColumn.DataPropertyName = "Nazwa";
            this.nazwaDataGridViewTextBoxColumn.FillWeight = 470F;
            this.nazwaDataGridViewTextBoxColumn.HeaderText = "Nazwa";
            this.nazwaDataGridViewTextBoxColumn.Name = "nazwaDataGridViewTextBoxColumn";
            this.nazwaDataGridViewTextBoxColumn.ReadOnly = true;
            this.nazwaDataGridViewTextBoxColumn.Width = 470;
            // 
            // infoDataGridViewTextBoxColumn
            // 
            this.infoDataGridViewTextBoxColumn.DataPropertyName = "Info";
            this.infoDataGridViewTextBoxColumn.FillWeight = 80F;
            this.infoDataGridViewTextBoxColumn.HeaderText = "Info";
            this.infoDataGridViewTextBoxColumn.Name = "infoDataGridViewTextBoxColumn";
            this.infoDataGridViewTextBoxColumn.ReadOnly = true;
            this.infoDataGridViewTextBoxColumn.Width = 80;
            // 
            // StandardowaIloscColumn
            // 
            this.StandardowaIloscColumn.DataPropertyName = "StandardowaIlosc";
            this.StandardowaIloscColumn.HeaderText = "Pakowane po";
            this.StandardowaIloscColumn.Name = "StandardowaIloscColumn";
            this.StandardowaIloscColumn.ReadOnly = true;
            // 
            // StanMagazynuColumn
            // 
            this.StanMagazynuColumn.DataPropertyName = "StanMagazynu";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.StanMagazynuColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.StanMagazynuColumn.FillWeight = 80F;
            this.StanMagazynuColumn.HeaderText = "Stan mag.";
            this.StanMagazynuColumn.Name = "StanMagazynuColumn";
            this.StanMagazynuColumn.Width = 80;
            // 
            // CenaNettoColumn
            // 
            this.CenaNettoColumn.DataPropertyName = "CenaNetto";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.CenaNettoColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.CenaNettoColumn.HeaderText = "Cena";
            this.CenaNettoColumn.Name = "CenaNettoColumn";
            // 
            // obrotFVDataGridViewTextBoxColumn
            // 
            this.obrotFVDataGridViewTextBoxColumn.DataPropertyName = "ObrotFV";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.obrotFVDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.obrotFVDataGridViewTextBoxColumn.FillWeight = 80F;
            this.obrotFVDataGridViewTextBoxColumn.HeaderText = "FV";
            this.obrotFVDataGridViewTextBoxColumn.Name = "obrotFVDataGridViewTextBoxColumn";
            this.obrotFVDataGridViewTextBoxColumn.ReadOnly = true;
            this.obrotFVDataGridViewTextBoxColumn.Width = 80;
            // 
            // obrotFKDataGridViewTextBoxColumn
            // 
            this.obrotFKDataGridViewTextBoxColumn.DataPropertyName = "ObrotFK";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.obrotFKDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.obrotFKDataGridViewTextBoxColumn.FillWeight = 80F;
            this.obrotFKDataGridViewTextBoxColumn.HeaderText = "FK";
            this.obrotFKDataGridViewTextBoxColumn.Name = "obrotFKDataGridViewTextBoxColumn";
            this.obrotFKDataGridViewTextBoxColumn.ReadOnly = true;
            this.obrotFKDataGridViewTextBoxColumn.Width = 80;
            // 
            // obrotSumaDataGridViewTextBoxColumn
            // 
            this.obrotSumaDataGridViewTextBoxColumn.DataPropertyName = "ObrotSuma";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.obrotSumaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.obrotSumaDataGridViewTextBoxColumn.FillWeight = 70F;
            this.obrotSumaDataGridViewTextBoxColumn.HeaderText = "Sprzedaż";
            this.obrotSumaDataGridViewTextBoxColumn.Name = "obrotSumaDataGridViewTextBoxColumn";
            this.obrotSumaDataGridViewTextBoxColumn.ReadOnly = true;
            this.obrotSumaDataGridViewTextBoxColumn.Width = 70;
            // 
            // sp2Column
            // 
            this.sp2Column.DataPropertyName = "ObrotSuma2";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            this.sp2Column.DefaultCellStyle = dataGridViewCellStyle6;
            this.sp2Column.FillWeight = 70F;
            this.sp2Column.HeaderText = "Sprzedaż 2";
            this.sp2Column.Name = "sp2Column";
            this.sp2Column.ReadOnly = true;
            this.sp2Column.Width = 70;
            // 
            // IloscZamColumn
            // 
            this.IloscZamColumn.DataPropertyName = "IloscZam";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IloscZamColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.IloscZamColumn.FillWeight = 80F;
            this.IloscZamColumn.HeaderText = "Ilość";
            this.IloscZamColumn.Name = "IloscZamColumn";
            this.IloscZamColumn.Width = 80;
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
            this.pozycjeBindingSource.DataSource = typeof(AbakTools.Zamowienia.Forms.ZamowienieFormularzForm.RaportFormularzWgGrupViewRow);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(1238, 647);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 23;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // anulujButton
            // 
            this.anulujButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.anulujButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.anulujButton.Location = new System.Drawing.Point(1157, 647);
            this.anulujButton.Name = "anulujButton";
            this.anulujButton.Size = new System.Drawing.Size(75, 23);
            this.anulujButton.TabIndex = 24;
            this.anulujButton.Text = "Anuluj";
            this.anulujButton.UseVisualStyleBackColor = true;
            // 
            // wypełnijButton
            // 
            this.wypełnijButton.Location = new System.Drawing.Point(1024, 39);
            this.wypełnijButton.Name = "wypełnijButton";
            this.wypełnijButton.Size = new System.Drawing.Size(105, 23);
            this.wypełnijButton.TabIndex = 25;
            this.wypełnijButton.Text = "Wypełnij z obrotu";
            this.wypełnijButton.UseVisualStyleBackColor = true;
            this.wypełnijButton.Click += new System.EventHandler(this.wypełnijButton_Click);
            // 
            // wyczyscButton
            // 
            this.wyczyscButton.Location = new System.Drawing.Point(1135, 39);
            this.wyczyscButton.Name = "wyczyscButton";
            this.wyczyscButton.Size = new System.Drawing.Size(102, 23);
            this.wyczyscButton.TabIndex = 26;
            this.wyczyscButton.Text = "Wyczyść";
            this.wyczyscButton.UseVisualStyleBackColor = true;
            this.wyczyscButton.Click += new System.EventHandler(this.wyczyscButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(252, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Zwroty do:";
            // 
            // dtpZwrotyDo
            // 
            this.dtpZwrotyDo.Location = new System.Drawing.Point(315, 67);
            this.dtpZwrotyDo.Name = "dtpZwrotyDo";
            this.dtpZwrotyDo.Size = new System.Drawing.Size(129, 20);
            this.dtpZwrotyDo.TabIndex = 29;
            // 
            // dtpZwrotyOd
            // 
            this.dtpZwrotyOd.Location = new System.Drawing.Point(103, 67);
            this.dtpZwrotyOd.Name = "dtpZwrotyOd";
            this.dtpZwrotyOd.Size = new System.Drawing.Size(129, 20);
            this.dtpZwrotyOd.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Zwroty od:";
            // 
            // obliczZZamowienButton
            // 
            this.obliczZZamowienButton.Location = new System.Drawing.Point(1135, 4);
            this.obliczZZamowienButton.Name = "obliczZZamowienButton";
            this.obliczZZamowienButton.Size = new System.Drawing.Size(105, 23);
            this.obliczZZamowienButton.TabIndex = 31;
            this.obliczZZamowienButton.Text = "Oblicz z zamówień";
            this.obliczZZamowienButton.UseVisualStyleBackColor = true;
            this.obliczZZamowienButton.Click += new System.EventHandler(this.obliczZZamowienButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(679, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Sprzedaż do:";
            // 
            // sp2ToDtp
            // 
            this.sp2ToDtp.Location = new System.Drawing.Point(754, 67);
            this.sp2ToDtp.Name = "sp2ToDtp";
            this.sp2ToDtp.Size = new System.Drawing.Size(129, 20);
            this.sp2ToDtp.TabIndex = 34;
            // 
            // sp2FromDtp
            // 
            this.sp2FromDtp.Location = new System.Drawing.Point(543, 67);
            this.sp2FromDtp.Name = "sp2FromDtp";
            this.sp2FromDtp.Size = new System.Drawing.Size(128, 20);
            this.sp2FromDtp.TabIndex = 33;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(468, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Sprzedaż od:";
            // 
            // sp2ActiveCheckBox
            // 
            this.sp2ActiveCheckBox.AutoSize = true;
            this.sp2ActiveCheckBox.Location = new System.Drawing.Point(583, 44);
            this.sp2ActiveCheckBox.Name = "sp2ActiveCheckBox";
            this.sp2ActiveCheckBox.Size = new System.Drawing.Size(15, 14);
            this.sp2ActiveCheckBox.TabIndex = 36;
            this.sp2ActiveCheckBox.UseVisualStyleBackColor = true;
            this.sp2ActiveCheckBox.CheckedChanged += new System.EventHandler(this.sp2ActiveCheckBox_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(471, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Sprzedaż II aktywna:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(616, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "Sprzedaż II z zamówień:";
            // 
            // sp2FromOrderCheckBox
            // 
            this.sp2FromOrderCheckBox.AutoSize = true;
            this.sp2FromOrderCheckBox.Location = new System.Drawing.Point(743, 45);
            this.sp2FromOrderCheckBox.Name = "sp2FromOrderCheckBox";
            this.sp2FromOrderCheckBox.Size = new System.Drawing.Size(15, 14);
            this.sp2FromOrderCheckBox.TabIndex = 38;
            this.sp2FromOrderCheckBox.UseVisualStyleBackColor = true;
            // 
            // ZamowienieFormularzForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1325, 682);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.sp2FromOrderCheckBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.sp2ActiveCheckBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.sp2ToDtp);
            this.Controls.Add(this.sp2FromDtp);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.obliczZZamowienButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpZwrotyDo);
            this.Controls.Add(this.dtpZwrotyOd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.wyczyscButton);
            this.Controls.Add(this.wypełnijButton);
            this.Controls.Add(this.anulujButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.obliczButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataDoDateTimePicker);
            this.Controls.Add(this.dataOdDateTimePicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.kontrahenciComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.przedstawicieleComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grupyTowaroweComboBox);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "ZamowienieFormularzForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zamowienie w/g grup towarowych";
            this.Load += new System.EventHandler(this.ZamowienieFormularzForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ZamowienieFormularzForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kontrahenciBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource grupyTowaroweBindingSource;
        private System.Windows.Forms.BindingSource kontrahenciBindingSource;
        private System.Windows.Forms.Button obliczButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dataDoDateTimePicker;
        private System.Windows.Forms.DateTimePicker dataOdDateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox kontrahenciComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox przedstawicieleComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox grupyTowaroweComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button anulujButton;
        private System.Windows.Forms.BindingSource pozycjeBindingSource;
        private System.Windows.Forms.Button wypełnijButton;
        private System.Windows.Forms.Button wyczyscButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpZwrotyDo;
        private System.Windows.Forms.DateTimePicker dtpZwrotyOd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button obliczZZamowienButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker sp2ToDtp;
        private System.Windows.Forms.DateTimePicker sp2FromDtp;
        private System.Windows.Forms.Label label9;
        private BAL.Forms.Controls.CheckBox sp2ActiveCheckBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private BAL.Forms.Controls.CheckBox sp2FromOrderCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn infoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StandardowaIloscColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StanMagazynuColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenaNettoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn obrotFVDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn obrotFKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn obrotSumaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sp2Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn IloscZamColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpisColumn;
    }
}