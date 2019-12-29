using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.CRM
{
    internal class ZUS : Business.GuidedRow, API.CRM.ZUS
    {
        public API.Core.Adres Adres
        {
            get { return FromEnova<API.Core.Adres>("Adres"); }
        }

        public bool Blokada
        {
            get { throw new NotImplementedException(); }
        }

        public string Kod
        {
            get { return FromEnova<string>("Kod"); }
        }

        public string Nazwa
        {
            get { return FromEnova<string>("Nazwa"); }
        }

        public string NazwaFormatowana
        {
            get { return FromEnova<string>("NazwaFormatowana"); }
        }

        public string NazwaPierwszaLinia
        {
            get { return FromEnova<string>("NazwaPierwszaLinia"); }
        }

        public decimal Rabat
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.RodzajPodmiotu RodzajPodmiotu
        {
            get { return FromEnova<API.Core.RodzajPodmiotu>("RodzajPodmiotu", Type.GetType("Soneta.Core.IPodmiot, Soneta.Core")); }
        }

        public API.Core.StatusPodmiotu StatusPodmiotu
        {
            get { return FromEnova<API.Core.StatusPodmiotu>("StatusPodmiotu", Type.GetType("Soneta.Core.IPodmiot, Soneta.Core")); }
        }

        public API.Core.TypPodmiotu Typ
        {
            get { return FromEnova<API.Core.TypPodmiotu>("Typ", Type.GetType("Soneta.Core.IPodmiot, Soneta.Core")); }
        }

        public API.Business.SubTable Rozrachunki
        {
            get { return FromEnova<API.Business.SubTable>("Rozrachunki"); }
        }

        public string NIP
        {
            get { throw new NotImplementedException(); }
        }

        public string EuVAT
        {
            get { throw new NotImplementedException(); }
        }


        public int KontrolaDni
        {
            get { throw new NotImplementedException(); }
        }

        public bool LimitNieograniczony
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
    }
}
