using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Business;
using Enova.API.Core;

namespace Enova.API.Kasa
{
    public interface PlatnyDokRozlicz : KsiegowalnyDokRozlicz, IDokumentPlatny2, IDokumentPlatny, IDokumentPodmiotu, IDokument, IRow, ISessionable, INumerDokumentuHost
    {
        //[AttributeInheritance]
        bool Bufor { get; set; }
        KierunekPlatnosci Kierunek { get; }
        [Browsable(false)]
        string NazwaPola { get; }
    }

}
