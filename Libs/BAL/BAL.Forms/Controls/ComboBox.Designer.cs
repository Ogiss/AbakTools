namespace BAL.Forms.Controls
{
    partial class ComboBox
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
            this.comboBoxControl = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxControl
            // 
            this.comboBoxControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxControl.FormattingEnabled = true;
            this.comboBoxControl.Location = new System.Drawing.Point(0, 0);
            this.comboBoxControl.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxControl.Name = "comboBoxControl";
            this.comboBoxControl.Size = new System.Drawing.Size(120, 21);
            this.comboBoxControl.TabIndex = 0;
            this.comboBoxControl.SelectionChangeCommitted += new System.EventHandler(this.comboBoxControl_SelectionChangeCommitted);
            // 
            // ComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.comboBoxControl);
            this.Name = "ComboBox";
            this.Size = new System.Drawing.Size(120, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxControl;
    }
}
