namespace AbakTools.Kadry.Forms
{
    partial class ProwizjeMagRocznyForm
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
            this.pracownikComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rokComboBox = new System.Windows.Forms.ComboBox();
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pracownik:";
            // 
            // pracownikComboBox
            // 
            this.pracownikComboBox.FormattingEnabled = true;
            this.pracownikComboBox.Items.AddRange(new object[] {
            "MG",
            "ŁD",
            "PD",
            "SD"});
            this.pracownikComboBox.Location = new System.Drawing.Point(90, 10);
            this.pracownikComboBox.Name = "pracownikComboBox";
            this.pracownikComboBox.Size = new System.Drawing.Size(107, 21);
            this.pracownikComboBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rok:";
            // 
            // rokComboBox
            // 
            this.rokComboBox.FormattingEnabled = true;
            this.rokComboBox.Location = new System.Drawing.Point(276, 10);
            this.rokComboBox.Name = "rokComboBox";
            this.rokComboBox.Size = new System.Drawing.Size(121, 21);
            this.rokComboBox.TabIndex = 3;
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(431, 8);
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
            this.reportViewer.Location = new System.Drawing.Point(1, 37);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1226, 565);
            this.reportViewer.TabIndex = 5;
            // 
            // ProwizjeMagRocznyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 614);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.rokComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pracownikComboBox);
            this.Controls.Add(this.label1);
            this.Name = "ProwizjeMagRocznyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProwizjeMagRocznyForm";
            this.Load += new System.EventHandler(this.ProwizjeMagRocznyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox pracownikComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox rokComboBox;
        private System.Windows.Forms.Button zatwierdzButton;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}