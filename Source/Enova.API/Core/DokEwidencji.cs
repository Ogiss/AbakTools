using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly:
    TypeMap("Soneta.Core.DokEwidencji, Soneta.Core", typeof(Enova.API.Core.DokEwidencji), typeof(Enova.API.Connector.Core.DokEwidencji)),
    RowMap("DokEwidencja",typeof(Enova.API.Core.DokEwidencji),typeof(Enova.API.Core.CoreModule))]

namespace Enova.API.Core
{
    public interface DokEwidencji : Business.GuidedRow
    {
        StanEwidencji Stan { get; }
        string NumerDokumentu { get; }
        Types.Date DataDokumentu { get; }
        TypDokumentu Typ { get; }
        Types.Currency Wartosc { get; }
        DefinicjaDokumentu Definicja { get; }
        IDokumentKsiegowalny Dokument { get; }
        IPodmiot Podmiot { get; }

    }
}
