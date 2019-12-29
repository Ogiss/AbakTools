using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class RozrachunekIdx : Business.Row, API.Kasa.RozrachunekIdx
    {
        public DateTime Data
        {
            get { return GetValue<DateTime>("Data"); }
        }

        public DateTime DataPierwszego
        {
            get { return GetValue<DateTime>("DataPierwszego"); }
        }

        public DateTime DataRozliczenia
        {
            get { return GetValue<DateTime>("DataRozliczenia"); }
        }

        public API.Types.Currency DoRozliczenia
        {
            get { return EnovaHelper.FromEnova<API.Types.Currency>(GetValue("DoRozliczenia")); }
        }

        public API.Types.Currency Kwota
        {
            get
            {
                return EnovaHelper.FromEnova<API.Types.Currency>(GetValue("Kwota"));

            }
        }

        public API.Types.Currency KwotaRozliczona
        {
            get { return EnovaHelper.FromEnova<API.Types.Currency>(GetValue("KwotaRozliczona")); }
        }

        public string Numer
        {
            get { return (string)GetValue("Numer"); }
        }

        public int PrzeterminowanoDni
        {
            get { return (int)GetValue("PrzeterminowanoDni"); }
        }

        public DateTime Termin
        {
            get { return GetValue<DateTime>("Termin"); }
        }

        public DateTime TerminPlanowany
        {
            get { return GetValue<DateTime>("TerminPlanowany"); }
        }

        public bool Zwrot
        {
            get { return (bool)GetValue("Zwrot"); }
        }


        public API.Kasa.TypRozrachunku Typ
        {
            get { return FromEnova<API.Kasa.TypRozrachunku>("Typ"); }
        }

        public API.Kasa.IRozliczalny Dokument
        {
            get { return (API.Kasa.IRozliczalny)FromEnova("Dokument"); }
        }
    }
}
