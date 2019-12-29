using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class Platnosc : Business.GuidedRow, API.Kasa.Platnosc
    {
        public bool Bufor
        {
            get { return (bool)GetValue("Bufoor"); }
        }

        public DateTime DataDokumentu
        {
            get { return GetValue <DateTime>("DataDokumentu"); }
        }

        public DateTime DataRozliczenia
        {
            get { return GetValue<DateTime>("DataRozliczenia"); }
        }

        public API.Types.Currency DoRozliczenia
        {
            get { return EnovaHelper.FromEnova<API.Types.Currency>(GetValue("DoRozliczenia")); }
        }

        public API.Kasa.EwidencjaSP EwidencjaSP
        {
            get { return (API.Kasa.EwidencjaSP)EnovaHelper.FromEnova(GetValue("EwidencjaSP")); }
            set { SetValue("EwidencjaSP", value == null ? null : value.EnovaObject); }
        }

        public int Info
        {
            get { return (int)GetValue("Info"); }
        }

        public API.Core.KierunekPlatnosci Kierunek
        {
            get
            {
                return (API.Core.KierunekPlatnosci)Enum.ToObject(typeof(API.Core.KierunekPlatnosci), (int)GetValue("Kietunek"));
            }
        }

        public bool KsiegujZbiorczo
        {
            get { return (bool)GetValue("KsiegujZbiorczo"); }
        }

        public double Kurs
        {
            get { return (double)GetValue("Kurs"); }
        }

        public API.Types.Currency Kwota
        {
            get { return EnovaHelper.FromEnova<API.Types.Currency>(GetValue("Kwota")); }
        }

        public API.Types.Currency KwotaKsiegi
        {
            get { return EnovaHelper.FromEnova<API.Types.Currency>(GetValue("KwotaKsiegi")); }
        }

        public API.Types.Currency KwotaRozliczona
        {
            get { return EnovaHelper.FromEnova<API.Types.Currency>(GetValue("KwotaRozliczona")); }
        }

        public API.Types.Currency Należność
        {
            get { return EnovaHelper.FromEnova<API.Types.Currency>(GetValue("Należność")); }
        }

        public string NumerDokumentu
        {
            get { return (string)GetValue("NumerDokumentu"); }
        }

        public string Opis
        {
            get { return (string)GetValue("Opis"); }
        }

        public API.Kasa.IPodmiotKasowy Podmiot
        {
            get { return (API.Kasa.IPodmiotKasowy)EnovaHelper.FromEnova(GetValue("Podmiot")); }
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

        public API.Kasa.SposobZaplaty SposobZaplaty
        {
            get { return (API.Kasa.SposobZaplaty)EnovaHelper.FromEnova(GetValue("SpozobZaplaty")); }
            set { SetValue("SposobZaplaty", value == null ? null : value.EnovaObject); }
        }

        public DateTime Termin
        {
            get { return GetValue<DateTime>("Termin"); }
        }

        public int TerminDni
        {
            get { return (int)GetValue("TerminDni"); }
            set { SetValue("TerminDni", value); }
        }

        public DateTime TerminPlanowany
        {
            get { return GetValue<DateTime>("TerminPlanowany"); }
        }

        public int TerminPlanowanyDni
        {
            get { return (int)GetValue("TerminPlanowanyDni"); }
        }

        public int TerminPlanowanyDniOdTerminu
        {
            get { return (int)GetValue("TerminPlanowanyDniOdTerminu"); }
        }

        public API.Kasa.TypRozrachunku Typ
        {
            get
            {
                return (API.Kasa.TypRozrachunku)Enum.ToObject(typeof(API.Kasa.TypRozrachunku), (int)GetValue("Typ"));
            }
        }

        public bool Zatwierdzona
        {
            get { return (bool)GetValue("Zatwierdzona"); }
        }

        public API.Types.Currency Zobowiązanie
        {
            get { return EnovaHelper.FromEnova<API.Types.Currency>(GetValue("Zobowiązanie")); }
        }

        public bool Zrealizowane
        {
            get { return (bool)GetValue("Zrealizowane"); }
        }

        public bool Zwrot
        {
            get { return (bool)GetValue("Zwrot"); }
        }


        bool API.Kasa.IRozliczalny.Należność
        {
            get { return (bool)GetValue("Nameżność"); }
        }

        public string NumeryDokumentow
        {
            get { return (string)GetValue("NumeryDokumentow"); }
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
            get { return GetValue<DateTime>("TerminPlatnosci"); }
        }

        public bool Zapłata
        {
            get { return (bool)GetValue("Zaplata"); }
        }

        bool API.Kasa.IRozliczalny.Zobowiązanie
        {
            get { return (bool)GetValue("Zobowiązanie"); }
        }

        public Types.Currency RozliczonoKsiDoDnia(Types.Date data)
        {
            throw new NotImplementedException();
        }

        public API.Core.IDokumentPodmiotu Dokument
        {
            get { return FromEnova <API.Core.IDokumentPodmiotu>("Dokument"); }
        }

        public API.Business.SubTable Zaplaty
        {
            get { return FromEnova<API.Business.SubTable>("Zaplaty"); }
        }
    }
}
