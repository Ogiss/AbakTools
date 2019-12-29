namespace AbakTools.Kadry.Forms
{
    partial class EtatyForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.pracownikComboBox = new System.Windows.Forms.ComboBox();
            this.PracownikColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WysokoscEtatuColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WynagrodzenieBruttoCoulumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.TopSplitContainer.Panel1.Controls.Add(this.pracownikComboBox);
            this.TopSplitContainer.Panel1.Controls.Add(this.label1);
            this.TopSplitContainer.Size = new System.Drawing.Size(644, 390);
            this.TopSplitContainer.SplitterDistance = 46;
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Etat);
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.Panel1Collapsed = true;
            this.LeftSplitContainer.Size = new System.Drawing.Size(644, 340);
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Panel2Collapsed = true;
            this.RightSplitContainer.Size = new System.Drawing.Size(644, 340);
            this.RightSplitContainer.SplitterDistance = 1198;
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Panel2Collapsed = true;
            this.BottomSplitContainer.Size = new System.Drawing.Size(644, 340);
            this.BottomSplitContainer.SplitterDistance = 481;
            // 
            // DataGrid
            // 
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PracownikColumn,
            this.DataColumn,
            this.WysokoscEtatuColumn,
            this.WynagrodzenieBruttoCoulumn});
            this.DataGrid.Size = new System.Drawing.Size(644, 340);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pracownik:";
            // 
            // pracownikComboBox
            // 
            this.pracownikComboBox.FormattingEnabled = true;
            this.pracownikComboBox.Items.AddRange(new object[] {
            "(wszyscy)",
            "MG",
            "ŁD",
            "PD",
            "SD"});
            this.pracownikComboBox.Location = new System.Drawing.Point(97, 12);
            this.pracownikComboBox.Name = "pracownikComboBox";
            this.pracownikComboBox.Size = new System.Drawing.Size(121, 21);
            this.pracownikComboBox.TabIndex = 1;
            // 
            // PracownikColumn
            // 
            this.PracownikColumn.DataPropertyName = "Pracownik";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PracownikColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.PracownikColumn.HeaderText = "Pracownik";
            this.PracownikColumn.Name = "PracownikColumn";
            this.PracownikColumn.ReadOnly = true;
            // 
            // DataColumn
            // 
            this.DataColumn.DataPropertyName = "Data";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.DataColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataColumn.HeaderText = "Data";
            this.DataColumn.Name = "DataColumn";
            this.DataColumn.ReadOnly = true;
            // 
            // WysokoscEtatuColumn
            // 
            this.WysokoscEtatuColumn.DataPropertyName = "WysokoscEtatu";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.WysokoscEtatuColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.WysokoscEtatuColumn.HeaderText = "Etat";
            this.WysokoscEtatuColumn.Name = "WysokoscEtatuColumn";
            this.WysokoscEtatuColumn.ReadOnly = true;
            // 
            // WynagrodzenieBruttoCoulumn
            // 
            this.WynagrodzenieBruttoCoulumn.DataPropertyName = "WynagrodzenieBrutto";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.WynagrodzenieBruttoCoulumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.WynagrodzenieBruttoCoulumn.HeaderText = "Wynagrodzenie";
            this.WynagrodzenieBruttoCoulumn.Name = "WynagrodzenieBruttoCoulumn";
            this.WynagrodzenieBruttoCoulumn.ReadOnly = true;
            // 
            // EtatyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(644, 415);
            this.DataSource = typeof(Enova.Business.Old.DB.Web.Etat);
            this.LeftPanelCollapsed = true;
            this.Name = "EtatyForm";
            this.RightPanelCollapsed = true;
            this.Text = "Etaty";
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

        private System.Windows.Forms.ComboBox pracownikComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PracownikColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WysokoscEtatuColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WynagrodzenieBruttoCoulumn;
    }
}
