namespace AbakTools.Towary.Forms
{
    partial class MagazynForm
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
            this.kategorieTreeView = new Enova.Business.Old.Controls.KategorieTreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.aktywnoscComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dostepnoscComboBox = new System.Windows.Forms.ComboBox();
            this.KodColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NazwaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AtrybutNazwaPelnaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenaNettoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StawkaVatValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AktywnyColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DostepnyColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AvailableMsgColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AvailableDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.TopSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.TopSplitContainer.IsSplitterFixed = true;
            // 
            // TopSplitContainer.Panel1
            // 
            this.TopSplitContainer.Panel1.Controls.Add(this.dostepnoscComboBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.label3);
            this.TopSplitContainer.Panel1.Controls.Add(this.aktywnoscComboBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.label2);
            this.TopSplitContainer.Panel1.Controls.Add(this.findTextBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.label1);
            this.TopSplitContainer.Size = new System.Drawing.Size(1585, 664);
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.ProduktAtrybut);
            // 
            // LeftSplitContainer
            // 
            // 
            // LeftSplitContainer.Panel1
            // 
            this.LeftSplitContainer.Panel1.Controls.Add(this.kategorieTreeView);
            this.LeftSplitContainer.Size = new System.Drawing.Size(1585, 580);
            this.LeftSplitContainer.SplitterDistance = 378;
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Panel2Collapsed = true;
            this.RightSplitContainer.Size = new System.Drawing.Size(1203, 580);
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Panel2Collapsed = true;
            this.BottomSplitContainer.Size = new System.Drawing.Size(1203, 580);
            // 
            // DataGrid
            // 
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KodColumn,
            this.NazwaColumn,
            this.AtrybutNazwaPelnaColumn,
            this.CenaNettoColumn,
            this.StawkaVatValueColumn,
            this.AktywnyColumn,
            this.DostepnyColumn,
            this.AvailableMsgColumn,
            this.AvailableDateColumn});
            this.DataGrid.ReadOnly = false;
            this.DataGrid.Size = new System.Drawing.Size(1203, 580);
            this.DataGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGrid_CellMouseUp);
            this.DataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellValueChanged);
            this.DataGrid.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DataGrid_RowPrePaint);
            this.DataGrid.SelectionChanged += new System.EventHandler(this.DataGrid_SelectionChanged);
            this.DataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGrid_KeyDown);
            this.DataGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DataGrid_KeyPress);
            // 
            // kategorieTreeView
            // 
            this.kategorieTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kategorieTreeView.Location = new System.Drawing.Point(0, 0);
            this.kategorieTreeView.Name = "kategorieTreeView";
            this.kategorieTreeView.SelectedNode = null;
            this.kategorieTreeView.Size = new System.Drawing.Size(378, 580);
            this.kategorieTreeView.TabIndex = 0;
            this.kategorieTreeView.WithEmptyRoot = false;
            this.kategorieTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.kategorieTreeView_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Szukaj:";
            // 
            // findTextBox
            // 
            this.findTextBox.Location = new System.Drawing.Point(64, 20);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(513, 20);
            this.findTextBox.TabIndex = 1;
            this.findTextBox.TextChanged += new System.EventHandler(this.findTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(595, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Aktywność:";
            // 
            // aktywnoscComboBox
            // 
            this.aktywnoscComboBox.FormattingEnabled = true;
            this.aktywnoscComboBox.Items.AddRange(new object[] {
            "Razem",
            "Aktywne",
            "Nieaktywne"});
            this.aktywnoscComboBox.Location = new System.Drawing.Point(663, 19);
            this.aktywnoscComboBox.Name = "aktywnoscComboBox";
            this.aktywnoscComboBox.Size = new System.Drawing.Size(102, 21);
            this.aktywnoscComboBox.TabIndex = 3;
            this.aktywnoscComboBox.SelectionChangeCommitted += new System.EventHandler(this.aktywnoscComboBox_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(780, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Dostepność:";
            // 
            // dostepnoscComboBox
            // 
            this.dostepnoscComboBox.FormattingEnabled = true;
            this.dostepnoscComboBox.Items.AddRange(new object[] {
            "Razem",
            "Dostepne",
            "Niedostepne"});
            this.dostepnoscComboBox.Location = new System.Drawing.Point(853, 19);
            this.dostepnoscComboBox.Name = "dostepnoscComboBox";
            this.dostepnoscComboBox.Size = new System.Drawing.Size(107, 21);
            this.dostepnoscComboBox.TabIndex = 5;
            this.dostepnoscComboBox.SelectionChangeCommitted += new System.EventHandler(this.dostepnoscComboBox_SelectionChangeCommitted);
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
            this.AtrybutNazwaPelnaColumn.FillWeight = 150F;
            this.AtrybutNazwaPelnaColumn.HeaderText = "Atrybut";
            this.AtrybutNazwaPelnaColumn.Name = "AtrybutNazwaPelnaColumn";
            this.AtrybutNazwaPelnaColumn.ReadOnly = true;
            this.AtrybutNazwaPelnaColumn.Width = 150;
            // 
            // CenaNettoColumn
            // 
            this.CenaNettoColumn.DataPropertyName = "CenaNetto";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.CenaNettoColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.CenaNettoColumn.HeaderText = "Cena";
            this.CenaNettoColumn.Name = "CenaNettoColumn";
            this.CenaNettoColumn.ReadOnly = true;
            // 
            // StawkaVatValueColumn
            // 
            this.StawkaVatValueColumn.DataPropertyName = "StawkaVatValue";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.StawkaVatValueColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.StawkaVatValueColumn.HeaderText = "VAT";
            this.StawkaVatValueColumn.Name = "StawkaVatValueColumn";
            this.StawkaVatValueColumn.ReadOnly = true;
            // 
            // AktywnyColumn
            // 
            this.AktywnyColumn.DataPropertyName = "Aktywny";
            this.AktywnyColumn.HeaderText = "Aktywny";
            this.AktywnyColumn.Name = "AktywnyColumn";
            this.AktywnyColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AktywnyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DostepnyColumn
            // 
            this.DostepnyColumn.DataPropertyName = "Dostepny";
            this.DostepnyColumn.HeaderText = "Dostepny";
            this.DostepnyColumn.Name = "DostepnyColumn";
            // 
            // AvailableMsgColumn
            // 
            this.AvailableMsgColumn.DataPropertyName = "AvailableMsg";
            this.AvailableMsgColumn.FillWeight = 250F;
            this.AvailableMsgColumn.HeaderText = "Wyświetlany tekst";
            this.AvailableMsgColumn.Name = "AvailableMsgColumn";
            this.AvailableMsgColumn.Width = 250;
            // 
            // AvailableDateColumn
            // 
            this.AvailableDateColumn.DataPropertyName = "AvailableDate";
            this.AvailableDateColumn.HeaderText = "Data dostawy";
            this.AvailableDateColumn.Name = "AvailableDateColumn";
            // 
            // MagazynForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(1585, 689);
            this.ConfigFile = "Grids\\magazyn.xml";
            this.DataSource = typeof(Enova.Business.Old.DB.Web.ProduktAtrybut);
            this.Name = "MagazynForm";
            this.RightPanelCollapsed = true;
            this.Text = "Magazyn - Dostepność towarów";
            this.Load += new System.EventHandler(this.MagazynForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MagazynForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MagazynForm_KeyPress);
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

        private Enova.Business.Old.Controls.KategorieTreeView kategorieTreeView;
        private System.Windows.Forms.TextBox findTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox dostepnoscComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox aktywnoscComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NazwaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AtrybutNazwaPelnaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenaNettoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StawkaVatValueColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AktywnyColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DostepnyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AvailableMsgColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AvailableDateColumn;
    }
}
