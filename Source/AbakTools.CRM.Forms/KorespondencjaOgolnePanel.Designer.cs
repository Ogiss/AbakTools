namespace AbakTools.CRM.Forms
{
    partial class KorespondencjaOgolnePanel
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
            this.dodajRodzajButton = new System.Windows.Forms.Button();
            this.rodzajComboBox = new System.Windows.Forms.ComboBox();
            this.rodzajBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.kodTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.adresKoresCheckBox = new System.Windows.Forms.CheckBox();
            this.dataDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.opisTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.miejscowoscTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.kodPocztTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.adresTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nazwaTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dataWysylkiDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.kontrahentSelect = new Enova.Forms.Controls.KontrahentEnovaSelect();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rodzajBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BindingSource
            // 
            this.BindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Korespondencja);
            // 
            // dodajRodzajButton
            // 
            this.dodajRodzajButton.Location = new System.Drawing.Point(457, 250);
            this.dodajRodzajButton.Name = "dodajRodzajButton";
            this.dodajRodzajButton.Size = new System.Drawing.Size(75, 23);
            this.dodajRodzajButton.TabIndex = 60;
            this.dodajRodzajButton.Text = "Dodaj";
            this.dodajRodzajButton.UseVisualStyleBackColor = true;
            this.dodajRodzajButton.Click += new System.EventHandler(this.dodajRodzajButton_Click);
            // 
            // rodzajComboBox
            // 
            this.rodzajComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.BindingSource, "RodzajID", true));
            this.rodzajComboBox.DataSource = this.rodzajBindingSource;
            this.rodzajComboBox.DisplayMember = "NazwaPelna";
            this.rodzajComboBox.FormattingEnabled = true;
            this.rodzajComboBox.Location = new System.Drawing.Point(84, 252);
            this.rodzajComboBox.Name = "rodzajComboBox";
            this.rodzajComboBox.Size = new System.Drawing.Size(367, 21);
            this.rodzajComboBox.TabIndex = 59;
            this.rodzajComboBox.ValueMember = "ID";
            // 
            // rodzajBindingSource
            // 
            this.rodzajBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.RodzajKorespondencji);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(35, 255);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 58;
            this.label9.Text = "Rodzaj:";
            // 
            // kodTextBox
            // 
            this.kodTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Kod", true));
            this.kodTextBox.Location = new System.Drawing.Point(84, 78);
            this.kodTextBox.Name = "kodTextBox";
            this.kodTextBox.Size = new System.Drawing.Size(448, 20);
            this.kodTextBox.TabIndex = 44;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(49, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 57;
            this.label8.Text = "Kod:";
            // 
            // adresKoresCheckBox
            // 
            this.adresKoresCheckBox.AutoSize = true;
            this.adresKoresCheckBox.Location = new System.Drawing.Point(84, 55);
            this.adresKoresCheckBox.Name = "adresKoresCheckBox";
            this.adresKoresCheckBox.Size = new System.Drawing.Size(142, 17);
            this.adresKoresCheckBox.TabIndex = 43;
            this.adresKoresCheckBox.Text = "Adres korespondencyjny";
            this.adresKoresCheckBox.UseVisualStyleBackColor = true;
            this.adresKoresCheckBox.CheckedChanged += new System.EventHandler(this.adresKoresCheckBox_CheckedChanged);
            // 
            // dataDateTimePicker
            // 
            this.dataDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.BindingSource, "Data", true));
            this.dataDateTimePicker.Location = new System.Drawing.Point(84, 1);
            this.dataDateTimePicker.Name = "dataDateTimePicker";
            this.dataDateTimePicker.Size = new System.Drawing.Size(131, 20);
            this.dataDateTimePicker.TabIndex = 41;
            // 
            // opisTextBox
            // 
            this.opisTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Opis", true));
            this.opisTextBox.Location = new System.Drawing.Point(84, 182);
            this.opisTextBox.Multiline = true;
            this.opisTextBox.Name = "opisTextBox";
            this.opisTextBox.Size = new System.Drawing.Size(448, 64);
            this.opisTextBox.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Opis:";
            // 
            // miejscowoscTextBox
            // 
            this.miejscowoscTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Miejscowosc", true));
            this.miejscowoscTextBox.Location = new System.Drawing.Point(256, 156);
            this.miejscowoscTextBox.Name = "miejscowoscTextBox";
            this.miejscowoscTextBox.Size = new System.Drawing.Size(276, 20);
            this.miejscowoscTextBox.TabIndex = 48;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Miejscowość:";
            // 
            // kodPocztTextBox
            // 
            this.kodPocztTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "KodPocztowy", true));
            this.kodPocztTextBox.Location = new System.Drawing.Point(84, 156);
            this.kodPocztTextBox.Name = "kodPocztTextBox";
            this.kodPocztTextBox.Size = new System.Drawing.Size(77, 20);
            this.kodPocztTextBox.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 159);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "Kod pocztowy:";
            // 
            // adresTextBox
            // 
            this.adresTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Adres", true));
            this.adresTextBox.Location = new System.Drawing.Point(84, 130);
            this.adresTextBox.Name = "adresTextBox";
            this.adresTextBox.Size = new System.Drawing.Size(448, 20);
            this.adresTextBox.TabIndex = 46;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 52;
            this.label4.Text = "Adres:";
            // 
            // nazwaTextBox
            // 
            this.nazwaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Nazwa", true));
            this.nazwaTextBox.Location = new System.Drawing.Point(84, 104);
            this.nazwaTextBox.Name = "nazwaTextBox";
            this.nazwaTextBox.Size = new System.Drawing.Size(448, 20);
            this.nazwaTextBox.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Nazwa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Kontrahent:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Data:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(253, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "Data wysyłki:";
            // 
            // dataWysylkiDateTimePicker
            // 
            this.dataWysylkiDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.BindingSource, "DataWysylki", true));
            this.dataWysylkiDateTimePicker.Location = new System.Drawing.Point(330, 1);
            this.dataWysylkiDateTimePicker.Name = "dataWysylkiDateTimePicker";
            this.dataWysylkiDateTimePicker.Size = new System.Drawing.Size(140, 20);
            this.dataWysylkiDateTimePicker.TabIndex = 62;
            // 
            // kontrahentSelect
            // 
            this.kontrahentSelect.DataContext = null;
            this.kontrahentSelect.DisplayMember = null;
            this.kontrahentSelect.Location = new System.Drawing.Point(84, 27);
            this.kontrahentSelect.Name = "kontrahentSelect";
            this.kontrahentSelect.ReadOnly = false;
            this.kontrahentSelect.SelectedItem = null;
            this.kontrahentSelect.Size = new System.Drawing.Size(448, 20);
            this.kontrahentSelect.TabIndex = 63;
            this.kontrahentSelect.ValueMember = null;
            this.kontrahentSelect.ValueChanged += new System.EventHandler(this.kontrahentSelect_ValueChanged);
            // 
            // KorespondencjaOgolnePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.kontrahentSelect);
            this.Controls.Add(this.dataWysylkiDateTimePicker);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dodajRodzajButton);
            this.Controls.Add(this.rodzajComboBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.kodTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.adresKoresCheckBox);
            this.Controls.Add(this.dataDateTimePicker);
            this.Controls.Add(this.opisTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.miejscowoscTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.kodPocztTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.adresTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nazwaTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "KorespondencjaOgolnePanel";
            this.Size = new System.Drawing.Size(537, 276);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rodzajBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dodajRodzajButton;
        private System.Windows.Forms.ComboBox rodzajComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox kodTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox adresKoresCheckBox;
        private System.Windows.Forms.DateTimePicker dataDateTimePicker;
        private System.Windows.Forms.TextBox opisTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox miejscowoscTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox kodPocztTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox adresTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nazwaTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource rodzajBindingSource;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dataWysylkiDateTimePicker;
        private Enova.Forms.Controls.KontrahentEnovaSelect kontrahentSelect;
    }
}
