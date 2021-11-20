namespace AbakTools.Towary.Forms
{
    partial class TowaryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.kategorieTreeView = new Enova.Business.Old.Controls.KategorieTreeView();
            this.nieaktywneCheckBox = new System.Windows.Forms.CheckBox();
            this.towaryEnovaCheckBox = new System.Windows.Forms.CheckBox();
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dostawcaComboBox = new System.Windows.Forms.ComboBox();
            this.dostawcaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Kod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Aktywny = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Dostepny = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Gotowy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UserState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Synchronizacja = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.dostawcaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // TopSplitContainer
            // 
            this.TopSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            // 
            // TopSplitContainer.Panel1
            // 
            this.TopSplitContainer.Panel1.Controls.Add(this.dostawcaComboBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.label2);
            this.TopSplitContainer.Panel1.Controls.Add(this.findTextBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.label1);
            this.TopSplitContainer.Panel1.Controls.Add(this.towaryEnovaCheckBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.nieaktywneCheckBox);
            this.TopSplitContainer.SplitterDistance = 48;
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Produkt);
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            // 
            // LeftSplitContainer.Panel1
            // 
            this.LeftSplitContainer.Panel1.Controls.Add(this.kategorieTreeView);
            this.LeftSplitContainer.Size = new System.Drawing.Size(1351, 612);
            this.LeftSplitContainer.SplitterDistance = 212;
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.RightSplitContainer.Panel2Collapsed = true;
            this.RightSplitContainer.Size = new System.Drawing.Size(1135, 612);
            this.RightSplitContainer.SplitterDistance = 1007;
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.BottomSplitContainer.Panel2Collapsed = true;
            this.BottomSplitContainer.Size = new System.Drawing.Size(1135, 612);
            this.BottomSplitContainer.SplitterDistance = 549;
            // 
            // DataGrid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.DataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGrid.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.DataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kod,
            this.Nazwa,
            this.Cena,
            this.Aktywny,
            this.Dostepny,
            this.Gotowy,
            this.UserState,
            this.Synchronizacja});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGrid.DefaultCellStyle = dataGridViewCellStyle4;
            this.DataGrid.EnableHeadersVisualStyles = false;
            this.DataGrid.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DataGrid.RowHeadersVisible = true;
            this.DataGrid.Size = new System.Drawing.Size(1135, 612);
            this.DataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellContentClick);
            this.DataGrid.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DataGrid_RowPrePaint);
            this.DataGrid.SelectionChanged += new System.EventHandler(this.DataGrid_SelectionChanged);
            this.DataGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DataGrid_KeyPress);
            // 
            // kategorieTreeView
            // 
            this.kategorieTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kategorieTreeView.Location = new System.Drawing.Point(0, 0);
            this.kategorieTreeView.Name = "kategorieTreeView";
            this.kategorieTreeView.SelectedNode = null;
            this.kategorieTreeView.Size = new System.Drawing.Size(212, 612);
            this.kategorieTreeView.TabIndex = 0;
            this.kategorieTreeView.WithEmptyRoot = true;
            this.kategorieTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.kategorieTreeView_AfterSelect);
            // 
            // nieaktywneCheckBox
            // 
            this.nieaktywneCheckBox.AutoSize = true;
            this.nieaktywneCheckBox.Checked = true;
            this.nieaktywneCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nieaktywneCheckBox.Location = new System.Drawing.Point(722, 17);
            this.nieaktywneCheckBox.Name = "nieaktywneCheckBox";
            this.nieaktywneCheckBox.Size = new System.Drawing.Size(82, 17);
            this.nieaktywneCheckBox.TabIndex = 2;
            this.nieaktywneCheckBox.Text = "Nieaktywne";
            this.nieaktywneCheckBox.UseVisualStyleBackColor = true;
            this.nieaktywneCheckBox.CheckedChanged += new System.EventHandler(this.nieaktywneCheckBox_CheckedChanged);
            // 
            // towaryEnovaCheckBox
            // 
            this.towaryEnovaCheckBox.AutoSize = true;
            this.towaryEnovaCheckBox.Location = new System.Drawing.Point(810, 17);
            this.towaryEnovaCheckBox.Name = "towaryEnovaCheckBox";
            this.towaryEnovaCheckBox.Size = new System.Drawing.Size(95, 17);
            this.towaryEnovaCheckBox.TabIndex = 3;
            this.towaryEnovaCheckBox.Text = "Towary Enova";
            this.towaryEnovaCheckBox.UseVisualStyleBackColor = true;
            this.towaryEnovaCheckBox.Visible = false;
            this.towaryEnovaCheckBox.CheckedChanged += new System.EventHandler(this.towaryEnovaCheckBox_CheckedChanged);
            // 
            // findTextBox
            // 
            this.findTextBox.Location = new System.Drawing.Point(58, 15);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(376, 20);
            this.findTextBox.TabIndex = 5;
            this.findTextBox.TextChanged += new System.EventHandler(this.findTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Szukaj:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Dostawca:";
            // 
            // dostawcaComboBox
            // 
            this.dostawcaComboBox.DataSource = this.dostawcaBindingSource;
            this.dostawcaComboBox.DisplayMember = "Nazwa";
            this.dostawcaComboBox.FormattingEnabled = true;
            this.dostawcaComboBox.Location = new System.Drawing.Point(506, 15);
            this.dostawcaComboBox.Name = "dostawcaComboBox";
            this.dostawcaComboBox.Size = new System.Drawing.Size(198, 21);
            this.dostawcaComboBox.TabIndex = 7;
            this.dostawcaComboBox.ValueMember = "ID";
            this.dostawcaComboBox.SelectionChangeCommitted += new System.EventHandler(this.dostawcaComboBox_SelectionChangeCommitted);
            // 
            // dostawcaBindingSource
            // 
            this.dostawcaBindingSource.DataSource = typeof(AbakTools.Towary.Forms.DostawcaInfo);
            // 
            // Kod
            // 
            this.Kod.DataPropertyName = "Kod";
            this.Kod.FillWeight = 200F;
            this.Kod.HeaderText = "Kod";
            this.Kod.Name = "Kod";
            this.Kod.ReadOnly = true;
            this.Kod.Width = 200;
            // 
            // Nazwa
            // 
            this.Nazwa.DataPropertyName = "Nazwa";
            this.Nazwa.FillWeight = 400F;
            this.Nazwa.HeaderText = "Nazwa";
            this.Nazwa.Name = "Nazwa";
            this.Nazwa.ReadOnly = true;
            this.Nazwa.Width = 400;
            // 
            // Cena
            // 
            this.Cena.DataPropertyName = "Cena";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.Cena.DefaultCellStyle = dataGridViewCellStyle3;
            this.Cena.HeaderText = "Cena";
            this.Cena.Name = "Cena";
            this.Cena.ReadOnly = true;
            // 
            // Aktywny
            // 
            this.Aktywny.DataPropertyName = "IsActive";
            this.Aktywny.HeaderText = "Aktywny";
            this.Aktywny.Name = "Aktywny";
            this.Aktywny.ReadOnly = true;
            this.Aktywny.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Aktywny.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Dostepny
            // 
            this.Dostepny.DataPropertyName = "Dostepny";
            this.Dostepny.HeaderText = "Dostepny";
            this.Dostepny.Name = "Dostepny";
            this.Dostepny.ReadOnly = true;
            // 
            // Gotowy
            // 
            this.Gotowy.DataPropertyName = "Gotowy";
            this.Gotowy.HeaderText = "Gotowy";
            this.Gotowy.Name = "Gotowy";
            this.Gotowy.ReadOnly = true;
            this.Gotowy.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Gotowy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // UserState
            // 
            this.UserState.DataPropertyName = "UserState";
            this.UserState.FillWeight = 250F;
            this.UserState.HeaderText = "Status";
            this.UserState.Name = "UserState";
            this.UserState.ReadOnly = true;
            this.UserState.Width = 250;
            // 
            // Synchronizacja
            // 
            this.Synchronizacja.DataPropertyName = "SynchronizacjaStr";
            this.Synchronizacja.HeaderText = "Synchronizacja";
            this.Synchronizacja.Name = "Synchronizacja";
            this.Synchronizacja.ReadOnly = true;
            // 
            // TowaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(1351, 689);
            this.DataSource = typeof(Enova.Business.Old.DB.Web.Produkt);
            this.Name = "TowaryForm";
            this.RightPanelCollapsed = true;
            this.Text = "Towary WEB";
            this.Load += new System.EventHandler(this.TowaryForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TowaryForm_KeyDown);
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
            ((System.ComponentModel.ISupportInitialize)(this.dostawcaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Enova.Business.Old.Controls.KategorieTreeView kategorieTreeView;
        private System.Windows.Forms.CheckBox towaryEnovaCheckBox;
        private System.Windows.Forms.CheckBox nieaktywneCheckBox;
        private System.Windows.Forms.TextBox findTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox dostawcaComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource dostawcaBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cena;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Aktywny;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Dostepny;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Gotowy;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserState;
        private System.Windows.Forms.DataGridViewTextBoxColumn Synchronizacja;
    }
}
