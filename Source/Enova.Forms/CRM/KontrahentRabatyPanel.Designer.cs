namespace Enova.Forms.CRM
{
    partial class KontrahentRabatyPanel
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
            this.label29 = new System.Windows.Forms.Label();
            this.kopiujRabatyButton = new System.Windows.Forms.Button();
            this.filterGrupyTowComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.rabatOgólnyTextBox = new System.Windows.Forms.TextBox();
            this.rabatyDataGridView = new System.Windows.Forms.DataGridView();
            this.grupaTowarowaNazwaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabatColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabatZdefiniowanyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rabatyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rabatyDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rabatyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BindingSource
            // 
            this.BindingSource.DataSource = typeof(Enova.API.CRM.Kontrahent);
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(435, 76);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(94, 13);
            this.label29.TabIndex = 9;
            this.label29.Text = "RABAT OGÓLNY:";
            // 
            // kopiujRabatyButton
            // 
            this.kopiujRabatyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kopiujRabatyButton.Location = new System.Drawing.Point(447, 27);
            this.kopiujRabatyButton.Name = "kopiujRabatyButton";
            this.kopiujRabatyButton.Size = new System.Drawing.Size(106, 23);
            this.kopiujRabatyButton.TabIndex = 8;
            this.kopiujRabatyButton.Text = "Kopiuj rabaty z ...";
            this.kopiujRabatyButton.UseVisualStyleBackColor = true;
            this.kopiujRabatyButton.Click += new System.EventHandler(this.kopiujRabatyButton_Click);
            // 
            // filterGrupyTowComboBox
            // 
            this.filterGrupyTowComboBox.DisplayMember = "Name";
            this.filterGrupyTowComboBox.FormattingEnabled = true;
            this.filterGrupyTowComboBox.Location = new System.Drawing.Point(54, 0);
            this.filterGrupyTowComboBox.Name = "filterGrupyTowComboBox";
            this.filterGrupyTowComboBox.Size = new System.Drawing.Size(202, 21);
            this.filterGrupyTowComboBox.TabIndex = 6;
            this.filterGrupyTowComboBox.ValueMember = "ID";
            this.filterGrupyTowComboBox.SelectionChangeCommitted += new System.EventHandler(this.filterGrupyTowComboBox_SelectionChangeCommitted);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Grupa:";
            // 
            // rabatOgólnyTextBox
            // 
            this.rabatOgólnyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rabatOgólnyTextBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.rabatOgólnyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rabatOgólnyTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Rabat", true));
            this.rabatOgólnyTextBox.Location = new System.Drawing.Point(535, 76);
            this.rabatOgólnyTextBox.Name = "rabatOgólnyTextBox";
            this.rabatOgólnyTextBox.ReadOnly = true;
            this.rabatOgólnyTextBox.Size = new System.Drawing.Size(100, 13);
            this.rabatOgólnyTextBox.TabIndex = 10;
            // 
            // rabatyDataGridView
            // 
            this.rabatyDataGridView.AllowUserToAddRows = false;
            this.rabatyDataGridView.AllowUserToDeleteRows = false;
            this.rabatyDataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.rabatyDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.rabatyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rabatyDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grupaTowarowaNazwaDataGridViewTextBoxColumn,
            this.rabatColumn,
            this.rabatZdefiniowanyDataGridViewCheckBoxColumn});
            this.rabatyDataGridView.DataSource = this.rabatyBindingSource;
            this.rabatyDataGridView.Location = new System.Drawing.Point(3, 27);
            this.rabatyDataGridView.Name = "rabatyDataGridView";
            this.rabatyDataGridView.RowHeadersVisible = false;
            this.rabatyDataGridView.Size = new System.Drawing.Size(426, 512);
            this.rabatyDataGridView.TabIndex = 11;
            this.rabatyDataGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.rabatyDataGridView_CellParsing);
            // 
            // grupaTowarowaNazwaDataGridViewTextBoxColumn
            // 
            this.grupaTowarowaNazwaDataGridViewTextBoxColumn.DataPropertyName = "GrupaTowarowaNazwa";
            this.grupaTowarowaNazwaDataGridViewTextBoxColumn.FillWeight = 150F;
            this.grupaTowarowaNazwaDataGridViewTextBoxColumn.HeaderText = "Grupa towarowa";
            this.grupaTowarowaNazwaDataGridViewTextBoxColumn.Name = "grupaTowarowaNazwaDataGridViewTextBoxColumn";
            this.grupaTowarowaNazwaDataGridViewTextBoxColumn.ReadOnly = true;
            this.grupaTowarowaNazwaDataGridViewTextBoxColumn.Width = 150;
            // 
            // rabatColumn
            // 
            this.rabatColumn.DataPropertyName = "Rabat";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "P2";
            this.rabatColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.rabatColumn.HeaderText = "Rabat";
            this.rabatColumn.Name = "rabatColumn";
            // 
            // rabatZdefiniowanyDataGridViewCheckBoxColumn
            // 
            this.rabatZdefiniowanyDataGridViewCheckBoxColumn.DataPropertyName = "RabatZdefiniowany";
            this.rabatZdefiniowanyDataGridViewCheckBoxColumn.HeaderText = "Rabat zdefiniowany";
            this.rabatZdefiniowanyDataGridViewCheckBoxColumn.Name = "rabatZdefiniowanyDataGridViewCheckBoxColumn";
            // 
            // rabatyBindingSource
            // 
            this.rabatyBindingSource.DataSource = typeof(Enova.Forms.Towary.RabatGrupowy);
            // 
            // KontrahentRabatyPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.rabatyDataGridView);
            this.Controls.Add(this.rabatOgólnyTextBox);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.kopiujRabatyButton);
            this.Controls.Add(this.filterGrupyTowComboBox);
            this.Controls.Add(this.label11);
            this.Name = "KontrahentRabatyPanel";
            this.Size = new System.Drawing.Size(647, 542);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rabatyDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rabatyBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button kopiujRabatyButton;
        private System.Windows.Forms.ComboBox filterGrupyTowComboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox rabatOgólnyTextBox;
        private System.Windows.Forms.BindingSource rabatyBindingSource;
        private System.Windows.Forms.DataGridView rabatyDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn grupaTowarowaNazwaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabatColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn rabatZdefiniowanyDataGridViewCheckBoxColumn;
    }
}
