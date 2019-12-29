using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Handel
{
    public enum SposobPrzenoszeniaZaliczki
    {
//        [Hidden, Caption("W częściach na dok. końcowy")]
        NaDokument = 1,
//        [Caption("Poprzez pozycje")]
        NaPozycje = 2,
        NieDotyczy = 0
    }
}
