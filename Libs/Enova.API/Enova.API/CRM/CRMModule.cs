using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.CRM
{
    public interface CRMModule : Business.Module
    {
        Kontrahenci Kontrahenci { get; }
        Banki Banki { get; }
        UrzedySkarbowe UrzedySkarbowe { get; }
        ZUSY ZUSY { get; }
    }
}
