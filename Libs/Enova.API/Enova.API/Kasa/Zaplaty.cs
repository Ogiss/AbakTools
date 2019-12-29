using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Kasa
{
    public interface Zaplaty : Business.GuidedTable<Zaplata>
    {
        Business.SubTable WgEwidencjaData(EwidencjaSP ewidencjasp, Types.Date datadokumentu = null);

    }
}
