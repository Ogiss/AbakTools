using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Magazyny
{
    internal class PartiaTowaru : Business.SubRow, API.Magazyny.PartiaTowaru
    {
        public API.Types.Time Czas
        {
            get { return FromEnova<API.Types.Time>("Czas"); }
        }

        public API.Types.Date Data
        {
            get { return FromEnova<API.Types.Date>("Data"); }
        }

        public API.Types.Date DataZasobu
        {
            get { return FromEnova<API.Types.Date>("DataZasobu"); }
        }

        public API.Handel.DokumentHandlowy Dokument
        {
            get
            {
                return FromEnova<API.Handel.DokumentHandlowy>("Dokument");
            }
            set
            {
                ToEnova("Dokument", value);
            }
        }

        API.Magazyny.Dostawy.IGrupaDostaw API.Magazyny.PartiaTowaru.PartiaTowaru
        {
            get { return FromEnova<API.Magazyny.Dostawy.IGrupaDostaw>("PartiaTowaru"); }
        }

        public int PozycjaIdent
        {
            get
            {
                return FromEnova<int>("PozycjaIdent");
            }
            set
            {
                ToEnova("PozycjaIdent", value);
            }
        }

        public API.Magazyny.TypPartii Typ
        {
            get
            {
                return FromEnova<API.Magazyny.TypPartii>("Typ");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal Wartosc
        {
            get
            {
                return FromEnova<decimal>("Wartosc");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public double Cena
        {
            get { throw new NotImplementedException(); }
        }

        public API.Magazyny.IDanePartiiTowaru Dane
        {
            get { throw new NotImplementedException(); }
        }

        public API.Handel.IlośćWartość IlośćWartość
        {
            get { throw new NotImplementedException(); }
        }

        public API.Handel.PozycjaDokHandlowego Pozycja
        {
            get { throw new NotImplementedException(); }
        }
    }
}
