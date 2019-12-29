namespace AbakTools.CRM.Forms
{
    partial class SMSSendForm
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
            this.toTextBox = new System.Windows.Forms.TextBox();
            this.msgTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sendButton = new System.Windows.Forms.Button();
            this.kontaktyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Odbiorca:";
            // 
            // toTextBox
            // 
            this.toTextBox.Location = new System.Drawing.Point(71, 6);
            this.toTextBox.Name = "toTextBox";
            this.toTextBox.Size = new System.Drawing.Size(210, 20);
            this.toTextBox.TabIndex = 1;
            // 
            // msgTextBox
            // 
            this.msgTextBox.Location = new System.Drawing.Point(12, 61);
            this.msgTextBox.Multiline = true;
            this.msgTextBox.Name = "msgTextBox";
            this.msgTextBox.Size = new System.Drawing.Size(309, 198);
            this.msgTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Wiadomość:";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(246, 265);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 4;
            this.sendButton.Text = "Wyślij";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // kontaktyButton
            // 
            this.kontaktyButton.Location = new System.Drawing.Point(288, 3);
            this.kontaktyButton.Name = "kontaktyButton";
            this.kontaktyButton.Size = new System.Drawing.Size(33, 23);
            this.kontaktyButton.TabIndex = 5;
            this.kontaktyButton.Text = "...";
            this.kontaktyButton.UseVisualStyleBackColor = true;
            this.kontaktyButton.Click += new System.EventHandler(this.kontaktyButton_Click);
            // 
            // SMSSendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 297);
            this.Controls.Add(this.kontaktyButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.msgTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toTextBox);
            this.Controls.Add(this.label1);
            this.Name = "SMSSendForm";
            this.Text = "SMSSendForm";
            this.Load += new System.EventHandler(this.SMSSendForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox toTextBox;
        private System.Windows.Forms.TextBox msgTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button kontaktyButton;
    }
}