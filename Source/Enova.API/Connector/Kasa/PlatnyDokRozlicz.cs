using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class PlatnyDokRozlicz: KsiegowalnyDokRozlicz, API.Kasa.PlatnyDokRozlicz
    {

        public API.Core.KierunekPlatnosci Kierunek
        {
            get { return FromEnova<API.Core.KierunekPlatnosci>("Kierunek"); }
        }

        public string NazwaPola
        {
            get { throw new NotImplementedException(); }
        }

        public bool Anulowany
        {
            get { throw new NotImplementedException(); }
        }

        public bool Ostrzeżenie
        {
            get { throw new NotImplementedException(); }
        }

        public bool PlanSplat
        {
            get { throw new NotImplementedException(); }
        }
    }
}
