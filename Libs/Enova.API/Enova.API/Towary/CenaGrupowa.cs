using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Towary.CenaGrupowa, Soneta.Handel", typeof(Enova.API.Towary.CenaGrupowa), typeof(Enova.API.Connector.Towary.CenaGrupowa)),
           RowMap("CenyGrupowe", typeof(Enova.API.Towary.CenaGrupowa), typeof(Enova.API.Towary.TowaryModule))]

namespace Enova.API.Towary
{
    public interface CenaGrupowa : Business.GuidedRow
    {
        Business.DictionaryItem Grupa { get; }
        Business.DictionaryItem GrupaTowarowa { get; }
        CRM.Kontrahent Kontrahent { get; }
        decimal Rabat { get; set; }
        bool RabatZdefiniowany { get; set; }
    }
}
