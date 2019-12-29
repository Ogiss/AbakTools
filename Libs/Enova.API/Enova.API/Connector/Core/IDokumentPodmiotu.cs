using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Core
{
    internal class IDokumentPodmiotu : IDokument, API.Core.IDokumentPodmiotu
    {

        public API.Core.IPodmiot Podmiot
        {
            get { return FromEnova<API.Core.IPodmiot>("Podmiot", Type.GetType("Soneta.Core.IDokumentPodmiotu, Soneta.Core")); }
        }

    }
}
