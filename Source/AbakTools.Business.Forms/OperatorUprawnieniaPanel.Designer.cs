namespace AbakTools.Business.Forms
{
    partial class OperatorUprawnieniaPanel
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
            this.przedstawicielCheckBox = new System.Windows.Forms.CheckBox();
            this.magazynierCheckBox = new System.Windows.Forms.CheckBox();
            this.pakowaczCheckBox = new System.Windows.Forms.CheckBox();
            this.kierownikCheckBox = new System.Windows.Forms.CheckBox();
            this.adminCheckBox = new System.Windows.Forms.CheckBox();
            this.superAdminCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // przedstawicielCheckBox
            // 
            this.przedstawicielCheckBox.AutoSize = true;
            this.przedstawicielCheckBox.Location = new System.Drawing.Point(31, 23);
            this.przedstawicielCheckBox.Name = "przedstawicielCheckBox";
            this.przedstawicielCheckBox.Size = new System.Drawing.Size(93, 17);
            this.przedstawicielCheckBox.TabIndex = 0;
            this.przedstawicielCheckBox.Text = "Przedstawiciel";
            this.przedstawicielCheckBox.UseVisualStyleBackColor = true;
            // 
            // magazynierCheckBox
            // 
            this.magazynierCheckBox.AutoSize = true;
            this.magazynierCheckBox.Location = new System.Drawing.Point(31, 46);
            this.magazynierCheckBox.Name = "magazynierCheckBox";
            this.magazynierCheckBox.Size = new System.Drawing.Size(80, 17);
            this.magazynierCheckBox.TabIndex = 1;
            this.magazynierCheckBox.Text = "Magazynier";
            this.magazynierCheckBox.UseVisualStyleBackColor = true;
            // 
            // pakowaczCheckBox
            // 
            this.pakowaczCheckBox.AutoSize = true;
            this.pakowaczCheckBox.Location = new System.Drawing.Point(188, 23);
            this.pakowaczCheckBox.Name = "pakowaczCheckBox";
            this.pakowaczCheckBox.Size = new System.Drawing.Size(76, 17);
            this.pakowaczCheckBox.TabIndex = 2;
            this.pakowaczCheckBox.Text = "Pakowacz";
            this.pakowaczCheckBox.UseVisualStyleBackColor = true;
            // 
            // kierownikCheckBox
            // 
            this.kierownikCheckBox.AutoSize = true;
            this.kierownikCheckBox.Location = new System.Drawing.Point(188, 46);
            this.kierownikCheckBox.Name = "kierownikCheckBox";
            this.kierownikCheckBox.Size = new System.Drawing.Size(72, 17);
            this.kierownikCheckBox.TabIndex = 3;
            this.kierownikCheckBox.Text = "Kierownik";
            this.kierownikCheckBox.UseVisualStyleBackColor = true;
            // 
            // adminCheckBox
            // 
            this.adminCheckBox.AutoSize = true;
            this.adminCheckBox.Location = new System.Drawing.Point(31, 81);
            this.adminCheckBox.Name = "adminCheckBox";
            this.adminCheckBox.Size = new System.Drawing.Size(86, 17);
            this.adminCheckBox.TabIndex = 4;
            this.adminCheckBox.Text = "Administrator";
            this.adminCheckBox.UseVisualStyleBackColor = true;
            // 
            // superAdminCheckBox
            // 
            this.superAdminCheckBox.AutoSize = true;
            this.superAdminCheckBox.Location = new System.Drawing.Point(31, 104);
            this.superAdminCheckBox.Name = "superAdminCheckBox";
            this.superAdminCheckBox.Size = new System.Drawing.Size(83, 17);
            this.superAdminCheckBox.TabIndex = 5;
            this.superAdminCheckBox.Text = "SuperAdmin";
            this.superAdminCheckBox.UseVisualStyleBackColor = true;
            // 
            // OperatorUprawnieniaPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.superAdminCheckBox);
            this.Controls.Add(this.adminCheckBox);
            this.Controls.Add(this.kierownikCheckBox);
            this.Controls.Add(this.pakowaczCheckBox);
            this.Controls.Add(this.magazynierCheckBox);
            this.Controls.Add(this.przedstawicielCheckBox);
            this.Name = "OperatorUprawnieniaPanel";
            this.Size = new System.Drawing.Size(293, 177);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox przedstawicielCheckBox;
        private System.Windows.Forms.CheckBox magazynierCheckBox;
        private System.Windows.Forms.CheckBox pakowaczCheckBox;
        private System.Windows.Forms.CheckBox kierownikCheckBox;
        private System.Windows.Forms.CheckBox adminCheckBox;
        private System.Windows.Forms.CheckBox superAdminCheckBox;
    }
}
