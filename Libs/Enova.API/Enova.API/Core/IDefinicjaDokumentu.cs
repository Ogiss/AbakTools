using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Business;

[assembly: Enova.API.TypeMap("Soneta.Core.IDefinicjaDokumentu, Soneta.Core", typeof(Enova.API.Core.IDefinicjaDokumentu), typeof(Enova.API.Connector.Core.IDefinicjaDokumentu))]

namespace Enova.API.Core
{
    public interface IDefinicjaDokumentu : IRow, ISessionable
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
