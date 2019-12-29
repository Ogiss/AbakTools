namespace AbakTools.Zwroty.Forms
{
    partial class ZwrotyForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.rejestrujButton = new System.Windows.Forms.Button();
            this.analizujButton = new System.Windows.Forms.Button();
            this.korygujButton = new System.Windows.Forms.Button();
            this.połaczButton = new System.Windows.Forms.Button();
            this.towaryDoSkorygowaniaButton = new System.Windows.Forms.Button();
            this.okresSpan = new Enova.Business.Old.Controls.DateTimeSpanControl();
            this.label2 = new System.Windows.Forms.Label();
            this.sezonComboBox = new System.Windows.Forms.ComboBox();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataDodania = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataModyfikacji = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Przedstawiciel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KontrahentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WartoscNettoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IloscPaczek = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SezonAll = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OstStatusZwrotu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Opis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kontrahentSelectControl = new Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect();
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
            this.TopSplitContainer.Panel1.Controls.Add(this.kontrahentSelectControl);
            this.TopSplitContainer.Panel1.Controls.Add(this.sezonComboBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.label2);
            this.TopSplitContainer.Panel1.Controls.Add(this.okresSpan);
            this.TopSplitContainer.Panel1.Controls.Add(this.towaryDoSkorygowaniaButton);
            this.TopSplitContainer.Panel1.Controls.Add(this.połaczButton);
            this.TopSplitContainer.Panel1.Controls.Add(this.korygujButton);
            this.TopSplitContainer.Panel1.Controls.Add(this.analizujButton);
            this.TopSplitContainer.Panel1.Controls.Add(this.rejestrujButton);
            this.TopSplitContainer.Panel1.Controls.Add(this.label1);
            this.TopSplitContainer.SplitterDistance = 67;
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Zwrot);
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.Panel1Collapsed = true;
            this.LeftSplitContainer.Size = new System.Drawing.Size(1351, 593);
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Panel2Collapsed = true;
            this.RightSplitContainer.Size = new System.Drawing.Size(1351, 593);
            this.RightSplitContainer.SplitterDistance = 1198;
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Panel2Collapsed = true;
            this.BottomSplitContainer.Size = new System.Drawing.Size(1351, 593);
            this.BottomSplitContainer.SplitterDistance = 504;
            // 
            // DataGrid
            // 
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDColumn,
            this.DataDodania,
            this.DataModyfikacji,
            this.Przedstawiciel,
            this.KontrahentColumn,
            this.WartoscNettoColumn,
            this.IloscPaczek,
            this.SezonAll,
            this.OstStatusZwrotu,
            this.Opis});
            this.DataGrid.RowHeadersVisible = true;
            this.DataGrid.Size = new System.Drawing.Size(1351, 593);
            this.DataGrid.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DataGrid_RowPostPaint);
            this.DataGrid.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DataGrid_RowPrePaint);
            this.DataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGrid_KeyDown);
            this.DataGrid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.DataGrid_PreviewKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Okres:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // rejestrujButton
            // 
            this.rejestrujButton.Location = new System.Drawing.Point(69, 38);
            this.rejestrujButton.Name = "rejestrujButton";
            this.rejestrujButton.Size = new System.Drawing.Size(141, 23);
            this.rejestrujButton.TabIndex = 3;
            this.rejestrujButton.Text = "Rejestruj zwrot";
            this.rejestrujButton.UseVisualStyleBackColor = true;
            this.rejestrujButton.Click += new System.EventHandler(this.rejestrujButton_Click);
            // 
            // analizujButton
            // 
            this.analizujButton.Location = new System.Drawing.Point(250, 38);
            this.analizujButton.Name = "analizujButton";
            this.analizujButton.Size = new System.Drawing.Size(130, 23);
            this.analizujButton.TabIndex = 4;
            this.analizujButton.Text = "Analizuj";
            this.analizujButton.UseVisualStyleBackColor = true;
            this.analizujButton.Click += new System.EventHandler(this.analizujButton_Click);
            // 
            // korygujButton
            // 
            this.korygujButton.Location = new System.Drawing.Point(422, 38);
            this.korygujButton.Name = "korygujButton";
            this.korygujButton.Size = new System.Drawing.Size(140, 23);
            this.korygujButton.TabIndex = 5;
            this.korygujButton.Text = "Wystaw korekty";
            this.korygujButton.UseVisualStyleBackColor = true;
            this.korygujButton.Click += new System.EventHandler(this.korygujButton_Click);
            // 
            // połaczButton
            // 
            this.połaczButton.Location = new System.Drawing.Point(640, 38);
            this.połaczButton.Name = "połaczButton";
            this.połaczButton.Size = new System.Drawing.Size(141, 23);
            this.połaczButton.TabIndex = 6;
            this.połaczButton.Text = "Połacz zwroty";
            this.połaczButton.UseVisualStyleBackColor = true;
            this.połaczButton.Click += new System.EventHandler(this.połaczButton_Click);
            // 
            // towaryDoSkorygowaniaButton
            // 
            this.towaryDoSkorygowaniaButton.Location = new System.Drawing.Point(813, 38);
            this.towaryDoSkorygowaniaButton.Name = "towaryDoSkorygowaniaButton";
            this.towaryDoSkorygowaniaButton.Size = new System.Drawing.Size(154, 23);
            this.towaryDoSkorygowaniaButton.TabIndex = 7;
            this.towaryDoSkorygowaniaButton.Text = "Towary do skorygowania";
            this.towaryDoSkorygowaniaButton.UseVisualStyleBackColor = true;
            this.towaryDoSkorygowaniaButton.Click += new System.EventHandler(this.towaryDoSkorygowaniaButton_Click);
            // 
            // okresSpan
            // 
            this.okresSpan.DateFrom = new System.DateTime(2013, 12, 1, 0, 0, 0, 0);
            this.okresSpan.DateTo = new System.DateTime(2013, 12, 31, 23, 59, 59, 0);
            this.okresSpan.Location = new System.Drawing.Point(72, 11);
            this.okresSpan.Name = "okresSpan";
            this.okresSpan.Size = new System.Drawing.Size(171, 21);
            this.okresSpan.TabIndex = 8;
            this.okresSpan.Changed += new System.EventHandler(this.okresSpan_Changed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1000, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Sezon:";
            // 
            // sezonComboBox
            // 
            this.sezonComboBox.FormattingEnabled = true;
            this.sezonComboBox.Location = new System.Drawing.Point(1046, 10);
            this.sezonComboBox.Name = "sezonComboBox";
            this.sezonComboBox.Size = new System.Drawing.Size(166, 21);
            this.sezonComboBox.TabIndex = 11;
            this.sezonComboBox.SelectionChangeCommitted += new System.EventHandler(this.sezonComboBox_SelectionChangeCommitted);
            // 
            // IDColumn
            // 
            this.IDColumn.DataPropertyName = "ID";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.IDColumn.FillWeight = 60F;
            this.IDColumn.HeaderText = "ID";
            this.IDColumn.Name = "IDColumn";
            this.IDColumn.ReadOnly = true;
            this.IDColumn.Width = 60;
            // 
            // DataDodania
            // 
            this.DataDodania.DataPropertyName = "DataDodania";
            this.DataDodania.HeaderText = "Data dodania";
            this.DataDodania.Name = "DataDodania";
            this.DataDodania.ReadOnly = true;
            // 
            // DataModyfikacji
            // 
            this.DataModyfikacji.DataPropertyName = "DataModyfikacji";
            this.DataModyfikacji.HeaderText = "Data modyfikacji";
            this.DataModyfikacji.Name = "DataModyfikacji";
            this.DataModyfikacji.ReadOnly = true;
            // 
            // Przedstawiciel
            // 
            this.Przedstawiciel.DataPropertyName = "Przedstawiciel";
            this.Przedstawiciel.FillWeight = 40F;
            this.Przedstawiciel.HeaderText = "PR";
            this.Przedstawiciel.Name = "Przedstawiciel";
            this.Przedstawiciel.ReadOnly = true;
            this.Przedstawiciel.Width = 40;
            // 
            // KontrahentColumn
            // 
            this.KontrahentColumn.DataPropertyName = "Kontrahent";
            this.KontrahentColumn.FillWeight = 500F;
            this.KontrahentColumn.HeaderText = "Kontrahent";
            this.KontrahentColumn.Name = "KontrahentColumn";
            this.KontrahentColumn.ReadOnly = true;
            this.KontrahentColumn.Width = 500;
            // 
            // WartoscNettoColumn
            // 
            this.WartoscNettoColumn.DataPropertyName = "WartoscNetto";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.WartoscNettoColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.WartoscNettoColumn.HeaderText = "Wartość netto";
            this.WartoscNettoColumn.Name = "WartoscNettoColumn";
            this.WartoscNettoColumn.ReadOnly = true;
            // 
            // IloscPaczek
            // 
            this.IloscPaczek.DataPropertyName = "IloscPaczek";
            this.IloscPaczek.FillWeight = 45F;
            this.IloscPaczek.HeaderText = "Paczki";
            this.IloscPaczek.Name = "IloscPaczek";
            this.IloscPaczek.ReadOnly = true;
            this.IloscPaczek.Width = 45;
            // 
            // SezonAll
            // 
            this.SezonAll.DataPropertyName = "SezonAll";
            this.SezonAll.FillWeight = 150F;
            this.SezonAll.HeaderText = "Sezon";
            this.SezonAll.Name = "SezonAll";
            this.SezonAll.ReadOnly = true;
            this.SezonAll.Width = 150;
            // 
            // OstStatusZwrotu
            // 
            this.OstStatusZwrotu.DataPropertyName = "OstatniStatusStr";
            this.OstStatusZwrotu.FillWeight = 150F;
            this.OstStatusZwrotu.HeaderText = "Status";
            this.OstStatusZwrotu.Name = "OstStatusZwrotu";
            this.OstStatusZwrotu.ReadOnly = true;
            this.OstStatusZwrotu.Width = 150;
            // 
            // Opis
            // 
            this.Opis.DataPropertyName = "OpisLine";
            this.Opis.FillWeight = 300F;
            this.Opis.HeaderText = "Opis";
            this.Opis.Name = "Opis";
            this.Opis.ReadOnly = true;
            this.Opis.Width = 300;
            // 
            // kontrahentSelectControl
            // 
            this.kontrahentSelectControl.DataContext = null;
            this.kontrahentSelectControl.Location = new System.Drawing.Point(250, 9);
            this.kontrahentSelectControl.Name = "kontrahentSelectControl";
            this.kontrahentSelectControl.ReadOnly = false;
            this.kontrahentSelectControl.Size = new System.Drawing.Size(735, 22);
            this.kontrahentSelectControl.TabIndex = 12;
            this.kontrahentSelectControl.Changed += new System.EventHandler(this.kontrahentSelectControl_Changed);
            this.kontrahentSelectControl.KontrahentChanged += new System.EventHandler(this.kontrahentSelectControl_KontrahentChanged);
            // 
            // ZwrotyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(1351, 689);
            this.DataSource = typeof(Enova.Business.Old.DB.Web.Zwrot);
            this.LeftPanelCollapsed = true;
            this.Name = "ZwrotyForm";
            this.RightPanelCollapsed = true;
            this.Text = "Zwroty";
            this.PrintItemClick += new System.EventHandler(this.ZwrotyForm_PrintItemClick);
            this.Load += new System.EventHandler(this.ZwrotyForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ZwrotyForm_KeyDown);
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

        
        private System.Windows.Forms.Label label1;
      
        private System.Windows.Forms.Button rejestrujButton;
        private System.Windows.Forms.Button analizujButton;
        private System.Windows.Forms.Button korygujButton;
        private System.Windows.Forms.Button połaczButton;
        private System.Windows.Forms.Button towaryDoSkorygowaniaButton;
        private Enova.Business.Old.Controls.DateTimeSpanControl okresSpan;
        private System.Windows.Forms.ComboBox sezonComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataDodania;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataModyfikacji;
        private System.Windows.Forms.DataGridViewTextBoxColumn Przedstawiciel;
        private System.Windows.Forms.DataGridViewTextBoxColumn KontrahentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WartoscNettoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IloscPaczek;
        private System.Windows.Forms.DataGridViewTextBoxColumn SezonAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn OstStatusZwrotu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Opis;
        private Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect kontrahentSelectControl;

    }
}
