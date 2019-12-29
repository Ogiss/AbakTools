using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class Login : API.Types.ObjectBase, API.Business.Login
    {
        #region Properties

        public string OperatorName
        {
            get { return GetValue<string>("OperatorName"); }
        }

        #endregion

        #region Methods

        public API.Business.Session CreateSession(bool readOnly, bool config, string name)
        {
            var obj = CallMethod("CreateSession", readOnly, config, name);
            return obj == null ? null : new Session() { EnovaObject = obj };
        }

        public void Dispose()
        {
            CallMethod("Dispose");
        }

        #endregion

    }
}
