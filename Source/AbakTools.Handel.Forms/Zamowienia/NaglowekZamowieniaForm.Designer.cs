namespace AbakTools.Zamowienia.Forms
{
    partial class NaglowekZamowieniaForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.naKiedyDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.transportComboBox = new System.Windows.Forms.ComboBox();
            this.pilneCheckBox = new System.Windows.Forms.CheckBox();
            this.terminDataTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.terminTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.kontrahentEnovaSelect = new Enova.Forms.Controls.KontrahentEnovaSelect();
            this.kopiujRabatyCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(403, 145);
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(322, 145);
            // 
            // DataSourceBinding
            // 
            this.DataSourceBinding.DataSource = typeof(Enova.Business.Old.DB.Web.Zamowienie);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kontrahent:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Na kiedy:";
            // 
            // naKiedyDateTimePicker
            // 
            this.naKiedyDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.DataSourceBinding, "NaKiedy", true));
            this.naKiedyDateTimePicker.Location = new System.Drawing.Point(94, 33);
            this.naKiedyDateTimePicker.Name = "naKiedyDateTimePicker";
            this.naKiedyDateTimePicker.Size = new System.Drawing.Size(128, 20);
            this.naKiedyDateTimePicker.TabIndex = 5;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(228, 35);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(33, 17);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "N";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(267, 35);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(33, 17);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.Text = "R";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(306, 35);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(32, 17);
            this.radioButton3.TabIndex = 8;
            this.radioButton3.Text = "P";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(344, 35);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(36, 17);
            this.radioButton4.TabIndex = 9;
            this.radioButton4.Text = "W";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Transport:";
            // 
            // transportComboBox
            // 
            this.transportComboBox.FormattingEnabled = true;
            this.transportComboBox.Items.AddRange(new object[] {
            "Nie wybrano",
            "Kurier",
            "Przedstawiciel",
            "Do dostawcy"});
            this.transportComboBox.Location = new System.Drawing.Point(94, 60);
            this.transportComboBox.Name = "transportComboBox";
            this.transportComboBox.Size = new System.Drawing.Size(128, 21);
            this.transportComboBox.TabIndex = 11;
            this.transportComboBox.SelectionChangeCommitted += new System.EventHandler(this.transportComboBox_SelectionChangeCommitted);
            // 
            // pilneCheckBox
            // 
            this.pilneCheckBox.AutoSize = true;
            this.pilneCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.DataSourceBinding, "Pilne", true));
            this.pilneCheckBox.Location = new System.Drawing.Point(413, 36);
            this.pilneCheckBox.Name = "pilneCheckBox";
            this.pilneCheckBox.Size = new System.Drawing.Size(57, 17);
            this.pilneCheckBox.TabIndex = 12;
            this.pilneCheckBox.Text = "PILNE";
            this.pilneCheckBox.UseVisualStyleBackColor = true;
            this.pilneCheckBox.Visible = false;
            // 
            // terminDataTimePicker
            // 
            this.terminDataTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.DataSourceBinding, "TerminData", true));
            this.terminDataTimePicker.Location = new System.Drawing.Point(283, 87);
            this.terminDataTimePicker.Name = "terminDataTimePicker";
            this.terminDataTimePicker.Size = new System.Drawing.Size(131, 20);
            this.terminDataTimePicker.TabIndex = 46;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(211, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 45;
            this.label10.Text = "Termin data:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(168, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "dni";
            // 
            // terminTextBox
            // 
            this.terminTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "TerminPlatnosci", true));
            this.terminTextBox.Location = new System.Drawing.Point(94, 87);
            this.terminTextBox.Name = "terminTextBox";
            this.terminTextBox.Size = new System.Drawing.Size(68, 20);
            this.terminTextBox.TabIndex = 43;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(46, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "Termin:";
            // 
            // kontrahentEnovaSelect
            // 
            this.kontrahentEnovaSelect.DataContext = null;
            this.kontrahentEnovaSelect.DisplayMember = null;
            this.kontrahentEnovaSelect.Location = new System.Drawing.Point(94, 6);
            this.kontrahentEnovaSelect.Name = "kontrahentEnovaSelect";
            this.kontrahentEnovaSelect.ReadOnly = false;
            this.kontrahentEnovaSelect.SelectedItem = null;
            this.kontrahentEnovaSelect.Size = new System.Drawing.Size(359, 20);
            this.kontrahentEnovaSelect.TabIndex = 53;
            this.kontrahentEnovaSelect.ValueMember = null;
            this.kontrahentEnovaSelect.ValueChanged += new System.EventHandler(this.kontrahentEnovaSelect_ValueChanged);
            // 
            // kopiujRabatyCheckBox
            // 
            this.kopiujRabatyCheckBox.AutoSize = true;
            this.kopiujRabatyCheckBox.Location = new System.Drawing.Point(94, 113);
            this.kopiujRabatyCheckBox.Name = "kopiujRabatyCheckBox";
            this.kopiujRabatyCheckBox.Size = new System.Drawing.Size(15, 14);
            this.kopiujRabatyCheckBox.TabIndex = 54;
            this.kopiujRabatyCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "Kopiuj rabaty:";
            // 
            // NaglowekZamowieniaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(490, 180);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.kopiujRabatyCheckBox);
            this.Controls.Add(this.kontrahentEnovaSelect);
            this.Controls.Add(this.terminDataTimePicker);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.terminTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.transportComboBox);
            this.Controls.Add(this.naKiedyDateTimePicker);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pilneCheckBox);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NaglowekZamowieniaForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nagłówek zamówienia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NaglowekZamowieniaForm_FormClosing);
            this.Load += new System.EventHandler(this.NaglowekZamowieniaForm_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.radioButton2, 0);
            this.Controls.SetChildIndex(this.radioButton3, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.radioButton4, 0);
            this.Controls.SetChildIndex(this.pilneCheckBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.radioButton1, 0);
            this.Controls.SetChildIndex(this.naKiedyDateTimePicker, 0);
            this.Controls.SetChildIndex(this.transportComboBox, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.anulujButton, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.terminTextBox, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.terminDataTimePicker, 0);
            this.Controls.SetChildIndex(this.kontrahentEnovaSelect, 0);
            this.Controls.SetChildIndex(this.kopiujRabatyCheckBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker naKiedyDateTimePicker;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox transportComboBox;
        private System.Windows.Forms.CheckBox pilneCheckBox;
        private System.Windows.Forms.DateTimePicker terminDataTimePicker;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox terminTextBox;
        private System.Windows.Forms.Label label8;
        private Enova.Forms.Controls.KontrahentEnovaSelect kontrahentEnovaSelect;
        private System.Windows.Forms.CheckBox kopiujRabatyCheckBox;
        private System.Windows.Forms.Label label4;
    }
}
