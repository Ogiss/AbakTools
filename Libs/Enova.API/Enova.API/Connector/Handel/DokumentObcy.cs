using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Handel.DokumentObcy, SonetaHandel", typeof(Enova.API.Handel.DokumentObcy), typeof(Enova.API.Connector.Handel.DokumentObcy))]

namespace Enova.API.Connector.Handel
{
    public class DokumentObcy : Business.SubRow, API.Handel.DokumentObcy
    {
        public Types.Date DataOtrzymania
        {
            get { return FromEnova<Types.Date>("DataOtrzymania"); }
            set { ToEnova("DataOtrzymania", value); }
        }

        public API.Handel.DokumentHandlowy Dokument
        {
            get { return FromEnova<API.Handel.DokumentHandlowy>("Dokument"); }
        }

        public string Numer
        {
            get { return FromEnova<string>("Numer"); }
            set { ToEnova("Numer", value); }
        }
    }
}
