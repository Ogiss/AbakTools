using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class Kompensata : KompensataBase, API.Kasa.Kompensata
    {

        public Types.Currency Kwota
        {
            get { return FromEnova<Types.Currency>("Kwota"); }
        }

        public Types.Currency Kwota1
        {
            get
            {
                return FromEnova<Types.Currency>("Kwota1");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Types.Currency Kwota2
        {
            get
            {
                return FromEnova<Types.Currency>("Kwota2");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool PodlegaNocie
        {
            get
            {
                return FromEnova<bool>("PodlegaNocie");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Słownie
        {
            get { return FromEnova<string>("Słownie"); }
        }

        public string SłownieUpr
        {
            get { return FromEnova<string>("SłownieUpr"); }
        }
    }
}
