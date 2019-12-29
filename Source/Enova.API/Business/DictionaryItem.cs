using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Business.Db.DictionaryItem, Soneta.Business", typeof(Enova.API.Business.DictionaryItem), typeof(Enova.API.Connector.Business.DictionaryItem)),
           RowMap("Dictionary", typeof(Enova.API.Business.DictionaryItem), typeof(Enova.API.Business.BusinessModule))]

namespace Enova.API.Business
{
    public interface DictionaryItem : GuidedRow
    {
        string Category { get; }
        string Value { get; }
        int Lp { get; }
        string Path { get; }
        DictionaryItem Parent { get; }
    }
}
