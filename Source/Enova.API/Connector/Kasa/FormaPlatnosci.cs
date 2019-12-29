using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class FormaPlatnosci : Business.GuidedRow, Enova.API.Kasa.FormaPlatnosci
    {
        public string Nazwa
        {
            get { return (string)GetValue("Nazwa"); }
        }

        public Enova.API.Kasa.SposobZaplaty SposobZaplaty
        {
            get { return new SposobZaplaty(){ EnovaObject = GetValue("SposobZaplaty") }; }
        }


        public int Termin
        {
            get { return (int)GetValue("Termin"); }
        }

        public Enova.API.Kasa.EwidencjaSP EwidencjaSP
        {
            get { return new EwidencjaSP() { EnovaObject = GetValue("EwidencjaSP") }; }
        }
    }
}
