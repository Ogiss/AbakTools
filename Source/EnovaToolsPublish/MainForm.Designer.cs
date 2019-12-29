namespace EnovaToolsPublish
{
    partial class MainForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.lastTextBox = new System.Windows.Forms.TextBox();
            this.currentTextBox = new System.Windows.Forms.TextBox();
            this.publishButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.updatePathTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ostatnia wersja:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Obecna wersja:";
            // 
            // lastTextBox
            // 
            this.lastTextBox.Location = new System.Drawing.Point(158, 34);
            this.lastTextBox.Name = "lastTextBox";
            this.lastTextBox.ReadOnly = true;
            this.lastTextBox.Size = new System.Drawing.Size(200, 20);
            this.lastTextBox.TabIndex = 2;
            this.lastTextBox.TabStop = false;
            // 
            // currentTextBox
            // 
            this.currentTextBox.Location = new System.Drawing.Point(158, 59);
            this.currentTextBox.Name = "currentTextBox";
            this.currentTextBox.ReadOnly = true;
            this.currentTextBox.Size = new System.Drawing.Size(200, 20);
            this.currentTextBox.TabIndex = 3;
            this.currentTextBox.TabStop = false;
            // 
            // publishButton
            // 
            this.publishButton.Location = new System.Drawing.Point(283, 101);
            this.publishButton.Name = "publishButton";
            this.publishButton.Size = new System.Drawing.Size(75, 23);
            this.publishButton.TabIndex = 4;
            this.publishButton.Text = "Publikuj";
            this.publishButton.UseVisualStyleBackColor = true;
            this.publishButton.Click += new System.EventHandler(this.publishButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(370, 101);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "Zamknij";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // updatePathTextBox
            // 
            this.updatePathTextBox.Location = new System.Drawing.Point(158, 8);
            this.updatePathTextBox.Name = "updatePathTextBox";
            this.updatePathTextBox.ReadOnly = true;
            this.updatePathTextBox.Size = new System.Drawing.Size(200, 20);
            this.updatePathTextBox.TabIndex = 7;
            this.updatePathTextBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ścieżka aktualizacji:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 136);
            this.Controls.Add(this.updatePathTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.publishButton);
            this.Controls.Add(this.currentTextBox);
            this.Controls.Add(this.lastTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "EnovaTools pubish";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lastTextBox;
        private System.Windows.Forms.TextBox currentTextBox;
        private System.Windows.Forms.Button publishButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox updatePathTextBox;
        private System.Windows.Forms.Label label3;
    }
}

