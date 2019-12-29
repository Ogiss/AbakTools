using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kadry
{
    [TableName("Pracownicy")]
    internal class Pracownik : Business.GuidedRow, API.Kadry.IPracownik, API.Kasa.IPodmiotKasowy
    {
        public string Imie
        {
            get
            {
                return (string)FromEnova("Imie");
            }
            set
            {
                ToEnova("Imie", value);
            }
        }

        public string Nazwisko
        {
            get
            {
                return (string)FromEnova("Nazwisko");
            }
            set
            {
                ToEnova("Nazwisko", value);
            }
        }

        public string ImięNazwisko
        {
            get { return (string)FromEnova("ImięNazwisko"); }
        }

        public string NazwiskoImię
        {
            get { return (string)FromEnova("NazwiskoImię"); }
        }

        public string Kod
        {
            get
            {
                return (string)FromEnova("Kod");
            }
            set
            {
                ToEnova("Kod", value);
            }
        }

        public string Nazwa
        {
            get { return (string)FromEnova("Nazwa"); }
        }

        public string NazwaFormatowana
        {
            get { return (string)FromEnova("NazwaFormatowana"); }
        }

        public string NazwaPierwszaLinia
        {
            get { return (string)FromEnova("NazwaPierwszaLinia"); }
        }

        public Types.Currency Kwota
        {
            get
            {
                return FromEnova<Types.Currency>("Kwota");
            }
            set
            {
                ToEnova("Kwota", value);
            }
        }

        public string PESEL
        {
            get { return (string)FromEnova("PESEL"); }
        }

        public API.Business.SubTable Platnosci
        {
            get { return FromEnova<API.Business.SubTable>("Platnosci"); }
        }

        public API.Business.SubTable Rozrachunki
        {
            get { return FromEnova<API.Business.SubTable>("Rozrachunki"); }
        }

        public API.Kasa.FormaPlatnosci SposobZaplaty
        {
            get
            {
                return FromEnova<API.Kasa.FormaPlatnosci>("SposobZaplaty");
            }
            set
            {
                ToEnova("SposobyZaplaty", value);
            }
        }

        public API.Kadry.TypPracownika Typ
        {
            get { return FromEnova<API.Kadry.TypPracownika>("Typ"); }
        }

        public API.Business.SubTable Wyplaty
        {
            get { return FromEnova<API.Business.SubTable>("Wyplaty"); }
        }

        public API.Business.SubTable Zaliczki
        {
            get { return FromEnova<API.Business.SubTable>("Zaliczki"); }
        }

        public API.Business.SubTable Zaplaty
        {
            get { return FromEnova<API.Business.SubTable>("Zaplaty"); }
        }

        public API.Core.Adres Adres
        {
            get { return FromEnova<API.Core.Adres>("Adres"); }
        }

        public string EMAIL
        {
            get
            {
                return (string)FromEnova("EMAIL");
            }
            set
            {
                ToEnova("EMAIL", value);
            }
        }


        public bool Blokada
        {
            get { return (bool)FromEnova("Blokada"); }
        }

        public decimal Rabat
        {
            get { return (decimal)FromEnova("Rabat"); }
        }

        public API.Core.RodzajPodmiotu RodzajPodmiotu
        {
            get { return FromEnova<API.Core.RodzajPodmiotu>("RodzajPodmiotu"); }
        }

        public API.Core.StatusPodmiotu StatusPodmiotu
        {
            get { return FromEnova<API.Core.StatusPodmiotu>("StatusPodmiotu"); }
        }

        API.Core.TypPodmiotu API.Core.IPodmiot.Typ
        {
            get { return FromEnova<API.Core.TypPodmiotu, API.Core.IPodmiot>("Typ"); }
        }

        public string NIP
        {
            get { throw new NotImplementedException(); }
        }

        public string EuVAT
        {
            get { throw new NotImplementedException(); }
        }


        int API.Kasa.IPodmiotKasowy.KontrolaDni
        {
            get { return FromEnova<int>("KontrolaDni", Type.GetType("Soneta.Kasa.IPodmiotKasowy,Soneta.Kasa")); }
        }

        public bool LimitNieograniczony
        {
            get { return FromEnova<bool>("LimitNieograniczony", Type.GetType("Soneta.Kasa.IPodmiotKasowy,Soneta.Kasa")); }
        }

        public API.Kasa.IPodmiotKasowy Platnik
        {
            get { return FromEnova<API.Kasa.IPodmiotKasowy>("Platnik", Type.GetType("Soneta.Kasa.IPodmiotKasowy,Soneta.Kasa")); }
        }

        public bool PrzeterminowanieNieograniczone
        {
            get { return FromEnova<bool>("PrzeterminowanieNieograniczone", Type.GetType("Soneta.Kasa.IPodmiotKasowy,Soneta.Kasa")); }
        }

        public int Termin
        {
            get { return FromEnova<int>("Termin", Type.GetType("Soneta.Kasa.IPodmiotKasowy,Soneta.Kasa")); }
        }

        public int TerminPlanowany
        {
            get { return FromEnova<int>("TerminPlanowany", Type.GetType("Soneta.Kasa.IPodmiotKasowy,Soneta.Kasa")); }
        }
    }
}
