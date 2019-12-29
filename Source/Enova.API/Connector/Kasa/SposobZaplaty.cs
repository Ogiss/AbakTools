using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class SposobZaplaty : Business.GuidedRow, Enova.API.Kasa.SposobZaplaty
    {
        public string Nazwa
        {
            get { return (string)GetValue("Nazwa"); }
        }

        public Enova.API.Kasa.TypySposobowZaplaty Typ
        {
            get { return (Enova.API.Kasa.TypySposobowZaplaty)(int)GetValue("Typ"); }
        }
    }
}
