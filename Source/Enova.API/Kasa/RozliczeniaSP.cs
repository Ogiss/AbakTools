using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Kasa
{
    public interface RozliczeniaSP : Business.GuidedTable<RozliczenieSP>
    {
        IEnumerable<RozliczenieSP> this[IPodmiotKasowy podmiot] { get; }
        IEnumerable<RozliczenieSP> this[IPodmiotKasowy podmiot, Types.FromTo okres] { get; }
        Business.SubTable WgZaplata(IRozliczalny zaplata);
    }
}
