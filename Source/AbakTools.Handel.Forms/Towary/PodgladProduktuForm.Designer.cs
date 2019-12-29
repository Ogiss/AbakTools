namespace AbakTools.Towary.Forms
{
    partial class PodgladProduktuForm
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.imageLegendTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.kodAbakTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.kodprodTextBox = new System.Windows.Forms.TextBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(300, 300);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // imageLegendTextBox
            // 
            this.imageLegendTextBox.BackColor = System.Drawing.Color.White;
            this.imageLegendTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.imageLegendTextBox.Enabled = false;
            this.imageLegendTextBox.Location = new System.Drawing.Point(12, 312);
            this.imageLegendTextBox.Name = "imageLegendTextBox";
            this.imageLegendTextBox.ReadOnly = true;
            this.imageLegendTextBox.Size = new System.Drawing.Size(300, 13);
            this.imageLegendTextBox.TabIndex = 2;
            this.imageLegendTextBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 334);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Kod ABAK:";
            // 
            // kodAbakTextBox
            // 
            this.kodAbakTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kodAbakTextBox.Location = new System.Drawing.Point(79, 334);
            this.kodAbakTextBox.Name = "kodAbakTextBox";
            this.kodAbakTextBox.ReadOnly = true;
            this.kodAbakTextBox.Size = new System.Drawing.Size(233, 13);
            this.kodAbakTextBox.TabIndex = 4;
            this.kodAbakTextBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 353);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Kod prod:";
            // 
            // kodprodTextBox
            // 
            this.kodprodTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kodprodTextBox.Location = new System.Drawing.Point(79, 353);
            this.kodprodTextBox.Name = "kodprodTextBox";
            this.kodprodTextBox.ReadOnly = true;
            this.kodprodTextBox.Size = new System.Drawing.Size(233, 13);
            this.kodprodTextBox.TabIndex = 6;
            this.kodprodTextBox.TabStop = false;
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(12, 372);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(300, 296);
            this.webBrowser.TabIndex = 7;
            // 
            // PodgladProduktuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 685);
            this.ControlBox = false;
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.kodprodTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.kodAbakTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imageLegendTextBox);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PodgladProduktuForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PodgladProduktuForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PodgladProduktuForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox imageLegendTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox kodAbakTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox kodprodTextBox;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}