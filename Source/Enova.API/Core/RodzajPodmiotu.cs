using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Core
{
    public enum RodzajPodmiotu
    {
//        [Caption("Bez VAT")]
        BezVAT = 5,
        Eksportowy = 1,
        EksportowyPodróżny = 2,
        Krajowy = 0,
        Unijny = 3,
        UnijnyTrójstronny = 4
    }
}
