using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Handel.DefRelacjiKorekta, Soneta.Handel", typeof(Enova.API.Handel.DefRelacjiKorekta), typeof(Enova.API.Connector.Handel.DefRelacjiKorekta)),
           RowMap("DefRelHandlowych", typeof(Enova.API.Handel.DefRelacjiKorekta), typeof(Enova.API.Handel.HandelModule))]

namespace Enova.API.Handel
{
    public interface DefRelacjiKorekta : DefRelacjiHandlowej
    {
        DokumentHandlowy KorygujDokument(DokumentHandlowy d1);
    }
}
