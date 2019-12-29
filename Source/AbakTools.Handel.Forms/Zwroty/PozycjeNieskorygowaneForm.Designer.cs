namespace AbakTools.Zwroty.Forms
{
    partial class PozycjeNieskorygowaneForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.nrZrotuTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.kontrahentTextBox = new System.Windows.Forms.TextBox();
            this.generujZwrotButton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.towarNazwaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iloscPozostaloDoSkorygowaniaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cenaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zwrot:";
            // 
            // nrZrotuTextBox
            // 
            this.nrZrotuTextBox.Location = new System.Drawing.Point(55, 6);
            this.nrZrotuTextBox.Name = "nrZrotuTextBox";
            this.nrZrotuTextBox.ReadOnly = true;
            this.nrZrotuTextBox.Size = new System.Drawing.Size(100, 20);
            this.nrZrotuTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kontrahent:";
            // 
            // kontrahentTextBox
            // 
            this.kontrahentTextBox.Location = new System.Drawing.Point(243, 6);
            this.kontrahentTextBox.Name = "kontrahentTextBox";
            this.kontrahentTextBox.Size = new System.Drawing.Size(342, 20);
            this.kontrahentTextBox.TabIndex = 3;
            // 
            // generujZwrotButton
            // 
            this.generujZwrotButton.Location = new System.Drawing.Point(607, 4);
            this.generujZwrotButton.Name = "generujZwrotButton";
            this.generujZwrotButton.Size = new System.Drawing.Size(96, 23);
            this.generujZwrotButton.TabIndex = 4;
            this.generujZwrotButton.Text = "Generuj zwrot";
            this.generujZwrotButton.UseVisualStyleBackColor = true;
            this.generujZwrotButton.Click += new System.EventHandler(this.generujZwrotButton_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.identDataGridViewTextBoxColumn,
            this.towarNazwaDataGridViewTextBoxColumn,
            this.iloscPozostaloDoSkorygowaniaDataGridViewTextBoxColumn,
            this.cenaDataGridViewTextBoxColumn,
            this.opisDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.bindingSource;
            this.dataGridView.Location = new System.Drawing.Point(12, 32);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(865, 440);
            this.dataGridView.TabIndex = 5;
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.PozycjaZwrotu);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Ident";
            this.dataGridViewTextBoxColumn1.HeaderText = "Lp";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TowarNazwa";
            this.dataGridViewTextBoxColumn2.FillWeight = 300F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Towar";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 300;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "IloscPozostaloDoSkorygowania";
            this.dataGridViewTextBoxColumn3.HeaderText = "Pozostało";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Cena";
            this.dataGridViewTextBoxColumn4.HeaderText = "Cena";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Opis";
            this.dataGridViewTextBoxColumn5.FillWeight = 200F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Opis";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // identDataGridViewTextBoxColumn
            // 
            this.identDataGridViewTextBoxColumn.DataPropertyName = "Ident";
            this.identDataGridViewTextBoxColumn.FillWeight = 30F;
            this.identDataGridViewTextBoxColumn.HeaderText = "Lp";
            this.identDataGridViewTextBoxColumn.Name = "identDataGridViewTextBoxColumn";
            this.identDataGridViewTextBoxColumn.ReadOnly = true;
            this.identDataGridViewTextBoxColumn.Width = 30;
            // 
            // towarNazwaDataGridViewTextBoxColumn
            // 
            this.towarNazwaDataGridViewTextBoxColumn.DataPropertyName = "TowarNazwa";
            this.towarNazwaDataGridViewTextBoxColumn.FillWeight = 300F;
            this.towarNazwaDataGridViewTextBoxColumn.HeaderText = "Towar";
            this.towarNazwaDataGridViewTextBoxColumn.Name = "towarNazwaDataGridViewTextBoxColumn";
            this.towarNazwaDataGridViewTextBoxColumn.ReadOnly = true;
            this.towarNazwaDataGridViewTextBoxColumn.Width = 300;
            // 
            // iloscPozostaloDoSkorygowaniaDataGridViewTextBoxColumn
            // 
            this.iloscPozostaloDoSkorygowaniaDataGridViewTextBoxColumn.DataPropertyName = "IloscPozostaloDoSkorygowania";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.iloscPozostaloDoSkorygowaniaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.iloscPozostaloDoSkorygowaniaDataGridViewTextBoxColumn.HeaderText = "Pozostało";
            this.iloscPozostaloDoSkorygowaniaDataGridViewTextBoxColumn.Name = "iloscPozostaloDoSkorygowaniaDataGridViewTextBoxColumn";
            this.iloscPozostaloDoSkorygowaniaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cenaDataGridViewTextBoxColumn
            // 
            this.cenaDataGridViewTextBoxColumn.DataPropertyName = "Cena";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.cenaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.cenaDataGridViewTextBoxColumn.HeaderText = "Cena";
            this.cenaDataGridViewTextBoxColumn.Name = "cenaDataGridViewTextBoxColumn";
            this.cenaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // opisDataGridViewTextBoxColumn
            // 
            this.opisDataGridViewTextBoxColumn.DataPropertyName = "Opis";
            this.opisDataGridViewTextBoxColumn.FillWeight = 200F;
            this.opisDataGridViewTextBoxColumn.HeaderText = "Opis";
            this.opisDataGridViewTextBoxColumn.Name = "opisDataGridViewTextBoxColumn";
            this.opisDataGridViewTextBoxColumn.ReadOnly = true;
            this.opisDataGridViewTextBoxColumn.Width = 200;
            // 
            // PozycjeNieskorygowaneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 484);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.generujZwrotButton);
            this.Controls.Add(this.kontrahentTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nrZrotuTextBox);
            this.Controls.Add(this.label1);
            this.Name = "PozycjeNieskorygowaneForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pozycje nieskorygowane";
            this.Load += new System.EventHandler(this.PozycjeNieskorygowaneForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nrZrotuTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox kontrahentTextBox;
        private System.Windows.Forms.Button generujZwrotButton;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn identDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn towarNazwaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iloscPozostaloDoSkorygowaniaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cenaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}