namespace AbakTools.CRM.Forms
{
    partial class RodzajKorespondencjiEditForm
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
            this.kosztTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(351, 82);
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(270, 82);
            // 
            // DataSourceBinding
            // 
            this.DataSourceBinding.DataSource = typeof(Enova.Business.Old.DB.Web.RodzajKorespondencji);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nazwa:";
            // 
            // nazwaTextBox
            // 
            this.nazwaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "Nazwa", true));
            this.nazwaTextBox.Location = new System.Drawing.Point(61, 13);
            this.nazwaTextBox.Name = "nazwaTextBox";
            this.nazwaTextBox.Size = new System.Drawing.Size(365, 20);
            this.nazwaTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Koszt:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // kosztTextBox
            // 
            this.kosztTextBox.Location = new System.Drawing.Point(61, 39);
            this.kosztTextBox.Name = "kosztTextBox";
            this.kosztTextBox.Size = new System.Drawing.Size(120, 20);
            this.kosztTextBox.TabIndex = 5;
            this.kosztTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.kosztTextBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // RodzajKorespondencjiEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(438, 117);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nazwaTextBox);
            this.Controls.Add(this.kosztTextBox);
            this.Controls.Add(this.label2);
            this.Name = "RodzajKorespondencjiEditForm";
            this.Text = "Rodzaj korespondencji";
            this.Load += new System.EventHandler(this.RodzajKorespondencjiEditForm_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.kosztTextBox, 0);
            this.Controls.SetChildIndex(this.nazwaTextBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
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
        private System.Windows.Forms.TextBox kosztTextBox;
    }
}
