using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Types;
using Enova.API.Business;
using Enova.API.Core;

[assembly: TypeMap("Soneta.Kasa.KompensataBase, Soneta.Kasa", typeof(Enova.API.Kasa.KompensataBase), typeof(Enova.API.Connector.Kasa.KompensataBase))]

namespace Enova.API.Kasa
{
    public interface KompensataBase : PlatnyDokRozlicz, IDokumentKsiegowalny, IDokumentPodmiotu, IDokument, IRow, ISessionable
    {
        [Browsable(false)]
        Currency Koszt { get; set; }
        Currency Należność { get; }
        string NazwaPola { get; }
        Currency Zobowiązanie { get; }
    }

}
