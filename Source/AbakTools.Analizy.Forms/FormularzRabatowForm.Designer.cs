namespace AbakTools.Analizy.Forms
{
    partial class FormularzRabatowForm
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
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.kontrahentSelect = new Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect();
            this.SuspendLayout();
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(1122, 10);
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
            this.reportViewer.Location = new System.Drawing.Point(12, 39);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1185, 519);
            this.reportViewer.TabIndex = 5;
            // 
            // kontrahentSelect
            // 
            this.kontrahentSelect.DataContext = null;
            this.kontrahentSelect.Location = new System.Drawing.Point(13, 10);
            this.kontrahentSelect.Name = "kontrahentSelect";
            this.kontrahentSelect.ReadOnly = false;
            this.kontrahentSelect.Size = new System.Drawing.Size(735, 22);
            this.kontrahentSelect.TabIndex = 6;
            // 
            // FormularzRabatowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 570);
            this.Controls.Add(this.kontrahentSelect);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.zatwierdzButton);
            this.Name = "FormularzRabatowForm";
            this.Text = "FormularzRabatowForm";
            this.Load += new System.EventHandler(this.FormularzRabatowForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button zatwierdzButton;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect kontrahentSelect;
    }
}