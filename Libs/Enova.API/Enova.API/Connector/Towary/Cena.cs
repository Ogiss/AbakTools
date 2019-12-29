using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Towary
{
    internal class Cena : Business.Row, API.Towary.Cena
    {
        public Types.DoubleCy Brutto
        {
            get
            {
                return FromEnova<Types.DoubleCy>("Brutto");
            }
            set
            {
                SetValue("Brutto", ToEnova(value));
            }
        }

        public API.Towary.DefinicjaCeny Definicja
        {
            get { return FromEnova <API.Towary.DefinicjaCeny>("Definicja"); }
        }

        public bool Korygowana
        {
            get
            {
                return (bool)GetValue("Korygowana");
            }
            set
            {
                SetValue("Korygowana", value);
            }
        }

        public Types.DoubleCy Netto
        {
            get
            {
                return FromEnova<Types.DoubleCy>("Netto");
            }
            set
            {
                SetValue("Netto", ToEnova(value));
            }
        }

        public bool Polacz
        {
            get
            {
                return (bool)GetValue("Polacz");
            }
            set
            {
                SetValue("Polacz", value);
            }
        }

        public double StandardowaIlosc
        {
            get
            {
                return (double)GetValue("StandardowaIlość");
            }
            set
            {
                SetValue("StandardowaIlość", value);
            }
        }

        public API.Towary.Towar Towar
        {
            get { return FromEnova<API.Towary.Towar>("Towar"); }
        }

        public Types.DoubleCy MarżaOstatniejCeny
        {
            get
            {
                return FromEnova<Types.DoubleCy>("MarżaOstatniejCeny");
            }
            set
            {
                SetValue("MarżaOstatniejCeny", value);
            }
        }

        public Types.Percent ProcentMarżyOstatniejCeny
        {
            get
            {
                return FromEnova<Types.Percent>("ProcentMarżyOstatniejCeny");
            }
            set
            {
                SetValue("ProcentMarżyOstatniejCeny", ToEnova(value));
            }
        }

        public Types.Percent ProcentNarzutuOstatniejCeny
        {
            get
            {
                return FromEnova<Types.Percent>("ProcentNarzutuOstatniejCeny");
            }
            set
            {
                SetValue("ProcentNarzutuOstatniejCeny", ToEnova(value));
            }
        }
    }
}
