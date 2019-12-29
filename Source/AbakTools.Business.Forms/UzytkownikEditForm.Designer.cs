namespace AbakTools.Business.Forms
{
    partial class UzytkownikEditForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.repasswordTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.adminCheckBox = new System.Windows.Forms.CheckBox();
            this.agentCheckBox = new System.Windows.Forms.CheckBox();
            this.agentcodeTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.changePasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.warehousemanCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.ogolneTabPage = new System.Windows.Forms.TabPage();
            this.synchProfTabPage = new System.Windows.Forms.TabPage();
            this.synchZamFiltrAgentCheckBox = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.synchZamowEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.synchTowaryEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.synchKontrahFiltrAgentCheckBox = new System.Windows.Forms.CheckBox();
            this.synchKontrahEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.synchAgentKodeTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.enovaLoginTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.enovaPasswordTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).BeginInit();
            this.tabControl.SuspendLayout();
            this.ogolneTabPage.SuspendLayout();
            this.synchProfTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(352, 337);
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(271, 337);
            // 
            // DataSourceBinding
            // 
            this.DataSourceBinding.DataSource = typeof(Enova.Business.Old.DB.Web.User);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Użytkownik:";
            // 
            // userTextBox
            // 
            this.userTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "Login", true));
            this.userTextBox.Location = new System.Drawing.Point(93, 13);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.Size = new System.Drawing.Size(168, 20);
            this.userTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hasło:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Powtórz hasło:";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(93, 39);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(168, 20);
            this.passwordTextBox.TabIndex = 6;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // repasswordTextBox
            // 
            this.repasswordTextBox.Location = new System.Drawing.Point(93, 65);
            this.repasswordTextBox.Name = "repasswordTextBox";
            this.repasswordTextBox.Size = new System.Drawing.Size(168, 20);
            this.repasswordTextBox.TabIndex = 7;
            this.repasswordTextBox.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Administrator:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Agent:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Kod agenta:";
            // 
            // adminCheckBox
            // 
            this.adminCheckBox.AutoSize = true;
            this.adminCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.DataSourceBinding, "IsAdmin", true));
            this.adminCheckBox.Location = new System.Drawing.Point(94, 91);
            this.adminCheckBox.Name = "adminCheckBox";
            this.adminCheckBox.Size = new System.Drawing.Size(15, 14);
            this.adminCheckBox.TabIndex = 11;
            this.adminCheckBox.UseVisualStyleBackColor = true;
            // 
            // agentCheckBox
            // 
            this.agentCheckBox.AutoSize = true;
            this.agentCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.DataSourceBinding, "IsAgent", true));
            this.agentCheckBox.Location = new System.Drawing.Point(159, 91);
            this.agentCheckBox.Name = "agentCheckBox";
            this.agentCheckBox.Size = new System.Drawing.Size(15, 14);
            this.agentCheckBox.TabIndex = 12;
            this.agentCheckBox.UseVisualStyleBackColor = true;
            // 
            // agentcodeTextBox
            // 
            this.agentcodeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "AgentCode", true));
            this.agentcodeTextBox.Location = new System.Drawing.Point(94, 111);
            this.agentcodeTextBox.Name = "agentcodeTextBox";
            this.agentcodeTextBox.Size = new System.Drawing.Size(168, 20);
            this.agentcodeTextBox.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Użytkowni musi zmienić hasło:";
            // 
            // changePasswordCheckBox
            // 
            this.changePasswordCheckBox.AutoSize = true;
            this.changePasswordCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.DataSourceBinding, "ChangePassword", true));
            this.changePasswordCheckBox.Location = new System.Drawing.Point(162, 149);
            this.changePasswordCheckBox.Name = "changePasswordCheckBox";
            this.changePasswordCheckBox.Size = new System.Drawing.Size(15, 14);
            this.changePasswordCheckBox.TabIndex = 15;
            this.changePasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // warehousemanCheckBox
            // 
            this.warehousemanCheckBox.AutoSize = true;
            this.warehousemanCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.DataSourceBinding, "IsWarehouseman", true));
            this.warehousemanCheckBox.Location = new System.Drawing.Point(250, 90);
            this.warehousemanCheckBox.Name = "warehousemanCheckBox";
            this.warehousemanCheckBox.Size = new System.Drawing.Size(15, 14);
            this.warehousemanCheckBox.TabIndex = 16;
            this.warehousemanCheckBox.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(180, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Magazynier:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.ogolneTabPage);
            this.tabControl.Controls.Add(this.synchProfTabPage);
            this.tabControl.Location = new System.Drawing.Point(1, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(437, 329);
            this.tabControl.TabIndex = 18;
            // 
            // ogolneTabPage
            // 
            this.ogolneTabPage.Controls.Add(this.groupBox1);
            this.ogolneTabPage.Controls.Add(this.passwordTextBox);
            this.ogolneTabPage.Controls.Add(this.label8);
            this.ogolneTabPage.Controls.Add(this.label1);
            this.ogolneTabPage.Controls.Add(this.warehousemanCheckBox);
            this.ogolneTabPage.Controls.Add(this.userTextBox);
            this.ogolneTabPage.Controls.Add(this.changePasswordCheckBox);
            this.ogolneTabPage.Controls.Add(this.label2);
            this.ogolneTabPage.Controls.Add(this.label7);
            this.ogolneTabPage.Controls.Add(this.label3);
            this.ogolneTabPage.Controls.Add(this.agentcodeTextBox);
            this.ogolneTabPage.Controls.Add(this.repasswordTextBox);
            this.ogolneTabPage.Controls.Add(this.agentCheckBox);
            this.ogolneTabPage.Controls.Add(this.label4);
            this.ogolneTabPage.Controls.Add(this.adminCheckBox);
            this.ogolneTabPage.Controls.Add(this.label5);
            this.ogolneTabPage.Controls.Add(this.label6);
            this.ogolneTabPage.Location = new System.Drawing.Point(4, 22);
            this.ogolneTabPage.Name = "ogolneTabPage";
            this.ogolneTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ogolneTabPage.Size = new System.Drawing.Size(429, 303);
            this.ogolneTabPage.TabIndex = 0;
            this.ogolneTabPage.Text = "Ogólne";
            this.ogolneTabPage.UseVisualStyleBackColor = true;
            // 
            // synchProfTabPage
            // 
            this.synchProfTabPage.Controls.Add(this.synchZamFiltrAgentCheckBox);
            this.synchProfTabPage.Controls.Add(this.label14);
            this.synchProfTabPage.Controls.Add(this.synchZamowEnabledCheckBox);
            this.synchProfTabPage.Controls.Add(this.label13);
            this.synchProfTabPage.Controls.Add(this.synchTowaryEnabledCheckBox);
            this.synchProfTabPage.Controls.Add(this.label12);
            this.synchProfTabPage.Controls.Add(this.synchKontrahFiltrAgentCheckBox);
            this.synchProfTabPage.Controls.Add(this.synchKontrahEnabledCheckBox);
            this.synchProfTabPage.Controls.Add(this.label11);
            this.synchProfTabPage.Controls.Add(this.label10);
            this.synchProfTabPage.Controls.Add(this.synchAgentKodeTextBox);
            this.synchProfTabPage.Controls.Add(this.label9);
            this.synchProfTabPage.Location = new System.Drawing.Point(4, 22);
            this.synchProfTabPage.Name = "synchProfTabPage";
            this.synchProfTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.synchProfTabPage.Size = new System.Drawing.Size(429, 303);
            this.synchProfTabPage.TabIndex = 1;
            this.synchProfTabPage.Text = "Synchronizacja";
            this.synchProfTabPage.UseVisualStyleBackColor = true;
            // 
            // synchZamFiltrAgentCheckBox
            // 
            this.synchZamFiltrAgentCheckBox.AutoSize = true;
            this.synchZamFiltrAgentCheckBox.Location = new System.Drawing.Point(225, 157);
            this.synchZamFiltrAgentCheckBox.Name = "synchZamFiltrAgentCheckBox";
            this.synchZamFiltrAgentCheckBox.Size = new System.Drawing.Size(15, 14);
            this.synchZamFiltrAgentCheckBox.TabIndex = 11;
            this.synchZamFiltrAgentCheckBox.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(38, 157);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(181, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "Filtruj zamówienia po kodzie przedst.:";
            // 
            // synchZamowEnabledCheckBox
            // 
            this.synchZamowEnabledCheckBox.AutoSize = true;
            this.synchZamowEnabledCheckBox.Location = new System.Drawing.Point(225, 128);
            this.synchZamowEnabledCheckBox.Name = "synchZamowEnabledCheckBox";
            this.synchZamowEnabledCheckBox.Size = new System.Drawing.Size(15, 14);
            this.synchZamowEnabledCheckBox.TabIndex = 9;
            this.synchZamowEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(72, 129);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(147, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Synch. zamówień dozwolona:";
            // 
            // synchTowaryEnabledCheckBox
            // 
            this.synchTowaryEnabledCheckBox.AutoSize = true;
            this.synchTowaryEnabledCheckBox.Location = new System.Drawing.Point(225, 102);
            this.synchTowaryEnabledCheckBox.Name = "synchTowaryEnabledCheckBox";
            this.synchTowaryEnabledCheckBox.Size = new System.Drawing.Size(15, 14);
            this.synchTowaryEnabledCheckBox.TabIndex = 7;
            this.synchTowaryEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(46, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(173, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Synch. bazy towarowej dozwolona:";
            // 
            // synchKontrahFiltrAgentCheckBox
            // 
            this.synchKontrahFiltrAgentCheckBox.AutoSize = true;
            this.synchKontrahFiltrAgentCheckBox.Location = new System.Drawing.Point(225, 71);
            this.synchKontrahFiltrAgentCheckBox.Name = "synchKontrahFiltrAgentCheckBox";
            this.synchKontrahFiltrAgentCheckBox.Size = new System.Drawing.Size(15, 14);
            this.synchKontrahFiltrAgentCheckBox.TabIndex = 5;
            this.synchKontrahFiltrAgentCheckBox.UseVisualStyleBackColor = true;
            // 
            // synchKontrahEnabledCheckBox
            // 
            this.synchKontrahEnabledCheckBox.AutoSize = true;
            this.synchKontrahEnabledCheckBox.Location = new System.Drawing.Point(225, 43);
            this.synchKontrahEnabledCheckBox.Name = "synchKontrahEnabledCheckBox";
            this.synchKontrahEnabledCheckBox.Size = new System.Drawing.Size(15, 14);
            this.synchKontrahEnabledCheckBox.TabIndex = 4;
            this.synchKontrahEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(191, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Filtruj kontrahentow po kodzie przedst.:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(54, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Synch. kontrahentów dozwolona:";
            // 
            // synchAgentKodeTextBox
            // 
            this.synchAgentKodeTextBox.Location = new System.Drawing.Point(94, 9);
            this.synchAgentKodeTextBox.Name = "synchAgentKodeTextBox";
            this.synchAgentKodeTextBox.Size = new System.Drawing.Size(100, 20);
            this.synchAgentKodeTextBox.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Kod przedst.:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.enovaPasswordTextBox);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.enovaLoginTextBox);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Location = new System.Drawing.Point(13, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 87);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enova";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(22, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(36, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Login:";
            // 
            // enovaLoginTextBox
            // 
            this.enovaLoginTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "EnovaOperator", true));
            this.enovaLoginTextBox.Location = new System.Drawing.Point(64, 17);
            this.enovaLoginTextBox.Name = "enovaLoginTextBox";
            this.enovaLoginTextBox.Size = new System.Drawing.Size(167, 20);
            this.enovaLoginTextBox.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(19, 46);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Hasło:";
            // 
            // enovaPasswordTextBox
            // 
            this.enovaPasswordTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataSourceBinding, "EnovaPassword", true));
            this.enovaPasswordTextBox.Location = new System.Drawing.Point(64, 43);
            this.enovaPasswordTextBox.Name = "enovaPasswordTextBox";
            this.enovaPasswordTextBox.Size = new System.Drawing.Size(167, 20);
            this.enovaPasswordTextBox.TabIndex = 3;
            this.enovaPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // UzytkownikEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(439, 372);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UzytkownikEditForm";
            this.Text = "Edycja użytkownika";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UzytkownikEditForm_FormClosing);
            this.Load += new System.EventHandler(this.UzytkownikEditForm_Load);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.anulujButton, 0);
            this.Controls.SetChildIndex(this.tabControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceBinding)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ogolneTabPage.ResumeLayout(false);
            this.ogolneTabPage.PerformLayout();
            this.synchProfTabPage.ResumeLayout(false);
            this.synchProfTabPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox repasswordTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox adminCheckBox;
        private System.Windows.Forms.CheckBox agentCheckBox;
        private System.Windows.Forms.TextBox agentcodeTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox changePasswordCheckBox;
        private System.Windows.Forms.CheckBox warehousemanCheckBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage ogolneTabPage;
        private System.Windows.Forms.TabPage synchProfTabPage;
        private System.Windows.Forms.CheckBox synchZamFiltrAgentCheckBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox synchZamowEnabledCheckBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox synchTowaryEnabledCheckBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox synchKontrahFiltrAgentCheckBox;
        private System.Windows.Forms.CheckBox synchKontrahEnabledCheckBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox synchAgentKodeTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox enovaPasswordTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox enovaLoginTextBox;
        private System.Windows.Forms.Label label15;
    }
}
