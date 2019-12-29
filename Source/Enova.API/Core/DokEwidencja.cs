using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Core
{
    public interface DokEwidencja : Business.GuidedTable<DokEwidencji>
    {
        Business.SubTable WgTyp(TypDokumentu typ, Types.Date datadokumentu = null);
        Business.SubTable WgPodmiot(IPodmiot podmiot, Types.Date datadokumentu = null); 
    }
}
