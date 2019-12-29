namespace AbakTools.Core.Controls
{
    partial class HistoriaDokumentuListForm
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
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView = new AbakTools.Forms.Controls.GridView();
            this.DataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusDokumentuColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperatorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpisColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.HistoriaDokumentu);
            // 
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.AutoGenerateColumns = false;
            this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataColumn,
            this.StatusDokumentuColumn,
            this.OperatorColumn,
            this.OpisColumn});
            this.gridView.DataContext = null;
            this.gridView.DataSource = this.bindingSource;
            this.gridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridView.Location = new System.Drawing.Point(0, 0);
            this.gridView.Name = "gridView";
            this.gridView.RowHeadersVisible = false;
            this.gridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridView.Size = new System.Drawing.Size(862, 278);
            this.gridView.TabIndex = 0;
            this.gridView.VirtualMode = true;
            // 
            // DataColumn
            // 
            this.DataColumn.DataPropertyName = "Data";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DataColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataColumn.HeaderText = "Data";
            this.DataColumn.Name = "DataColumn";
            this.DataColumn.ReadOnly = true;
            // 
            // StatusDokumentuColumn
            // 
            this.StatusDokumentuColumn.DataPropertyName = "StatusDokumentu";
            this.StatusDokumentuColumn.FillWeight = 200F;
            this.StatusDokumentuColumn.HeaderText = "Status";
            this.StatusDokumentuColumn.Name = "StatusDokumentuColumn";
            this.StatusDokumentuColumn.ReadOnly = true;
            this.StatusDokumentuColumn.Width = 200;
            // 
            // OperatorColumn
            // 
            this.OperatorColumn.DataPropertyName = "Operator";
            this.OperatorColumn.HeaderText = "Operator";
            this.OperatorColumn.Name = "OperatorColumn";
            this.OperatorColumn.ReadOnly = true;
            // 
            // OpisColumn
            // 
            this.OpisColumn.DataPropertyName = "Opis";
            this.OpisColumn.FillWeight = 400F;
            this.OpisColumn.HeaderText = "Opis";
            this.OpisColumn.Name = "OpisColumn";
            this.OpisColumn.ReadOnly = true;
            this.OpisColumn.Width = 400;
            // 
            // HistoriaDokumentuListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 278);
            this.Controls.Add(this.gridView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HistoriaDokumentuListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Historia dokumenu";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource;
        private AbakTools.Forms.Controls.GridView gridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusDokumentuColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperatorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpisColumn;
    }
}