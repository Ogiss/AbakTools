using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    [Obsolete("Do usunięcia")]
    public enum RowSynchronizeOld {
        Synchronized = 0,
        Notsaved = 1,
        NotsynchronizedNew = 2,
        NotsynchronizedEdit = 3,
        NotsynchronizedDelete = 4,
        Synchronizing = 5
    }

    public enum SynchronizeImage
    {
        None,
        Synchronized,
        New,
        Delete
    }

    public enum RodzajTransportu
    {
        NieWybrano = 0,
        Kurier = 1,
        Przedstawiciel = 2,
        DoDostawcy = 3
    }

    public enum StatusyZamowieniaTyp
    {
        Brak = 0,
        NoweZamowienie = 1,
        DoMagazynu = 2,
        DoDostawcy = 3,
        Pakowane = 4,
        Spakowane = 5,
        Kurier = 6,
        Przedstawiciel = 7,
        Wyslane = 8,
        Wstrzymane = 9,
        Anulowane = 10,
        Blokada = 11
    }

    public enum OutOfStockType { Niedozwolone = 0, Dozwolone = 1, Domyslnie = 2 }

}
