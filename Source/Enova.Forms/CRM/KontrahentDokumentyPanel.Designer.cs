namespace Enova.Forms.CRM
{
    partial class KontrahentDokumentyPanel
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
            this.gridViewControl = new BAL.Forms.Controls.GridViewControl();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewControl
            // 
            this.gridViewControl.DataContext = null;
            this.gridViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridViewControl.Location = new System.Drawing.Point(0, 0);
            this.gridViewControl.Name = "gridViewControl";
            this.gridViewControl.Size = new System.Drawing.Size(720, 506);
            this.gridViewControl.TabIndex = 2;
            // 
            // KontrahentDokumentyPanelTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.gridViewControl);
            this.Name = "KontrahentDokumentyPanelTest";
            this.Size = new System.Drawing.Size(720, 506);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BAL.Forms.Controls.GridViewControl gridViewControl;

    }
}
