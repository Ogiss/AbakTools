using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.Platnosc, Soneta.Kasa", typeof(Enova.API.Kasa.Platnosc), typeof(Enova.API.Connector.Kasa.Platnosc)),
           RowMap("Platnosci", typeof(Enova.API.Kasa.Platnosc), typeof(Enova.API.Kasa.KasaModule))]


namespace Enova.API.Kasa
{
    public interface Platnosc : Business.GuidedRow, IRozliczalnyCy
    {
        bool Bufor { get;}
        DateTime DataDokumentu { get;}
        DateTime DataRozliczenia { get; }
        Types.Currency DoRozliczenia { get; }
        EwidencjaSP EwidencjaSP { get; set; }
        int Info { get; }
        Core.KierunekPlatnosci Kierunek { get; }
        bool KsiegujZbiorczo { get;}
        double Kurs { get;}
        Types.Currency Kwota { get;}
        Types.Currency KwotaKsiegi { get; }
        Types.Currency KwotaRozliczona { get; }
        Types.Currency Należność { get; }
        string NumerDokumentu { get; }
        string Opis { get;}
        IPodmiotKasowy Podmiot { get; }
        bool Rozliczana { get;  }
        IEnumerable Rozliczenia { get; }
        string RozliczoneDokumenty { get; }
        bool Rozliczono { get; }
        string Słownie { get; }
        SposobZaplaty SposobZaplaty { get; set; }
        DateTime Termin { get;  }
        int TerminDni { get; set; }
        DateTime TerminPlanowany { get;  }
        int TerminPlanowanyDni { get;  }
        int TerminPlanowanyDniOdTerminu { get;  }
        TypRozrachunku Typ { get; }
        Business.SubTable Zaplaty { get; }
        bool Zatwierdzona { get;  }
        Types.Currency Zobowiązanie { get; }
        bool Zrealizowane { get; }
        bool Zwrot { get;  }
    }
}
