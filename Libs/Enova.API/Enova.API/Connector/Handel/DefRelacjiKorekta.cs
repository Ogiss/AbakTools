using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//[assembly: TypeMap("Soneta.Handel.DefRelacjiKorekta, Soneta.Handel", typeof(Enova.API.Handel.IDefRelacjiKorekta), typeof(Enova.API.Connector.Handel.DefRelacjiKorekta))]

namespace Enova.API.Connector.Handel
{
    internal class DefRelacjiKorekta : DefRelacjiHandlowej, API.Handel.DefRelacjiKorekta
    {
        public API.Handel.DokumentHandlowy KorygujDokument(API.Handel.DokumentHandlowy d1)
        {
            return EnovaHelper.FromEnova<API.Handel.DokumentHandlowy>(CallMethod("KorygujDokument", EnovaHelper.ToEnova(d1)));
        }
    }
}
