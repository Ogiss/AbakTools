using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

[assembly: AppModule("CRM", typeof(BAL.Test.CRM.CRMModule), typeof(BAL.Test.CRM.CRMContext))]

namespace BAL.Test.CRM
{
    public partial class CRMModule : Module
    {
        #region Methods

        public CRMModule(Session session)
            : base(session, "CRM")
        {
            this.tableKontrahenci = new Kontrahenci();

            this.AddTable(this.tableKontrahenci);
        }

        public CRMModule GetInstance(ISessionable session)
        {
            if (session != null && session.Session != null)
                return (CRMModule)session.Session.Modules[typeof(CRMModule)];
            return null;
        }

        #endregion
    }
}
