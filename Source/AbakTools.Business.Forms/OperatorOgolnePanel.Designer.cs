namespace AbakTools.Business.Forms
{
    partial class OperatorOgolnePanel
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
            this.nazwaTextBox = new System.Windows.Forms.TextBox();
            this.hasloTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.powtorzHasloTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bezHaslaCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.wymZmianaHaslaCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.blokadaCheckBox = new System.Windows.Forms.CheckBox();
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
            this.label1.Location = new System.Drawing.Point(94, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nazwa:";
            // 
            // nazwaTextBox
            // 
            this.nazwaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Nazwa", true));
            this.nazwaTextBox.Location = new System.Drawing.Point(143, 5);
            this.nazwaTextBox.Name = "nazwaTextBox";
            this.nazwaTextBox.Size = new System.Drawing.Size(249, 20);
            this.nazwaTextBox.TabIndex = 1;
            // 
            // hasloTextBox
            // 
            this.hasloTextBox.Location = new System.Drawing.Point(143, 52);
            this.hasloTextBox.Name = "hasloTextBox";
            this.hasloTextBox.Size = new System.Drawing.Size(249, 20);
            this.hasloTextBox.TabIndex = 3;
            this.hasloTextBox.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hasło:";
            // 
            // powtorzHasloTextBox
            // 
            this.powtorzHasloTextBox.Location = new System.Drawing.Point(143, 78);
            this.powtorzHasloTextBox.Name = "powtorzHasloTextBox";
            this.powtorzHasloTextBox.Size = new System.Drawing.Size(249, 20);
            this.powtorzHasloTextBox.TabIndex = 5;
            this.powtorzHasloTextBox.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Powtórz hasło:";
            // 
            // bezHaslaCheckBox
            // 
            this.bezHaslaCheckBox.AutoSize = true;
            this.bezHaslaCheckBox.Location = new System.Drawing.Point(143, 31);
            this.bezHaslaCheckBox.Name = "bezHaslaCheckBox";
            this.bezHaslaCheckBox.Size = new System.Drawing.Size(15, 14);
            this.bezHaslaCheckBox.TabIndex = 6;
            this.bezHaslaCheckBox.UseVisualStyleBackColor = true;
            this.bezHaslaCheckBox.CheckedChanged += new System.EventHandler(this.bezHaslaCheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Bez hasła:";
            // 
            // wymZmianaHaslaCheckBox
            // 
            this.wymZmianaHaslaCheckBox.AutoSize = true;
            this.wymZmianaHaslaCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.BindingSource, "WymaganaZmianaHasla", true));
            this.wymZmianaHaslaCheckBox.Location = new System.Drawing.Point(143, 104);
            this.wymZmianaHaslaCheckBox.Name = "wymZmianaHaslaCheckBox";
            this.wymZmianaHaslaCheckBox.Size = new System.Drawing.Size(15, 14);
            this.wymZmianaHaslaCheckBox.TabIndex = 8;
            this.wymZmianaHaslaCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Wymagana zmiana hasła:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Blokada:";
            // 
            // blokadaCheckBox
            // 
            this.blokadaCheckBox.AutoSize = true;
            this.blokadaCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.BindingSource, "Blokada", true));
            this.blokadaCheckBox.Location = new System.Drawing.Point(143, 124);
            this.blokadaCheckBox.Name = "blokadaCheckBox";
            this.blokadaCheckBox.Size = new System.Drawing.Size(15, 14);
            this.blokadaCheckBox.TabIndex = 11;
            this.blokadaCheckBox.UseVisualStyleBackColor = true;
            // 
            // OperatorOgolnePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.blokadaCheckBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.wymZmianaHaslaCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bezHaslaCheckBox);
            this.Controls.Add(this.powtorzHasloTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hasloTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nazwaTextBox);
            this.Controls.Add(this.label1);
            this.Name = "OperatorOgolnePanel";
            this.Size = new System.Drawing.Size(405, 152);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nazwaTextBox;
        private System.Windows.Forms.TextBox hasloTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox powtorzHasloTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox bezHaslaCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox wymZmianaHaslaCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox blokadaCheckBox;
    }
}
