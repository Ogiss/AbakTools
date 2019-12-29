using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.CRM
{
    internal class Bank : Business.GuidedRow, API.CRM.Bank
    {
        public API.Core.Adres Adres
        {
            get { return FromEnova<API.Core.Adres>("Adres"); }
        }

        public string Kod
        {
            get
            {
                return FromEnova<string>("Kod");
            }
            set
            {
                ToEnova("Kod", value);
            }
        }

        public string Nazwa
        {
            get
            {
                return FromEnova<string>("Nazwa");
            }
            set
            {
                ToEnova("Nazwa", value);
            }
        }

        public string NazwaFormatowana
        {
            get
            {
                return FromEnova<string>("NazwaFormatowana");
            }
            set
            {
                ToEnova("throw new NotImplementedException();", value);
            }
        }

        public string NazwaPierwszaLinia
        {
            get { return FromEnova<string>("NazwaPierwszaLinia"); }
        }


        public int KontrolaDni
        {
            get { throw new NotImplementedException(); }
        }

        public bool LimitNieograniczony
        {
            get { throw new NotImplementedException(); }
        }

        public string NIP
        {
            get { throw new NotImplementedException(); }
        }

        public API.Kasa.IPodmiotKasowy Platnik
        {
            get { throw new NotImplementedException(); }
        }

        public bool PrzeterminowanieNieograniczone
        {
            get { throw new NotImplementedException(); }
        }

        public int Termin
        {
            get { throw new NotImplementedException(); }
        }

        public int TerminPlanowany
        {
            get { throw new NotImplementedException(); }
        }


        public bool Blokada
        {
            get { throw new NotImplementedException(); }
        }

        public decimal Rabat
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.RodzajPodmiotu RodzajPodmiotu
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.StatusPodmiotu StatusPodmiotu
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.TypPodmiotu Typ
        {
            get { throw new NotImplementedException(); }
        }


        public string Kierunek
        {
            get { throw new NotImplementedException(); }
        }

        public string SWIFT
        {
            get { throw new NotImplementedException(); }
        }


        public string KontaktEMAIL
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string KontaktWWW
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string KontaktTelefonKomorkowy
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Komunikat
        {
            get { throw new NotImplementedException(); }
        }

        public bool BlokadaSprzedazy
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool BlokadaSprzedaży
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public API.Core.Adres AdresDoKorespondencji
        {
            get { throw new NotImplementedException(); }
        }

        public API.Kasa.FormaPlatnosci SposobZaplaty
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<API.Towary.CenaGrupowa> CenyGrupowe
        {
            get { throw new NotImplementedException(); }
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public API.Business.SubTable Rozrachunki
        {
            get { return FromEnova<API.Business.SubTable>("Rozrachunki"); }
        }


        public string EMAIL
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string EuVAT
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.SubTable Lokalizacje
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.View OsobyZOsobyKontrahent
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.SubTable Projekty
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.SubTable Urzadzenia
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.SubTable Zadania
        {
            get { throw new NotImplementedException(); }
        }

    }
}
