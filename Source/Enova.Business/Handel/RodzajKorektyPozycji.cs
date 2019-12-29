using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Handel
{
    public enum RodzajKorektyPozycji
    {
        //[Caption("")]
        Brak = 0,
        Ceny = 4,
        Ilości = 3,
        //[Caption("Ilości ceny")]
        IlościICeny = 5,
        Nowa = 1,
        //[Caption("St.VAT")]
        StawkiVAT = 6,
        Zwrot = 2
    }
}
