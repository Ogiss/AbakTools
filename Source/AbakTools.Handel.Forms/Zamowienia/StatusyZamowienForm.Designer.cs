namespace EnovaTools.Forms.Web
{
    partial class StatusyZamowienForm
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
            this.NazwaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WyslacEmailColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FakturaColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
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
            this.TopSplitContainer.Size = new System.Drawing.Size(802, 475);
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.StatusZamowienia);
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.Panel1Collapsed = true;
            this.LeftSplitContainer.Size = new System.Drawing.Size(802, 475);
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Panel2Collapsed = true;
            this.RightSplitContainer.Size = new System.Drawing.Size(802, 475);
            this.RightSplitContainer.SplitterDistance = 1198;
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Panel2Collapsed = true;
            this.BottomSplitContainer.Size = new System.Drawing.Size(802, 475);
            this.BottomSplitContainer.SplitterDistance = 549;
            // 
            // DataGrid
            // 
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NazwaColumn,
            this.WyslacEmailColumn,
            this.FakturaColumn});
            this.DataGrid.RowHeadersVisible = true;
            this.DataGrid.Size = new System.Drawing.Size(802, 475);
            this.DataGrid.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DataGrid_RowPrePaint);
            // 
            // NazwaColumn
            // 
            this.NazwaColumn.DataPropertyName = "Nazwa";
            this.NazwaColumn.FillWeight = 300F;
            this.NazwaColumn.HeaderText = "Nazwa";
            this.NazwaColumn.Name = "NazwaColumn";
            this.NazwaColumn.ReadOnly = true;
            this.NazwaColumn.Width = 300;
            // 
            // WyslacEmailColumn
            // 
            this.WyslacEmailColumn.DataPropertyName = "WyslacEmail";
            this.WyslacEmailColumn.HeaderText = "Wysłać Email";
            this.WyslacEmailColumn.Name = "WyslacEmailColumn";
            this.WyslacEmailColumn.ReadOnly = true;
            // 
            // FakturaColumn
            // 
            this.FakturaColumn.DataPropertyName = "Faktura";
            this.FakturaColumn.HeaderText = "Faktura";
            this.FakturaColumn.Name = "FakturaColumn";
            this.FakturaColumn.ReadOnly = true;
            // 
            // StatusyZamowienForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(802, 500);
            this.DataSource = typeof(Enova.Business.Old.DB.Web.StatusZamowienia);
            this.LeftPanelCollapsed = true;
            this.Name = "StatusyZamowienForm";
            this.RightPanelCollapsed = true;
            this.Text = "Statusy zamówień";
            this.TopPanelCollapsed = true;
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

        private System.Windows.Forms.DataGridViewTextBoxColumn NazwaColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn WyslacEmailColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn FakturaColumn;
    }
}
