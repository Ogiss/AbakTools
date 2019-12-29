namespace AbakTools.Business.Forms
{
    partial class OperatorEnovaPanel
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
            this.enovaLoginTextBox = new System.Windows.Forms.TextBox();
            this.enovaHasloTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BindingSource
            // 
            this.BindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Operator);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enova login:";
            // 
            // enovaLoginTextBox
            // 
            this.enovaLoginTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "EnovaLogin", true));
            this.enovaLoginTextBox.Location = new System.Drawing.Point(123, 3);
            this.enovaLoginTextBox.Name = "enovaLoginTextBox";
            this.enovaLoginTextBox.Size = new System.Drawing.Size(174, 20);
            this.enovaLoginTextBox.TabIndex = 1;
            // 
            // enovaHasloTextBox
            // 
            this.enovaHasloTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "EnovaPassword", true));
            this.enovaHasloTextBox.Location = new System.Drawing.Point(123, 29);
            this.enovaHasloTextBox.Name = "enovaHasloTextBox";
            this.enovaHasloTextBox.Size = new System.Drawing.Size(174, 20);
            this.enovaHasloTextBox.TabIndex = 3;
            this.enovaHasloTextBox.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Enova hasło:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kod przedstawiciela:";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "KodPrzedstawiciela", true));
            this.textBox1.Location = new System.Drawing.Point(123, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(62, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Kody przedstawicieli:";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "KodyPrzedstawicieli", true));
            this.textBox2.Location = new System.Drawing.Point(123, 81);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(174, 20);
            this.textBox2.TabIndex = 7;
            // 
            // OperatorEnovaPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.enovaHasloTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.enovaLoginTextBox);
            this.Controls.Add(this.label1);
            this.Name = "OperatorEnovaPanel";
            this.Size = new System.Drawing.Size(304, 108);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox enovaLoginTextBox;
        private System.Windows.Forms.TextBox enovaHasloTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
    }
}
