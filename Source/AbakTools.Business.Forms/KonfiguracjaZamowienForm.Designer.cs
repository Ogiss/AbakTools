namespace AbakTools.Business.Forms
{
    partial class KonfiguracjaZamowienForm
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
            this.domyslnaGrupaComboBox = new System.Windows.Forms.ComboBox();
            this.grupyTowaroweBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.domyslnyOkrOddDatePicker = new System.Windows.Forms.DateTimePicker();
            this.domyslnyOkrDoDatePicker = new System.Windows.Forms.DateTimePicker();
            this.okButton = new System.Windows.Forms.Button();
            this.anulujButton = new System.Windows.Forms.Button();
            this.previewCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpZwrotyDo = new System.Windows.Forms.DateTimePicker();
            this.dtpZwrotyOd = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.sp2AktywnaCheckBox = new System.Windows.Forms.CheckBox();
            this.sp2DoDtp = new System.Windows.Forms.DateTimePicker();
            this.sp2OdDtp = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.sp2FromOrderCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Domyślna grupa na formularzu zamówień:";
            // 
            // domyslnaGrupaComboBox
            // 
            this.domyslnaGrupaComboBox.DataSource = this.grupyTowaroweBindingSource;
            this.domyslnaGrupaComboBox.DisplayMember = "Name";
            this.domyslnaGrupaComboBox.FormattingEnabled = true;
            this.domyslnaGrupaComboBox.Location = new System.Drawing.Point(235, 17);
            this.domyslnaGrupaComboBox.Name = "domyslnaGrupaComboBox";
            this.domyslnaGrupaComboBox.Size = new System.Drawing.Size(195, 21);
            this.domyslnaGrupaComboBox.TabIndex = 1;
            this.domyslnaGrupaComboBox.ValueMember = "ID";
            // 
            // grupyTowaroweBindingSource
            // 
            this.grupyTowaroweBindingSource.DataSource = typeof(Enova.API.Business.FeatureDefinition);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Domyślny okres sprzedarzy od:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Domyślny okres sprzedaży do:";
            // 
            // domyslnyOkrOddDatePicker
            // 
            this.domyslnyOkrOddDatePicker.Location = new System.Drawing.Point(235, 44);
            this.domyslnyOkrOddDatePicker.Name = "domyslnyOkrOddDatePicker";
            this.domyslnyOkrOddDatePicker.Size = new System.Drawing.Size(133, 20);
            this.domyslnyOkrOddDatePicker.TabIndex = 4;
            // 
            // domyslnyOkrDoDatePicker
            // 
            this.domyslnyOkrDoDatePicker.Location = new System.Drawing.Point(235, 70);
            this.domyslnyOkrDoDatePicker.Name = "domyslnyOkrDoDatePicker";
            this.domyslnyOkrDoDatePicker.Size = new System.Drawing.Size(133, 20);
            this.domyslnyOkrDoDatePicker.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(409, 328);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // anulujButton
            // 
            this.anulujButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.anulujButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.anulujButton.Location = new System.Drawing.Point(328, 328);
            this.anulujButton.Name = "anulujButton";
            this.anulujButton.Size = new System.Drawing.Size(75, 23);
            this.anulujButton.TabIndex = 7;
            this.anulujButton.Text = "Anuluj";
            this.anulujButton.UseVisualStyleBackColor = true;
            // 
            // previewCheckBox
            // 
            this.previewCheckBox.AutoSize = true;
            this.previewCheckBox.Checked = true;
            this.previewCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.previewCheckBox.Location = new System.Drawing.Point(235, 259);
            this.previewCheckBox.Name = "previewCheckBox";
            this.previewCheckBox.Size = new System.Drawing.Size(15, 14);
            this.previewCheckBox.TabIndex = 9;
            this.previewCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(136, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Podgląd wydruku:";
            // 
            // dtpZwrotyDo
            // 
            this.dtpZwrotyDo.Location = new System.Drawing.Point(235, 122);
            this.dtpZwrotyDo.Name = "dtpZwrotyDo";
            this.dtpZwrotyDo.Size = new System.Drawing.Size(133, 20);
            this.dtpZwrotyDo.TabIndex = 14;
            // 
            // dtpZwrotyOd
            // 
            this.dtpZwrotyOd.Location = new System.Drawing.Point(235, 96);
            this.dtpZwrotyOd.Name = "dtpZwrotyOd";
            this.dtpZwrotyOd.Size = new System.Drawing.Size(133, 20);
            this.dtpZwrotyOd.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(88, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Domyślny okres zwrotów do:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Domyślny okres zwrotów od:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(90, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Okres sprzedaży II aktywny:";
            // 
            // sp2AktywnaCheckBox
            // 
            this.sp2AktywnaCheckBox.AutoSize = true;
            this.sp2AktywnaCheckBox.Checked = true;
            this.sp2AktywnaCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sp2AktywnaCheckBox.Location = new System.Drawing.Point(235, 148);
            this.sp2AktywnaCheckBox.Name = "sp2AktywnaCheckBox";
            this.sp2AktywnaCheckBox.Size = new System.Drawing.Size(15, 14);
            this.sp2AktywnaCheckBox.TabIndex = 15;
            this.sp2AktywnaCheckBox.UseVisualStyleBackColor = true;
            // 
            // sp2DoDtp
            // 
            this.sp2DoDtp.Location = new System.Drawing.Point(235, 217);
            this.sp2DoDtp.Name = "sp2DoDtp";
            this.sp2DoDtp.Size = new System.Drawing.Size(133, 20);
            this.sp2DoDtp.TabIndex = 20;
            // 
            // sp2OdDtp
            // 
            this.sp2OdDtp.Location = new System.Drawing.Point(235, 191);
            this.sp2OdDtp.Name = "sp2OdDtp";
            this.sp2OdDtp.Size = new System.Drawing.Size(133, 20);
            this.sp2OdDtp.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(71, 223);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(158, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Domyślny okres sprzedaży II do:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(68, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(161, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Domyślny okres sprzedarzy II od:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(107, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Sprzedaż II z zamówień:";
            // 
            // sp2FromOrderCheckBox
            // 
            this.sp2FromOrderCheckBox.AutoSize = true;
            this.sp2FromOrderCheckBox.Checked = true;
            this.sp2FromOrderCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sp2FromOrderCheckBox.Location = new System.Drawing.Point(235, 171);
            this.sp2FromOrderCheckBox.Name = "sp2FromOrderCheckBox";
            this.sp2FromOrderCheckBox.Size = new System.Drawing.Size(15, 14);
            this.sp2FromOrderCheckBox.TabIndex = 21;
            this.sp2FromOrderCheckBox.UseVisualStyleBackColor = true;
            // 
            // KonfiguracjaZamowienForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 363);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.sp2FromOrderCheckBox);
            this.Controls.Add(this.sp2DoDtp);
            this.Controls.Add(this.sp2OdDtp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.sp2AktywnaCheckBox);
            this.Controls.Add(this.dtpZwrotyDo);
            this.Controls.Add(this.dtpZwrotyOd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.previewCheckBox);
            this.Controls.Add(this.anulujButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.domyslnyOkrDoDatePicker);
            this.Controls.Add(this.domyslnyOkrOddDatePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.domyslnaGrupaComboBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KonfiguracjaZamowienForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Konfiguracja systemu zamówień";
            this.Load += new System.EventHandler(this.KonfiguracjaZamowienForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox domyslnaGrupaComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker domyslnyOkrOddDatePicker;
        private System.Windows.Forms.DateTimePicker domyslnyOkrDoDatePicker;
        private System.Windows.Forms.BindingSource grupyTowaroweBindingSource;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button anulujButton;
        private System.Windows.Forms.CheckBox previewCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpZwrotyDo;
        private System.Windows.Forms.DateTimePicker dtpZwrotyOd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox sp2AktywnaCheckBox;
        private System.Windows.Forms.DateTimePicker sp2DoDtp;
        private System.Windows.Forms.DateTimePicker sp2OdDtp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox sp2FromOrderCheckBox;
    }
}