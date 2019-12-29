namespace Enova.Forms.CRM
{
    partial class KontrahentRozrachunkiPanel
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
            this.rozrachunkiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewControl = new BAL.Forms.Controls.GridViewControl();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rozrachunkiBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rozrachunkiBindingSource
            // 
            this.rozrachunkiBindingSource.DataSource = typeof(Enova.API.Kasa.RozrachunekIdx);
            // 
            // gridViewControl
            // 
            this.gridViewControl.DataContext = null;
            this.gridViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridViewControl.Location = new System.Drawing.Point(0, 0);
            this.gridViewControl.Name = "gridViewControl";
            this.gridViewControl.ReadOnly = false;
            this.gridViewControl.Size = new System.Drawing.Size(719, 492);
            this.gridViewControl.TabIndex = 12;
            // 
            // KontrahentRozrachunkiPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.gridViewControl);
            this.Name = "KontrahentRozrachunkiPanel";
            this.Size = new System.Drawing.Size(719, 492);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rozrachunkiBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource rozrachunkiBindingSource;
        private BAL.Forms.Controls.GridViewControl gridViewControl;
    }
}
