using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    [Serializable]
    public enum FeatureTypeNumber
    {
        //        [Caption("Ilość")]
        Amount = 0x12,
        //        [Caption("Wielowartościowa")]
        Array = 14,
        //        [Caption("Wielohierarchiczna")]
        ArrayOfTrees = 0x10,
        //        [Caption("Warunek")]
        Bool = 1,
        //        [Caption("Kwota z walutą")]
        Currency = 9,
        //        [Caption("Data")]
        Date = 5,
        //        [Caption("Kwota")]
        Decimal = 3,
        //        [Caption("Liczba rzeczywista")]
        Double = 4,
        //        [Caption("Liczba z walutą")]
        DoubleCy = 11,
        //        [Caption("Ułamek")]
        Fraction = 8,
        //        [Caption("Okres dat")]
        FromTo = 7,
        //        [Caption("Liczba całkowita")]
        Int = 0,
        //        [Caption("Procent")]
        Percent = 10,
        //        [Caption("Referencja")]
        Reference = 13,
        //        [Caption("Tekst")]
        String = 2,
        //        [Caption("Czas")]
        Time = 6,
        //        [Caption("Czas z dokładnością do sekundy")]
        TimeSec = 0x11,
        //        [Caption("Hierarchiczna")]
        Tree = 15,
        //        [Caption("Miesiąc w roku")]
        YearMonth = 12
    }
}
