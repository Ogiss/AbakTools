namespace Enova.Business.Old.Forms
{
    partial class DataEditTabForm
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.mainTabPage = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).BeginInit();
            this.TabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(652, 547);
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(571, 547);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.mainTabPage);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(739, 582);
            this.TabControl.TabIndex = 2;
            // 
            // mainTabPage
            // 
            this.mainTabPage.Location = new System.Drawing.Point(4, 22);
            this.mainTabPage.Name = "mainTabPage";
            this.mainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainTabPage.Size = new System.Drawing.Size(731, 556);
            this.mainTabPage.TabIndex = 0;
            this.mainTabPage.Text = "Ogólne";
            this.mainTabPage.UseVisualStyleBackColor = true;
            // 
            // DataEditTabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(739, 582);
            this.Controls.Add(this.TabControl);
            this.Name = "DataEditTabForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.SetChildIndex(this.TabControl, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.anulujButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TabControl TabControl;
        protected System.Windows.Forms.TabPage mainTabPage;
    }
}
