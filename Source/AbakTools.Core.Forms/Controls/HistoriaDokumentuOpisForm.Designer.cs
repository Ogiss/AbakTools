namespace AbakTools.Core.Forms.Controls
{
    partial class HistoriaDokumentuOpisForm
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
            this.opisTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // opisTextBox
            // 
            this.opisTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.opisTextBox.Location = new System.Drawing.Point(12, 12);
            this.opisTextBox.Multiline = true;
            this.opisTextBox.Name = "opisTextBox";
            this.opisTextBox.Size = new System.Drawing.Size(400, 194);
            this.opisTextBox.TabIndex = 0;
            // 
            // HistoriaDokumentuOpisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 218);
            this.Controls.Add(this.opisTextBox);
            this.KeyPreview = true;
            this.Name = "HistoriaDokumentuOpisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historia dokumentu : Opis";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HistoriaDokumentuOpisForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox opisTextBox;
    }
}