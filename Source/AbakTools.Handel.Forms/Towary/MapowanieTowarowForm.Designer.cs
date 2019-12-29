namespace AbakTools.Towary.Forms
{
    partial class MapowanieTowarowForm
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
            this.SrcKodColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SrcNazwaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DstKodColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DstNazwaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.TopSplitContainer.Size = new System.Drawing.Size(1144, 569);
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.Types.MapowanieTowaruRow);
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.Panel1Collapsed = true;
            this.LeftSplitContainer.Size = new System.Drawing.Size(1144, 569);
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Panel2Collapsed = true;
            this.RightSplitContainer.Size = new System.Drawing.Size(1144, 569);
            this.RightSplitContainer.SplitterDistance = 1198;
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Panel2Collapsed = true;
            this.BottomSplitContainer.Size = new System.Drawing.Size(1144, 569);
            this.BottomSplitContainer.SplitterDistance = 549;
            // 
            // DataGrid
            // 
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrcKodColumn,
            this.SrcNazwaColumn,
            this.DstKodColumn,
            this.DstNazwaColumn});
            this.DataGrid.Size = new System.Drawing.Size(1144, 569);
            // 
            // SrcKodColumn
            // 
            this.SrcKodColumn.DataPropertyName = "SrcKod";
            this.SrcKodColumn.FillWeight = 150F;
            this.SrcKodColumn.HeaderText = "Źródło kod";
            this.SrcKodColumn.Name = "SrcKodColumn";
            this.SrcKodColumn.ReadOnly = true;
            this.SrcKodColumn.Width = 150;
            // 
            // SrcNazwaColumn
            // 
            this.SrcNazwaColumn.DataPropertyName = "SrcNazwa";
            this.SrcNazwaColumn.FillWeight = 400F;
            this.SrcNazwaColumn.HeaderText = "Źródło nazwa";
            this.SrcNazwaColumn.Name = "SrcNazwaColumn";
            this.SrcNazwaColumn.ReadOnly = true;
            this.SrcNazwaColumn.Width = 400;
            // 
            // DstKodColumn
            // 
            this.DstKodColumn.DataPropertyName = "DstKod";
            this.DstKodColumn.FillWeight = 150F;
            this.DstKodColumn.HeaderText = "Cel kod";
            this.DstKodColumn.Name = "DstKodColumn";
            this.DstKodColumn.ReadOnly = true;
            this.DstKodColumn.Width = 150;
            // 
            // DstNazwaColumn
            // 
            this.DstNazwaColumn.DataPropertyName = "DstNazwa";
            this.DstNazwaColumn.FillWeight = 400F;
            this.DstNazwaColumn.HeaderText = "Cel nazwa";
            this.DstNazwaColumn.Name = "DstNazwaColumn";
            this.DstNazwaColumn.ReadOnly = true;
            this.DstNazwaColumn.Width = 400;
            // 
            // MapowanieTowarowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(1144, 594);
            this.DataSource = typeof(Enova.Business.Old.Types.MapowanieTowaruRow);
            this.LeftPanelCollapsed = true;
            this.Name = "MapowanieTowarowForm";
            this.RightPanelCollapsed = true;
            this.Text = "Mapowanie towarów";
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

        private System.Windows.Forms.DataGridViewTextBoxColumn SrcKodColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrcNazwaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DstKodColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DstNazwaColumn;

    }
}
