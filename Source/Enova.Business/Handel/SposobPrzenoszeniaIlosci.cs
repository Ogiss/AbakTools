using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Old.Types;

namespace Enova.Old.Handel
{
    public enum SposobPrzenoszeniaIlosci
    {
        IlośćICena = 4,
        IlośćIWartość = 0,
        NiePrzenosiPozycji = 3,
        TylkoIlość = 1,
        TylkoInicjuje = 2,
        [Caption("Ilość i wartość, usługi tylko wg wartości")]
        UslugiWgWartosci = 5
    }

}
