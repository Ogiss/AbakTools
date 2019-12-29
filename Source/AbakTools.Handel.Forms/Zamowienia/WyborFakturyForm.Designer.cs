namespace AbakTools.Zamowienia.Forms
{
    partial class WyborFakturyForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.okresDateSpan = new Enova.Business.Old.Controls.DateTimeSpanControl();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zatwierdzonyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numerPelnyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kontrahentKodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sumaNettoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumaVatColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sumaBruttoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Okres:";
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
            this.zatwierdzonyDataGridViewCheckBoxColumn,
            this.dataDataGridViewTextBoxColumn,
            this.numerPelnyDataGridViewTextBoxColumn,
            this.kontrahentKodDataGridViewTextBoxColumn,
            this.sumaNettoDataGridViewTextBoxColumn,
            this.SumaVatColumn,
            this.sumaBruttoDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.bindingSource;
            this.dataGridView.Location = new System.Drawing.Point(12, 36);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(947, 266);
            this.dataGridView.TabIndex = 2;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(Enova.Forms.Handel.DokumentHandlowyView);
            // 
            // okresDateSpan
            // 
            this.okresDateSpan.DateFrom = new System.DateTime(2011, 8, 1, 0, 0, 0, 0);
            this.okresDateSpan.DateTo = new System.DateTime(2011, 8, 31, 23, 59, 59, 0);
            this.okresDateSpan.Location = new System.Drawing.Point(56, 9);
            this.okresDateSpan.Name = "okresDateSpan";
            this.okresDateSpan.Size = new System.Drawing.Size(213, 21);
            this.okresDateSpan.TabIndex = 0;
            this.okresDateSpan.Changed += new System.EventHandler(this.okresDateSpan_Changed);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Przedstawiciel";
            this.dataGridViewTextBoxColumn1.FillWeight = 40F;
            this.dataGridViewTextBoxColumn1.HeaderText = "PR";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Data";
            this.dataGridViewTextBoxColumn2.FillWeight = 80F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Data";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "NumerPelny";
            this.dataGridViewTextBoxColumn3.HeaderText = "Numer";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "KontrahentKod";
            this.dataGridViewTextBoxColumn4.FillWeight = 300F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Kontrahent";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 300;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "SumaNetto";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn5.HeaderText = "Suma netto";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "SumaVAT";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn6.HeaderText = "Suma VAT";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "SumaBrutto";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn7.HeaderText = "Suma brutto";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // zatwierdzonyDataGridViewCheckBoxColumn
            // 
            this.zatwierdzonyDataGridViewCheckBoxColumn.DataPropertyName = "Zatwierdzony";
            this.zatwierdzonyDataGridViewCheckBoxColumn.FillWeight = 30F;
            this.zatwierdzonyDataGridViewCheckBoxColumn.HeaderText = "Z";
            this.zatwierdzonyDataGridViewCheckBoxColumn.Name = "zatwierdzonyDataGridViewCheckBoxColumn";
            this.zatwierdzonyDataGridViewCheckBoxColumn.ReadOnly = true;
            this.zatwierdzonyDataGridViewCheckBoxColumn.Width = 30;
            // 
            // dataDataGridViewTextBoxColumn
            // 
            this.dataDataGridViewTextBoxColumn.DataPropertyName = "Data";
            this.dataDataGridViewTextBoxColumn.FillWeight = 80F;
            this.dataDataGridViewTextBoxColumn.HeaderText = "Data";
            this.dataDataGridViewTextBoxColumn.Name = "dataDataGridViewTextBoxColumn";
            this.dataDataGridViewTextBoxColumn.ReadOnly = true;
            this.dataDataGridViewTextBoxColumn.Width = 80;
            // 
            // numerPelnyDataGridViewTextBoxColumn
            // 
            this.numerPelnyDataGridViewTextBoxColumn.DataPropertyName = "NumerPelny";
            this.numerPelnyDataGridViewTextBoxColumn.HeaderText = "Numer";
            this.numerPelnyDataGridViewTextBoxColumn.Name = "numerPelnyDataGridViewTextBoxColumn";
            this.numerPelnyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // kontrahentKodDataGridViewTextBoxColumn
            // 
            this.kontrahentKodDataGridViewTextBoxColumn.DataPropertyName = "KontrahentKod";
            this.kontrahentKodDataGridViewTextBoxColumn.FillWeight = 300F;
            this.kontrahentKodDataGridViewTextBoxColumn.HeaderText = "Kontrahent";
            this.kontrahentKodDataGridViewTextBoxColumn.Name = "kontrahentKodDataGridViewTextBoxColumn";
            this.kontrahentKodDataGridViewTextBoxColumn.ReadOnly = true;
            this.kontrahentKodDataGridViewTextBoxColumn.Width = 300;
            // 
            // sumaNettoDataGridViewTextBoxColumn
            // 
            this.sumaNettoDataGridViewTextBoxColumn.DataPropertyName = "SumaNetto";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.sumaNettoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.sumaNettoDataGridViewTextBoxColumn.HeaderText = "Suma netto";
            this.sumaNettoDataGridViewTextBoxColumn.Name = "sumaNettoDataGridViewTextBoxColumn";
            this.sumaNettoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // SumaVatColumn
            // 
            this.SumaVatColumn.DataPropertyName = "SumaVAT";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.SumaVatColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.SumaVatColumn.HeaderText = "Suma Vat";
            this.SumaVatColumn.Name = "SumaVatColumn";
            this.SumaVatColumn.ReadOnly = true;
            // 
            // sumaBruttoDataGridViewTextBoxColumn
            // 
            this.sumaBruttoDataGridViewTextBoxColumn.DataPropertyName = "SumaBrutto";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.sumaBruttoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.sumaBruttoDataGridViewTextBoxColumn.HeaderText = "Suma brutto";
            this.sumaBruttoDataGridViewTextBoxColumn.Name = "sumaBruttoDataGridViewTextBoxColumn";
            this.sumaBruttoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // WyborFakturyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 314);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okresDateSpan);
            this.Name = "WyborFakturyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wybór faktury";
            this.Load += new System.EventHandler(this.WyborFakturyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Enova.Business.Old.Controls.DateTimeSpanControl okresDateSpan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn zatwierdzonyDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numerPelnyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kontrahentKodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sumaNettoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SumaVatColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sumaBruttoDataGridViewTextBoxColumn;
    }
}