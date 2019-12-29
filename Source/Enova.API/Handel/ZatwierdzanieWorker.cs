using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Handel.ZatwierdzanieWorker, Soneta.Handel", typeof(Enova.API.Handel.ZatwierdzanieWorker), typeof(Enova.API.Connector.Handel.ZatwierdzanieWorker))]

namespace Enova.API.Handel
{
    public interface ZatwierdzanieWorker : Types.IObjectBase
    {
        object Zatwierdź();
        object ZatwierdźLista();
        DokumentHandlowy Dokument { get; set; }
        DokumentHandlowy[] Dokumenty { get; set; }
    }
}
