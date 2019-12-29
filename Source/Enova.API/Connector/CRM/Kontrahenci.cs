using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.CRM
{
    internal class Kontrahenci : Business.GuidedTable<API.CRM.Kontrahent>, API.CRM.Kontrahenci
    {
        #region Properties

        public override string TableName
        {
            get
            {
                return "Kontrahenci";
            }
        }

        #endregion

    }
}
