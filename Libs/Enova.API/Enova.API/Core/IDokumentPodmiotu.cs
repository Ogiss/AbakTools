using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Business;

[assembly: Enova.API.TypeMap("Soneta.Core.IDokumentPodmiotu, Soneta.Core", typeof(Enova.API.Core.IDokumentPodmiotu), typeof(Enova.API.Connector.Core.IDokumentPodmiotu))]

namespace Enova.API.Core
{
    public interface IDokumentPodmiotu : IDokument, IRow, ISessionable
    {
        // Properties
        IPodmiot Podmiot { get; }
    }
}
