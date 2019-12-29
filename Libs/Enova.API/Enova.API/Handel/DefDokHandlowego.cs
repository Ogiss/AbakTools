using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap( "Soneta.Handel.DefDokHandlowego, Soneta.Handel", typeof(Enova.API.Handel.DefDokHandlowego), typeof(Enova.API.Connector.Handel.DefDokHandlowego)),
           RowMap("DefDokHandlowych", typeof(Enova.API.Handel.DefDokHandlowego), typeof(Enova.API.Handel.HandelModule))]

namespace Enova.API.Handel
{
    public interface DefDokHandlowego : Business.GuidedRow
    {
        string Symbol { get; }
        string Nazwa { get; }
        bool Blokada { get; }
        Towary.DefinicjaCeny Cena { get; }
        DefRelacjiHandlowej RelacjaMagazynowaDefinicja { get; }

        DokumentHandlowy NowyDokument(CRM.Kontrahent kontrahent, Magazyny.Magazyn magazyn);
    }
}
