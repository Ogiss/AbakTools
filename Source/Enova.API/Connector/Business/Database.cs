using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class Database : API.Types.ObjectBase
    {
        #region Properties

        public string Name
        {
            get { return GetValue<string>("Name"); }
        }

        #endregion

        #region Methods

        public Login Login(bool withAuth, string user, string password)
        {
            var obj = CallMethodFull("Login", new Type[] { typeof(bool), typeof(string), typeof(string) }, new object[] { withAuth, user, password });
            return obj != null ? new Login() { EnovaObject = obj } : null;
        }

        #endregion
    }
}
