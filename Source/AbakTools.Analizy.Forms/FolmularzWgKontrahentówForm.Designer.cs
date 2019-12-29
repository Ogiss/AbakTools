namespace AbakTools.Analizy.Forms
{
    partial class FolmularzWgKontrahentówForm
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
            this.przedstawicielComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grupaComboBox = new System.Windows.Forms.ComboBox();
            this.grupyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.dataOdDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dataDoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.bezZerowychCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.trasyComboBox = new System.Windows.Forms.ComboBox();
            this.rokWsteczCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grupyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Przedstawiciel:";
            // 
            // przedstawicielComboBox
            // 
            this.przedstawicielComboBox.FormattingEnabled = true;
            this.przedstawicielComboBox.Location = new System.Drawing.Point(95, 6);
            this.przedstawicielComboBox.MaxDropDownItems = 20;
            this.przedstawicielComboBox.Name = "przedstawicielComboBox";
            this.przedstawicielComboBox.Size = new System.Drawing.Size(94, 21);
            this.przedstawicielComboBox.TabIndex = 1;
            this.przedstawicielComboBox.SelectionChangeCommitted += new System.EventHandler(this.przedstawicielComboBox_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(435, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Grupa:";
            // 
            // grupaComboBox
            // 
            this.grupaComboBox.DataSource = this.grupyBindingSource;
            this.grupaComboBox.DisplayMember = "Name";
            this.grupaComboBox.FormattingEnabled = true;
            this.grupaComboBox.Location = new System.Drawing.Point(480, 6);
            this.grupaComboBox.MaxDropDownItems = 20;
            this.grupaComboBox.Name = "grupaComboBox";
            this.grupaComboBox.Size = new System.Drawing.Size(231, 21);
            this.grupaComboBox.TabIndex = 3;
            this.grupaComboBox.ValueMember = "Name";
            // 
            // grupyBindingSource
            // 
            this.grupyBindingSource.DataSource = typeof(Enova.API.Business.FeatureDefinition);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(726, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "DataOd:";
            // 
            // dataOdDateTimePicker
            // 
            this.dataOdDateTimePicker.Location = new System.Drawing.Point(779, 3);
            this.dataOdDateTimePicker.Name = "dataOdDateTimePicker";
            this.dataOdDateTimePicker.Size = new System.Drawing.Size(131, 20);
            this.dataOdDateTimePicker.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(925, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "DataDo:";
            // 
            // dataDoDateTimePicker
            // 
            this.dataDoDateTimePicker.Location = new System.Drawing.Point(978, 3);
            this.dataDoDateTimePicker.Name = "dataDoDateTimePicker";
            this.dataDoDateTimePicker.Size = new System.Drawing.Size(131, 20);
            this.dataDoDateTimePicker.TabIndex = 9;
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(1125, 2);
            this.zatwierdzButton.Name = "zatwierdzButton";
            this.zatwierdzButton.Size = new System.Drawing.Size(75, 23);
            this.zatwierdzButton.TabIndex = 10;
            this.zatwierdzButton.Text = "Zatwierdź";
            this.zatwierdzButton.UseVisualStyleBackColor = true;
            this.zatwierdzButton.Click += new System.EventHandler(this.zatwierdzButton_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(12, 56);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1307, 521);
            this.reportViewer.TabIndex = 11;
            // 
            // bezZerowychCheckBox
            // 
            this.bezZerowychCheckBox.AutoSize = true;
            this.bezZerowychCheckBox.Location = new System.Drawing.Point(97, 33);
            this.bezZerowychCheckBox.Name = "bezZerowychCheckBox";
            this.bezZerowychCheckBox.Size = new System.Drawing.Size(92, 17);
            this.bezZerowychCheckBox.TabIndex = 12;
            this.bezZerowychCheckBox.Text = "Bez zerowych";
            this.bezZerowychCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Trasy:";
            // 
            // trasyComboBox
            // 
            this.trasyComboBox.FormattingEnabled = true;
            this.trasyComboBox.Location = new System.Drawing.Point(248, 6);
            this.trasyComboBox.Name = "trasyComboBox";
            this.trasyComboBox.Size = new System.Drawing.Size(181, 21);
            this.trasyComboBox.TabIndex = 15;
            // 
            // rokWsteczCheckBox
            // 
            this.rokWsteczCheckBox.AutoSize = true;
            this.rokWsteczCheckBox.Location = new System.Drawing.Point(209, 33);
            this.rokWsteczCheckBox.Name = "rokWsteczCheckBox";
            this.rokWsteczCheckBox.Size = new System.Drawing.Size(121, 17);
            this.rokWsteczCheckBox.TabIndex = 16;
            this.rokWsteczCheckBox.Text = "Dodaj poprzedni rok";
            this.rokWsteczCheckBox.UseVisualStyleBackColor = true;
            // 
            // FolmularzWgKontrahentówForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 589);
            this.Controls.Add(this.rokWsteczCheckBox);
            this.Controls.Add(this.trasyComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bezZerowychCheckBox);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.dataDoDateTimePicker);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataOdDateTimePicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grupaComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.przedstawicielComboBox);
            this.Controls.Add(this.label1);
            this.Name = "FolmularzWgKontrahentówForm";
            this.Text = "Folmularz Wg Kontrahentów";
            this.Load += new System.EventHandler(this.FolmularzWgKontrahentówForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grupyBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox przedstawicielComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox grupaComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dataOdDateTimePicker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dataDoDateTimePicker;
        private System.Windows.Forms.Button zatwierdzButton;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource grupyBindingSource;
        private System.Windows.Forms.CheckBox bezZerowychCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox trasyComboBox;
        private System.Windows.Forms.CheckBox rokWsteczCheckBox;
    }
}