using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Enova.Business.Old.DB.Web
{
    public partial class User : IComparable
    {
        public static User LoginedUser = null;
       // public static DBSession Session = null;

        public Enova.Business.Old.App.Login LoginedEnova { get; set; }

        
        public DB.Operator EnovaOperator
        {
            get
            {
                var dc = Enova.Business.Old.Core.ContextManager.DataContext;
                DB.Operator op = null;
                if (!string.IsNullOrEmpty(Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperatorLogin))
                {
                    op = dc.OperatorByName(Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperatorLogin);
                }
                else
                {
                    op = dc.OperatorByName(Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperatorLogin);
                }
                return op;
            }
        }
        

        public string DecryptedPassword = null;

        public string Encrypt()
        {
            string str = this.Login.ToLower() + "|" + this.Password + "|" + this.IsAdmin.ToString() + "|" + this.IsAgent.ToString() + "|" + this.AgentCode
                + "|" + this.ChangePassword.ToString();
            this.Password = Enova.Business.Old.Core.Crypto.Encrypt(str);
            return this.Password;
        }

        public static bool Authentication(string user, string password)
        {
            User login = Enova.Business.Old.Core.ContextManager.WebContext.Users.Where(u => u.Login.ToLower() == user.ToLower()).FirstOrDefault();

            if (login!=null && login.Authorization(password))
            {
                LoginedUser = login;
                login.DecryptedPassword = password;
               // Session = new DBSession();
                return true;
            }

            return false;
        }

        internal bool Authorization(string password)
        {
            string encryptPasswd = this.Password;
            this.Password = password;
            this.Encrypt();
            bool ret = encryptPasswd == this.Password;
            this.Password = encryptPasswd;
            return ret;
        }

        public void SetPassword(string password)
        {
            this.Password = password;
            this.ChangePassword = false;
            this.Encrypt();
            Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();
        }

        public bool CheckPerissions(bool? isAdmin, bool? isAgent, bool showMessage = true)
        {
            if ((isAdmin == null || isAdmin.Value == this.IsAdmin) && (isAgent == null || isAgent.Value == this.IsAgent))
                return true;
            if (showMessage)
                MessageBox.Show("Nie posiadasz wystarczajacych uprawnień", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;

        }

        public bool CheckAgent(string agentKode)
        {
            return !string.IsNullOrEmpty(this.AgentCode) ? this.AgentCode == agentKode : this.PracownikKod == agentKode;
        }

        /*
        public static Pracownik GetPracownik(User user)
        {
            if (user != null)
            {
                string kod = string.IsNullOrEmpty(user.EnovaOperatorLogin) ? user.Login : user.EnovaOperatorLogin;
                return Enova.Business.Old.Core.ContextManager.WebContext.Pracownicy.Where(p => p.Kod == kod).FirstOrDefault();
            }
            return null;
        }
        */

        public static Operator GetOperator(User user)
        {
            if(user != null)
            {
                string kod = string.IsNullOrEmpty(user.EnovaOperatorLogin) ? user.Login : user.EnovaOperatorLogin;
                return Core.ContextManager.WebContext.Operatorzy.Where(x => x.Nazwa == kod).SingleOrDefault();
            }
            return null;
        }


        public EntityCollection<Konfiguracja> Konfiguracje
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !RelationKonfiguracje.IsLoaded)
                    RelationKonfiguracje.Load();
                return RelationKonfiguracje;
            }
        }

        public bool Administrator
        {
            get
            {
                return (this.IsAdmin != null && this.IsAdmin.Value) || (this.IsSuperAdmin != null && this.IsSuperAdmin.Value);
            }
        }

        public override string ToString()
        {
            return this.Login;
        }


        public int CompareTo(object obj)
        {
            return this.Login.CompareTo(((User)obj).Login);
        }
    }
}
