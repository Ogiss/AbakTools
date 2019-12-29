using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Handel
{
    public enum RodzajIntrastat
    {
        IntrastatKorekta = 0x200,
        NieUwzględniaj = 0,
        Przywóz = 0x101,
        PrzywózWPodrzędnym = 1,
//        [Hidden]
        Uwzględniaj = 0x400,
        Wywóz = 0x102,
        WywózWPodrzędnym = 2
    }
}
