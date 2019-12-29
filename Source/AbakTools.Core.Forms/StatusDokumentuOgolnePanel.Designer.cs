namespace AbakTools.Core.Forms
{
    partial class StatusDokumentuOgolnePanel
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
            this.kategoriaComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nazwaTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.kolejnoscTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.kolorTextBox = new System.Windows.Forms.TextBox();
            this.kolorButton = new System.Windows.Forms.Button();
            this.opcjeGroupBox = new System.Windows.Forms.GroupBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.opcjeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BindingSource
            // 
            this.BindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.StatusDokumentu);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kategoria:";
            // 
            // kategoriaComboBox
            // 
            this.kategoriaComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.BindingSource, "Kategoria", true));
            this.kategoriaComboBox.FormattingEnabled = true;
            this.kategoriaComboBox.Location = new System.Drawing.Point(74, 4);
            this.kategoriaComboBox.Name = "kategoriaComboBox";
            this.kategoriaComboBox.Size = new System.Drawing.Size(258, 21);
            this.kategoriaComboBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nazwa:";
            // 
            // nazwaTextBox
            // 
            this.nazwaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Nazwa", true));
            this.nazwaTextBox.Location = new System.Drawing.Point(74, 31);
            this.nazwaTextBox.Name = "nazwaTextBox";
            this.nazwaTextBox.Size = new System.Drawing.Size(258, 20);
            this.nazwaTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kolejność:";
            // 
            // kolejnoscTextBox
            // 
            this.kolejnoscTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Kolejnosc", true));
            this.kolejnoscTextBox.Location = new System.Drawing.Point(74, 57);
            this.kolejnoscTextBox.Name = "kolejnoscTextBox";
            this.kolejnoscTextBox.Size = new System.Drawing.Size(92, 20);
            this.kolejnoscTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Kolor:";
            // 
            // kolorTextBox
            // 
            this.kolorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Kolor", true));
            this.kolorTextBox.Location = new System.Drawing.Point(74, 83);
            this.kolorTextBox.Name = "kolorTextBox";
            this.kolorTextBox.Size = new System.Drawing.Size(100, 20);
            this.kolorTextBox.TabIndex = 7;
            // 
            // kolorButton
            // 
            this.kolorButton.Location = new System.Drawing.Point(180, 81);
            this.kolorButton.Name = "kolorButton";
            this.kolorButton.Size = new System.Drawing.Size(30, 23);
            this.kolorButton.TabIndex = 8;
            this.kolorButton.UseVisualStyleBackColor = true;
            this.kolorButton.Click += new System.EventHandler(this.kolorButton_Click);
            // 
            // opcjeGroupBox
            // 
            this.opcjeGroupBox.Controls.Add(this.checkBox5);
            this.opcjeGroupBox.Controls.Add(this.checkBox4);
            this.opcjeGroupBox.Controls.Add(this.checkBox3);
            this.opcjeGroupBox.Controls.Add(this.checkBox2);
            this.opcjeGroupBox.Controls.Add(this.checkBox1);
            this.opcjeGroupBox.Location = new System.Drawing.Point(16, 120);
            this.opcjeGroupBox.Name = "opcjeGroupBox";
            this.opcjeGroupBox.Size = new System.Drawing.Size(316, 100);
            this.opcjeGroupBox.TabIndex = 9;
            this.opcjeGroupBox.TabStop = false;
            this.opcjeGroupBox.Text = "Opcje";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(114, 19);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(110, 17);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Tag = "8";
            this.checkBox4.Text = "Zawsze widoczny";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.opcjeCheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(21, 67);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(70, 17);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Tag = "4";
            this.checkBox3.Text = "Końcowy";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.opcjeCheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(21, 43);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(77, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Tag = "2";
            this.checkBox2.Text = "Niezależny";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.opcjeCheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(21, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(71, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Tag = "1";
            this.checkBox1.Text = "Domyślny";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.opcjeCheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(114, 43);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(101, 17);
            this.checkBox5.TabIndex = 4;
            this.checkBox5.Tag = "16";
            this.checkBox5.Text = "Wymagany opis";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.opcjeCheckedChanged);
            // 
            // StatusDokumentuOgolnePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.opcjeGroupBox);
            this.Controls.Add(this.kolorButton);
            this.Controls.Add(this.kolorTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.kolejnoscTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nazwaTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.kategoriaComboBox);
            this.Controls.Add(this.label1);
            this.Name = "StatusDokumentuOgolnePanel";
            this.Size = new System.Drawing.Size(344, 253);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.opcjeGroupBox.ResumeLayout(false);
            this.opcjeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox kategoriaComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nazwaTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox kolejnoscTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox kolorTextBox;
        private System.Windows.Forms.Button kolorButton;
        private System.Windows.Forms.GroupBox opcjeGroupBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox5;

    }
}
