using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Kasa
{
    public interface SposobZaplaty : Business.GuidedRow
    {
        string Nazwa { get; }
        TypySposobowZaplaty Typ { get; }
    }
}
