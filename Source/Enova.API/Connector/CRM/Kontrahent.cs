using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Connector;

/*
[assembly:
RowMap(
    "Soneta.CRM.Kontrahent",
    "Kontrahenci",
    typeof(Enova.API.CRM.IKontrahent),
    typeof(Enova.API.Connector.CRM.Kontrahent),
    typeof(Enova.API.CRM.ICRMModule)
 )]
 */

namespace Enova.API.Connector.CRM
{
    [TableName("Kontrahenci")]
    internal class Kontrahent : Business.GuidedRow, Enova.API.CRM.Kontrahent
    {
        public string Kod
        {
            get
            {
                return (string)GetValue("Kod");
            }
        }

        public string Nazwa
        {
            get { return (string)GetValue("Nazwa"); }
        }


        public string KontaktEMAIL
        {
            get
            {
                return (string)GetObjValue(GetValue("Kontakt"), "EMAIL");
            }
            set
            {
                SetObjValue(GetValue("Kontakt"), "EMAIL", value);
            }
        }

        public string KontaktWWW
        {
            get
            {
                return (string)GetObjValue(GetValue("Kontakt"), "WWW");
            }
            set
            {
                SetObjValue(GetValue("Kontakt"), "WWW", value);
            }
        }

        public string KontaktTelefonKomorkowy
        {
            get
            {
                return (string)GetObjValue(GetValue("Kontakt"), "TelefonKomorkowy");
            }
            set
            {
                SetObjValue(GetValue("Kontakt"), "TelefonKomorkowy", value);
            }
        }

        public string NIP
        {
            get
            {
                return GetValue<string>("NIP");
            }
        }

        public bool Blokada
        {
            get { return GetValue<bool>("Blokada"); }
        }

        public bool BlokadaSprzedazy
        {
            get { return GetValue<bool>("BlokadaSprzedazy"); }
            set { SetValue("BlokadaSprzedazy", value); }
        }

        public bool BlokadaSprzedaży
        {
            get { return this.BlokadaSprzedazy; }
            set { this.BlokadaSprzedazy = value; }
        }


        public decimal Rabat
        {
            get { return GetValue<decimal>("Rabat"); }
        }

        public Enova.API.Core.Adres Adres
        {
            get
            {
                var adres = GetValue("Adres");
                return adres == null ? null : new Core.Adres() { EnovaObject = adres };
            }
        }

        public Enova.API.Core.Adres AdresDoKorespondencji
        {
            get
            {
                var adres = GetValue("AdresDoKorespondencji");
                return adres == null ? null : new Core.Adres() { EnovaObject = adres };
            }
        }

        public Enova.API.Kasa.FormaPlatnosci SposobZaplaty
        {
            get
            {
                return new Kasa.FormaPlatnosci() { EnovaObject = GetValue("SposobZaplaty") };
            }
        }

        public string Komunikat
        {
            get { return (string)GetValue("Komunikat"); }
        }

        public Kontrahent() : this(null) { }

        public Kontrahent(object record)
        {
            if (record != null)
                base.EnovaObject = record;
        }

        public override string ToString()
        {
            return this.Kod + " - " + this.Nazwa.Replace("\r\n", " ");
        }

        public IEnumerable<API.Towary.CenaGrupowa> CenyGrupowe
        {
            get
            {
                return new Business.EnovaEnumerable<API.Towary.CenaGrupowa>(GetValue("CenyGrupowe"));
            }
        }

        public int KontrolaDni
        {
            get
            {
                return (int)GetValue("KontrolaDni");
            }
        }

        public bool LimitNieograniczony
        {
            get
            {
                return (bool)GetValue("LimitNieograniczony");
            }
        }

        public API.Kasa.IPodmiotKasowy Platnik
        {
            get
            {
                return null;
            }
        }

        public bool PrzeterminowanieNieograniczone
        {
            get
            {
                return (bool)GetValue("PrzeterminowanieNieograniczone");
            }
        }

        public int Termin
        {
            get
            {
                return (int)GetValue("Termin");
            }
        }

        public int TerminPlanowany
        {
            get
            {
                return (int)GetValue("TerminPlanowany");
            }
        }


        public string NazwaFormatowana
        {
            get
            {
                return (string)GetValue("NazwaFormatowana");
            }
        }

        public string NazwaPierwszaLinia
        {
            get
            {
                return (string)GetValue("NazwaPierwszaLinia");
            }
        }

        public API.Core.RodzajPodmiotu RodzajPodmiotu
        {
            get
            {
                return (API.Core.RodzajPodmiotu)(int)GetValue("RodzajPodmiotu");
            }
        }

        public API.Core.StatusPodmiotu StatusPodmiotu
        {
            get
            {
                return (API.Core.StatusPodmiotu)(int)GetValue("StatusPodmiotu");
            }
        }

        public API.Core.TypPodmiotu Typ
        {
            get
            {
                throw new NotImplementedException("Enova.API.Connector.CRM.Kontrahent.Typ");
                //return (API.Core.TypPodmiotu)(int)((Soneta.Core.IPodmiot)Row).Typ : (API.Core.TypPodmiotu)0;
            }
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


        public Types.Currency LimitKredytu
        {
            get
            {
                return FromEnova<Types.Currency>("LimitKredytu");
            }
            set
            {
                ToEnova("LimitKredytu", value);
            }
        }
    }
}
