using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Core
{
    internal class IDefinicjaDokumentu : Business.Row, API.Core.IDefinicjaDokumentu
    {
        private static Type fromType;

        static IDefinicjaDokumentu()
        {
            fromType = Type.GetType("Soneta.Core.IDefinicjaDokumentu, Soneta.Core");
        }

        public string DomyślnaNumeracja
        {
            get { return FromEnova<string>("DomyślnaNumeracja", fromType); }
        }

        public API.Core.DefinicjaNumeracji Numeracja
        {
            get { return FromEnova<API.Core.DefinicjaNumeracji>("Numeracja", fromType); }
        }

        public string Symbol
        {
            get { return FromEnova<string>("Symbol", fromType); }
        }

        public API.Core.TypDokumentu Typ
        {
            get { return FromEnova<API.Core.TypDokumentu>("Typ", fromType); }
        }

        public Type TypDokumentu
        {
            get { throw new NotImplementedException(); }
        }
    }
}
