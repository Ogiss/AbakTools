using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.CRM
{
    internal class ZUSY : Business.GuidedTable<API.CRM.ZUS>, API.CRM.ZUSY
    {
        public override string TableName
        {
            get
            {
                return "ZUSY";
            }
        }
    }
}
