namespace AbakTools.Towary.Forms
{
    partial class MapowanieTowaruEditForm
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
            this.srcSelectTextBox = new Enova.Business.Old.Controls.SelectTextBox();
            this.dstSelectTextBox = new Enova.Business.Old.Controls.SelectTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(695, 131);
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(614, 131);
            // 
            // srcSelectTextBox
            // 
            this.srcSelectTextBox.DataSource = null;
            this.srcSelectTextBox.DisplayMember = "FullName";
            this.srcSelectTextBox.Location = new System.Drawing.Point(64, 29);
            this.srcSelectTextBox.Name = "srcSelectTextBox";
            this.srcSelectTextBox.SelectedItem = null;
            this.srcSelectTextBox.SelectFormType = null;
            this.srcSelectTextBox.Size = new System.Drawing.Size(706, 24);
            this.srcSelectTextBox.TabIndex = 2;
            this.srcSelectTextBox.ValueMember = "ID";
            // 
            // dstSelectTextBox
            // 
            this.dstSelectTextBox.DataSource = null;
            this.dstSelectTextBox.DisplayMember = "FullName";
            this.dstSelectTextBox.Location = new System.Drawing.Point(64, 59);
            this.dstSelectTextBox.Name = "dstSelectTextBox";
            this.dstSelectTextBox.SelectedItem = null;
            this.dstSelectTextBox.SelectFormType = null;
            this.dstSelectTextBox.Size = new System.Drawing.Size(706, 21);
            this.dstSelectTextBox.TabIndex = 3;
            this.dstSelectTextBox.ValueMember = "ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Źródło:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cel:";
            // 
            // MapowanieTowaruEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(782, 166);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.srcSelectTextBox);
            this.Controls.Add(this.dstSelectTextBox);
            this.Controls.Add(this.label2);
            this.Name = "MapowanieTowaruEditForm";
            this.Text = "Mapowanie towaru";
            this.Load += new System.EventHandler(this.MapowanieTowaruEditForm_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dstSelectTextBox, 0);
            this.Controls.SetChildIndex(this.srcSelectTextBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.anulujButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Enova.Business.Old.Controls.SelectTextBox srcSelectTextBox;
        private Enova.Business.Old.Controls.SelectTextBox dstSelectTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
