namespace AbakTools.Zwroty.Forms
{
    partial class WyborTowaruForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.towaryGrid = new AbakTools.Handel.Forms.Zwroty.TowaryGrid();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NazwaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenaHurtowaNettoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.towaryGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(Enova.Business.Old.DB.Towar);
            // 
            // towaryGrid
            // 
            this.towaryGrid.AllowUserToAddRows = false;
            this.towaryGrid.AllowUserToDeleteRows = false;
            this.towaryGrid.AutoGenerateColumns = false;
            this.towaryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.towaryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KodColumn,
            this.NazwaColumn,
            this.CenaHurtowaNettoColumn});
            this.towaryGrid.DataSource = this.bindingSource;
            this.towaryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.towaryGrid.Location = new System.Drawing.Point(0, 0);
            this.towaryGrid.MultiSelect = false;
            this.towaryGrid.Name = "towaryGrid";
            this.towaryGrid.RowHeadersVisible = false;
            this.towaryGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.towaryGrid.Size = new System.Drawing.Size(765, 274);
            this.towaryGrid.TabIndex = 0;
            this.towaryGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.towaryGrid_CellDoubleClick);
            this.towaryGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.towaryGrid_KeyDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Kod";
            this.dataGridViewTextBoxColumn1.FillWeight = 200F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Kod";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Nazwa";
            this.dataGridViewTextBoxColumn2.FillWeight = 400F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Nazwa";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 400;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "CenaHurtowaNetto";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn3.HeaderText = "Cena";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
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
            // CenaHurtowaNettoColumn
            // 
            this.CenaHurtowaNettoColumn.DataPropertyName = "CenaHurtowaNetto";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CenaHurtowaNettoColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.CenaHurtowaNettoColumn.HeaderText = "Cena";
            this.CenaHurtowaNettoColumn.Name = "CenaHurtowaNettoColumn";
            this.CenaHurtowaNettoColumn.ReadOnly = true;
            // 
            // WyborTowaruForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 274);
            this.Controls.Add(this.towaryGrid);
            this.Name = "WyborTowaruForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wybór towaru";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.towaryGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private Handel.Forms.Zwroty.TowaryGrid towaryGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NazwaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenaHurtowaNettoColumn;
    }
}