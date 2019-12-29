using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Kasa
{
    public interface RozrachunkiIdx : Business.Table<RozrachunekIdx>
    {
        IEnumerable<RozrachunekIdx> Nierozliczone(IPodmiotKasowy podmiot, Types.FromTo okres, DateTime aktualny);
        IEnumerable<RozrachunekIdx> Rozliczone(IPodmiotKasowy podmiot, Types.FromTo okres, DateTime aktualny);
        IEnumerable<RozrachunekIdx> Wszystkie(IPodmiotKasowy podmiot, Types.FromTo okres);
        Business.SubTable WgPodmiot(IPodmiotKasowy podmiot);

    }
}
