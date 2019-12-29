namespace AbakTools.Zwroty.Forms
{
    partial class RejestracjaZwrotuForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.opisTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.iloscPaczekTextBox = new System.Windows.Forms.TextBox();
            this.kontrahentEnovaSelect = new Enova.Forms.Controls.KontrahentEnovaSelect();
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(514, 163);
            this.okButton.TabIndex = 6;
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(433, 163);
            this.anulujButton.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Kontrahent:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Opis:";
            // 
            // opisTextBox
            // 
            this.opisTextBox.Location = new System.Drawing.Point(92, 64);
            this.opisTextBox.Multiline = true;
            this.opisTextBox.Name = "opisTextBox";
            this.opisTextBox.Size = new System.Drawing.Size(488, 81);
            this.opisTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ilość paczek:";
            // 
            // iloscPaczekTextBox
            // 
            this.iloscPaczekTextBox.Location = new System.Drawing.Point(92, 38);
            this.iloscPaczekTextBox.Name = "iloscPaczekTextBox";
            this.iloscPaczekTextBox.Size = new System.Drawing.Size(100, 20);
            this.iloscPaczekTextBox.TabIndex = 3;
            // 
            // kontrahentEnovaSelect
            // 
            this.kontrahentEnovaSelect.DataContext = null;
            this.kontrahentEnovaSelect.DisplayMember = null;
            this.kontrahentEnovaSelect.Location = new System.Drawing.Point(92, 12);
            this.kontrahentEnovaSelect.Name = "kontrahentEnovaSelect";
            this.kontrahentEnovaSelect.SelectedItem = null;
            this.kontrahentEnovaSelect.Size = new System.Drawing.Size(488, 20);
            this.kontrahentEnovaSelect.TabIndex = 8;
            this.kontrahentEnovaSelect.ValueMember = null;
            this.kontrahentEnovaSelect.ValueChanged += new System.EventHandler(this.kontrahentEnovaSelect_ValueChanged);
            // 
            // RejestracjaZwrotuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(601, 198);
            this.Controls.Add(this.kontrahentEnovaSelect);
            this.Controls.Add(this.iloscPaczekTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.opisTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RejestracjaZwrotuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rejestracja zwrotu";
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.anulujButton, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.opisTextBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.iloscPaczekTextBox, 0);
            this.Controls.SetChildIndex(this.kontrahentEnovaSelect, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox opisTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox iloscPaczekTextBox;
        private Enova.Forms.Controls.KontrahentEnovaSelect kontrahentEnovaSelect;

    }
}
