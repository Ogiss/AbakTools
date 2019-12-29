namespace Enova.Forms.Controls
{
    partial class KontrahentEnovaCheckBoxSelect
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
            this.trasaComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.przedstawicielComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.kontrahentComboBox = new BAL.Forms.Controls.CheckedComboBox();
            this.SuspendLayout();
            // 
            // trasaComboBox
            // 
            this.trasaComboBox.DisplayMember = "Value";
            this.trasaComboBox.FormattingEnabled = true;
            this.trasaComboBox.Location = new System.Drawing.Point(238, 1);
            this.trasaComboBox.MaxDropDownItems = 20;
            this.trasaComboBox.Name = "trasaComboBox";
            this.trasaComboBox.Size = new System.Drawing.Size(148, 21);
            this.trasaComboBox.TabIndex = 8;
            this.trasaComboBox.ValueMember = "Value";
            this.trasaComboBox.SelectionChangeCommitted += new System.EventHandler(this.trasaComboBox_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Trasa:";
            // 
            // przedstawicielComboBox
            // 
            this.przedstawicielComboBox.DisplayMember = "Value";
            this.przedstawicielComboBox.FormattingEnabled = true;
            this.przedstawicielComboBox.Location = new System.Drawing.Point(86, 1);
            this.przedstawicielComboBox.MaxDropDownItems = 20;
            this.przedstawicielComboBox.Name = "przedstawicielComboBox";
            this.przedstawicielComboBox.Size = new System.Drawing.Size(103, 21);
            this.przedstawicielComboBox.TabIndex = 6;
            this.przedstawicielComboBox.ValueMember = "Value";
            this.przedstawicielComboBox.SelectionChangeCommitted += new System.EventHandler(this.przedstawicielComboBox_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Przedstawiciel:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(392, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Kontrahent:";
            // 
            // kontrahentComboBox
            // 
            this.kontrahentComboBox.AllRowItem = true;
            this.kontrahentComboBox.AllRowItemText = "(Wszyscy)";
            this.kontrahentComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kontrahentComboBox.DataContext = null;
            this.kontrahentComboBox.DataSource = null;
            this.kontrahentComboBox.DisplayMember = "Kod";
            this.kontrahentComboBox.FireSelectedOnCheckChanged = false;
            this.kontrahentComboBox.Location = new System.Drawing.Point(460, 1);
            this.kontrahentComboBox.Name = "kontrahentComboBox";
            this.kontrahentComboBox.ReadOnly = false;
            this.kontrahentComboBox.SelectedIndex = -1;
            this.kontrahentComboBox.SelectedItem = null;
            this.kontrahentComboBox.Size = new System.Drawing.Size(272, 20);
            this.kontrahentComboBox.TabIndex = 11;
            this.kontrahentComboBox.ValueMember = "ID";
            this.kontrahentComboBox.SelectionChangeCommitted += new System.EventHandler(this.kontrahentComboBox_SelectionChangeCommitted);
            // 
            // PrzedstawicielTrasaKontrahentSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.kontrahentComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trasaComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.przedstawicielComboBox);
            this.Controls.Add(this.label1);
            this.Name = "PrzedstawicielTrasaKontrahentSelect";
            this.Size = new System.Drawing.Size(735, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox trasaComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox przedstawicielComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private BAL.Forms.Controls.CheckedComboBox kontrahentComboBox;
    }
}
