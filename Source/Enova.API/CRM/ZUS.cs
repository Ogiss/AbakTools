using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly:
    TypeMap("Soneta.CRM.ZUS, Soneta.CRM", typeof(Enova.API.CRM.ZUS), typeof(Enova.API.Connector.CRM.ZUS)),
    RowMap("ZUSY", typeof(Enova.API.CRM.ZUS), typeof(Enova.API.CRM.CRMModule))]

namespace Enova.API.CRM
{
    public interface ZUS : Business.GuidedRow, Kasa.IPodmiotKasowy, Core.IPodmiot, Kasa.IPodmiotRozrachunki
    {
    }
}
