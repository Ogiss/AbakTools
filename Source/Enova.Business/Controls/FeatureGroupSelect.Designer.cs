namespace Enova.Business.Old.Controls
{
    partial class FeatureGroupSelect
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
            this.label = new System.Windows.Forms.Label();
            this.grupaComboBox = new System.Windows.Forms.ComboBox();
            this.grupyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grupyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(3, 3);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(39, 13);
            this.label.TabIndex = 0;
            this.label.Text = "Grupa:";
            // 
            // grupaComboBox
            // 
            this.grupaComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grupaComboBox.DataSource = this.grupyBindingSource;
            this.grupaComboBox.DisplayMember = "Name";
            this.grupaComboBox.FormattingEnabled = true;
            this.grupaComboBox.Location = new System.Drawing.Point(45, 0);
            this.grupaComboBox.MaxDropDownItems = 20;
            this.grupaComboBox.Name = "grupaComboBox";
            this.grupaComboBox.Size = new System.Drawing.Size(195, 21);
            this.grupaComboBox.TabIndex = 1;
            this.grupaComboBox.ValueMember = "ID";
            this.grupaComboBox.SelectionChangeCommitted += new System.EventHandler(this.grupaComboBox_SelectionChangeCommitted);
            // 
            // grupyBindingSource
            // 
            this.grupyBindingSource.DataSource = typeof(Enova.Business.Old.DB.FeatureDef);
            // 
            // FeatureGroupSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grupaComboBox);
            this.Controls.Add(this.label);
            this.Name = "FeatureGroupSelect";
            this.Size = new System.Drawing.Size(240, 20);
            this.Load += new System.EventHandler(this.FeatureGroupSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grupyBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ComboBox grupaComboBox;
        private System.Windows.Forms.BindingSource grupyBindingSource;
    }
}
