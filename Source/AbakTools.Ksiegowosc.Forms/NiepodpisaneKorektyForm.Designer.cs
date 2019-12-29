namespace AbakTools.Ksiegowosc.Forms
{
    partial class NiepodpisaneKorektyForm
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
            this.przedstawicielComboBox = new System.Windows.Forms.ComboBox();
            this.dataDoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label3 = new System.Windows.Forms.Label();
            this.dataOdDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Przedstawiciel:";
            // 
            // przedstawicielComboBox
            // 
            this.przedstawicielComboBox.FormattingEnabled = true;
            this.przedstawicielComboBox.Location = new System.Drawing.Point(95, 6);
            this.przedstawicielComboBox.Name = "przedstawicielComboBox";
            this.przedstawicielComboBox.Size = new System.Drawing.Size(114, 21);
            this.przedstawicielComboBox.TabIndex = 1;
            // 
            // dataDoDateTimePicker
            // 
            this.dataDoDateTimePicker.Location = new System.Drawing.Point(503, 5);
            this.dataDoDateTimePicker.Name = "dataDoDateTimePicker";
            this.dataDoDateTimePicker.Size = new System.Drawing.Size(141, 20);
            this.dataDoDateTimePicker.TabIndex = 2;
            this.dataDoDateTimePicker.Value = new System.DateTime(2011, 2, 1, 23, 59, 59, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(449, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Data do:";
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(674, 4);
            this.zatwierdzButton.Name = "zatwierdzButton";
            this.zatwierdzButton.Size = new System.Drawing.Size(75, 23);
            this.zatwierdzButton.TabIndex = 4;
            this.zatwierdzButton.Text = "Zatwierdź";
            this.zatwierdzButton.UseVisualStyleBackColor = true;
            this.zatwierdzButton.Click += new System.EventHandler(this.zatwierdzButton_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(12, 33);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1222, 562);
            this.reportViewer.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Data od:";
            // 
            // dataOdDateTimePicker
            // 
            this.dataOdDateTimePicker.Location = new System.Drawing.Point(275, 7);
            this.dataOdDateTimePicker.Name = "dataOdDateTimePicker";
            this.dataOdDateTimePicker.Size = new System.Drawing.Size(141, 20);
            this.dataOdDateTimePicker.TabIndex = 6;
            this.dataOdDateTimePicker.Value = new System.DateTime(2011, 2, 1, 0, 0, 0, 0);
            // 
            // NiepodpisaneKorektyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 607);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataOdDateTimePicker);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataDoDateTimePicker);
            this.Controls.Add(this.przedstawicielComboBox);
            this.Controls.Add(this.label1);
            this.Name = "NiepodpisaneKorektyForm";
            this.Text = "Niepodpisane Korekty";
            this.Load += new System.EventHandler(this.NiepodpisaneKorektyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox przedstawicielComboBox;
        private System.Windows.Forms.DateTimePicker dataDoDateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button zatwierdzButton;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dataOdDateTimePicker;
    }
}