using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class PozycjaDokRozlicz : Business.Row, API.Kasa.PozycjaDokRozlicz
    {
        public Types.Date DataDokumentu
        {
            get
            {
                return FromEnova<Types.Date>("DataDokumentu");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Types.Date DataZaplaty
        {
            get
            {
                return FromEnova<Types.Date>("DataZaplaty");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public API.Kasa.DokRozliczBase Dokument
        {
            get { return FromEnova<API.Kasa.DokRozliczBase>("Dokument"); }
        }

        public string NumerDokumentu
        {
            get
            {
                return FromEnova<string>("NumerDokumentu");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Opis
        {
            get
            {
                return FromEnova<string>("Opis");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public API.Kasa.IRozliczalny Platnosc
        {
            get
            {
                return FromEnova<API.Kasa.IRozliczalny>("Platnosc");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Types.Percent Procent
        {
            get
            {
                return FromEnova<Types.Percent>("Procent");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public API.Kasa.RozliczenieSP Rozliczenie
        {
            get
            {
                return FromEnova<API.Kasa.RozliczenieSP>("Rozliczenie");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Types.Date TerminZaplaty
        {
            get
            {
                return FromEnova<Types.Date>("TerminZaplaty");
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

        public API.Kasa.IRozliczalny DokRozliczany
        {
            get { return FromEnova<API.Kasa.IRozliczalny>("DokRozliczany"); }
        }

        public string NumerEwidencji
        {
            get { return FromEnova<string>("NumerEwidencji"); }
        }
    }
}
