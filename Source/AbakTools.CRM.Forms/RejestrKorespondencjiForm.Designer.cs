namespace AbakTools.CRM.Forms
{
    partial class RejestrKorespondencjiForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataWysylki = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdresPelny = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Opis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RodzajKorespondencjiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UzytkownikColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).BeginInit();
            this.TopSplitContainer.Panel2.SuspendLayout();
            this.TopSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).BeginInit();
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
            this.SuspendLayout();
            // 
            // TopSplitContainer
            // 
            this.TopSplitContainer.Panel1Collapsed = true;
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Korespondencja);
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.Panel1Collapsed = true;
            this.LeftSplitContainer.Size = new System.Drawing.Size(1351, 664);
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Panel2Collapsed = true;
            this.RightSplitContainer.Size = new System.Drawing.Size(1351, 664);
            this.RightSplitContainer.SplitterDistance = 1198;
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Panel2Collapsed = true;
            this.BottomSplitContainer.Size = new System.Drawing.Size(1351, 664);
            this.BottomSplitContainer.SplitterDistance = 549;
            // 
            // DataGrid
            // 
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDColumn,
            this.DataWysylki,
            this.KodColumn,
            this.Nazwa,
            this.AdresPelny,
            this.Opis,
            this.RodzajKorespondencjiColumn,
            this.UzytkownikColumn});
            this.DataGrid.Size = new System.Drawing.Size(1351, 664);
            // 
            // IDColumn
            // 
            this.IDColumn.DataPropertyName = "ID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.IDColumn.FillWeight = 60F;
            this.IDColumn.HeaderText = "ID";
            this.IDColumn.Name = "IDColumn";
            this.IDColumn.ReadOnly = true;
            this.IDColumn.Width = 60;
            // 
            // DataWysylki
            // 
            this.DataWysylki.DataPropertyName = "DataWysylki";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.DataWysylki.DefaultCellStyle = dataGridViewCellStyle4;
            this.DataWysylki.HeaderText = "Data";
            this.DataWysylki.Name = "DataWysylki";
            this.DataWysylki.ReadOnly = true;
            // 
            // KodColumn
            // 
            this.KodColumn.DataPropertyName = "Kod";
            this.KodColumn.FillWeight = 200F;
            this.KodColumn.HeaderText = "Kod";
            this.KodColumn.Name = "KodColumn";
            this.KodColumn.ReadOnly = true;
            this.KodColumn.Width = 200;
            // 
            // Nazwa
            // 
            this.Nazwa.DataPropertyName = "Nazwa";
            this.Nazwa.FillWeight = 300F;
            this.Nazwa.HeaderText = "Nazwa";
            this.Nazwa.Name = "Nazwa";
            this.Nazwa.ReadOnly = true;
            this.Nazwa.Width = 300;
            // 
            // AdresPelny
            // 
            this.AdresPelny.DataPropertyName = "AdresPelny";
            this.AdresPelny.FillWeight = 350F;
            this.AdresPelny.HeaderText = "Adres";
            this.AdresPelny.Name = "AdresPelny";
            this.AdresPelny.ReadOnly = true;
            this.AdresPelny.Width = 350;
            // 
            // Opis
            // 
            this.Opis.DataPropertyName = "Opis";
            this.Opis.FillWeight = 300F;
            this.Opis.HeaderText = "Opis";
            this.Opis.Name = "Opis";
            this.Opis.ReadOnly = true;
            this.Opis.Width = 300;
            // 
            // RodzajKorespondencjiColumn
            // 
            this.RodzajKorespondencjiColumn.DataPropertyName = "RodzajKorespondencji";
            this.RodzajKorespondencjiColumn.FillWeight = 300F;
            this.RodzajKorespondencjiColumn.HeaderText = "Rodzaj";
            this.RodzajKorespondencjiColumn.Name = "RodzajKorespondencjiColumn";
            this.RodzajKorespondencjiColumn.ReadOnly = true;
            this.RodzajKorespondencjiColumn.Width = 300;
            // 
            // UzytkownikColumn
            // 
            this.UzytkownikColumn.DataPropertyName = "Uzytkownik";
            this.UzytkownikColumn.FillWeight = 50F;
            this.UzytkownikColumn.HeaderText = "Użytkownik";
            this.UzytkownikColumn.Name = "UzytkownikColumn";
            this.UzytkownikColumn.ReadOnly = true;
            this.UzytkownikColumn.Width = 50;
            // 
            // RejestrKorespondencjiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(1351, 689);
            this.ConfigFile = "Grids\\rejestrkorespondencji.xml";
            this.DataSource = typeof(Enova.Business.Old.DB.Web.Korespondencja);
            this.LeftPanelCollapsed = true;
            this.Name = "RejestrKorespondencjiForm";
            this.RightPanelCollapsed = true;
            this.Text = "Korespondencja seryjna";
            this.TopPanelCollapsed = true;
            this.Load += new System.EventHandler(this.RejestrKorespondencjiForm_Load);
            this.Controls.SetChildIndex(this.TopSplitContainer, 0);
            this.TopSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).EndInit();
            this.TopSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).EndInit();
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

        private System.Windows.Forms.DataGridViewTextBoxColumn IDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataWysylki;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdresPelny;
        private System.Windows.Forms.DataGridViewTextBoxColumn Opis;
        private System.Windows.Forms.DataGridViewTextBoxColumn RodzajKorespondencjiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UzytkownikColumn;








    }
}
