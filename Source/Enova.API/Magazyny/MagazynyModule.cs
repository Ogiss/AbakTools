using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Magazyny
{
    public interface MagazynyModule : Business.Module
    {
        Magazyny Magazyny { get; }
        Obroty Obroty { get; }
        OkresyMag OkresyMag { get; }
    }
}
