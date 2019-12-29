using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Business;
using Enova.API.Core;

namespace Enova.API.Kasa
{
    public interface IDokumentPlatny : IDokumentPodmiotu, IDokument, IRow, ISessionable
    {
        // Properties
        bool Bufor { get; }
        DokEwidencji Ewidencja { get; }
        SubTable Platnosci { get; }
    }

}
