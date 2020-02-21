using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business.App
{
    public class LoginEventArgs
    {
        private ILogin login;

        public ILogin Login
        {
            get { return this.login; }
        }

        public LoginEventArgs(ILogin login)
        {
            this.login = login;
        }
    }

    public delegate void LoginEventHandler(LoginEventArgs e);
}
