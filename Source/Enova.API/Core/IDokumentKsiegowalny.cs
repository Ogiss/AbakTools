using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Business;

namespace Enova.API.Core
{
    public interface IDokumentKsiegowalny : IDokumentPodmiotu, IDokument, IRow, ISessionable
    {
        // Properties
        Date DataOperacji { get; }
        Date DataWpływu { get; }
        DokEwidencji Ewidencja { get; }
        //OddzialFirmy Oddział { get; }
        TypDokumentu Typ { get; }
        Currency Wartosc { get; }
    }
}
