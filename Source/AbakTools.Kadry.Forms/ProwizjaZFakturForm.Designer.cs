namespace AbakTools.Kadry.Forms
{
    partial class ProwizjaZFakturForm
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
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.okresDateSpan = new Enova.Business.Old.Controls.DateTimeSpanControl();
            this.kontrahentSelect = new Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(787, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Okres:";
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(12, 40);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1366, 582);
            this.reportViewer.TabIndex = 3;
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(1073, 10);
            this.zatwierdzButton.Name = "zatwierdzButton";
            this.zatwierdzButton.Size = new System.Drawing.Size(75, 23);
            this.zatwierdzButton.TabIndex = 4;
            this.zatwierdzButton.Text = "Zatwierdź";
            this.zatwierdzButton.UseVisualStyleBackColor = true;
            this.zatwierdzButton.Click += new System.EventHandler(this.zatwierdzButton_Click);
            // 
            // okresDateSpan
            // 
            this.okresDateSpan.DateFrom = new System.DateTime(2012, 6, 1, 0, 0, 0, 0);
            this.okresDateSpan.DateTo = new System.DateTime(2012, 6, 30, 23, 59, 59, 0);
            this.okresDateSpan.Location = new System.Drawing.Point(831, 13);
            this.okresDateSpan.Name = "okresDateSpan";
            this.okresDateSpan.Size = new System.Drawing.Size(213, 21);
            this.okresDateSpan.TabIndex = 1;
            // 
            // kontrahentSelect
            // 
            this.kontrahentSelect.DataContext = null;
            this.kontrahentSelect.Location = new System.Drawing.Point(12, 10);
            this.kontrahentSelect.Name = "kontrahentSelect";
            this.kontrahentSelect.ReadOnly = false;
            this.kontrahentSelect.Size = new System.Drawing.Size(735, 22);
            this.kontrahentSelect.TabIndex = 5;
            // 
            // ProwizjaZFakturForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 634);
            this.Controls.Add(this.kontrahentSelect);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okresDateSpan);
            this.Name = "ProwizjaZFakturForm";
            this.Text = "ProwizjaZFakturForm";
            this.Load += new System.EventHandler(this.ProwizjaZFakturForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Enova.Business.Old.Controls.DateTimeSpanControl okresDateSpan;
        private System.Windows.Forms.Label label1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Button zatwierdzButton;
        private Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect kontrahentSelect;
    }
}