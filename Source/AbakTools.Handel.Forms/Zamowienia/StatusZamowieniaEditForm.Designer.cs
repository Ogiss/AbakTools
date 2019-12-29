namespace EnovaTools.Forms.Web
{
    partial class StatusZamowieniaEditForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.kolorTextBox = new System.Windows.Forms.TextBox();
            this.fakturaCheckBox = new System.Windows.Forms.CheckBox();
            this.ukrytyCheckBox = new System.Windows.Forms.CheckBox();
            this.emailCheckBox = new System.Windows.Forms.CheckBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.kolorButton = new System.Windows.Forms.Button();
            this.pakowanieCheckBox = new System.Windows.Forms.CheckBox();
            this.spakowaneCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(323, 219);
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(242, 219);
            // 
            // DataSourceBinding
            // 
            this.DataSourceBinding.DataSource = typeof(Enova.Business.Old.DB.Web.StatusZamowienia);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nazwa:";
            // 
            // nazwaTextBox
            // 
            this.nazwaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "Nazwa", true));
            this.nazwaTextBox.Location = new System.Drawing.Point(77, 16);
            this.nazwaTextBox.Name = "nazwaTextBox";
            this.nazwaTextBox.Size = new System.Drawing.Size(321, 20);
            this.nazwaTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Kolor:";
            // 
            // kolorTextBox
            // 
            this.kolorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "Kolor", true));
            this.kolorTextBox.Location = new System.Drawing.Point(77, 42);
            this.kolorTextBox.Name = "kolorTextBox";
            this.kolorTextBox.Size = new System.Drawing.Size(100, 20);
            this.kolorTextBox.TabIndex = 5;
            // 
            // fakturaCheckBox
            // 
            this.fakturaCheckBox.AutoSize = true;
            this.fakturaCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.DataSourceBinding, "Faktura", true));
            this.fakturaCheckBox.Location = new System.Drawing.Point(77, 79);
            this.fakturaCheckBox.Name = "fakturaCheckBox";
            this.fakturaCheckBox.Size = new System.Drawing.Size(264, 17);
            this.fakturaCheckBox.TabIndex = 9;
            this.fakturaCheckBox.Text = "Pozwól klientowi pobrać i zobaczyć w fakturę PDF";
            this.fakturaCheckBox.UseVisualStyleBackColor = true;
            // 
            // ukrytyCheckBox
            // 
            this.ukrytyCheckBox.AutoSize = true;
            this.ukrytyCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.DataSourceBinding, "Ukryty", true));
            this.ukrytyCheckBox.Location = new System.Drawing.Point(77, 102);
            this.ukrytyCheckBox.Name = "ukrytyCheckBox";
            this.ukrytyCheckBox.Size = new System.Drawing.Size(208, 17);
            this.ukrytyCheckBox.TabIndex = 10;
            this.ukrytyCheckBox.Text = "Ukryj ten status zamówienia dla klienta";
            this.ukrytyCheckBox.UseVisualStyleBackColor = true;
            // 
            // emailCheckBox
            // 
            this.emailCheckBox.AutoSize = true;
            this.emailCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.DataSourceBinding, "WyslacEmail", true));
            this.emailCheckBox.Location = new System.Drawing.Point(77, 125);
            this.emailCheckBox.Name = "emailCheckBox";
            this.emailCheckBox.Size = new System.Drawing.Size(321, 17);
            this.emailCheckBox.TabIndex = 11;
            this.emailCheckBox.Text = "Wyślij wiadomość klientowi, gdy zamówienie zmieni swój status";
            this.emailCheckBox.UseVisualStyleBackColor = true;
            // 
            // kolorButton
            // 
            this.kolorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kolorButton.Location = new System.Drawing.Point(183, 40);
            this.kolorButton.Name = "kolorButton";
            this.kolorButton.Size = new System.Drawing.Size(26, 23);
            this.kolorButton.TabIndex = 126;
            this.kolorButton.UseVisualStyleBackColor = true;
            this.kolorButton.Click += new System.EventHandler(this.kolorButton_Click);
            // 
            // pakowanieCheckBox
            // 
            this.pakowanieCheckBox.AutoSize = true;
            this.pakowanieCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.DataSourceBinding, "Pakowanie", true));
            this.pakowanieCheckBox.Location = new System.Drawing.Point(77, 149);
            this.pakowanieCheckBox.Name = "pakowanieCheckBox";
            this.pakowanieCheckBox.Size = new System.Drawing.Size(127, 17);
            this.pakowanieCheckBox.TabIndex = 127;
            this.pakowanieCheckBox.Text = "W trakcie pakowania";
            this.pakowanieCheckBox.UseVisualStyleBackColor = true;
            // 
            // spakowaneCheckBox
            // 
            this.spakowaneCheckBox.AutoSize = true;
            this.spakowaneCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.DataSourceBinding, "Spakowane", true));
            this.spakowaneCheckBox.Location = new System.Drawing.Point(77, 173);
            this.spakowaneCheckBox.Name = "spakowaneCheckBox";
            this.spakowaneCheckBox.Size = new System.Drawing.Size(83, 17);
            this.spakowaneCheckBox.TabIndex = 128;
            this.spakowaneCheckBox.Text = "Spakowane";
            this.spakowaneCheckBox.UseVisualStyleBackColor = true;
            // 
            // StatusZamowieniaEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(410, 254);
            this.Controls.Add(this.spakowaneCheckBox);
            this.Controls.Add(this.pakowanieCheckBox);
            this.Controls.Add(this.kolorButton);
            this.Controls.Add(this.kolorTextBox);
            this.Controls.Add(this.emailCheckBox);
            this.Controls.Add(this.ukrytyCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fakturaCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nazwaTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StatusZamowieniaEditForm";
            this.Text = "Status zamowienia";
            this.Load += new System.EventHandler(this.StatusZamowieniaEditForm_Load);
            this.Controls.SetChildIndex(this.nazwaTextBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.fakturaCheckBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ukrytyCheckBox, 0);
            this.Controls.SetChildIndex(this.emailCheckBox, 0);
            this.Controls.SetChildIndex(this.kolorTextBox, 0);
            this.Controls.SetChildIndex(this.kolorButton, 0);
            this.Controls.SetChildIndex(this.pakowanieCheckBox, 0);
            this.Controls.SetChildIndex(this.spakowaneCheckBox, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.anulujButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nazwaTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox kolorTextBox;
        private System.Windows.Forms.CheckBox fakturaCheckBox;
        private System.Windows.Forms.CheckBox ukrytyCheckBox;
        private System.Windows.Forms.CheckBox emailCheckBox;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button kolorButton;
        private System.Windows.Forms.CheckBox pakowanieCheckBox;
        private System.Windows.Forms.CheckBox spakowaneCheckBox;
    }
}
