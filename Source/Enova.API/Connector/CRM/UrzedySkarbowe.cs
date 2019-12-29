using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.CRM
{
    internal class UrzedySkarbowe : Business.GuidedTable<API.CRM.UrzadSkarbowy>, API.CRM.UrzedySkarbowe
    {
        public override string TableName
        {
            get
            {
                return "UrzedySkarbowe";
            }
        }
    }
}
