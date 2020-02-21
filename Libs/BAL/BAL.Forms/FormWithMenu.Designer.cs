namespace BAL.Forms
{
    partial class FormWithMenu
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
            this.MainMenu = new BAL.Forms.Controls.MenuBar();
            this.StatusLine = new BAL.Forms.Controls.StatusLine();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1014, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuBar1";
            // 
            // StatusLine
            // 
            this.StatusLine.Location = new System.Drawing.Point(0, 460);
            this.StatusLine.Name = "StatusLine";
            this.StatusLine.Size = new System.Drawing.Size(1014, 22);
            this.StatusLine.TabIndex = 1;
            this.StatusLine.Text = "statusLine1";
            // 
            // FormWithMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1014, 482);
            this.Controls.Add(this.StatusLine);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "FormWithMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormWithMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Controls.MenuBar MainMenu;
        public Controls.StatusLine StatusLine;


    }
}
