using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Towary
{
    public interface TowaryModule : Business.Module
    {
        Towary Towary { get; }
        DefinicjeCen DefinicjeCen { get; }
        CenyGrupowe CenyGrupowe { get; }

        decimal WyliczRabat(Guid kontrahentGuid, Guid towarGuid);
    }
}
