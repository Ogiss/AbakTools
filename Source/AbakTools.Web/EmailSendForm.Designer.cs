namespace AbakTools.Web
{
    partial class EmailSendForm
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
            this.mailToTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mailSubjectTextBox = new System.Windows.Forms.TextBox();
            this.mailBodyTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.attachmentsButton = new System.Windows.Forms.Button();
            this.addressBookButton = new System.Windows.Forms.Button();
            this.potwierdzenieCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Do:";
            // 
            // mailToTextBox
            // 
            this.mailToTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mailToTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.mailToTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.mailToTextBox.Location = new System.Drawing.Point(57, 10);
            this.mailToTextBox.Name = "mailToTextBox";
            this.mailToTextBox.Size = new System.Drawing.Size(869, 20);
            this.mailToTextBox.TabIndex = 1;
            this.mailToTextBox.TextChanged += new System.EventHandler(this.mailToTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tytuł:";
            // 
            // mailSubjectTextBox
            // 
            this.mailSubjectTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mailSubjectTextBox.Location = new System.Drawing.Point(57, 36);
            this.mailSubjectTextBox.Name = "mailSubjectTextBox";
            this.mailSubjectTextBox.Size = new System.Drawing.Size(907, 20);
            this.mailSubjectTextBox.TabIndex = 3;
            // 
            // mailBodyTextBox
            // 
            this.mailBodyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mailBodyTextBox.Location = new System.Drawing.Point(12, 86);
            this.mailBodyTextBox.Multiline = true;
            this.mailBodyTextBox.Name = "mailBodyTextBox";
            this.mailBodyTextBox.Size = new System.Drawing.Size(952, 369);
            this.mailBodyTextBox.TabIndex = 4;
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sendButton.Location = new System.Drawing.Point(12, 457);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 5;
            this.sendButton.Text = "Wyślij";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // attachmentsButton
            // 
            this.attachmentsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.attachmentsButton.Location = new System.Drawing.Point(120, 457);
            this.attachmentsButton.Name = "attachmentsButton";
            this.attachmentsButton.Size = new System.Drawing.Size(101, 23);
            this.attachmentsButton.TabIndex = 6;
            this.attachmentsButton.Text = "Załączniki";
            this.attachmentsButton.UseVisualStyleBackColor = true;
            this.attachmentsButton.Click += new System.EventHandler(this.attachmentsButton_Click);
            // 
            // addressBookButton
            // 
            this.addressBookButton.Location = new System.Drawing.Point(932, 8);
            this.addressBookButton.Name = "addressBookButton";
            this.addressBookButton.Size = new System.Drawing.Size(32, 23);
            this.addressBookButton.TabIndex = 7;
            this.addressBookButton.Text = "...";
            this.addressBookButton.UseVisualStyleBackColor = true;
            this.addressBookButton.Click += new System.EventHandler(this.addressBookButton_Click);
            // 
            // potwierdzenieCheckBox
            // 
            this.potwierdzenieCheckBox.AutoSize = true;
            this.potwierdzenieCheckBox.Location = new System.Drawing.Point(57, 63);
            this.potwierdzenieCheckBox.Name = "potwierdzenieCheckBox";
            this.potwierdzenieCheckBox.Size = new System.Drawing.Size(121, 17);
            this.potwierdzenieCheckBox.TabIndex = 8;
            this.potwierdzenieCheckBox.Text = "Żądaj potwierdzenia";
            this.potwierdzenieCheckBox.UseVisualStyleBackColor = true;
            // 
            // EmailSendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 490);
            this.Controls.Add(this.potwierdzenieCheckBox);
            this.Controls.Add(this.addressBookButton);
            this.Controls.Add(this.attachmentsButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.mailBodyTextBox);
            this.Controls.Add(this.mailSubjectTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mailToTextBox);
            this.Controls.Add(this.label1);
            this.Name = "EmailSendForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nowa wiadomość";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mailToTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mailSubjectTextBox;
        private System.Windows.Forms.TextBox mailBodyTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button attachmentsButton;
        private System.Windows.Forms.Button addressBookButton;
        private System.Windows.Forms.CheckBox potwierdzenieCheckBox;
    }
}