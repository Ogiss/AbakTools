using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Business;
using BAL.Forms;
using AbakTools.Business;

namespace AbakTools.Business.Forms
{
    public partial class LoginForm : Form
    {
        public const string EnovaToolsLogin = "EnovaTools";
        public const string EnovaToolsPassword = "mk024315ws";

        public Form MainForm = null;
        private bool canClose = false;

        public static bool LoginToEnova = false;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            loadUsers();
        }

        private void loadUsers()
        {
            var allUsers = Enova.Business.Old.Core.ContextManager.WebContext.Users.OrderBy(u => u.Login).ToList();
            var s = Enova.Business.Old.Core.Configuration.GetSetting("AvailableOperators");
            if (!string.IsNullOrEmpty(s))
            {
                var available = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                usersBindingSource.DataSource = allUsers.Where(u => available.Contains(u.Login));
                return;
            }

            s = Enova.Business.Old.Core.Configuration.GetSetting("NotAvailableOperators");
            if (!string.IsNullOrEmpty(s))
            {
                var notavailable = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                usersBindingSource.DataSource = allUsers.Where(u => !notavailable.Contains(u.Login));
                return;
            }

            usersBindingSource.DataSource = allUsers;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Enova.Business.Old.DB.Web.User login = (Enova.Business.Old.DB.Web.User)userComboBox.SelectedItem;
            if (login != null)
            {
                if (Enova.Business.Old.DB.Web.User.Authentication(this.userComboBox.Text, this.passwordTextBox.Text))
                {
                    using (var session = AppController.Instance.CurrentLogin.CreateSession(false, true, "Login"))
                    {
                        var dc = Enova.Business.Old.Core.ContextManager.WebContext;
                        Enova.Business.Old.DB.Web.Operator oper = dc.GetOperatorByNazwa(this.userComboBox.Text);
                        if (oper == null)
                        {
                            oper = new Enova.Business.Old.DB.Web.Operator();
                            dc.Operatorzy.AddObject(oper);
                            oper.Nazwa = login.Login;
                            oper.SetPassword(this.passwordTextBox.Text);
                            oper.KodPrzedstawiciela = login.AgentCode;
                            oper.EnovaLogin = login.EnovaOperatorLogin;
                            oper.EnovaPassword = login.EnovaPassword;
                            if (login.IsAgent == true)
                                oper.SetPrawaDostepu(OperatorPrawaDostepu.Przedstawiciel);
                            if (login.IsWarehouseman == true)
                                oper.SetPrawaDostepu(OperatorPrawaDostepu.Magazynier);
                            if (login.IsPakowacz == true)
                                oper.SetPrawaDostepu(OperatorPrawaDostepu.Pakowacz);
                            if (login.IsAdmin == true)
                                oper.SetPrawaDostepu(OperatorPrawaDostepu.Administrator);
                            if (login.IsSuperAdmin == true)
                                oper.SetPrawaDostepu(OperatorPrawaDostepu.SuperAdmin);
                            session.Save();
                        }

                        Enova.Business.Old.DB.Web.Operator.CurrentOperator = oper;
                    }

                    string enovaLogin = string.IsNullOrEmpty(login.EnovaOperatorLogin) ? EnovaToolsLogin : login.EnovaOperatorLogin;
                    string enovaPassword = string.IsNullOrEmpty(login.EnovaOperatorLogin) ? EnovaToolsPassword : login.EnovaPassword;
                    if (enovaPassword == null)
                        enovaPassword = string.Empty;

                    if (!string.IsNullOrEmpty(enovaLogin))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        try
                        {

                            var dbsetings = Enova.Business.Old.Core.ContextManager.DataBaseSettings;
                            Enova.Business.Old.App.Database database = null;
                            if (Enova.Business.Old.App.Database.Databases.ContainsKey(dbsetings.Name))
                                database = Enova.Business.Old.App.Database.Databases[dbsetings.Name];
                            else
                                database = new Enova.Business.Old.App.Database(typeof(Enova.Business.Old.DB.EnovaContext), dbsetings.Name, dbsetings.ConnectionString);
                            if (database != null)
                            {
                                login.LoginedEnova = database.Login(enovaLogin, enovaPassword);
                                if (login.LoginedEnova == null)
                                    login.LoginedEnova = database.Login("EnovaTools", "mk024315ws");
                            } 

                            if (LoginForm.LoginToEnova)
                            {
                                var enovaDatabase = Enova.Business.Old.Core.Configuration.GetSetting("EnovaDatabase");
                                Enova.API.EnovaService.Instance.Login(enovaDatabase, enovaLogin, enovaPassword);
                            }

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            this.Cursor = Cursors.Default;
                        }
                    }
                    this.Close();
                }
                else
                {
                    BAL.Forms.FormManager.Alert("Niewłaściwy login lub hasło");
                }
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose && Enova.Business.Old.DB.Web.User.LoginedUser == null)
            {
                MessageBox.Show("Nieprawidłowy login lub hasło", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                passwordTextBox.Focus();
                passwordTextBox.SelectAll();
            }
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                okButton_Click(sender, null);
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Enova.Business.Old.DB.Web.User.LoginedUser != null && Enova.Business.Old.DB.Web.User.LoginedUser.ChangePassword!=null && Enova.Business.Old.DB.Web.User.LoginedUser.ChangePassword.Value)
                new ChangePasswordForm() { Login = Enova.Business.Old.DB.Web.User.LoginedUser }.ShowDialog();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (MainForm != null)
            {
                canClose = true;
                this.Close();
            }
        }

        private void usersBindingSource_DataSourceChanged(object sender, EventArgs e)
        {
            userComboBox.SelectedItem = null;
        }
        
    }
}
