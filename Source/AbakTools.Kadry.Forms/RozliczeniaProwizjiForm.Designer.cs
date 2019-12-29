namespace AbakTools.Kadry.Forms
{
    partial class RozliczeniaProwizjiForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.kodTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.kpCheckBox = new System.Windows.Forms.CheckBox();
            this.kwCheckBox = new System.Windows.Forms.CheckBox();
            this.kzTheckBox = new System.Windows.Forms.CheckBox();
            this.krCheckBox = new System.Windows.Forms.CheckBox();
            this.prCheckBox = new System.Windows.Forms.CheckBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.kodPodmiotuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KwotaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.okresDateSpan = new Enova.Business.Old.Controls.DateTimeSpanControl();
            this.sumaTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kod:";
            // 
            // kodTextBox
            // 
            this.kodTextBox.BackColor = System.Drawing.Color.White;
            this.kodTextBox.Location = new System.Drawing.Point(47, 6);
            this.kodTextBox.Name = "kodTextBox";
            this.kodTextBox.ReadOnly = true;
            this.kodTextBox.Size = new System.Drawing.Size(100, 20);
            this.kodTextBox.TabIndex = 1;
            this.kodTextBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Okres:";
            // 
            // kpCheckBox
            // 
            this.kpCheckBox.AutoSize = true;
            this.kpCheckBox.Location = new System.Drawing.Point(423, 8);
            this.kpCheckBox.Name = "kpCheckBox";
            this.kpCheckBox.Size = new System.Drawing.Size(40, 17);
            this.kpCheckBox.TabIndex = 4;
            this.kpCheckBox.Text = "KP";
            this.kpCheckBox.UseVisualStyleBackColor = true;
            this.kpCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // kwCheckBox
            // 
            this.kwCheckBox.AutoSize = true;
            this.kwCheckBox.Location = new System.Drawing.Point(469, 8);
            this.kwCheckBox.Name = "kwCheckBox";
            this.kwCheckBox.Size = new System.Drawing.Size(44, 17);
            this.kwCheckBox.TabIndex = 5;
            this.kwCheckBox.Text = "KW";
            this.kwCheckBox.UseVisualStyleBackColor = true;
            this.kwCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // kzTheckBox
            // 
            this.kzTheckBox.AutoSize = true;
            this.kzTheckBox.Location = new System.Drawing.Point(519, 8);
            this.kzTheckBox.Name = "kzTheckBox";
            this.kzTheckBox.Size = new System.Drawing.Size(40, 17);
            this.kzTheckBox.TabIndex = 6;
            this.kzTheckBox.Text = "KZ";
            this.kzTheckBox.UseVisualStyleBackColor = true;
            this.kzTheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // krCheckBox
            // 
            this.krCheckBox.AutoSize = true;
            this.krCheckBox.Location = new System.Drawing.Point(565, 8);
            this.krCheckBox.Name = "krCheckBox";
            this.krCheckBox.Size = new System.Drawing.Size(41, 17);
            this.krCheckBox.TabIndex = 7;
            this.krCheckBox.Text = "KR";
            this.krCheckBox.UseVisualStyleBackColor = true;
            this.krCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // prCheckBox
            // 
            this.prCheckBox.AutoSize = true;
            this.prCheckBox.Checked = true;
            this.prCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prCheckBox.Location = new System.Drawing.Point(612, 8);
            this.prCheckBox.Name = "prCheckBox";
            this.prCheckBox.Size = new System.Drawing.Size(41, 17);
            this.prCheckBox.TabIndex = 8;
            this.prCheckBox.Text = "PR";
            this.prCheckBox.UseVisualStyleBackColor = true;
            this.prCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.AutoGenerateColumns = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kodPodmiotuDataGridViewTextBoxColumn,
            this.dataDataGridViewTextBoxColumn,
            this.numerDataGridViewTextBoxColumn,
            this.KwotaColumn,
            this.opisDataGridViewTextBoxColumn});
            this.dataGrid.DataSource = this.bindingSource;
            this.dataGrid.Location = new System.Drawing.Point(12, 33);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.ShowEditingIcon = false;
            this.dataGrid.ShowRowErrors = false;
            this.dataGrid.Size = new System.Drawing.Size(775, 355);
            this.dataGrid.TabIndex = 9;
            // 
            // kodPodmiotuDataGridViewTextBoxColumn
            // 
            this.kodPodmiotuDataGridViewTextBoxColumn.DataPropertyName = "KodPodmiotu";
            this.kodPodmiotuDataGridViewTextBoxColumn.HeaderText = "Kod";
            this.kodPodmiotuDataGridViewTextBoxColumn.Name = "kodPodmiotuDataGridViewTextBoxColumn";
            // 
            // dataDataGridViewTextBoxColumn
            // 
            this.dataDataGridViewTextBoxColumn.DataPropertyName = "Data";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataDataGridViewTextBoxColumn.HeaderText = "Data";
            this.dataDataGridViewTextBoxColumn.Name = "dataDataGridViewTextBoxColumn";
            // 
            // numerDataGridViewTextBoxColumn
            // 
            this.numerDataGridViewTextBoxColumn.DataPropertyName = "Numer";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.numerDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.numerDataGridViewTextBoxColumn.HeaderText = "Numer";
            this.numerDataGridViewTextBoxColumn.Name = "numerDataGridViewTextBoxColumn";
            // 
            // KwotaColumn
            // 
            this.KwotaColumn.DataPropertyName = "Kwota";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.KwotaColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.KwotaColumn.HeaderText = "Kwota";
            this.KwotaColumn.Name = "KwotaColumn";
            this.KwotaColumn.ReadOnly = true;
            // 
            // opisDataGridViewTextBoxColumn
            // 
            this.opisDataGridViewTextBoxColumn.DataPropertyName = "Opis";
            this.opisDataGridViewTextBoxColumn.FillWeight = 400F;
            this.opisDataGridViewTextBoxColumn.HeaderText = "Opis";
            this.opisDataGridViewTextBoxColumn.Name = "opisDataGridViewTextBoxColumn";
            this.opisDataGridViewTextBoxColumn.Width = 400;
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(ZaplatyView);
            // 
            // okresDateSpan
            // 
            this.okresDateSpan.DateFrom = new System.DateTime(2012, 3, 1, 0, 0, 0, 0);
            this.okresDateSpan.DateTo = new System.DateTime(2012, 3, 31, 23, 59, 59, 0);
            this.okresDateSpan.Location = new System.Drawing.Point(216, 6);
            this.okresDateSpan.Name = "okresDateSpan";
            this.okresDateSpan.Size = new System.Drawing.Size(182, 21);
            this.okresDateSpan.TabIndex = 3;
            this.okresDateSpan.Changed += new System.EventHandler(this.okresDateSpan_Changed);
            // 
            // sumaTextBox
            // 
            this.sumaTextBox.Location = new System.Drawing.Point(325, 394);
            this.sumaTextBox.Name = "sumaTextBox";
            this.sumaTextBox.ReadOnly = true;
            this.sumaTextBox.Size = new System.Drawing.Size(100, 20);
            this.sumaTextBox.TabIndex = 10;
            this.sumaTextBox.TabStop = false;
            this.sumaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(276, 397);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Razem:";
            // 
            // RozliczeniaProwizjiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 428);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sumaTextBox);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.prCheckBox);
            this.Controls.Add(this.krCheckBox);
            this.Controls.Add(this.kzTheckBox);
            this.Controls.Add(this.kwCheckBox);
            this.Controls.Add(this.kpCheckBox);
            this.Controls.Add(this.okresDateSpan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.kodTextBox);
            this.Controls.Add(this.label1);
            this.Name = "RozliczeniaProwizjiForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rozliczenia pracownika";
            this.Load += new System.EventHandler(this.RozliczeniaProwizjiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox kodTextBox;
        private System.Windows.Forms.Label label2;
        private Enova.Business.Old.Controls.DateTimeSpanControl okresDateSpan;
        private System.Windows.Forms.CheckBox kpCheckBox;
        private System.Windows.Forms.CheckBox kwCheckBox;
        private System.Windows.Forms.CheckBox kzTheckBox;
        private System.Windows.Forms.CheckBox krCheckBox;
        private System.Windows.Forms.CheckBox prCheckBox;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodPodmiotuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn KwotaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opisDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox sumaTextBox;
        private System.Windows.Forms.Label label3;
    }
}