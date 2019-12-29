using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class Zaplata : Business.GuidedRow, API.Kasa.Zaplata
    {
        public bool Bufor
        {
            get { return (bool)GetValue("Buffor"); }
        }

        public DateTime DataDokumentu
        {
            get { return FromEnova<DateTime>("DataDokumentu"); }
        }

        public DateTime DataRozliczenia
        {
            get { return FromEnova<DateTime>("DataRozliczenia"); }
        }

        public API.Types.Currency DoPrzelania
        {
            get { return FromEnova<API.Types.Currency>("DoPrzelania"); }
        }

        public API.Types.Currency DoRozliczenia
        {
            get { return FromEnova<API.Types.Currency>("DpRozliczenia"); }
        }

        public bool Handlowa
        {
            get { return (bool)GetValue("Handlowa"); }
        }

        public API.Core.KierunekPlatnosci Kierunek
        {
            get { return FromEnova<API.Core.KierunekPlatnosci>("Kierunek"); }
        }

        public bool KsiegujZbiorczo
        {
            get { return (bool)GetValue("KsiegujZbiorczo"); }
        }

        public double Kurs
        {
            get { return FromEnova<double>("Kurs"); }
        }

        public API.Types.Currency Kwota
        {
            get { return FromEnova<API.Types.Currency>("Kwota"); }
        }

        public API.Types.Currency KwotaRaportu
        {
            get { return FromEnova<API.Types.Currency>("KwotaRaportu"); }
        }

        public API.Types.Currency KwotaRozliczona
        {
            get { return FromEnova<API.Types.Currency>("KwotaRozliczona"); }
        }

        public int Lp
        {
            get { return (int)GetValue("Lp"); }
        }

        public string NumerDokumentu
        {
            get { return (string)GetValue("NumerDokumentu"); }
        }

        public string NumeryDokumentow
        {
            get { return (string)GetValue("NumeryDokumentow"); }
        }

        public string Opis
        {
            get { return (string)FromEnova("Opis"); }
        }

        public API.Kasa.IPodmiotKasowy Podmiot
        {
            get { return FromEnova<API.Kasa.IPodmiotKasowy>("Podmiot"); }
        }

        public API.Types.Currency Przelano
        {
            get { return FromEnova<API.Types.Currency>("Przelano"); }
        }

        public bool Rozliczana
        {
            get { return (bool)GetValue("Rozliczana"); }
        }

        public System.Collections.IEnumerable Rozliczenia
        {
            get { return new Business.EnovaEnumerable() { EnovaObject = GetValue("Rozliczenia") }; }
        }

        public string RozliczoneDokumenty
        {
            get { return (string)GetValue("RozliczoneDokumenty"); }
        }

        public bool Rozliczono
        {
            get { return (bool)GetValue("Rozliczono"); }
        }

        public string Słownie
        {
            get { return (string)GetValue("Słownie"); }
        }

        public string SłownieUpr
        {
            get { return (string)GetValue("SłownieUpr"); }
        }

        public API.Kasa.SposobZaplaty SposobZaplaty
        {
            get { return FromEnova<API.Kasa.SposobZaplaty>("SposobZaplaty"); }
        }

        public API.Kasa.TypRozrachunku Typ
        {
            get { return FromEnova<API.Kasa.TypRozrachunku>("Typ"); }
        }

        public API.Types.Currency WartoscWgKursu
        {
            get { return FromEnova<API.Types.Currency>("WartoscWgKursu"); }
        }

        public API.Types.Currency WartoscWgMagazynuWalut
        {
            get { return FromEnova<API.Types.Currency>("WartoscWgMagazynuWalut"); }
        }

        public API.Types.Currency Wartość
        {
            get { return FromEnova<API.Types.Currency>("Wartość"); }
        }

        public API.Types.Currency Wpłata
        {
            get { return FromEnova<API.Types.Currency>("Wpłata"); }
        }

        public API.Types.Currency Wypłata
        {
            get { return FromEnova<API.Types.Currency>("Wypłata"); }
        }

        public bool Zaksięgowana
        {
            get { return (bool)GetValue("Zaksięgowana"); }
        }

        public bool Zatwierdzona
        {
            get { return (bool)GetValue("Zatwierdzona"); }
        }

        public bool Zwrot
        {
            get { return (bool)GetValue("Zwrot"); }
        }


        public bool Należność
        {
            get { return (bool)GetValue("Należność"); }
        }

        public bool Płatność
        {
            get { return (bool)GetValue("Płatność"); }
        }

        public bool PodlegaRozliczeniu
        {
            get { return (bool)GetValue("PodlegaRozliczeniu"); }
        }

        public DateTime TerminPlatnosci
        {
            get { return FromEnova<DateTime>("TerminPlatnosci"); }
        }

        public bool Zapłata
        {
            get { return (bool)FromEnova("Zapłata"); }
        }

        public bool Zobowiązanie
        {
            get { return (bool)FromEnova("Zobowiązanie"); }
        }


        public API.Core.IDokumentPodmiotu Dokument
        {
            get { return (API.Core.IDokumentPodmiotu)FromEnova("Dokument"); }
        }
    }
}
