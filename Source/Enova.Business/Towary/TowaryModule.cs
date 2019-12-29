using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;
using Enova.Business.Old.DB;

namespace Enova.Old.Towary
{
    public partial class TowaryModule : EnovaModule
    {
        #region Fields

        private Towary towaryTable;

        #endregion

        #region Properties

        public Towary Towary
        {
            get { return this.towaryTable; }
        }

        #endregion

        #region Methods

        public TowaryModule(Session session)
            : base(session, "Towary")
        {
            this.towaryTable = new Towary();
            this.AddTable(towaryTable);
        }

        public static TowaryModule GetInstance(ISessionable session)
        {
            if (session != null && session.Session != null)
                return (TowaryModule)session.Session.Modules[typeof(TowaryModule)];
            return null;
        }

        #endregion
    }
}
