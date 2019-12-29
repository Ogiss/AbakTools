namespace AbakTools.Kadry.Forms
{
    partial class RaporOkresowyProwizjiForm
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.rokTextBox = new System.Windows.Forms.TextBox();
            this.przedstawicielSelect = new Enova.Business.Old.Controls.PrzedstawicielSelect();
            this.SuspendLayout();
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(368, 9);
            this.zatwierdzButton.Name = "zatwierdzButton";
            this.zatwierdzButton.Size = new System.Drawing.Size(75, 23);
            this.zatwierdzButton.TabIndex = 6;
            this.zatwierdzButton.Text = "Zatwierdź";
            this.zatwierdzButton.UseVisualStyleBackColor = true;
            this.zatwierdzButton.Click += new System.EventHandler(this.zatwierdzButton_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "Prowizja";
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "";
            this.reportViewer.LocalReport.ReportPath = "Reports\\RaportOkresowyProwizji.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(12, 40);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1083, 601);
            this.reportViewer.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(211, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Rok:";
            // 
            // rokTextBox
            // 
            this.rokTextBox.Location = new System.Drawing.Point(247, 11);
            this.rokTextBox.Name = "rokTextBox";
            this.rokTextBox.Size = new System.Drawing.Size(100, 20);
            this.rokTextBox.TabIndex = 9;
            // 
            // przedstawicielSelect
            // 
            this.przedstawicielSelect.Location = new System.Drawing.Point(12, 11);
            this.przedstawicielSelect.Name = "przedstawicielSelect";
            this.przedstawicielSelect.Przedstawiciel = null;
            this.przedstawicielSelect.Size = new System.Drawing.Size(177, 22);
            this.przedstawicielSelect.TabIndex = 1;
            // 
            // RaporOkresowyProwizjiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 653);
            this.Controls.Add(this.rokTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.przedstawicielSelect);
            this.Name = "RaporOkresowyProwizjiForm";
            this.Text = "Rapor okresowy prowizji";
            this.Load += new System.EventHandler(this.RaporOkresowyProwizjiForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Enova.Business.Old.Controls.PrzedstawicielSelect przedstawicielSelect;
        private System.Windows.Forms.Button zatwierdzButton;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rokTextBox;
    }
}