namespace EnovaTools.Forms.Web
{
    partial class PozycjaZamowieniaEditForm
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
            this.nazwaTextBox = new System.Windows.Forms.TextBox();
            this.atrybutLabel = new System.Windows.Forms.TextBox();
            this.atrybutComboBox = new System.Windows.Forms.ComboBox();
            this.atrybutyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.iloscTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.atrybutyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(271, 116);
            this.anulujButton.TabIndex = 11;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(352, 116);
            this.okButton.TabIndex = 10;
            // 
            // DataSourceBinding
            // 
            this.DataSourceBinding.DataSource = typeof(Enova.Business.Old.DB.Web.PozycjaZamowienia);
            // 
            // nazwaTextBox
            // 
            this.nazwaTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.nazwaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nazwaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "ProductNazwaPelna", true));
            this.nazwaTextBox.Location = new System.Drawing.Point(12, 12);
            this.nazwaTextBox.Name = "nazwaTextBox";
            this.nazwaTextBox.ReadOnly = true;
            this.nazwaTextBox.Size = new System.Drawing.Size(415, 20);
            this.nazwaTextBox.TabIndex = 2;
            this.nazwaTextBox.TabStop = false;
            this.nazwaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // atrybutLabel
            // 
            this.atrybutLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.atrybutLabel.Location = new System.Drawing.Point(12, 47);
            this.atrybutLabel.Name = "atrybutLabel";
            this.atrybutLabel.ReadOnly = true;
            this.atrybutLabel.Size = new System.Drawing.Size(116, 13);
            this.atrybutLabel.TabIndex = 3;
            this.atrybutLabel.TabStop = false;
            this.atrybutLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // atrybutComboBox
            // 
            this.atrybutComboBox.DataSource = this.atrybutyBindingSource;
            this.atrybutComboBox.DisplayMember = "AtrybutNazwa";
            this.atrybutComboBox.FormattingEnabled = true;
            this.atrybutComboBox.Location = new System.Drawing.Point(134, 47);
            this.atrybutComboBox.Name = "atrybutComboBox";
            this.atrybutComboBox.Size = new System.Drawing.Size(225, 21);
            this.atrybutComboBox.TabIndex = 4;
            this.atrybutComboBox.ValueMember = "ID";
            this.atrybutComboBox.SelectionChangeCommitted += new System.EventHandler(this.atrybutComboBox_SelectionChangeCommitted);
            this.atrybutComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.atrybutComboBox_KeyDown);
            // 
            // atrybutyBindingSource
            // 
            this.atrybutyBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.AtrybutProduktu);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ilość:";
            // 
            // iloscTextBox
            // 
            this.iloscTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "Ilosc", true));
            this.iloscTextBox.Location = new System.Drawing.Point(134, 75);
            this.iloscTextBox.Name = "iloscTextBox";
            this.iloscTextBox.Size = new System.Drawing.Size(123, 20);
            this.iloscTextBox.TabIndex = 6;
            this.iloscTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.iloscTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.iloscTextBox_KeyDown);
            // 
            // PozycjaZamowieniaEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(439, 151);
            this.Controls.Add(this.iloscTextBox);
            this.Controls.Add(this.atrybutComboBox);
            this.Controls.Add(this.nazwaTextBox);
            this.Controls.Add(this.atrybutLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PozycjaZamowieniaEditForm";
            this.Text = "Edycja pozycji zamowienia";
            this.Load += new System.EventHandler(this.PozycjaZamowieniaEditForm_Load);
            this.Shown += new System.EventHandler(this.PozycjaZamowieniaEditForm_Shown);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.atrybutLabel, 0);
            this.Controls.SetChildIndex(this.nazwaTextBox, 0);
            this.Controls.SetChildIndex(this.atrybutComboBox, 0);
            this.Controls.SetChildIndex(this.iloscTextBox, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.anulujButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.atrybutyBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nazwaTextBox;
        private System.Windows.Forms.TextBox atrybutLabel;
        private System.Windows.Forms.ComboBox atrybutComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox iloscTextBox;
        private System.Windows.Forms.BindingSource atrybutyBindingSource;
    }
}
