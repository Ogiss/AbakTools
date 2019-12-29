using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public enum TypStatusuZamowienia
    {
        Nieznany = 0,
        NoweZamowienie = 1,
        DoMagazynu = 2,
        DoDostawcy = 3,
        Pakowanie = 4,
        Spakowane = 5,
        Kurier = 6,
        Przedstawiciel = 7,
        Wyslane = 8,
        Wstrzymane = 9,
        Anulowane = 10,
        Blokada = 11
    }
}
