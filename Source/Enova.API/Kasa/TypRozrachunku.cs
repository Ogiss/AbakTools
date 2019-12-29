using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.TypRozrachunku, Soneta.Kasa",null,typeof(Enova.API.Kasa.TypRozrachunku))]

namespace Enova.API.Kasa
{
    public enum TypRozrachunku
    {
        Należność = 10,
        Wpłata = 20,
        Wypłata = 0x15,
        Zobowiązanie = 11
    }
}
