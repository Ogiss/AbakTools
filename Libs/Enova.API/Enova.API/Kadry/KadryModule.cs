using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Kadry
{
    public interface KadryModule : Business.Module
    {
        Pracownicy Pracownicy { get; }
    }
}
