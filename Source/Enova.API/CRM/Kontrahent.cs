using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.CRM.Kontrahent, Soneta.CRM", typeof(Enova.API.CRM.Kontrahent), typeof(Enova.API.Connector.CRM.Kontrahent)),
           RowMap("Kontrahenci", typeof(Enova.API.CRM.Kontrahent), typeof(Enova.API.CRM.CRMModule))]

namespace Enova.API.CRM
{
    [TableName("Kontrahenci")]
    public interface Kontrahent : Business.GuidedRow, Kasa.IPodmiotKasowy, Core.IPodmiot, Kasa.IPodmiotRozrachunki, Core.IKontrahent
    {
        int ID { get; }
        string Kod { get; }
        string Nazwa { get; }
        string KontaktEMAIL { get; set; }
        string KontaktWWW { get; set; }
        string KontaktTelefonKomorkowy { get; set; }
        string NIP { get; }
        string Komunikat { get; }
        bool Blokada { get; }
        bool BlokadaSprzedazy { get; set; }
        [Obsolete("Należy używać BlokadaSprzedazy")]
        bool BlokadaSprzedaży { get; set; }
        decimal Rabat { get; }
        Core.Adres Adres { get; }
        Core.Adres AdresDoKorespondencji { get; }
        Kasa.FormaPlatnosci SposobZaplaty { get; }
        IEnumerable<Towary.CenaGrupowa> CenyGrupowe { get; }
        Types.Currency LimitKredytu { get; set; }
        bool LimitNieograniczony { get; }
    }
}
