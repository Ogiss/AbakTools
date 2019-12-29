namespace AbakTools.Towary.Forms
{
    partial class TowaryEnovaForm
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
            this.kategorieTreeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.szukajTextBox = new System.Windows.Forms.TextBox();
            this.KodColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            // TopSplitContainer
            // 
            // 
            // TopSplitContainer.Panel1
            // 
            this.TopSplitContainer.Panel1.Controls.Add(this.szukajTextBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.label1);
            this.TopSplitContainer.SplitterDistance = 51;
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Produkt);
            // 
            // LeftSplitContainer
            // 
            // 
            // LeftSplitContainer.Panel1
            // 
            this.LeftSplitContainer.Panel1.Controls.Add(this.kategorieTreeView);
            this.LeftSplitContainer.Size = new System.Drawing.Size(1351, 609);
            this.LeftSplitContainer.SplitterDistance = 246;
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Panel2Collapsed = true;
            this.RightSplitContainer.Size = new System.Drawing.Size(1101, 609);
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Panel2Collapsed = true;
            this.BottomSplitContainer.Size = new System.Drawing.Size(1101, 609);
            this.BottomSplitContainer.SplitterDistance = 504;
            // 
            // DataGrid
            // 
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KodColumn,
            this.Nazwa,
            this.CenaColumn});
            this.DataGrid.Size = new System.Drawing.Size(1101, 609);
            this.DataGrid.SelectionChanged += new System.EventHandler(this.DataGrid_SelectionChanged);
            this.DataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGrid_KeyDown);
            this.DataGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DataGrid_KeyPress);
            // 
            // kategorieTreeView
            // 
            this.kategorieTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kategorieTreeView.HideSelection = false;
            this.kategorieTreeView.Location = new System.Drawing.Point(0, 0);
            this.kategorieTreeView.Name = "kategorieTreeView";
            this.kategorieTreeView.Size = new System.Drawing.Size(246, 609);
            this.kategorieTreeView.TabIndex = 0;
            this.kategorieTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.kategorieTreeView_AfterSelect);
            this.kategorieTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kategorieTreeView_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Szukaj:";
            // 
            // szukajTextBox
            // 
            this.szukajTextBox.Location = new System.Drawing.Point(72, 12);
            this.szukajTextBox.Name = "szukajTextBox";
            this.szukajTextBox.Size = new System.Drawing.Size(374, 20);
            this.szukajTextBox.TabIndex = 1;
            this.szukajTextBox.TextChanged += new System.EventHandler(this.szukajTextBox_TextChanged);
            // 
            // KodColumn
            // 
            this.KodColumn.DataPropertyName = "Kod";
            this.KodColumn.FillWeight = 150F;
            this.KodColumn.HeaderText = "Kod";
            this.KodColumn.Name = "KodColumn";
            this.KodColumn.ReadOnly = true;
            this.KodColumn.Width = 150;
            // 
            // Nazwa
            // 
            this.Nazwa.DataPropertyName = "Nazwa";
            this.Nazwa.FillWeight = 450F;
            this.Nazwa.HeaderText = "Nazwa";
            this.Nazwa.Name = "Nazwa";
            this.Nazwa.ReadOnly = true;
            this.Nazwa.Width = 450;
            // 
            // CenaColumn
            // 
            this.CenaColumn.DataPropertyName = "Cena";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.CenaColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.CenaColumn.HeaderText = "Cena";
            this.CenaColumn.Name = "CenaColumn";
            this.CenaColumn.ReadOnly = true;
            // 
            // TowaryEnovaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(1351, 689);
            this.DataSource = typeof(Enova.Business.Old.DB.Web.Produkt);
            this.Name = "TowaryEnovaForm";
            this.RightPanelCollapsed = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TowaryEnovaForm_FormClosing);
            this.Load += new System.EventHandler(this.TowaryEnovaForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TowaryEnovaForm_KeyDown);
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

        private System.Windows.Forms.TreeView kategorieTreeView;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenaColumn;
        private System.Windows.Forms.TextBox szukajTextBox;
        private System.Windows.Forms.Label label1;
    }
}
