using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Kasa
{
    public interface IRozliczalny : Business.IRow , Business.ISessionable
    {
        // Methods
        //Currency RozliczonoDoDnia(Date data);

        // Properties
        bool Bufor { get; }
        DateTime DataDokumentu { get; }
        DateTime DataRozliczenia { get; }
        Core.IDokumentPodmiotu Dokument { get; }
        //SubTable Dokumenty { get; }
        Types.Currency DoRozliczenia { get; }
        //DokEwidencji Ewidencja { get; }
        //EwidencjaSP EwidencjaSP { get; }
        //FeatureCollection Features { get; }
        //DateSubTable HistoriaWindykacji { get; }
        Core.KierunekPlatnosci Kierunek { get; }
        bool KsiegujZbiorczo { get; }
        Types.Currency Kwota { get; }
        Types.Currency KwotaRozliczona { get; }
        bool Należność { get; }
        string NumerDokumentu { get; }
        string NumeryDokumentow { get; }
        string Opis { get; }
        bool Płatność { get; }
        bool PodlegaRozliczeniu { get; }
        IPodmiotKasowy Podmiot { get; }
        IEnumerable Rozliczenia { get; }
        string RozliczoneDokumenty { get; }
        bool Rozliczono { get; }
        //[Caption("Spos\x00f3b zapłaty")]
        SposobZaplaty SposobZaplaty { get; }
        //StanRozliczenia StanRozliczenia { get; }
        DateTime TerminPlatnosci { get; }
        TypRozrachunku Typ { get; }
        //WindykacjaInfo Windykacja { get; }
        //SubTable Zaplaty { get; }
        bool Zapłata { get; }
        bool Zatwierdzona { get; }
        bool Zobowiązanie { get; }
        bool Zwrot { get; }
    }
}
