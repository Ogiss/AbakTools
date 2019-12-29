using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Core
{
    [Flags]
    public enum OpcjeStatusuDokumentu
    {
        Brak = 0x0000,
        Domyslny = 0x0001,
        Niezalezny = 0x0002,
        Koncowy = 0x0004,
        ZawszeWidoczny = 0x0008,
        WymaganyOpis = 0x0010
    }
}
