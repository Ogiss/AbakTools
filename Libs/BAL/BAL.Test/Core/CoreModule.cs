using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

[assembly: AppModule("Core", typeof(BAL.Test.Core.CoreModule), typeof(BAL.Test.Core.CoreContext))]

namespace BAL.Test.Core
{
    public partial class CoreModule : Module
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods

        public CoreModule(Session session)
            : base(session, "Core")
        {
            this.tableAdresy = new Adresy();

            this.AddTable(this.tableAdresy);
        }

        public static CoreModule GetInstance(ISessionable session)
        {
            if (session != null && session.Session != null)
                return (CoreModule)session.Session.Modules[typeof(CoreModule)];
            return null;
        }

        #endregion
    }
}
