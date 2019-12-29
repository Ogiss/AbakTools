namespace AbakTools.Zamowienia.Forms
{
    partial class WystawFaktureForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.magazynyComboBox = new System.Windows.Forms.ComboBox();
            this.magazynyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kontrahentTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.sezonyComboBox = new System.Windows.Forms.ComboBox();
            this.sezonBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.komunikatTextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.zaPobraniemCheckBox = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.placiWysylkeComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.iloscPaczekTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nrListuTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.przewoznikComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.przedstawicielComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.zatwierdzCheckBox = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.kosztWysyłkiTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.doliczWysylkeCheckBox = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.terminTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.definicjeDokComboBox = new System.Windows.Forms.ComboBox();
            this.defDokBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label18 = new System.Windows.Forms.Label();
            this.sposobyZaplatyComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.magazynyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sezonBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defDokBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data wystawienia:";
            // 
            // dataDateTimePicker
            // 
            this.dataDateTimePicker.Location = new System.Drawing.Point(111, 15);
            this.dataDateTimePicker.Name = "dataDateTimePicker";
            this.dataDateTimePicker.Size = new System.Drawing.Size(144, 20);
            this.dataDateTimePicker.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Magazyn:";
            // 
            // magazynyComboBox
            // 
            this.magazynyComboBox.DataSource = this.magazynyBindingSource;
            this.magazynyComboBox.DisplayMember = "Nazwa";
            this.magazynyComboBox.FormattingEnabled = true;
            this.magazynyComboBox.Location = new System.Drawing.Point(331, 15);
            this.magazynyComboBox.Name = "magazynyComboBox";
            this.magazynyComboBox.Size = new System.Drawing.Size(171, 21);
            this.magazynyComboBox.TabIndex = 3;
            this.magazynyComboBox.ValueMember = "ID";
            // 
            // magazynyBindingSource
            // 
            this.magazynyBindingSource.DataSource = typeof(Enova.API.Magazyny.Magazyn);
            // 
            // kontrahentTextBox
            // 
            this.kontrahentTextBox.Location = new System.Drawing.Point(111, 67);
            this.kontrahentTextBox.Name = "kontrahentTextBox";
            this.kontrahentTextBox.Size = new System.Drawing.Size(391, 20);
            this.kontrahentTextBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Kontrahent:";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(444, 484);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Zatwierdź";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(363, 484);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Sezon:";
            // 
            // sezonyComboBox
            // 
            this.sezonyComboBox.DisplayMember = "ID";
            this.sezonyComboBox.FormattingEnabled = true;
            this.sezonyComboBox.Location = new System.Drawing.Point(96, 31);
            this.sezonyComboBox.Name = "sezonyComboBox";
            this.sezonyComboBox.Size = new System.Drawing.Size(214, 21);
            this.sezonyComboBox.TabIndex = 9;
            this.sezonyComboBox.ValueMember = "ID";
            // 
            // sezonBindingSource
            // 
            this.sezonBindingSource.DataSource = typeof(Enova.API.Business.DictionaryItem);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.komunikatTextBox);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.zaPobraniemCheckBox);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.placiWysylkeComboBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.iloscPaczekTextBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.nrListuTextBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.przewoznikComboBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.przedstawicielComboBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.sezonyComboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(15, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 337);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cechy";
            // 
            // komunikatTextBox
            // 
            this.komunikatTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.komunikatTextBox.Location = new System.Drawing.Point(16, 237);
            this.komunikatTextBox.Multiline = true;
            this.komunikatTextBox.Name = "komunikatTextBox";
            this.komunikatTextBox.ReadOnly = true;
            this.komunikatTextBox.Size = new System.Drawing.Size(465, 94);
            this.komunikatTextBox.TabIndex = 23;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 220);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 13);
            this.label17.TabIndex = 22;
            this.label17.Text = "Komunikat:";
            // 
            // zaPobraniemCheckBox
            // 
            this.zaPobraniemCheckBox.AutoSize = true;
            this.zaPobraniemCheckBox.Location = new System.Drawing.Point(96, 191);
            this.zaPobraniemCheckBox.Name = "zaPobraniemCheckBox";
            this.zaPobraniemCheckBox.Size = new System.Drawing.Size(15, 14);
            this.zaPobraniemCheckBox.TabIndex = 21;
            this.zaPobraniemCheckBox.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 192);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Za pobraniem:";
            // 
            // placiWysylkeComboBox
            // 
            this.placiWysylkeComboBox.FormattingEnabled = true;
            this.placiWysylkeComboBox.Location = new System.Drawing.Point(96, 164);
            this.placiWysylkeComboBox.Name = "placiWysylkeComboBox";
            this.placiWysylkeComboBox.Size = new System.Drawing.Size(121, 21);
            this.placiWysylkeComboBox.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 167);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Płaci wysyłkę:";
            // 
            // iloscPaczekTextBox
            // 
            this.iloscPaczekTextBox.Location = new System.Drawing.Point(96, 138);
            this.iloscPaczekTextBox.Name = "iloscPaczekTextBox";
            this.iloscPaczekTextBox.Size = new System.Drawing.Size(100, 20);
            this.iloscPaczekTextBox.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Ilość paczek:";
            // 
            // nrListuTextBox
            // 
            this.nrListuTextBox.Location = new System.Drawing.Point(96, 112);
            this.nrListuTextBox.Name = "nrListuTextBox";
            this.nrListuTextBox.Size = new System.Drawing.Size(214, 20);
            this.nrListuTextBox.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(48, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Nr listu:";
            // 
            // przewoznikComboBox
            // 
            this.przewoznikComboBox.FormattingEnabled = true;
            this.przewoznikComboBox.Items.AddRange(new object[] {
            "Nie wybrano",
            "OPEK",
            "BUS",
            "DHL",
            "TBA"});
            this.przewoznikComboBox.Location = new System.Drawing.Point(96, 85);
            this.przewoznikComboBox.Name = "przewoznikComboBox";
            this.przewoznikComboBox.Size = new System.Drawing.Size(121, 21);
            this.przewoznikComboBox.TabIndex = 13;
            this.przewoznikComboBox.SelectionChangeCommitted += new System.EventHandler(this.przewoznikComboBox_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Przewoźnik:";
            // 
            // przedstawicielComboBox
            // 
            this.przedstawicielComboBox.FormattingEnabled = true;
            this.przedstawicielComboBox.Location = new System.Drawing.Point(96, 58);
            this.przedstawicielComboBox.Name = "przedstawicielComboBox";
            this.przedstawicielComboBox.Size = new System.Drawing.Size(121, 21);
            this.przedstawicielComboBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Przedstawiciel:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Zatwierdź fakturę:";
            // 
            // zatwierdzCheckBox
            // 
            this.zatwierdzCheckBox.AutoSize = true;
            this.zatwierdzCheckBox.Checked = true;
            this.zatwierdzCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.zatwierdzCheckBox.Location = new System.Drawing.Point(117, 124);
            this.zatwierdzCheckBox.Name = "zatwierdzCheckBox";
            this.zatwierdzCheckBox.Size = new System.Drawing.Size(15, 14);
            this.zatwierdzCheckBox.TabIndex = 12;
            this.zatwierdzCheckBox.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(278, 125);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Koszty wysyłki:";
            // 
            // kosztWysyłkiTextBox
            // 
            this.kosztWysyłkiTextBox.Enabled = false;
            this.kosztWysyłkiTextBox.Location = new System.Drawing.Point(363, 121);
            this.kosztWysyłkiTextBox.Name = "kosztWysyłkiTextBox";
            this.kosztWysyłkiTextBox.Size = new System.Drawing.Size(70, 20);
            this.kosztWysyłkiTextBox.TabIndex = 14;
            this.kosztWysyłkiTextBox.Text = "12,00";
            this.kosztWysyłkiTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(147, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Dolicz koszt wys.:";
            // 
            // doliczWysylkeCheckBox
            // 
            this.doliczWysylkeCheckBox.AutoSize = true;
            this.doliczWysylkeCheckBox.Location = new System.Drawing.Point(244, 125);
            this.doliczWysylkeCheckBox.Name = "doliczWysylkeCheckBox";
            this.doliczWysylkeCheckBox.Size = new System.Drawing.Size(15, 14);
            this.doliczWysylkeCheckBox.TabIndex = 16;
            this.doliczWysylkeCheckBox.UseVisualStyleBackColor = true;
            this.doliczWysylkeCheckBox.CheckedChanged += new System.EventHandler(this.doliczWysylkeCheckBox_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(440, 125);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "Netto";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(63, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 13);
            this.label15.TabIndex = 18;
            this.label15.Text = "Termin:";
            // 
            // terminTextBox
            // 
            this.terminTextBox.Location = new System.Drawing.Point(111, 41);
            this.terminTextBox.Name = "terminTextBox";
            this.terminTextBox.Size = new System.Drawing.Size(62, 20);
            this.terminTextBox.TabIndex = 19;
            this.terminTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(30, 96);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(75, 13);
            this.label16.TabIndex = 20;
            this.label16.Text = "Definicja dok.:";
            // 
            // definicjeDokComboBox
            // 
            this.definicjeDokComboBox.DataSource = this.defDokBindingSource;
            this.definicjeDokComboBox.DisplayMember = "Nazwa";
            this.definicjeDokComboBox.FormattingEnabled = true;
            this.definicjeDokComboBox.Location = new System.Drawing.Point(111, 93);
            this.definicjeDokComboBox.Name = "definicjeDokComboBox";
            this.definicjeDokComboBox.Size = new System.Drawing.Size(202, 21);
            this.definicjeDokComboBox.TabIndex = 21;
            this.definicjeDokComboBox.ValueMember = "Symbol";
            // 
            // defDokBindingSource
            // 
            this.defDokBindingSource.DataSource = typeof(Enova.API.Handel.DefDokHandlowego);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(241, 44);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(84, 13);
            this.label18.TabIndex = 22;
            this.label18.Text = "Sposób zapłaty:";
            // 
            // sposobyZaplatyComboBox
            // 
            this.sposobyZaplatyComboBox.FormattingEnabled = true;
            this.sposobyZaplatyComboBox.Location = new System.Drawing.Point(331, 41);
            this.sposobyZaplatyComboBox.Name = "sposobyZaplatyComboBox";
            this.sposobyZaplatyComboBox.Size = new System.Drawing.Size(171, 21);
            this.sposobyZaplatyComboBox.TabIndex = 23;
            this.sposobyZaplatyComboBox.SelectionChangeCommitted += new System.EventHandler(this.sposobyZaplatyComboBox_SelectionChangeCommitted);
            // 
            // WystawFaktureForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(531, 519);
            this.Controls.Add(this.sposobyZaplatyComboBox);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.definicjeDokComboBox);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.terminTextBox);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.doliczWysylkeCheckBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.kosztWysyłkiTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.zatwierdzCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.kontrahentTextBox);
            this.Controls.Add(this.magazynyComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataDateTimePicker);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WystawFaktureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wystaw fakturę VAT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WystawFaktureForm_FormClosing);
            this.Load += new System.EventHandler(this.WystawFaktureFormcs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.magazynyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sezonBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defDokBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dataDateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox magazynyComboBox;
        private System.Windows.Forms.TextBox kontrahentTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox sezonyComboBox;
        private System.Windows.Forms.BindingSource magazynyBindingSource;
        private System.Windows.Forms.BindingSource sezonBindingSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox zaPobraniemCheckBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox placiWysylkeComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox iloscPaczekTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox nrListuTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox przewoznikComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox przedstawicielComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox zatwierdzCheckBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox kosztWysyłkiTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox doliczWysylkeCheckBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox terminTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox definicjeDokComboBox;
        private System.Windows.Forms.BindingSource defDokBindingSource;
        private System.Windows.Forms.TextBox komunikatTextBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox sposobyZaplatyComboBox;
    }
}