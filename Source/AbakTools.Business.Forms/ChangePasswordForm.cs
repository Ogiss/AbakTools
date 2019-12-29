using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Business.Forms
{
    public partial class ChangePasswordForm : Form
    {
        private bool canClose = false;
        public Enova.Business.Old.DB.Web.User Login = null;

        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            passwordTextBox.Focus();
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                repasswordTextBox.Focus();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.Text != repasswordTextBox.Text)
            {
                MessageBox.Show("Hasła są różne. Wprowadź ponownie", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                passwordTextBox.Text = null;
                repasswordTextBox.Text = null;
                passwordTextBox.Focus();
                return;
            }

            Login.SetPassword(passwordTextBox.Text);
            canClose = true;
            this.Close();
        }

        private void repasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                okButton_Click(null, null);
        }

        private void ChangePasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
                e.Cancel = true;
        }
    }
}
