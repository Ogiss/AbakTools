using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business.App
{
    public partial class Login : ILogin
    {
        #region Fields

        private string user;
        private string password;
        private string domain;
        private IDatabase database;

        #endregion

        #region Properties

        public string User
        {
            get { return this.user; }
        }

        public string Password
        {
            get { return this.password; }
        }

        public string Domain
        {
            get { return this.domain; }
        }

        public IDatabase Database
        {
            get { return this.database; }
        }

        #endregion

        #region Methods

        public Login(IDatabase database, string domain, string user, string password)
        {
            this.database = database;
            this.domain = domain;
            this.user = user;
            this.password = password;
        }

        public void Logout()
        {
            this.database.Logout(this);
        }

        [Obsolete("AppControler.SetLogin(ILogin, Session)")]
        public Session CreateSession(bool readOnly, bool isInternal, string name)
        {
            Session session = new Session(this, readOnly, isInternal, name);
            
            return session;
        }

        public override bool Equals(object obj)
        {
            if (obj is ILogin)
            {
                ILogin login = (ILogin)obj;
                return this.domain == login.Domain && this.user == login.User;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (this.Domain + "|" + this.User).GetHashCode();
        }

        #endregion
    }
}
