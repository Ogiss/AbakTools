using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Towary
{
    public interface CenySubTable : Business.SubTable
    {
        ICena this[string nazwa] { get; }
        ICena this[DefinicjaCeny definicja] { get; }

    }
}
