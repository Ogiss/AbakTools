using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Handel.Helpers
{
    [Flags]
    public enum Flags
    {
        KursWalutyGroup = 2,
        KursWalutyZmieniony = 2,
        KWPZAgregowanieGroup = 1,
        KWPZAgregowanieWgDokumentow = 1,
        None = 0,
        ZmianaParametrowZasobuGroup = 0x30,
        ZmianaParametrowZasobuZmianaIlosci = 0x10,
        ZmianaParametrowZasobuZmianaWartosc = 0x20,
        ZmianaZatwierdzonegoDokumentu = 4,
        ZmianaZatwierdzonegoDokumentuGroup = 4
    }
}
