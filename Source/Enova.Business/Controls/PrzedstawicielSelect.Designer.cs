namespace Enova.Business.Old.Controls
{
    partial class PrzedstawicielSelect
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Przedstawiciel:";
            // 
            // comboBox
            // 
            this.comboBox.DataSource = this.bindingSource;
            this.comboBox.DisplayMember = "Value";
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(86, 0);
            this.comboBox.MaxDropDownItems = 20;
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(91, 21);
            this.comboBox.TabIndex = 1;
            this.comboBox.ValueMember = "Value";
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(Enova.Business.Old.DB.Dictionary);
            // 
            // PrzedstawicielSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.label1);
            this.Name = "PrzedstawicielSelect";
            this.Size = new System.Drawing.Size(177, 22);
            this.Load += new System.EventHandler(this.PrzedstawicielSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.BindingSource bindingSource;
    }
}
