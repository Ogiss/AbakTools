using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Core
{
    [Flags]
    public enum AlgorytmStatusuDokumentu
    {
        Brak = 0x0000,
        PrzedZmiana = 0x0001,
        PoZmianie = 0x0002
    }
}
