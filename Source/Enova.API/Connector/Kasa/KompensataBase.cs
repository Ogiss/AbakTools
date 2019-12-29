using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class KompensataBase : PlatnyDokRozlicz, API.Kasa.KompensataBase
    {

        public Types.Currency Należność
        {
            get { return FromEnova<Types.Currency>("Należność"); }
        }

        public Types.Currency Zobowiązanie
        {
            get { return FromEnova<Types.Currency>("Zobowiązanie"); }
        }
    }
}
