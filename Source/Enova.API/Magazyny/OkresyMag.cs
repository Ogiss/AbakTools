using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Magazyny
{
    public interface OkresyMag : Business.GuidedTable<OkresMagazynowy>
    {
        OkresMagazynowy Aktualny { get; }
        OkresMagazynowy this[Types.Date data] { get; }
    }
}
