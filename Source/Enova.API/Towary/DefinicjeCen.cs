using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Towary
{
    public interface DefinicjeCen : Business.GuidedTable<DefinicjaCeny>
    {
        DefinicjaCeny this[string nazwa] { get; }
        IEnumerable<Business.FeatureDefinition> GrupyTowarowe { get; }
    }
}
