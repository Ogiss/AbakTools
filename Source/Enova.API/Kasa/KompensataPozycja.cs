using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Types;

[assembly: TypeMap("Soneta.Kasa.KompensataPozycja, Soneta.Kasa", typeof(Enova.API.Kasa.KompensataPozycja), typeof(Enova.API.Connector.Kasa.KompensataPozycja))]

namespace Enova.API.Kasa
{
    public interface KompensataPozycja : PozycjaDokRozlicz
    {
        bool IsNależność { get; }
        KompensataBase Kompensata { get; }
        IRozliczalny Kompensowany { get; }
        Currency Kwota { get; set; }
        Currency Należność { get; set; }
        string NumerZaplaty { get; }
        Platnosc Płatność { get; }
        //[AttributeInheritance]
        RozliczenieSP Rozliczenie { get; }
        Currency Zobowiązanie { get; set; }

    }
}
