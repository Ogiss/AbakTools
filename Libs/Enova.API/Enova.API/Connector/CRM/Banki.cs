using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.CRM
{
    internal class Banki : Business.GuidedTable<API.CRM.Bank>, API.CRM.Banki
    {
        public override string TableName
        {
            get
            {
                return "Banki";
            }
        }
    }
}
