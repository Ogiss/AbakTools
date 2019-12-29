using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Handel
{
    public enum TypRelacjiHandlowej
    {
        //[Hidden]
        Brak = 0,
        Cykliczna = 13,
        HandlowoMagazynowa = 3,
        Inwentaryzacja = 6,
        InwentaryzacjaStrata = 7,
        Kaucji = 14,
        Kompletacja = 9,
        KompletacjaSkładniki = 10,
        Kopiowania = 2,
        Korekta = 1,
        KorektaPWZ = 8,
        //[Caption("Produkcyjna produkty")]
        ProdukcjaProdukty = 0x11,
        //[Caption("Produkcyjna surowce")]
        ProdukcjaSurowce = 0x10,
        Przesunięcie = 4,
        PrzesunięcieDo = 5,
        [Obsolete("DefRelacjiRozliczaniaKaucji nie jest już obsługiwana. Należy ją traktować jak usuniętą.")/*, Hidden*/]
        RozliczenieKaucji = 15,
        Wiązania = 11,
        Zaliczka = 12
    }




}
