namespace AbakTools.Business.Forms
{
    partial class KonfiguracjaZwrotowForm
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
            this.domyslnyOkrDoDatePicker = new System.Windows.Forms.DateTimePicker();
            this.domyslnyOkrOddDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.domyslnaGrupaComboBox = new System.Windows.Forms.ComboBox();
            this.grupyTowaroweBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.anulujButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbStatusDopieroPrzyjedzie = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // domyslnyOkrDoDatePicker
            // 
            this.domyslnyOkrDoDatePicker.Location = new System.Drawing.Point(221, 59);
            this.domyslnyOkrDoDatePicker.Name = "domyslnyOkrDoDatePicker";
            this.domyslnyOkrDoDatePicker.Size = new System.Drawing.Size(133, 20);
            this.domyslnyOkrDoDatePicker.TabIndex = 11;
            // 
            // domyslnyOkrOddDatePicker
            // 
            this.domyslnyOkrOddDatePicker.Location = new System.Drawing.Point(221, 33);
            this.domyslnyOkrOddDatePicker.Name = "domyslnyOkrOddDatePicker";
            this.domyslnyOkrOddDatePicker.Size = new System.Drawing.Size(133, 20);
            this.domyslnyOkrOddDatePicker.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Domyślny okres do:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Domyślny okres od:";
            // 
            // domyslnaGrupaComboBox
            // 
            this.domyslnaGrupaComboBox.DataSource = this.grupyTowaroweBindingSource;
            this.domyslnaGrupaComboBox.DisplayMember = "Name";
            this.domyslnaGrupaComboBox.FormattingEnabled = true;
            this.domyslnaGrupaComboBox.Location = new System.Drawing.Point(221, 6);
            this.domyslnaGrupaComboBox.Name = "domyslnaGrupaComboBox";
            this.domyslnaGrupaComboBox.Size = new System.Drawing.Size(195, 21);
            this.domyslnaGrupaComboBox.TabIndex = 7;
            this.domyslnaGrupaComboBox.ValueMember = "ID";
            // 
            // grupyTowaroweBindingSource
            // 
            this.grupyTowaroweBindingSource.DataSource = typeof(Enova.API.Business.FeatureDefinition);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Domyślna grupa na formularzu zwrotów:";
            // 
            // anulujButton
            // 
            this.anulujButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.anulujButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.anulujButton.Location = new System.Drawing.Point(273, 162);
            this.anulujButton.Name = "anulujButton";
            this.anulujButton.Size = new System.Drawing.Size(75, 23);
            this.anulujButton.TabIndex = 13;
            this.anulujButton.Text = "Anuluj";
            this.anulujButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(354, 162);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 12;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Status \"Dopiero przyjedzie\" ID:";
            // 
            // tbStatusDopieroPrzyjedzie
            // 
            this.tbStatusDopieroPrzyjedzie.Location = new System.Drawing.Point(221, 85);
            this.tbStatusDopieroPrzyjedzie.Name = "tbStatusDopieroPrzyjedzie";
            this.tbStatusDopieroPrzyjedzie.Size = new System.Drawing.Size(55, 20);
            this.tbStatusDopieroPrzyjedzie.TabIndex = 15;
            this.tbStatusDopieroPrzyjedzie.Text = "5";
            this.tbStatusDopieroPrzyjedzie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // KonfiguracjaZwrotowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 197);
            this.Controls.Add(this.tbStatusDopieroPrzyjedzie);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.anulujButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.domyslnyOkrDoDatePicker);
            this.Controls.Add(this.domyslnyOkrOddDatePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.domyslnaGrupaComboBox);
            this.Controls.Add(this.label1);
            this.Name = "KonfiguracjaZwrotowForm";
            this.Text = "KonfiguracjaZwrotowForm";
            this.Load += new System.EventHandler(this.KonfiguracjaZwrotowForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker domyslnyOkrDoDatePicker;
        private System.Windows.Forms.DateTimePicker domyslnyOkrOddDatePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox domyslnaGrupaComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button anulujButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.BindingSource grupyTowaroweBindingSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbStatusDopieroPrzyjedzie;
    }
}