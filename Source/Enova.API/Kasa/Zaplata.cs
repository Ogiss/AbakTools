using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.Zaplata, Soneta.Kasa", typeof(Enova.API.Kasa.Zaplata), typeof(Enova.API.Connector.Kasa.Zaplata))]

namespace Enova.API.Kasa
{
    public interface Zaplata : Business.GuidedRow, IRozliczalny
    {
        bool Bufor { get; }
        //DaneKontrahenta { get; }
        DateTime DataDokumentu { get;  }
        DateTime DataRozliczenia { get; }
        Types.Currency DoPrzelania { get; }
        Types.Currency DoRozliczenia { get; }
        //DokEwidencji Ewidencja { get; }
        bool Handlowa { get;  }
        Core.KierunekPlatnosci Kierunek { get; }
        bool KsiegujZbiorczo { get;  }
        double Kurs { get;  }
        Types.Currency Kwota { get;  }
        Types.Currency KwotaRaportu { get;  }
        Types.Currency KwotaRozliczona { get; }
        int Lp { get;  }
        string NumerDokumentu { get;  }
        string NumeryDokumentow { get;  }
        string Opis { get;  }
        IPodmiotKasowy Podmiot { get;  }
        Types.Currency Przelano { get; }
        bool Rozliczana { get;  }
        IEnumerable Rozliczenia { get; }
        string RozliczoneDokumenty { get; }
        bool Rozliczono { get; }
        string Słownie { get; }
        string SłownieUpr { get; }
        SposobZaplaty SposobZaplaty { get;  }
        TypRozrachunku Typ { get; }
        Types.Currency WartoscWgKursu { get;  }
        Types.Currency WartoscWgMagazynuWalut { get; }
        Types.Currency Wartość { get; }
        Types.Currency Wpłata { get; }
        Types.Currency Wypłata { get; }
        bool Zaksięgowana { get; }
        bool Zatwierdzona { get; }
        bool Zwrot { get;  }

    }
}
