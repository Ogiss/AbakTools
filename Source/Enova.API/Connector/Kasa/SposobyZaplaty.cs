using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class SposobyZaplaty : Business.GuidedTable<API.Kasa.SposobZaplaty>, API.Kasa.SposobyZaplaty
    {
        public override string TableName
        {
            get
            {
                return "SposobyZaplaty";
            }
        }
    }
}
