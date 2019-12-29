using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AbakTools.Business.Forms;

namespace EnovaToolsExplorer
{
    public partial class MainForm : BAL.Forms.StdMainForm
    {
        #region Fields

        private object statusLineSynch = new object();

        #endregion

        #region Properties

        public override string StatusLineText
        {
            get
            {
                lock (statusLineSynch)
                    return statusLineText.Text;
            }
            set
            {
                setStatusLineText(value);
            }
        }

        #endregion

        #region Methods

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loginUser();
            var ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            loginedUserLabel.Text += " | Wersja: " + ver.ToString();
        }

        private void loginUser()
        {
            Enova.Business.Old.DB.Web.User.LoginedUser = null;
            Enova.API.EnovaService.Instance.Logout();
            loginedUserLabel.Text = "Użytkownik: ";
            LoginForm loginForm = new LoginForm() { MainForm = this };
            DialogResult result = loginForm.ShowDialog();

            if (Enova.Business.Old.DB.Web.User.LoginedUser != null)
            {
                loginedUserLabel.Text = "Użytkownik: " + Enova.Business.Old.DB.Web.User.LoginedUser.Login;

                
                if(Enova.API.EnovaService.Instance.CurrentLogin!=null)
                {
                    loginedUserLabel.Text += " | Enova: " + Enova.API.EnovaService.Instance.CurrentLogin.OperatorName;
                }
            }
            else
                this.Close();

        }

        private void setStatusLineText(string text)
        {
            if (this.InvokeRequired)
            {
                var d = new Action<string>(setStatusLineText);
                this.Invoke(d, text);
            }
            else
            {
                lock (statusLineSynch)
                    statusLineText.Text = text;
            }
        }

        #endregion

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

    }
}
