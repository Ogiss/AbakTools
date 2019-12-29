using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Magazyny
{
    public interface Magazyny : Business.GuidedTable<Magazyn>
    {
        Magazyn Firma { get; }
    }
}
