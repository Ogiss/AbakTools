using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public partial class BusinessModule : EnovaModule
    {
        #region Fields

        private SystemInfos tableSystemInfo;
        private Operators tableOperator;

        #endregion

        #region Properties

        public SystemInfos SystemInfos
        {
            get
            {
                return this.tableSystemInfo;
            }
        }

        public Operators Operators
        {
            get
            {
                return this.tableOperator;
            }
        }

        #endregion

        #region Methods

        public BusinessModule(Session session)
            : base(session, "Business")
        {
            this.tableSystemInfo = new SystemInfos();
            this.tableOperator = new Operators();

            this.AddTable(this.tableSystemInfo);
            this.AddTable(this.tableOperator);
        }

        public static BusinessModule GetInstance(ISessionable session)
        {
            if ((session != null) && (session.Session != null))
            {
                return (BusinessModule)session.Session.Modules[typeof(BusinessModule)];
            }
            return null;
        }

        #endregion
    }
}
