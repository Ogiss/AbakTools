using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Towary.Towar, Soneta.Handel", typeof(Enova.API.Towary.Towar), typeof(Enova.API.Connector.Towary.Towar)),
           RowMap("Towary", typeof(Enova.API.Towary.Towar), typeof(Enova.API.Towary.TowaryModule))]

namespace Enova.API.Towary
{
    public interface Towar : Business.GuidedRow
    {
        bool Blokada { get; }
        string Kod { get; }
        string Nazwa { get; }
        string EAN { get; }
        TypTowaru Typ { get; }
        IEnumerable<ElementKompletu> ElementyKompletu { get; }
        CenySubTable Ceny { get; }
        Business.MemoText Opis { get; set; }
        ICena OstatniaCenaZakupu { get; }
    }
}
