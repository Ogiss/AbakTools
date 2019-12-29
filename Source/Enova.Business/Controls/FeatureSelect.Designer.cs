namespace Enova.Business.Old.Controls
{
    partial class FeatureSelect
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
            this.featureComboBox = new System.Windows.Forms.ComboBox();
            this.valueComboBox = new System.Windows.Forms.ComboBox();
            this.groupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.valueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // featureComboBox
            // 
            this.featureComboBox.DataSource = this.groupBindingSource;
            this.featureComboBox.DisplayMember = "Name";
            this.featureComboBox.FormattingEnabled = true;
            this.featureComboBox.Location = new System.Drawing.Point(3, 3);
            this.featureComboBox.Name = "featureComboBox";
            this.featureComboBox.Size = new System.Drawing.Size(219, 21);
            this.featureComboBox.TabIndex = 0;
            this.featureComboBox.ValueMember = "ID";
            this.featureComboBox.SelectionChangeCommitted += new System.EventHandler(this.featureComboBox_SelectionChangeCommitted);
            // 
            // valueComboBox
            // 
            this.valueComboBox.DataSource = this.valueBindingSource;
            this.valueComboBox.DisplayMember = "Value";
            this.valueComboBox.FormattingEnabled = true;
            this.valueComboBox.Location = new System.Drawing.Point(228, 3);
            this.valueComboBox.Name = "valueComboBox";
            this.valueComboBox.Size = new System.Drawing.Size(231, 21);
            this.valueComboBox.TabIndex = 1;
            this.valueComboBox.ValueMember = "ID";
            // 
            // groupBindingSource
            // 
            this.groupBindingSource.DataSource = typeof(Enova.Business.Old.DB.FeatureDef);
            // 
            // valueBindingSource
            // 
            this.valueBindingSource.DataSource = typeof(Enova.Business.Old.DB.Dictionary);
            // 
            // FeatureSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.valueComboBox);
            this.Controls.Add(this.featureComboBox);
            this.Name = "FeatureSelect";
            this.Size = new System.Drawing.Size(462, 28);
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox featureComboBox;
        private System.Windows.Forms.BindingSource groupBindingSource;
        private System.Windows.Forms.ComboBox valueComboBox;
        private System.Windows.Forms.BindingSource valueBindingSource;
    }
}
