using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class EwidencjaSP : Business.GuidedRow, Enova.API.Kasa.EwidencjaSP
    {
        public string Nazwa
        {
            get { return (string)GetValue("Nazwa"); }
        }

        public string Symbol
        {
            get { return (string)GetValue("Symbol"); }
        }

        public Enova.API.Kasa.TypEwidencjiSP Typ
        {
            get { return (Enova.API.Kasa.TypEwidencjiSP)(int)GetValue("Typ"); }
        }

        public override string ToString()
        {
            return Nazwa;
        }
    }
}
