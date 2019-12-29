using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.RozrachunekIdx, Soneta.Kasa", typeof(Enova.API.Kasa.RozrachunekIdx), typeof(Enova.API.Connector.Kasa.RozrachunekIdx)),
           RowMap("RozrachunkiIdx", typeof(Enova.API.Kasa.RozrachunekIdx), typeof(Enova.API.Kasa.KasaModule))]

namespace Enova.API.Kasa
{
    public interface RozrachunekIdx : Business.Row
    {
        DateTime Data { get; }
        DateTime DataPierwszego { get; }
        DateTime DataRozliczenia { get; }
        Types.Currency DoRozliczenia { get; }
        Types.Currency Kwota { get; }
        Types.Currency KwotaRozliczona { get; }
        string Numer { get; }
        int PrzeterminowanoDni { get; }
        DateTime Termin { get; }
        DateTime TerminPlanowany { get; }
        bool Zwrot { get; }
        TypRozrachunku Typ { get; }
        IRozliczalny Dokument { get; }
    }
}
