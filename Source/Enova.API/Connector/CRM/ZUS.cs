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
            get { return (string)FromEnova("Kod"); }
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

        public decimal Rabat
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.RodzajPodmiotu RodzajPodmiotu
        {
            get { return FromEnova<API.Core.RodzajPodmiotu, API.Core.IPodmiot>("RodzajPodmiotu"); }
        }

        public API.Core.StatusPodmiotu StatusPodmiotu
        {
            get { return FromEnova<API.Core.StatusPodmiotu, API.Core.IPodmiot>("StatusPodmiotu"); }
        }

        public API.Core.TypPodmiotu Typ
        {
            get { return FromEnova<API.Core.TypPodmiotu, API.Core.IPodmiot>("Typ"); }
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
