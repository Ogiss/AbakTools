namespace AbakTools.Zwroty.Forms
{
    partial class ZwrotFormularzForm
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
            this.pozycjeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.obliczButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dataDoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dataOdDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.kontrahenciComboBox = new System.Windows.Forms.ComboBox();
            this.kontrahenciBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.przedstawicieleComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grupyTowaroweComboBox = new System.Windows.Forms.ComboBox();
            this.grupyTowaroweBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.anulujButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGrid = new Enova.Business.Old.Controls.DataGrid();
            this.Kod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Info = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StandardowaIlosc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObrotFV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObrotFK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObrotSuma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IloscFV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Opis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kontrahenciBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pozycjeBindingSource
            // 
            this.pozycjeBindingSource.DataSource = typeof(Enova.Business.Old.Types.RaportFormularzWgGrupViewRow);
            // 
            // obliczButton
            // 
            this.obliczButton.Location = new System.Drawing.Point(865, 7);
            this.obliczButton.Name = "obliczButton";
            this.obliczButton.Size = new System.Drawing.Size(105, 23);
            this.obliczButton.TabIndex = 37;
            this.obliczButton.Text = "Oblicz";
            this.obliczButton.UseVisualStyleBackColor = true;
            this.obliczButton.Click += new System.EventHandler(this.obliczButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(564, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Data do:";
            // 
            // dataDoDateTimePicker
            // 
            this.dataDoDateTimePicker.Location = new System.Drawing.Point(619, 42);
            this.dataDoDateTimePicker.Name = "dataDoDateTimePicker";
            this.dataDoDateTimePicker.Size = new System.Drawing.Size(136, 20);
            this.dataDoDateTimePicker.TabIndex = 35;
            // 
            // dataOdDateTimePicker
            // 
            this.dataOdDateTimePicker.Location = new System.Drawing.Point(407, 42);
            this.dataOdDateTimePicker.Name = "dataOdDateTimePicker";
            this.dataOdDateTimePicker.Size = new System.Drawing.Size(136, 20);
            this.dataOdDateTimePicker.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(352, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Data od:";
            // 
            // kontrahenciComboBox
            // 
            this.kontrahenciComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.kontrahenciComboBox.DataSource = this.kontrahenciBindingSource;
            this.kontrahenciComboBox.DisplayMember = "Kod";
            this.kontrahenciComboBox.FormattingEnabled = true;
            this.kontrahenciComboBox.Location = new System.Drawing.Point(619, 9);
            this.kontrahenciComboBox.Name = "kontrahenciComboBox";
            this.kontrahenciComboBox.Size = new System.Drawing.Size(224, 21);
            this.kontrahenciComboBox.TabIndex = 32;
            this.kontrahenciComboBox.ValueMember = "ID";
            // 
            // kontrahenciBindingSource
            // 
            this.kontrahenciBindingSource.DataSource = typeof(Enova.API.CRM.Kontrahent);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(550, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Kontrahent:";
            // 
            // przedstawicieleComboBox
            // 
            this.przedstawicieleComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.przedstawicieleComboBox.FormattingEnabled = true;
            this.przedstawicieleComboBox.Location = new System.Drawing.Point(407, 9);
            this.przedstawicieleComboBox.MaxDropDownItems = 20;
            this.przedstawicieleComboBox.Name = "przedstawicieleComboBox";
            this.przedstawicieleComboBox.Size = new System.Drawing.Size(136, 21);
            this.przedstawicieleComboBox.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Przedstawiciel:";
            // 
            // grupyTowaroweComboBox
            // 
            this.grupyTowaroweComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.grupyTowaroweComboBox.DataSource = this.grupyTowaroweBindingSource;
            this.grupyTowaroweComboBox.DisplayMember = "Name";
            this.grupyTowaroweComboBox.FormattingEnabled = true;
            this.grupyTowaroweComboBox.Location = new System.Drawing.Point(105, 9);
            this.grupyTowaroweComboBox.MaxDropDownItems = 20;
            this.grupyTowaroweComboBox.Name = "grupyTowaroweComboBox";
            this.grupyTowaroweComboBox.Size = new System.Drawing.Size(193, 21);
            this.grupyTowaroweComboBox.TabIndex = 28;
            this.grupyTowaroweComboBox.ValueMember = "ID";
            // 
            // grupyTowaroweBindingSource
            // 
            this.grupyTowaroweBindingSource.DataSource = typeof(Enova.API.Business.FeatureDefinition);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Grupa towarowa:";
            // 
            // anulujButton
            // 
            this.anulujButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.anulujButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.anulujButton.Location = new System.Drawing.Point(1311, 626);
            this.anulujButton.Name = "anulujButton";
            this.anulujButton.Size = new System.Drawing.Size(75, 23);
            this.anulujButton.TabIndex = 42;
            this.anulujButton.Text = "Anuluj";
            this.anulujButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(1392, 626);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 41;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "StandardowaIlosc";
            this.dataGridViewTextBoxColumn1.HeaderText = "Pakowane po";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "IloscZam";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.FillWeight = 80F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Ilość";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Opis";
            this.dataGridViewTextBoxColumn3.FillWeight = 300F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Opis";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // dataGrid
            // 
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.AutoGenerateColumns = false;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kod,
            this.Nazwa,
            this.Info,
            this.StandardowaIlosc,
            this.ObrotFV,
            this.ObrotFK,
            this.ObrotSuma,
            this.IloscFV,
            this.Opis});
            this.dataGrid.DataSource = this.pozycjeBindingSource;
            this.dataGrid.Location = new System.Drawing.Point(14, 78);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ParentForm = null;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGrid.Size = new System.Drawing.Size(1453, 542);
            this.dataGrid.TabIndex = 43;
            this.dataGrid.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGrid_RowPostPaint);
            // 
            // Kod
            // 
            this.Kod.DataPropertyName = "Kod";
            this.Kod.FillWeight = 150F;
            this.Kod.HeaderText = "Kod";
            this.Kod.Name = "Kod";
            this.Kod.ReadOnly = true;
            this.Kod.Width = 150;
            // 
            // Nazwa
            // 
            this.Nazwa.DataPropertyName = "Nazwa";
            this.Nazwa.FillWeight = 400F;
            this.Nazwa.HeaderText = "Nazwa";
            this.Nazwa.Name = "Nazwa";
            this.Nazwa.ReadOnly = true;
            this.Nazwa.Width = 400;
            // 
            // Info
            // 
            this.Info.DataPropertyName = "Info";
            this.Info.HeaderText = "Info";
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            // 
            // StandardowaIlosc
            // 
            this.StandardowaIlosc.DataPropertyName = "StandardowaIlosc";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.StandardowaIlosc.DefaultCellStyle = dataGridViewCellStyle2;
            this.StandardowaIlosc.FillWeight = 60F;
            this.StandardowaIlosc.HeaderText = "Pak.";
            this.StandardowaIlosc.Name = "StandardowaIlosc";
            this.StandardowaIlosc.ReadOnly = true;
            this.StandardowaIlosc.Width = 60;
            // 
            // ObrotFV
            // 
            this.ObrotFV.DataPropertyName = "ObrotFV";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ObrotFV.DefaultCellStyle = dataGridViewCellStyle3;
            this.ObrotFV.FillWeight = 60F;
            this.ObrotFV.HeaderText = "FV";
            this.ObrotFV.Name = "ObrotFV";
            this.ObrotFV.ReadOnly = true;
            this.ObrotFV.Width = 60;
            // 
            // ObrotFK
            // 
            this.ObrotFK.DataPropertyName = "ObrotFK";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ObrotFK.DefaultCellStyle = dataGridViewCellStyle4;
            this.ObrotFK.FillWeight = 60F;
            this.ObrotFK.HeaderText = "FK";
            this.ObrotFK.Name = "ObrotFK";
            this.ObrotFK.ReadOnly = true;
            this.ObrotFK.Width = 60;
            // 
            // ObrotSuma
            // 
            this.ObrotSuma.DataPropertyName = "ObrotSuma";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ObrotSuma.DefaultCellStyle = dataGridViewCellStyle5;
            this.ObrotSuma.FillWeight = 70F;
            this.ObrotSuma.HeaderText = "Sprzedaż";
            this.ObrotSuma.Name = "ObrotSuma";
            this.ObrotSuma.ReadOnly = true;
            this.ObrotSuma.Width = 70;
            // 
            // IloscFV
            // 
            this.IloscFV.DataPropertyName = "IloscZam";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IloscFV.DefaultCellStyle = dataGridViewCellStyle6;
            this.IloscFV.FillWeight = 70F;
            this.IloscFV.HeaderText = "Zwrot";
            this.IloscFV.Name = "IloscFV";
            this.IloscFV.Width = 70;
            // 
            // Opis
            // 
            this.Opis.DataPropertyName = "Opis";
            this.Opis.FillWeight = 300F;
            this.Opis.HeaderText = "Opis";
            this.Opis.Name = "Opis";
            this.Opis.Width = 300;
            // 
            // ZwrotFormularzForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1481, 661);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.anulujButton);
            this.Controls.Add(this.okButton);
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
            this.Name = "ZwrotFormularzForm";
            this.Text = "ZwrotFormularzForm";
            this.Load += new System.EventHandler(this.ZwrotFormularzForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kontrahenciBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Button anulujButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.BindingSource pozycjeBindingSource;
        private System.Windows.Forms.BindingSource grupyTowaroweBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private Enova.Business.Old.Controls.DataGrid dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Info;
        private System.Windows.Forms.DataGridViewTextBoxColumn StandardowaIlosc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObrotFV;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObrotFK;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObrotSuma;
        private System.Windows.Forms.DataGridViewTextBoxColumn IloscFV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Opis;
        private System.Windows.Forms.BindingSource kontrahenciBindingSource;
    }
}