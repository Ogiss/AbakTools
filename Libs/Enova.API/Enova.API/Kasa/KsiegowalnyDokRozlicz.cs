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
    public interface KsiegowalnyDokRozlicz : DokRozliczBase, IDokumentKsiegowalny, IDokumentPodmiotu, IDokument, IRow, ISessionable, INumerDokumentuHost
    {
        // Methods
        bool GenerujEwidencje();

        // Properties
        [Category("Og\x00f3lne")]
        bool Bufor { get; set; }
        Currency Diety { get; set; }
        DokEwidencji Ewidencja { get; }
        Currency Koszt { get; set; }
        Currency RazemRyczalty { get; }
        Currency RyczaltDojazdy { get; set; }
        Currency RyczaltNoclegi { get; set; }
        //[NumeratorItem, NumeratorInfo(NumeratorInfoType.str, "2015")]
        string SymbolOkresuWgDatyDokumentu { get; }
        Currency Wartosc { get; }
    }

}
