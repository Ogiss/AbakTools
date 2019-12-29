namespace AbakTools.Towary.Forms
{
    partial class MagazynAVForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.aktywnoscCcomboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dostepnoscComboBox = new System.Windows.Forms.ComboBox();
            this.KodColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NazwaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AtrybutNazwaPelnaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VisibleColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AktywnyColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DostepnyColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.autoSynchColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.stanMinColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).BeginInit();
            this.TopSplitContainer.Panel1.SuspendLayout();
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
            // 
            // TopSplitContainer.Panel1
            // 
            this.TopSplitContainer.Panel1.Controls.Add(this.dostepnoscComboBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.label3);
            this.TopSplitContainer.Panel1.Controls.Add(this.aktywnoscCcomboBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.label2);
            this.TopSplitContainer.Size = new System.Drawing.Size(1491, 664);
            this.TopSplitContainer.SplitterDistance = 59;
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.ProduktAtrybut);
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.Panel1Collapsed = true;
            this.LeftSplitContainer.Size = new System.Drawing.Size(1491, 601);
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Panel2Collapsed = true;
            this.RightSplitContainer.Size = new System.Drawing.Size(1491, 601);
            this.RightSplitContainer.SplitterDistance = 1198;
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Panel2Collapsed = true;
            this.BottomSplitContainer.Size = new System.Drawing.Size(1491, 601);
            this.BottomSplitContainer.SplitterDistance = 549;
            // 
            // DataGrid
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGrid.ColumnHeadersHeight = 40;
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KodColumn,
            this.NazwaColumn,
            this.AtrybutNazwaPelnaColumn,
            this.VisibleColumn,
            this.AktywnyColumn,
            this.DostepnyColumn,
            this.autoSynchColumn,
            this.stanMinColumn});
            this.DataGrid.ReadOnly = false;
            this.DataGrid.Size = new System.Drawing.Size(1491, 601);
            this.DataGrid.VirtualMode = true;
            this.DataGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGrid_CellMouseUp);
            this.DataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellValueChanged);
            this.DataGrid.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DataGrid_RowPrePaint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Aktywność:";
            // 
            // aktywnoscCcomboBox
            // 
            this.aktywnoscCcomboBox.FormattingEnabled = true;
            this.aktywnoscCcomboBox.Items.AddRange(new object[] {
            "Aktywny",
            "Nieaktywny",
            "Razem"});
            this.aktywnoscCcomboBox.Location = new System.Drawing.Point(89, 17);
            this.aktywnoscCcomboBox.Name = "aktywnoscCcomboBox";
            this.aktywnoscCcomboBox.Size = new System.Drawing.Size(121, 21);
            this.aktywnoscCcomboBox.TabIndex = 2;
            this.aktywnoscCcomboBox.SelectionChangeCommitted += new System.EventHandler(this.aktywnoscCcomboBox_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Dostepność:";
            // 
            // dostepnoscComboBox
            // 
            this.dostepnoscComboBox.FormattingEnabled = true;
            this.dostepnoscComboBox.Items.AddRange(new object[] {
            "Dostepny",
            "Niedostępny",
            "Razem"});
            this.dostepnoscComboBox.Location = new System.Drawing.Point(312, 17);
            this.dostepnoscComboBox.Name = "dostepnoscComboBox";
            this.dostepnoscComboBox.Size = new System.Drawing.Size(128, 21);
            this.dostepnoscComboBox.TabIndex = 4;
            this.dostepnoscComboBox.SelectionChangeCommitted += new System.EventHandler(this.dostepnoscComboBox_SelectionChangeCommitted);
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
            // NazwaColumn
            // 
            this.NazwaColumn.DataPropertyName = "Nazwa";
            this.NazwaColumn.FillWeight = 350F;
            this.NazwaColumn.HeaderText = "Nazwa";
            this.NazwaColumn.Name = "NazwaColumn";
            this.NazwaColumn.ReadOnly = true;
            this.NazwaColumn.Width = 350;
            // 
            // AtrybutNazwaPelnaColumn
            // 
            this.AtrybutNazwaPelnaColumn.DataPropertyName = "AtrybutNazwaPelna";
            this.AtrybutNazwaPelnaColumn.FillWeight = 160F;
            this.AtrybutNazwaPelnaColumn.HeaderText = "Atrybut";
            this.AtrybutNazwaPelnaColumn.Name = "AtrybutNazwaPelnaColumn";
            this.AtrybutNazwaPelnaColumn.ReadOnly = true;
            this.AtrybutNazwaPelnaColumn.Width = 160;
            // 
            // VisibleColumn
            // 
            this.VisibleColumn.DataPropertyName = "Visible";
            this.VisibleColumn.FillWeight = 150F;
            this.VisibleColumn.HeaderText = "Widoczny na tej liście";
            this.VisibleColumn.Name = "VisibleColumn";
            this.VisibleColumn.Width = 150;
            // 
            // AktywnyColumn
            // 
            this.AktywnyColumn.DataPropertyName = "Aktywny";
            this.AktywnyColumn.FillWeight = 150F;
            this.AktywnyColumn.HeaderText = "Widoczny na stronie i w toolsach";
            this.AktywnyColumn.Name = "AktywnyColumn";
            this.AktywnyColumn.Width = 150;
            // 
            // DostepnyColumn
            // 
            this.DostepnyColumn.DataPropertyName = "Dostepny";
            this.DostepnyColumn.FillWeight = 150F;
            this.DostepnyColumn.HeaderText = "Dostepny na stronie i w toolsach";
            this.DostepnyColumn.Name = "DostepnyColumn";
            this.DostepnyColumn.Width = 150;
            // 
            // autoSynchColumn
            // 
            this.autoSynchColumn.DataPropertyName = "AutoSynchDostepnosc";
            this.autoSynchColumn.HeaderText = "Auto synch.";
            this.autoSynchColumn.Name = "autoSynchColumn";
            this.autoSynchColumn.ReadOnly = true;
            // 
            // stanMinColumn
            // 
            this.stanMinColumn.DataPropertyName = "AutoSynchStanMin";
            this.stanMinColumn.HeaderText = "Stan min.";
            this.stanMinColumn.Name = "stanMinColumn";
            this.stanMinColumn.ReadOnly = true;
            // 
            // MagazynAVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(1491, 689);
            this.DataSource = typeof(Enova.Business.Old.DB.Web.ProduktAtrybut);
            this.LeftPanelCollapsed = true;
            this.Name = "MagazynAVForm";
            this.RightPanelCollapsed = true;
            this.Text = "Magazyn - Proponowane dostepne/niedostepne";
            this.Load += new System.EventHandler(this.MagazynAVForm_Load);
            this.Controls.SetChildIndex(this.TopSplitContainer, 0);
            this.TopSplitContainer.Panel1.ResumeLayout(false);
            this.TopSplitContainer.Panel1.PerformLayout();
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

        private System.Windows.Forms.ComboBox dostepnoscComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox aktywnoscCcomboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NazwaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AtrybutNazwaPelnaColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn VisibleColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AktywnyColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DostepnyColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn autoSynchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stanMinColumn;





    }
}
