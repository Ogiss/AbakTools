using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.EwidencjaSP, Soneta.Kasa", typeof(Enova.API.Kasa.EwidencjaSP), typeof(Enova.API.Connector.Kasa.EwidencjaSP)),
           RowMap("EwidencjeSP", typeof(Enova.API.Kasa.EwidencjaSP), typeof(Enova.API.Kasa.KasaModule))]

namespace Enova.API.Kasa
{
    public interface EwidencjaSP : Business.GuidedRow
    {
        string Nazwa { get; }
        string Symbol { get; }
        TypEwidencjiSP Typ { get; }
    }
}
