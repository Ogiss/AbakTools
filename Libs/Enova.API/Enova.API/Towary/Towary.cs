using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Towary
{
    public interface Towary : Business.GuidedTable<Towar>
    {
        Towar this[string kod] { get; }
    }
}
