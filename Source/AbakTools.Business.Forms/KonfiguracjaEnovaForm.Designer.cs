namespace AbakTools.Business.Forms
{
    partial class KonfiguracjaEnovaForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.enovaPathTextBox = new System.Windows.Forms.TextBox();
            this.enovaPathButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.enovaDatabaseTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.fvRaportTextBox = new System.Windows.Forms.TextBox();
            this.fvButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.fkRaportTextBox = new System.Windows.Forms.TextBox();
            this.fkButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(479, 328);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Zatwierdź";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(398, 328);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Katalog Enova:";
            // 
            // enovaPathTextBox
            // 
            this.enovaPathTextBox.Location = new System.Drawing.Point(98, 18);
            this.enovaPathTextBox.Name = "enovaPathTextBox";
            this.enovaPathTextBox.Size = new System.Drawing.Size(375, 20);
            this.enovaPathTextBox.TabIndex = 3;
            // 
            // enovaPathButton
            // 
            this.enovaPathButton.Location = new System.Drawing.Point(479, 16);
            this.enovaPathButton.Name = "enovaPathButton";
            this.enovaPathButton.Size = new System.Drawing.Size(75, 23);
            this.enovaPathButton.TabIndex = 4;
            this.enovaPathButton.Text = "Wybierz";
            this.enovaPathButton.UseVisualStyleBackColor = true;
            this.enovaPathButton.Click += new System.EventHandler(this.enovaPathButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Baza danych:";
            // 
            // enovaDatabaseTextBox
            // 
            this.enovaDatabaseTextBox.Location = new System.Drawing.Point(98, 45);
            this.enovaDatabaseTextBox.Name = "enovaDatabaseTextBox";
            this.enovaDatabaseTextBox.Size = new System.Drawing.Size(224, 20);
            this.enovaDatabaseTextBox.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fkButton);
            this.groupBox1.Controls.Add(this.fkRaportTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.fvButton);
            this.groupBox1.Controls.Add(this.fvRaportTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(15, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(539, 187);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wydruki";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Aspx|*.aspx|*.*|*.*";
            this.openFileDialog.RestoreDirectory = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Faktura sprzedaży:";
            // 
            // fvRaportTextBox
            // 
            this.fvRaportTextBox.Location = new System.Drawing.Point(108, 22);
            this.fvRaportTextBox.Name = "fvRaportTextBox";
            this.fvRaportTextBox.Size = new System.Drawing.Size(344, 20);
            this.fvRaportTextBox.TabIndex = 1;
            // 
            // fvButton
            // 
            this.fvButton.Location = new System.Drawing.Point(458, 22);
            this.fvButton.Name = "fvButton";
            this.fvButton.Size = new System.Drawing.Size(75, 23);
            this.fvButton.TabIndex = 2;
            this.fvButton.Text = "Wybierz";
            this.fvButton.UseVisualStyleBackColor = true;
            this.fvButton.Click += new System.EventHandler(this.fvButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Korekta sprzedaży:";
            // 
            // fkRaportTextBox
            // 
            this.fkRaportTextBox.Location = new System.Drawing.Point(108, 48);
            this.fkRaportTextBox.Name = "fkRaportTextBox";
            this.fkRaportTextBox.Size = new System.Drawing.Size(344, 20);
            this.fkRaportTextBox.TabIndex = 4;
            // 
            // fkButton
            // 
            this.fkButton.Location = new System.Drawing.Point(458, 46);
            this.fkButton.Name = "fkButton";
            this.fkButton.Size = new System.Drawing.Size(75, 23);
            this.fkButton.TabIndex = 5;
            this.fkButton.Text = "Wybierz";
            this.fkButton.UseVisualStyleBackColor = true;
            this.fkButton.Click += new System.EventHandler(this.fkButton_Click);
            // 
            // KonfiguracjaEnovaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 363);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.enovaDatabaseTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.enovaPathButton);
            this.Controls.Add(this.enovaPathTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KonfiguracjaEnovaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Konfiguracja połaczenia z programem Enova";
            this.Load += new System.EventHandler(this.KonfiguracjaEnovaForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox enovaPathTextBox;
        private System.Windows.Forms.Button enovaPathButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox enovaDatabaseTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button fkButton;
        private System.Windows.Forms.TextBox fkRaportTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button fvButton;
        private System.Windows.Forms.TextBox fvRaportTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}