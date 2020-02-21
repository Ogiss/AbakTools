namespace Enova.Forms.Handel
{
    partial class DokumentOgolnePanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.kontrahentTextBox = new System.Windows.Forms.TextBox();
            this.pozycjeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.pozycjeDokHanGrid = new Enova.Forms.Handel.PozycjeDokHanGrid();
            this.LpColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TowarColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IloscOrgColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IloscColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iloscLocalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenaOrgColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RabatOrgColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RabatColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeDokHanGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // BindingSource
            // 
            this.BindingSource.DataSource = typeof(Enova.API.Handel.DokumentHandlowy);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data:";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Data", true));
            this.textBox1.Location = new System.Drawing.Point(53, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(112, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Numer:";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "NumerPelny", true));
            this.textBox2.Location = new System.Drawing.Point(275, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(133, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kontrahent:";
            // 
            // kontrahentTextBox
            // 
            this.kontrahentTextBox.Location = new System.Drawing.Point(80, 29);
            this.kontrahentTextBox.Name = "kontrahentTextBox";
            this.kontrahentTextBox.ReadOnly = true;
            this.kontrahentTextBox.Size = new System.Drawing.Size(788, 20);
            this.kontrahentTextBox.TabIndex = 5;
            this.kontrahentTextBox.TabStop = false;
            // 
            // pozycjeBindingSource
            // 
            this.pozycjeBindingSource.DataSource = typeof(Enova.API.Handel.PozycjaDokHandlowego);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 580);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Netto:";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "WartoscNetto", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.textBox3.Location = new System.Drawing.Point(45, 577);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 8;
            this.textBox3.TabStop = false;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 580);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "VAT:";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "WartoscVat", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.textBox4.Location = new System.Drawing.Point(207, 577);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 10;
            this.textBox4.TabStop = false;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(334, 580);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Brutto:";
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "WartoscBrutto", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.textBox5.Location = new System.Drawing.Point(378, 577);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 12;
            this.textBox5.TabStop = false;
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1101, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Stan:";
            // 
            // textBox6
            // 
            this.textBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Stan", true));
            this.textBox6.Location = new System.Drawing.Point(1139, 3);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(119, 20);
            this.textBox6.TabIndex = 14;
            this.textBox6.TabStop = false;
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pozycjeDokHanGrid
            // 
            this.pozycjeDokHanGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pozycjeDokHanGrid.AutoGenerateColumns = false;
            this.pozycjeDokHanGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LpColumn,
            this.TowarColumn,
            this.IloscOrgColumn,
            this.IloscColumn,
            this.iloscLocalColumn,
            this.CenaOrgColumn,
            this.CenaColumn,
            this.RabatOrgColumn,
            this.RabatColumn});
            this.pozycjeDokHanGrid.DataSource = this.pozycjeBindingSource;
            this.pozycjeDokHanGrid.Location = new System.Drawing.Point(0, 67);
            this.pozycjeDokHanGrid.Name = "pozycjeDokHanGrid";
            this.pozycjeDokHanGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pozycjeDokHanGrid.Size = new System.Drawing.Size(1258, 499);
            this.pozycjeDokHanGrid.TabIndex = 6;
            this.pozycjeDokHanGrid.VirtualMode = true;
            // 
            // LpColumn
            // 
            this.LpColumn.DataPropertyName = "Lp";
            this.LpColumn.FillWeight = 50F;
            this.LpColumn.HeaderText = "Lp";
            this.LpColumn.Name = "LpColumn";
            this.LpColumn.ReadOnly = true;
            this.LpColumn.Width = 50;
            // 
            // TowarColumn
            // 
            this.TowarColumn.DataPropertyName = "Towar.Nazwa";
            this.TowarColumn.FillWeight = 300F;
            this.TowarColumn.HeaderText = "Towar";
            this.TowarColumn.Name = "TowarColumn";
            this.TowarColumn.ReadOnly = true;
            this.TowarColumn.Width = 300;
            // 
            // IloscOrgColumn
            // 
            this.IloscOrgColumn.DataPropertyName = "PozycjaKorygowana.Ilosc";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IloscOrgColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.IloscOrgColumn.FillWeight = 80F;
            this.IloscOrgColumn.HeaderText = "Ilość org.";
            this.IloscOrgColumn.Name = "IloscOrgColumn";
            this.IloscOrgColumn.ReadOnly = true;
            this.IloscOrgColumn.Width = 80;
            // 
            // IloscColumn
            // 
            this.IloscColumn.DataPropertyName = "Ilosc";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IloscColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.IloscColumn.FillWeight = 80F;
            this.IloscColumn.HeaderText = "Ilość";
            this.IloscColumn.Name = "IloscColumn";
            this.IloscColumn.ReadOnly = true;
            this.IloscColumn.Width = 80;
            // 
            // iloscLocalColumn
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.iloscLocalColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.iloscLocalColumn.HeaderText = "Ilość";
            this.iloscLocalColumn.Name = "iloscLocalColumn";
            this.iloscLocalColumn.Visible = false;
            // 
            // CenaOrgColumn
            // 
            this.CenaOrgColumn.DataPropertyName = "PozycjaKorygowana.Cena";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.CenaOrgColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.CenaOrgColumn.FillWeight = 80F;
            this.CenaOrgColumn.HeaderText = "Cena org.";
            this.CenaOrgColumn.Name = "CenaOrgColumn";
            this.CenaOrgColumn.ReadOnly = true;
            this.CenaOrgColumn.Width = 80;
            // 
            // CenaColumn
            // 
            this.CenaColumn.DataPropertyName = "Cena";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.CenaColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.CenaColumn.FillWeight = 80F;
            this.CenaColumn.HeaderText = "Cena";
            this.CenaColumn.Name = "CenaColumn";
            this.CenaColumn.ReadOnly = true;
            this.CenaColumn.Width = 80;
            // 
            // RabatOrgColumn
            // 
            this.RabatOrgColumn.DataPropertyName = "PozycjaKorygowana.Rabat";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "P2";
            this.RabatOrgColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.RabatOrgColumn.FillWeight = 80F;
            this.RabatOrgColumn.HeaderText = "Rabat org.";
            this.RabatOrgColumn.Name = "RabatOrgColumn";
            this.RabatOrgColumn.ReadOnly = true;
            this.RabatOrgColumn.Width = 80;
            // 
            // RabatColumn
            // 
            this.RabatColumn.DataPropertyName = "Rabat";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "P2";
            this.RabatColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.RabatColumn.FillWeight = 80F;
            this.RabatColumn.HeaderText = "Rabat";
            this.RabatColumn.Name = "RabatColumn";
            this.RabatColumn.ReadOnly = true;
            this.RabatColumn.Width = 80;
            // 
            // DokumentOgolnePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pozycjeDokHanGrid);
            this.Controls.Add(this.kontrahentTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "DokumentOgolnePanel";
            this.Size = new System.Drawing.Size(1261, 601);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeDokHanGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox kontrahentTextBox;
        private PozycjeDokHanGrid pozycjeDokHanGrid;
        private System.Windows.Forms.BindingSource pozycjeBindingSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.DataGridViewTextBoxColumn LpColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TowarColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IloscOrgColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IloscColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iloscLocalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenaOrgColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RabatOrgColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RabatColumn;
    }
}
