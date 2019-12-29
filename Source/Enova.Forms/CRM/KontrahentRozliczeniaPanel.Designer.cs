namespace Enova.Forms.CRM
{
    partial class KontrahentRozliczeniaPanel
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
            this.label12 = new System.Windows.Forms.Label();
            this.rozliczeniaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView = new BAL.Forms.Controls.GridView();
            this.DataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DokumentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KwotaZaplatyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZaplataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateFromToControl = new BAL.Forms.Controls.DateFromToControl();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rozliczeniaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(29, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Okres:";
            // 
            // rozliczeniaBindingSource
            // 
            this.rozliczeniaBindingSource.DataSource = typeof(Enova.API.Kasa.RozliczenieSP);
            // 
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridView.AutoGenerateColumns = false;
            this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataColumn,
            this.DokumentColumn,
            this.KwotaZaplatyColumn,
            this.ZaplataColumn});
            this.gridView.DataSource = this.rozliczeniaBindingSource;
            this.gridView.Location = new System.Drawing.Point(3, 31);
            this.gridView.Name = "gridView";
            this.gridView.ReadOnly = true;
            this.gridView.RowHeadersVisible = false;
            this.gridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridView.Size = new System.Drawing.Size(714, 472);
            this.gridView.TabIndex = 5;
            this.gridView.VirtualMode = true;
            // 
            // DataColumn
            // 
            this.DataColumn.DataPropertyName = "Data";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DataColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.DataColumn.FillWeight = 80F;
            this.DataColumn.HeaderText = "Data";
            this.DataColumn.Name = "DataColumn";
            this.DataColumn.ReadOnly = true;
            this.DataColumn.Width = 80;
            // 
            // DokumentColumn
            // 
            this.DokumentColumn.DataPropertyName = "Dokument.NumerDokumentu";
            this.DokumentColumn.FillWeight = 140F;
            this.DokumentColumn.HeaderText = "Dokument";
            this.DokumentColumn.Name = "DokumentColumn";
            this.DokumentColumn.ReadOnly = true;
            this.DokumentColumn.Width = 140;
            // 
            // KwotaZaplatyColumn
            // 
            this.KwotaZaplatyColumn.DataPropertyName = "KwotaZaplaty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.KwotaZaplatyColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.KwotaZaplatyColumn.HeaderText = "Kwota zaplaty";
            this.KwotaZaplatyColumn.Name = "KwotaZaplatyColumn";
            this.KwotaZaplatyColumn.ReadOnly = true;
            // 
            // ZaplataColumn
            // 
            this.ZaplataColumn.DataPropertyName = "Zaplata.NumerDokumentu";
            this.ZaplataColumn.FillWeight = 150F;
            this.ZaplataColumn.HeaderText = "Dok. zapłaty";
            this.ZaplataColumn.Name = "ZaplataColumn";
            this.ZaplataColumn.ReadOnly = true;
            this.ZaplataColumn.Width = 150;
            // 
            // dateFromToControl
            // 
            this.dateFromToControl.DataContext = null;
            this.dateFromToControl.Location = new System.Drawing.Point(73, 3);
            this.dateFromToControl.Name = "dateFromToControl";
            this.dateFromToControl.Size = new System.Drawing.Size(170, 24);
            this.dateFromToControl.TabIndex = 6;
            this.dateFromToControl.ValueChanged += new System.EventHandler(this.dateFromToControl_ValueChanged);
            // 
            // KontrahentRozliczeniaPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.dateFromToControl);
            this.Controls.Add(this.gridView);
            this.Controls.Add(this.label12);
            this.Name = "KontrahentRozliczeniaPanel";
            this.Size = new System.Drawing.Size(720, 506);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rozliczeniaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.BindingSource rozliczeniaBindingSource;
        private BAL.Forms.Controls.GridView gridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DokumentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn KwotaZaplatyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZaplataColumn;
        private BAL.Forms.Controls.DateFromToControl dateFromToControl;
    }
}
