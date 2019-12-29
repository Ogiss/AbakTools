using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class PozDokRozlicz : Business.Table<API.Kasa.PozycjaDokRozlicz>, API.Kasa.PozDokRozlicz
    {
        public override string TableName
        {
            get
            {
                return "PozDokRozlicz";
            }
        }
    }
}
