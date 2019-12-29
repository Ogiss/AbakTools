using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface GuidedTable<T> : Table<T>
        where T : GuidedRow
    {
        T this[Guid guid] { get; }
    }
}
