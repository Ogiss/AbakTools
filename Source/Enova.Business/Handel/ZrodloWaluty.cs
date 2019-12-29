using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Handel
{
    public enum ZrodloWaluty
    {
//        [Hidden]
        None = 0,
//        [Caption("Z cennika")]
        ZCennika = 0x10,
//        [Caption("Z definicji dokumentu")]
        ZDefinicji = 8,
//        [Caption("Z definicji podrzędnego")]
        ZDefinicjiPodrzednego = 4,
//        [Caption("Z karty kontrahenta")]
        ZKartyKontrahenta = 2,
//        [Caption("Z nadrzędnego")]
        ZNadrzednego = 1,
//        [Hidden]
        ZrodloWalutyPlatnosc = 10,
//        [Hidden]
        ZrodloWalutyPozycja = 0x1a,
//        [Hidden]
        ZrodloWalutyWRelacjiPlatnosc = 7,
//        [Hidden]
        ZrodloWalutyWRelacjiPozycja = 7
    }
}
