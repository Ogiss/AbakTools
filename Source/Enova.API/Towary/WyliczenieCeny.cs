using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Towary.WyliczenieCeny, Soneta.Handel", null, typeof(Enova.API.Towary.WyliczenieCeny))]

namespace Enova.API.Towary
{
    public enum WyliczenieCeny
    {
        Podgląd,
        PrzeliczenieCeny,
        PrzeliczenieRabatu,
        ZmianaCenyPozycji,
        DodaniePozycji,
        ZmianaIlościPozycji,
        ZmianaDostawyPozycji,
        ZmianaDatyDokumentu
    }
}
