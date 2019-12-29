using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface SubTable : IEnumerable, ISessionable
    {
        View CreateView();
        Type GetRowType();
        Table BaseTable { get; }
        bool IsEmpty { get; }
        ICollection Loaded { get; }
    }
}
