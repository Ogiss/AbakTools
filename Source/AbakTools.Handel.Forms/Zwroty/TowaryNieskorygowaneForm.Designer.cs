namespace AbakTools.Zwroty.Forms
{
    partial class TowaryNieskorygowaneForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.agregujCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dostawcyComboBox = new System.Windows.Forms.ComboBox();
            this.przedstawicielComboBox = new System.Windows.Forms.ComboBox();
            this.dostawcyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.przedstawicielBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dostawcyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.przedstawicielBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dostawca:";
            // 
            // agregujCheckBox
            // 
            this.agregujCheckBox.AutoSize = true;
            this.agregujCheckBox.Checked = true;
            this.agregujCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.agregujCheckBox.Location = new System.Drawing.Point(103, 69);
            this.agregujCheckBox.Name = "agregujCheckBox";
            this.agregujCheckBox.Size = new System.Drawing.Size(15, 14);
            this.agregujCheckBox.TabIndex = 2;
            this.agregujCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Agreguj pozycje:";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(313, 108);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Zatwierdź";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(232, 108);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Przedstawiciel:";
            // 
            // dostawcyComboBox
            // 
            this.dostawcyComboBox.DataSource = this.dostawcyBindingSource;
            this.dostawcyComboBox.DisplayMember = "Nazwa";
            this.dostawcyComboBox.FormattingEnabled = true;
            this.dostawcyComboBox.Location = new System.Drawing.Point(103, 15);
            this.dostawcyComboBox.Name = "dostawcyComboBox";
            this.dostawcyComboBox.Size = new System.Drawing.Size(242, 21);
            this.dostawcyComboBox.TabIndex = 7;
            this.dostawcyComboBox.ValueMember = "ID";
            // 
            // przedstawicielComboBox
            // 
            this.przedstawicielComboBox.DataSource = this.przedstawicielBindingSource;
            this.przedstawicielComboBox.DisplayMember = "Kod";
            this.przedstawicielComboBox.FormattingEnabled = true;
            this.przedstawicielComboBox.Location = new System.Drawing.Point(103, 42);
            this.przedstawicielComboBox.Name = "przedstawicielComboBox";
            this.przedstawicielComboBox.Size = new System.Drawing.Size(121, 21);
            this.przedstawicielComboBox.TabIndex = 8;
            this.przedstawicielComboBox.ValueMember = "ID";
            // 
            // dostawcyBindingSource
            // 
            this.dostawcyBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Dostawca);
            // 
            // przedstawicielBindingSource
            // 
            this.przedstawicielBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Kontrahent);
            // 
            // TowaryNieskorygowaneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 143);
            this.Controls.Add(this.przedstawicielComboBox);
            this.Controls.Add(this.dostawcyComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.agregujCheckBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TowaryNieskorygowaneForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Towary nieskorygowane";
            this.Load += new System.EventHandler(this.TowaryNieskorygowaneForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dostawcyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.przedstawicielBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox agregujCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox dostawcyComboBox;
        private System.Windows.Forms.ComboBox przedstawicielComboBox;
        private System.Windows.Forms.BindingSource dostawcyBindingSource;
        private System.Windows.Forms.BindingSource przedstawicielBindingSource;
    }
}