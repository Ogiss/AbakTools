using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;
using Enova.Business.Old.DB;

namespace Enova.Old.CRM
{
    public partial class CRMModule : EnovaModule
    {
        #region Fields

        private Kontrahenci kontrahenciTable;

        #endregion

        #region Properties

        public Kontrahenci Kontrahenci
        {
            get { return this.kontrahenciTable; }
        }

        #endregion

        #region Methods

        public CRMModule(Session session)
            : base(session, "CRM")
        {
            this.kontrahenciTable = new Kontrahenci();

            this.AddTable(this.kontrahenciTable);
        }

        public static CRMModule GetInstance(ISessionable session)
        {
            if (session != null && session.Session != null)
                return (CRMModule)session.Session.Modules[typeof(CRMModule)];
            return null;
        }

        #endregion
    }
}
