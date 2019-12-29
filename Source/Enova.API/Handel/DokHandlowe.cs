using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Business;

namespace Enova.API.Handel
{
    public interface DokHandlowe : GuidedTable<DokumentHandlowy>
    {
        IEnumerable<API.Handel.DokumentHandlowy> WgKontrahentDataDefinicja(API.CRM.Kontrahent kontrahent, Magazyny.Magazyn magazyn,
            API.Types.FromTo fromTo, DefDokHandlowego definicja, API.Handel.StanDokumentuHandlowego stan);
    }
}
