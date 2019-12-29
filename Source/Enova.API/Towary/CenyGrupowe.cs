using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Towary
{
    public interface CenyGrupowe : Business.GuidedTable<CenaGrupowa>
    {
        IEnumerable<CenaGrupowa> this[CRM.Kontrahent kontrahent] { get; }
        CenaGrupowa this[CRM.Kontrahent kontrahent, Business.DictionaryItem grupa, Business.DictionaryItem grupaTowarowa] { get; }
        CenaGrupowa Create(Business.DictionaryItem grupaTowarowa, CRM.Kontrahent kontrahent);
    }
}
