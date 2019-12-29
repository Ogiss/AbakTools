using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Core
{
    internal class IDokument : Business.Row, API.Core.IDokument
    {
        private static Type fromType;

        static IDokument()
        {
            fromType = Type.GetType("Soneta.Core.IDokument, Soneta.Core");
        }

        public Types.Date Data
        {
            get { return FromEnova<Types.Date>("Data", fromType); }
        }

        public API.Core.IDefinicjaDokumentu Definicja
        {
            get { return FromEnova<API.Core.IDefinicjaDokumentu>("Definicja", fromType); }
        }

        public API.Core.NumerDokumentu Numer
        {
            get { return FromEnova<Enova.API.Core.NumerDokumentu>("Numer", fromType); }
        }
    }
}
