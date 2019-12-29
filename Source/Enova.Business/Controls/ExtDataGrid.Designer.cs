namespace Enova.Business.Old.Controls
{
    partial class ExtDataGrid
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
            this.DataGridControl = new Enova.Business.Old.Controls.DataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridControl)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridControl
            // 
            this.DataGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridControl.AutoGenerateColumns = false;
            this.DataGridControl.Location = new System.Drawing.Point(0, 0);
            this.DataGridControl.Name = "DataGridControl";
            this.DataGridControl.Size = new System.Drawing.Size(813, 444);
            this.DataGridControl.TabIndex = 0;
            // 
            // ExtDataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DataGridControl);
            this.Name = "ExtDataGrid";
            this.Size = new System.Drawing.Size(813, 444);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DataGrid DataGridControl;
    }
}
