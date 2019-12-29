using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class KompensataPozycja : PozycjaDokRozlicz, API.Kasa.KompensataPozycja
    {
        public bool IsNależność
        {
            get { return FromEnova<bool>("IsNależność"); }
        }

        public API.Kasa.KompensataBase Kompensata
        {
            get { return FromEnova<API.Kasa.KompensataBase>("Kompensata"); }
        }

        public API.Kasa.IRozliczalny Kompensowany
        {
            get { throw new NotImplementedException(); }
        }

        public Types.Currency Kwota
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

        public Types.Currency Należność
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

        public string NumerZaplaty
        {
            get { throw new NotImplementedException(); }
        }

        public API.Kasa.Platnosc Płatność
        {
            get { throw new NotImplementedException(); }
        }

        public Types.Currency Zobowiązanie
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
    }
}
