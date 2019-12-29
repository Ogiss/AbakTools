using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface Dictionary : GuidedTable<DictionaryItem>
    {
        IEnumerable<API.Business.DictionaryItem> this[string category] { get; }
        IEnumerable<API.Business.DictionaryItem> WgParent(API.Business.DictionaryItem parent);
    }
}
