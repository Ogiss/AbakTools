using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;

namespace Enova.Old.Core
{
    [Obsolete("Brak implementacji WidoczneCechy")]
    public interface IDefinicjaDokumentu : IRow
    {
        // Properties
        string DomyślnaNumeracja { get; }
        DefinicjaNumeracji Numeracja { get; }
        string Symbol { get; }
        TypDokumentu Typ { get; }
        Type TypDokumentu { get; }
        //MemoText WidoczneCechy { get; }
    }
}
