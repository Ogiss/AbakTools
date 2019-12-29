namespace Enova.Forms.Towary
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.szukajTextBox = new System.Windows.Forms.TextBox();
            this.KodColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NazwaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IlośćColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenaHurtowaNettoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            // FeaturesTreeView
            // 
            this.FeaturesTreeView.Size = new System.Drawing.Size(188, 605);
            this.FeaturesTreeView.TableName = "Towary";
            // 
            // TopSplitContainer
            // 
            // 
            // TopSplitContainer.Panel1
            // 
            this.TopSplitContainer.Panel1.Controls.Add(this.szukajTextBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.label1);
            this.TopSplitContainer.Panel1Collapsed = false;
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.DB.TowarRow);
            this.DataGridBindingSource.Filter = "";
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.Size = new System.Drawing.Size(1351, 605);
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Size = new System.Drawing.Size(1159, 605);
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Size = new System.Drawing.Size(1159, 605);
            // 
            // DataGrid
            // 
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KodColumn,
            this.NazwaColumn,
            this.IlośćColumn,
            this.CenaHurtowaNettoColumn});
            this.DataGrid.Size = new System.Drawing.Size(1159, 605);
            this.DataGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGrid_CellMouseUp);
            this.DataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGrid_DataError);
            this.DataGrid.SelectionChanged += new System.EventHandler(this.DataGrid_SelectionChanged);
            this.DataGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DataGrid_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Szukaj:";
            // 
            // szukajTextBox
            // 
            this.szukajTextBox.Location = new System.Drawing.Point(70, 15);
            this.szukajTextBox.Name = "szukajTextBox";
            this.szukajTextBox.Size = new System.Drawing.Size(439, 20);
            this.szukajTextBox.TabIndex = 1;
            this.szukajTextBox.TextChanged += new System.EventHandler(this.szukajTextBox_TextChanged);
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
            this.NazwaColumn.FillWeight = 400F;
            this.NazwaColumn.HeaderText = "Nazwa";
            this.NazwaColumn.Name = "NazwaColumn";
            this.NazwaColumn.ReadOnly = true;
            this.NazwaColumn.Width = 400;
            // 
            // IlośćColumn
            // 
            this.IlośćColumn.DataPropertyName = "IlośćStr";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IlośćColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.IlośćColumn.HeaderText = "Ilość";
            this.IlośćColumn.Name = "IlośćColumn";
            this.IlośćColumn.ReadOnly = true;
            // 
            // CenaHurtowaNettoColumn
            // 
            this.CenaHurtowaNettoColumn.DataPropertyName = "CenaHurtowaNetto";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.CenaHurtowaNettoColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.CenaHurtowaNettoColumn.HeaderText = "Sprzedaż netto";
            this.CenaHurtowaNettoColumn.Name = "CenaHurtowaNettoColumn";
            this.CenaHurtowaNettoColumn.ReadOnly = true;
            // 
            // TowaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1351, 689);
            this.ConfigFile = "Grids\\towary.xml";
            this.DataSource = typeof(Enova.Business.Old.DB.TowarRow);
            this.Filter = "";
            this.Name = "TowaryForm";
            this.TableName = "Towary";
            this.TopPanelCollapsed = false;
            this.Load += new System.EventHandler(this.TowaryForm_Load);
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

        private System.Windows.Forms.TextBox szukajTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NazwaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IlośćColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenaHurtowaNettoColumn;










    }
}
