namespace AbakTools.CRM.Forms
{
    partial class KontrahenciForm
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
            this.KodColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrzedstawicielColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NazwaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NipColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Kontrahent);
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
            this.KodColumn,
            this.PrzedstawicielColumn,
            this.NazwaColumn,
            this.NipColumn,
            this.EmailColumn});
            this.DataGrid.Size = new System.Drawing.Size(1351, 664);
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
            // PrzedstawicielColumn
            // 
            this.PrzedstawicielColumn.DataPropertyName = "PrzedstawicielKod";
            this.PrzedstawicielColumn.FillWeight = 30F;
            this.PrzedstawicielColumn.HeaderText = "PR";
            this.PrzedstawicielColumn.Name = "PrzedstawicielColumn";
            this.PrzedstawicielColumn.ReadOnly = true;
            this.PrzedstawicielColumn.Width = 30;
            // 
            // NazwaColumn
            // 
            this.NazwaColumn.DataPropertyName = "Nazwa";
            this.NazwaColumn.FillWeight = 400F;
            this.NazwaColumn.HeaderText = "Nazwa";
            this.NazwaColumn.Name = "NazwaColumn";
            this.NazwaColumn.ReadOnly = true;
            this.NazwaColumn.Width = 400;
            // 
            // NipColumn
            // 
            this.NipColumn.DataPropertyName = "Nip";
            this.NipColumn.HeaderText = "Nip";
            this.NipColumn.Name = "NipColumn";
            this.NipColumn.ReadOnly = true;
            // 
            // EmailColumn
            // 
            this.EmailColumn.DataPropertyName = "Email";
            this.EmailColumn.FillWeight = 200F;
            this.EmailColumn.HeaderText = "Email";
            this.EmailColumn.Name = "EmailColumn";
            this.EmailColumn.ReadOnly = true;
            this.EmailColumn.Width = 200;
            // 
            // KontrahenciForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(1351, 689);
            this.DataSource = typeof(Enova.Business.Old.DB.Web.Kontrahent);
            this.LeftPanelCollapsed = true;
            this.Name = "KontrahenciForm";
            this.RightPanelCollapsed = true;
            this.Text = "Kontrahenci";
            this.TopPanelCollapsed = true;
            this.Load += new System.EventHandler(this.KontrahenciForm_Load);
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

        private System.Windows.Forms.DataGridViewTextBoxColumn KodColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrzedstawicielColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NazwaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NipColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailColumn;




    }
}
