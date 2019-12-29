using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.FormaPlatnosci, Soneta.Kasa", typeof(Enova.API.Kasa.FormaPlatnosci), typeof(Enova.API.Connector.Kasa.FormaPlatnosci)),
           RowMap("FormyPlatnosci", typeof(Enova.API.Kasa.FormaPlatnosci), typeof(Enova.API.Kasa.KasaModule))]

namespace Enova.API.Kasa
{
    public interface FormaPlatnosci : Business.GuidedRow
    {
        string Nazwa { get; }
        SposobZaplaty SposobZaplaty { get; }
        int Termin { get; }
        EwidencjaSP EwidencjaSP { get; }
    }
}
