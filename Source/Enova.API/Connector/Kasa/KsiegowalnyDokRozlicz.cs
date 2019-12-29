using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class KsiegowalnyDokRozlicz : DokRozliczBase, API.Kasa.KsiegowalnyDokRozlicz
    {
        public bool GenerujEwidencje()
        {
            throw new NotImplementedException();
        }

        public Types.Currency Diety
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

        public API.Core.DokEwidencji Ewidencja
        {
            get { return FromEnova<API.Core.DokEwidencji>("Ewidencja"); }
        }

        public Types.Currency Koszt
        {
            get
            {
                return FromEnova<Types.Currency>("Koszt");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Types.Currency RazemRyczalty
        {
            get { throw new NotImplementedException(); }
        }

        public Types.Currency RyczaltDojazdy
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

        public Types.Currency RyczaltNoclegi
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

        public string SymbolOkresuWgDatyDokumentu
        {
            get { throw new NotImplementedException(); }
        }

        public Types.Currency Wartosc
        {
            get { return FromEnova<Types.Currency>("Wartosc"); }
        }

        public new API.Core.IPodmiot Podmiot
        {
            get { return FromEnova<API.Core.IPodmiot>("Podmiot"); }
        }


        public new API.Core.IDefinicjaDokumentu Definicja
        {
            get { return (API.Core.IDefinicjaDokumentu)base.Definicja; }
        }

        public Types.Date DataOperacji
        {
            get { return FromEnova<Types.Date>("DataOperacji"); }
        }

        public Types.Date DataWpływu
        {
            get { return FromEnova<Types.Date>("DataOperacji"); }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
