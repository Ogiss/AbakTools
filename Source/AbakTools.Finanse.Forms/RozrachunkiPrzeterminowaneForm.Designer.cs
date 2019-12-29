namespace AbakTools.Finanse.Forms
{
    partial class RozrachunkiPrzeterminowaneForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.maxDataWystDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.zatwierdźDatęButton = new System.Windows.Forms.Button();
            this.Blokada = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BlokadaSprzedaży = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PrzdstawicielKontrahent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodKontrahenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NazwaKontrahenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataDokumentu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Termin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WartośćBrutto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Zapłacono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pozostało = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WartoscNierozliczonychKorekt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PozostaloPoKorektach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W1Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W2Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W3Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UTColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WWColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zwColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).BeginInit();
            this.TopSplitContainer.Panel1.SuspendLayout();
            this.TopSplitContainer.Panel2.SuspendLayout();
            this.TopSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftSplitContainer)).BeginInit();
            this.LeftSplitContainer.Panel1.SuspendLayout();
            this.LeftSplitContainer.Panel2.SuspendLayout();
            this.LeftSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RightSplitContainer)).BeginInit();
            this.RightSplitContainer.Panel1.SuspendLayout();
            this.RightSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomSplitContainer)).BeginInit();
            this.BottomSplitContainer.Panel1.SuspendLayout();
            this.BottomSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // FeaturesTreeView
            // 
            this.FeaturesTreeView.Size = new System.Drawing.Size(207, 617);
            this.FeaturesTreeView.TableName = "Kontrahenci";
            // 
            // TopSplitContainer
            // 
            // 
            // TopSplitContainer.Panel1
            // 
            this.TopSplitContainer.Panel1.Controls.Add(this.zatwierdźDatęButton);
            this.TopSplitContainer.Panel1.Controls.Add(this.maxDataWystDateTimePicker);
            this.TopSplitContainer.Panel1.Controls.Add(this.label1);
            this.TopSplitContainer.Panel1Collapsed = false;
            this.TopSplitContainer.Size = new System.Drawing.Size(1546, 664);
            this.TopSplitContainer.SplitterDistance = 43;
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.Types.RozrachunekRow);
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.Size = new System.Drawing.Size(1546, 617);
            this.LeftSplitContainer.SplitterDistance = 207;
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Size = new System.Drawing.Size(1335, 617);
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Size = new System.Drawing.Size(1335, 617);
            // 
            // DataGrid
            // 
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Blokada,
            this.BlokadaSprzedaży,
            this.PrzdstawicielKontrahent,
            this.KodKontrahenta,
            this.NazwaKontrahenta,
            this.DataDokumentu,
            this.Termin,
            this.WartośćBrutto,
            this.Zapłacono,
            this.Pozostało,
            this.WartoscNierozliczonychKorekt,
            this.PozostaloPoKorektach,
            this.teColumn,
            this.W1Column,
            this.W2Column,
            this.W3Column,
            this.UTColumn,
            this.SPColumn,
            this.WWColumn,
            this.zwColumn});
            this.DataGrid.Size = new System.Drawing.Size(1335, 617);
            this.DataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGrid_DataError);
            this.DataGrid.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DataGrid_RowPrePaint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Max data wyst.:";
            // 
            // maxDataWystDateTimePicker
            // 
            this.maxDataWystDateTimePicker.Location = new System.Drawing.Point(99, 8);
            this.maxDataWystDateTimePicker.Name = "maxDataWystDateTimePicker";
            this.maxDataWystDateTimePicker.Size = new System.Drawing.Size(131, 20);
            this.maxDataWystDateTimePicker.TabIndex = 1;
            this.maxDataWystDateTimePicker.ValueChanged += new System.EventHandler(this.maxDataWystDateTimePicker_ValueChanged);
            // 
            // zatwierdźDatęButton
            // 
            this.zatwierdźDatęButton.Location = new System.Drawing.Point(236, 7);
            this.zatwierdźDatęButton.Name = "zatwierdźDatęButton";
            this.zatwierdźDatęButton.Size = new System.Drawing.Size(88, 23);
            this.zatwierdźDatęButton.TabIndex = 2;
            this.zatwierdźDatęButton.Text = "Zatwierdź datę";
            this.zatwierdźDatęButton.UseVisualStyleBackColor = true;
            this.zatwierdźDatęButton.Click += new System.EventHandler(this.zatwierdźDatęButton_Click);
            // 
            // Blokada
            // 
            this.Blokada.DataPropertyName = "Blokada";
            this.Blokada.FillWeight = 30F;
            this.Blokada.HeaderText = "B";
            this.Blokada.Name = "Blokada";
            this.Blokada.ReadOnly = true;
            this.Blokada.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Blokada.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Blokada.Width = 30;
            // 
            // BlokadaSprzedaży
            // 
            this.BlokadaSprzedaży.DataPropertyName = "BlokadaSprzedaży";
            this.BlokadaSprzedaży.FillWeight = 30F;
            this.BlokadaSprzedaży.HeaderText = "BS";
            this.BlokadaSprzedaży.Name = "BlokadaSprzedaży";
            this.BlokadaSprzedaży.ReadOnly = true;
            this.BlokadaSprzedaży.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BlokadaSprzedaży.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.BlokadaSprzedaży.Width = 30;
            // 
            // PrzdstawicielKontrahent
            // 
            this.PrzdstawicielKontrahent.DataPropertyName = "PrzedstawicielKontrahent";
            this.PrzdstawicielKontrahent.FillWeight = 30F;
            this.PrzdstawicielKontrahent.HeaderText = "PK";
            this.PrzdstawicielKontrahent.Name = "PrzdstawicielKontrahent";
            this.PrzdstawicielKontrahent.ReadOnly = true;
            this.PrzdstawicielKontrahent.Width = 30;
            // 
            // KodKontrahenta
            // 
            this.KodKontrahenta.DataPropertyName = "KodKontrahenta";
            this.KodKontrahenta.FillWeight = 150F;
            this.KodKontrahenta.HeaderText = "Kod";
            this.KodKontrahenta.Name = "KodKontrahenta";
            this.KodKontrahenta.ReadOnly = true;
            this.KodKontrahenta.Width = 150;
            // 
            // NazwaKontrahenta
            // 
            this.NazwaKontrahenta.DataPropertyName = "NazwaKontrahentaLine";
            this.NazwaKontrahenta.FillWeight = 350F;
            this.NazwaKontrahenta.HeaderText = "Nazwa kontrahenta";
            this.NazwaKontrahenta.Name = "NazwaKontrahenta";
            this.NazwaKontrahenta.ReadOnly = true;
            this.NazwaKontrahenta.Width = 350;
            // 
            // DataDokumentu
            // 
            this.DataDokumentu.DataPropertyName = "DataDokumentu";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DataDokumentu.DefaultCellStyle = dataGridViewCellStyle1;
            this.DataDokumentu.HeaderText = "Data dok. (min.)";
            this.DataDokumentu.Name = "DataDokumentu";
            this.DataDokumentu.ReadOnly = true;
            // 
            // Termin
            // 
            this.Termin.DataPropertyName = "Termin";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Termin.DefaultCellStyle = dataGridViewCellStyle2;
            this.Termin.HeaderText = "Termin (min.)";
            this.Termin.Name = "Termin";
            this.Termin.ReadOnly = true;
            // 
            // WartośćBrutto
            // 
            this.WartośćBrutto.DataPropertyName = "WartośćBrutto";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.WartośćBrutto.DefaultCellStyle = dataGridViewCellStyle3;
            this.WartośćBrutto.HeaderText = "Wartość Brutto";
            this.WartośćBrutto.Name = "WartośćBrutto";
            this.WartośćBrutto.ReadOnly = true;
            // 
            // Zapłacono
            // 
            this.Zapłacono.DataPropertyName = "Zapłacono";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.Zapłacono.DefaultCellStyle = dataGridViewCellStyle4;
            this.Zapłacono.HeaderText = "Zapłacono";
            this.Zapłacono.Name = "Zapłacono";
            this.Zapłacono.ReadOnly = true;
            // 
            // Pozostało
            // 
            this.Pozostało.DataPropertyName = "Pozostało";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.Pozostało.DefaultCellStyle = dataGridViewCellStyle5;
            this.Pozostało.HeaderText = "Pozostało";
            this.Pozostało.Name = "Pozostało";
            this.Pozostało.ReadOnly = true;
            // 
            // WartoscNierozliczonychKorekt
            // 
            this.WartoscNierozliczonychKorekt.DataPropertyName = "WartoscNierozliczonychZobowiazan";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.WartoscNierozliczonychKorekt.DefaultCellStyle = dataGridViewCellStyle6;
            this.WartoscNierozliczonychKorekt.HeaderText = "Zobowiązania";
            this.WartoscNierozliczonychKorekt.Name = "WartoscNierozliczonychKorekt";
            this.WartoscNierozliczonychKorekt.ReadOnly = true;
            // 
            // PozostaloPoKorektach
            // 
            this.PozostaloPoKorektach.DataPropertyName = "PozostaloPoRozliczeniu";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "C2";
            dataGridViewCellStyle7.NullValue = null;
            this.PozostaloPoKorektach.DefaultCellStyle = dataGridViewCellStyle7;
            this.PozostaloPoKorektach.HeaderText = "Po rozliczeniu zob.";
            this.PozostaloPoKorektach.Name = "PozostaloPoKorektach";
            this.PozostaloPoKorektach.ReadOnly = true;
            // 
            // teColumn
            // 
            this.teColumn.DataPropertyName = "TE";
            this.teColumn.HeaderText = "TE";
            this.teColumn.Name = "teColumn";
            this.teColumn.ReadOnly = true;
            // 
            // W1Column
            // 
            this.W1Column.DataPropertyName = "W1";
            this.W1Column.HeaderText = "W1";
            this.W1Column.Name = "W1Column";
            this.W1Column.ReadOnly = true;
            // 
            // W2Column
            // 
            this.W2Column.DataPropertyName = "W2";
            this.W2Column.HeaderText = "W2";
            this.W2Column.Name = "W2Column";
            this.W2Column.ReadOnly = true;
            // 
            // W3Column
            // 
            this.W3Column.DataPropertyName = "W3";
            this.W3Column.HeaderText = "W3";
            this.W3Column.Name = "W3Column";
            this.W3Column.ReadOnly = true;
            // 
            // UTColumn
            // 
            this.UTColumn.DataPropertyName = "UT";
            this.UTColumn.HeaderText = "UT";
            this.UTColumn.Name = "UTColumn";
            this.UTColumn.ReadOnly = true;
            // 
            // SPColumn
            // 
            this.SPColumn.DataPropertyName = "SP";
            this.SPColumn.HeaderText = "SP";
            this.SPColumn.Name = "SPColumn";
            this.SPColumn.ReadOnly = true;
            // 
            // WWColumn
            // 
            this.WWColumn.DataPropertyName = "WW";
            this.WWColumn.HeaderText = "WW";
            this.WWColumn.Name = "WWColumn";
            this.WWColumn.ReadOnly = true;
            // 
            // zwColumn
            // 
            this.zwColumn.DataPropertyName = "ZakończenieWindykacji";
            this.zwColumn.HeaderText = "ZW";
            this.zwColumn.Name = "zwColumn";
            this.zwColumn.ReadOnly = true;
            // 
            // RozrachunkiPrzeterminowaneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1546, 689);
            this.ConfigFile = "Grids\\przeterminowane_rozrachunki.xml";
            this.DataSource = typeof(Enova.Business.Old.Types.RozrachunekRow);
            this.Name = "RozrachunkiPrzeterminowaneForm";
            this.TableName = "Kontrahenci";
            this.Text = "Przeterminowane rozrachunki";
            this.TopPanelCollapsed = false;
            this.PrintItemClick += new System.EventHandler(this.RozrachunkiPrzeterminowaneForm_PrintItemClick);
            this.Load += new System.EventHandler(this.RozrachunkiPrzeterminowaneForm_Load);
            this.Controls.SetChildIndex(this.TopSplitContainer, 0);
            this.TopSplitContainer.Panel1.ResumeLayout(false);
            this.TopSplitContainer.Panel1.PerformLayout();
            this.TopSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).EndInit();
            this.TopSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).EndInit();
            this.LeftSplitContainer.Panel1.ResumeLayout(false);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker maxDataWystDateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button zatwierdźDatęButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Blokada;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BlokadaSprzedaży;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrzdstawicielKontrahent;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodKontrahenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn NazwaKontrahenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataDokumentu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Termin;
        private System.Windows.Forms.DataGridViewTextBoxColumn WartośćBrutto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zapłacono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pozostało;
        private System.Windows.Forms.DataGridViewTextBoxColumn WartoscNierozliczonychKorekt;
        private System.Windows.Forms.DataGridViewTextBoxColumn PozostaloPoKorektach;
        private System.Windows.Forms.DataGridViewTextBoxColumn teColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn W1Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn W2Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn W3Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn UTColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WWColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zwColumn;







    }
}
