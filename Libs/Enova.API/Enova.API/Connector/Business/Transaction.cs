using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class Transaction : API.Types.ObjectBase, API.Business.Transaction
    {
        private Session session;


        public Transaction(Session session, bool editMode)
        {
            this.session = session;
            EnovaObject = session.CallMethod("Logout", editMode);
        }

        public void Commit()
        {
            CallMethod("Commit");
        }

        public void Dispose()
        {
            CallMethod("Dispose");
        }
    }
}
