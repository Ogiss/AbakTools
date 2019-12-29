namespace AbakTools.Zamowienia.Forms
{
    partial class BrakiFormReport
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
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.dataOdDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dataDoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.przedstawicielSelect = new Enova.Business.Old.Controls.PrzedstawicielSelect();
            this.featureSelect = new Enova.Business.Old.Controls.FeatureSelect();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(12, 40);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1375, 544);
            this.reportViewer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(798, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Data od:";
            // 
            // dataOdDateTimePicker
            // 
            this.dataOdDateTimePicker.Location = new System.Drawing.Point(852, 10);
            this.dataOdDateTimePicker.Name = "dataOdDateTimePicker";
            this.dataOdDateTimePicker.Size = new System.Drawing.Size(132, 20);
            this.dataOdDateTimePicker.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1015, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Data do:";
            // 
            // dataDoDateTimePicker
            // 
            this.dataDoDateTimePicker.Location = new System.Drawing.Point(1069, 10);
            this.dataDoDateTimePicker.Name = "dataDoDateTimePicker";
            this.dataDoDateTimePicker.Size = new System.Drawing.Size(132, 20);
            this.dataDoDateTimePicker.TabIndex = 5;
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(1221, 9);
            this.zatwierdzButton.Name = "zatwierdzButton";
            this.zatwierdzButton.Size = new System.Drawing.Size(75, 23);
            this.zatwierdzButton.TabIndex = 6;
            this.zatwierdzButton.Text = "Zatwierdź";
            this.zatwierdzButton.UseVisualStyleBackColor = true;
            this.zatwierdzButton.Click += new System.EventHandler(this.zatwierdzButton_Click);
            // 
            // przedstawicielSelect
            // 
            this.przedstawicielSelect.Location = new System.Drawing.Point(12, 12);
            this.przedstawicielSelect.Name = "przedstawicielSelect";
            this.przedstawicielSelect.Przedstawiciel = null;
            this.przedstawicielSelect.Size = new System.Drawing.Size(177, 22);
            this.przedstawicielSelect.TabIndex = 0;
            // 
            // featureSelect
            // 
            this.featureSelect.Location = new System.Drawing.Point(257, 9);
            this.featureSelect.Name = "featureSelect";
            this.featureSelect.Size = new System.Drawing.Size(462, 28);
            this.featureSelect.TabIndex = 7;
            this.featureSelect.TableName = "Towary";
            this.featureSelect.Changed += new System.EventHandler(this.featureSelect_Changed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Grupa:";
            // 
            // BrakiFormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1399, 596);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.featureSelect);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.dataDoDateTimePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataOdDateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.przedstawicielSelect);
            this.Name = "BrakiFormReport";
            this.Text = "Braki";
            this.Load += new System.EventHandler(this.BrakiForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Enova.Business.Old.Controls.PrzedstawicielSelect przedstawicielSelect;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dataOdDateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dataDoDateTimePicker;
        private System.Windows.Forms.Button zatwierdzButton;
        private Enova.Business.Old.Controls.FeatureSelect featureSelect;
        private System.Windows.Forms.Label label3;
    }
}