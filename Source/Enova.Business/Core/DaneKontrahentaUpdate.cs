using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Core
{
    [Flags]
    public enum DaneKontrahentaUpdate
    {
        All = 15,
        EuVAT = 2,
        NIP = 1,
        Rodzaj = 4,
        Status = 8
    }
}
