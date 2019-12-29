using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Core
{
    internal class DokEwidencji : Business.GuidedRow , API.Core.DokEwidencji
    {
        public API.Core.StanEwidencji Stan
        {
            get { return FromEnova<API.Core.StanEwidencji>("Stan"); }
        }

        public string NumerDokumentu
        {
            get { return FromEnova<string>("NumerDokumentu"); }
        }

        public Types.Date DataDokumentu
        {
            get { return FromEnova<Types.Date>("DataDokumentu"); }
        }

        public API.Core.TypDokumentu Typ
        {
            get { return FromEnova<API.Core.TypDokumentu>("Typ"); }
        }

        public Types.Currency Wartosc
        {
            get
            {
                return FromEnova<Types.Currency>("Wartosc");
            }
        }

        public API.Core.DefinicjaDokumentu Definicja
        {
            get
            {
                return FromEnova<API.Core.DefinicjaDokumentu>("Definicja");
            }
        }

        public API.Core.IDokumentKsiegowalny Dokument
        {
            get { return FromEnova<API.Core.IDokumentKsiegowalny>("Dokument",Type.GetType("Soneta.Core.DokEwidencji, Soneta.Core")); }
        }

        public API.Core.IPodmiot Podmiot
        {
            get
            {
                return FromEnova<API.Core.IPodmiot>("Podmiot");
            }
        }
    }
}
