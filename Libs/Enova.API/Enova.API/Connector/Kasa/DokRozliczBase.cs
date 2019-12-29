using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class DokRozliczBase : Business.GuidedRow, API.Kasa.DokRozliczBase
    {
        public bool Bufor
        {
            get
            {
                return (bool)GetValue("Bufor");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Types.Date Data
        {
            get
            {
                return FromEnova<Types.Date>("Data");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Types.Date DataKursu
        {
            get
            {
                return FromEnova<Types.Date>("DataKursu");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public API.Core.DefinicjaDokumentu Definicja
        {
            get
            {
                return FromEnova<API.Core.DefinicjaDokumentu>("Definicja");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public API.Business.SubTable DokumentyEwidencji
        {
            get { return FromEnova<API.Business.SubTable>("DokumentyEwidencji"); }
        }

        public API.Kasa.DokRozliczBase Nadrzedny
        {
            get { return FromEnova<API.Kasa.DokRozliczBase>("Nadrzedny"); }
        }

        public API.Core.NumerDokumentu Numer
        {
            get { return FromEnova<API.Core.NumerDokumentu>("Numer"); }
        }

        public string NumerDruku
        {
            get
            {
                return FromEnova<string>("NumerDruku");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string NumerProceduryISO
        {
            get { return FromEnova<string>("NumerProceduryISO"); }
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

        public API.Business.SubTable Platnosci
        {
            get { return FromEnova<API.Business.SubTable>("Platnosci"); }
        }

        public API.Kasa.IPodmiotKasowy Podmiot
        {
            get
            {
                return FromEnova<API.Kasa.IPodmiotKasowy>("Podmiot");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public API.Business.SubTable Pozycje
        {
            get { return FromEnova<API.Business.SubTable>("Pozycje"); }
        }

        public API.Business.SubTable PozycjeEwidencji
        {
            get { return FromEnova<API.Business.SubTable>("PozycjeEwidencji"); }
        }

        public string Seria
        {
            get
            {
                return FromEnova<string>("Seria");
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

        public bool Wielowalutowy
        {
            get
            {
                return FromEnova<bool>("Wielowalutowy");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public API.Business.SubTable Windykacje
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.SubTable ZadaniaCRM
        {
            get { throw new NotImplementedException(); }
        }

        public bool JestZrodlemStanowWindykacji
        {
            get { throw new NotImplementedException(); }
        }

        public API.Kasa.EwidencjaSP RachunekBankowy
        {
            get { throw new NotImplementedException(); }
        }

        public bool Wielooddzialowosc
        {
            get { throw new NotImplementedException(); }
        }

        public bool Zatwierdzony
        {
            get
            {
                return FromEnova<bool>("Zatwierdzony");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public API.Business.View GetListDefinicja()
        {
            throw new NotImplementedException();
        }

        public object GetListOddzial()
        {
            throw new NotImplementedException();
        }

        public API.Business.View GetListRachunekFirmy()
        {
            throw new NotImplementedException();
        }

        API.Core.IPodmiot API.Core.IDokumentPodmiotu.Podmiot
        {
            get { return FromEnova<API.Core.IPodmiot>("Podmiot", Type.GetType("Soneta.Core.IDokumentPodmiotu, Soneta.Core")); }
        }


        API.Core.IDefinicjaDokumentu API.Core.IDokument.Definicja
        {
            get { return FromEnova<API.Core.IDefinicjaDokumentu>("Definicja", Type.GetType("Soneta.Core.IDokument, Soneta.Core")); }
        }
    }
}
