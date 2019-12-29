using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class IPodmiotKasowy : Business.Row , API.Kasa.IPodmiotKasowy
    {
        public API.Core.Adres Adres
        {
            get { return FromEnova<API.Core.Adres>("Adres"); }
        }

        public string Kod
        {
            get { return FromEnova<string>("Kod"); }
        }

        public int KontrolaDni
        {
            get { return FromEnova<int>("KontrolaDni"); }
        }

        public bool LimitNieograniczony
        {
            get { return FromEnova<bool>("LimitNieograniczony"); }
        }

        public string Nazwa
        {
            get { return FromEnova<string>("Nazwa"); }
        }

        public string NIP
        {
            get { return FromEnova<string>("NIP"); }
        }

        public API.Kasa.IPodmiotKasowy Platnik
        {
            get { return FromEnova<API.Kasa.IPodmiotKasowy>("Platnik"); }
        }

        public bool PrzeterminowanieNieograniczone
        {
            get { return FromEnova<bool>("PrzeterminowanieNieograniczone"); }
        }

        public int Termin
        {
            get { return FromEnova<int>("Termin"); }
        }

        public int TerminPlanowany
        {
            get { return FromEnova<int>("TerminPlanowany"); }
        }


        public bool Blokada
        {
            get { return FromEnova<bool>("Blokada"); }
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
            get { return FromEnova<decimal>("Rabat"); }
        }

        public API.Core.RodzajPodmiotu RodzajPodmiotu
        {
            get { return FromEnova<API.Core.RodzajPodmiotu>("RodzajPodmiotu"); }
        }

        public API.Core.StatusPodmiotu StatusPodmiotu
        {
            get { return FromEnova<API.Core.StatusPodmiotu>("StatusPodmiotu"); }
        }

        public API.Core.TypPodmiotu Typ
        {
            get { return FromEnova<API.Core.TypPodmiotu>("Typ"); }
        }

        public string EuVAT
        {
            get { return FromEnova<string>("EuVAT"); }
        }
    }
}
