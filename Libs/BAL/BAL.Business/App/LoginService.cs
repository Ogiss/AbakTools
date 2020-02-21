using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business.App
{
    public class LoginService : AppServiceBase, ILoginService
    {
        #region Fields

        #endregion

        #region Methods

        public virtual ILogin Login(IDatabase database, string domain, string user,  string password)
        {
            return new Login(database, domain, user, password);
        }

        public virtual void Logout(ILogin login)
        {
        }

        #endregion
    }
}
