using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class EwidencjeSP : Business.GuidedTable<API.Kasa.EwidencjaSP>, API.Kasa.EwidencjeSP
    {
        public override string TableName
        {
            get
            {
                return "EwidencjeSP";
            }
        }
    }
}
