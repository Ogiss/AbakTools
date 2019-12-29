namespace AbakTools.Analizy.Forms
{
    partial class AnalizaWgGrupTowarowychForm
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
            this.okres1dateSpan = new Enova.Business.Old.Controls.DateTimeSpanControl();
            this.okres2DateSpan = new Enova.Business.Old.Controls.DateTimeSpanControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.zatwierdźButton = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.featureGroupSelect = new Enova.Business.Old.Controls.FeatureGroupSelect();
            this.kontrahentSelect = new Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect();
            this.SuspendLayout();
            // 
            // okres1dateSpan
            // 
            this.okres1dateSpan.DateFrom = new System.DateTime(2011, 4, 1, 0, 0, 0, 0);
            this.okres1dateSpan.DateTo = new System.DateTime(2011, 4, 30, 23, 59, 59, 0);
            this.okres1dateSpan.Location = new System.Drawing.Point(94, 41);
            this.okres1dateSpan.Name = "okres1dateSpan";
            this.okres1dateSpan.Size = new System.Drawing.Size(213, 21);
            this.okres1dateSpan.TabIndex = 1;
            // 
            // okres2DateSpan
            // 
            this.okres2DateSpan.DateFrom = new System.DateTime(2011, 4, 1, 0, 0, 0, 0);
            this.okres2DateSpan.DateTo = new System.DateTime(2011, 4, 30, 23, 59, 59, 0);
            this.okres2DateSpan.Location = new System.Drawing.Point(385, 41);
            this.okres2DateSpan.Name = "okres2DateSpan";
            this.okres2DateSpan.Size = new System.Drawing.Size(213, 21);
            this.okres2DateSpan.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Okres I:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(335, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Okres II";
            // 
            // zatwierdźButton
            // 
            this.zatwierdźButton.Location = new System.Drawing.Point(1095, 10);
            this.zatwierdźButton.Name = "zatwierdźButton";
            this.zatwierdźButton.Size = new System.Drawing.Size(75, 23);
            this.zatwierdźButton.TabIndex = 9;
            this.zatwierdźButton.Text = "Zatwierdź";
            this.zatwierdźButton.UseVisualStyleBackColor = true;
            this.zatwierdźButton.Click += new System.EventHandler(this.zatwierdźButton_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(11, 68);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1158, 555);
            this.reportViewer.TabIndex = 10;
            // 
            // featureGroupSelect
            // 
            this.featureGroupSelect.LabelText = "Grupa:";
            this.featureGroupSelect.Location = new System.Drawing.Point(806, 13);
            this.featureGroupSelect.Name = "featureGroupSelect";
            this.featureGroupSelect.Size = new System.Drawing.Size(240, 20);
            this.featureGroupSelect.TabIndex = 11;
            this.featureGroupSelect.TableName = "Towary";
            this.featureGroupSelect.WithAll = false;
            // 
            // kontrahentSelect
            // 
            this.kontrahentSelect.DataContext = null;
            this.kontrahentSelect.Location = new System.Drawing.Point(12, 13);
            this.kontrahentSelect.Name = "kontrahentSelect";
            this.kontrahentSelect.ReadOnly = false;
            this.kontrahentSelect.Size = new System.Drawing.Size(788, 22);
            this.kontrahentSelect.TabIndex = 12;
            // 
            // AnalizaWgGrupTowarowychForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1182, 635);
            this.Controls.Add(this.kontrahentSelect);
            this.Controls.Add(this.featureGroupSelect);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.zatwierdźButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okres2DateSpan);
            this.Controls.Add(this.okres1dateSpan);
            this.Name = "AnalizaWgGrupTowarowychForm";
            this.Text = "Analiza według grup towarowych po kontrahentach";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Enova.Business.Old.Controls.DateTimeSpanControl okres1dateSpan;
        private Enova.Business.Old.Controls.DateTimeSpanControl okres2DateSpan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button zatwierdźButton;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private Enova.Business.Old.Controls.FeatureGroupSelect featureGroupSelect;
        private Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect kontrahentSelect;
    }
}
