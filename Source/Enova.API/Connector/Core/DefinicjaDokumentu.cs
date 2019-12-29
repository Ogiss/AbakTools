using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Core
{
    internal class DefinicjaDokumentu : Business.GuidedRow, API.Core.DefinicjaDokumentu
    {
        public bool Blokada
        {
            get
            {
                return (bool)GetValue("Blokada");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Domyslna
        {
            get
            {
                return (bool)GetValue("Domyslna");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Nazwa
        {
            get
            {
                return (string)GetValue("Nazwa");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public API.Core.DefinicjaNumeracji Numeracja
        {
            get { return FromEnova<API.Core.DefinicjaNumeracji>("Numeracja"); }
        }

        public string Symbol
        {
            get
            {
                return (string)GetValue("Sumbol");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public API.Core.TypDokumentu Typ
        {
            get { return FromEnova<API.Core.TypDokumentu>("Typ"); }
        }

        public bool ZawszePrzeliczajOpisAnalityczny
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

        public string DomyślnaNumeracja
        {
            get { return (string)GetValue("DomyślnaNumeracja"); }
        }

        public Type TypDokumentu
        {
            get { throw new NotImplementedException(); }
        }
    }
}
