using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.SposobZaplaty, Soneta.Kasa", typeof(Enova.API.Kasa.SposobZaplaty), typeof(Enova.API.Connector.Kasa.SposobZaplaty)),
           RowMap("SposobyZaplaty", typeof(Enova.API.Kasa.SposobZaplaty), typeof(Enova.API.Kasa.KasaModule))]

namespace Enova.API.Kasa
{
    public interface SposobyZaplaty : Business.GuidedTable<SposobZaplaty>
    {
    }
}
