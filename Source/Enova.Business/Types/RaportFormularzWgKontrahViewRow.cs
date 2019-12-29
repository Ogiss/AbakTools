using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class RaportFormularzWgKontrahViewRow
    {
        public int IDKontrahenta { get; set; }
        public string KodKontrahenta { get; set; }
        public string NazwaKontrahenta { get; set; }
        public int IDTowaru { get; set; }
        public string KodTowaru { get; set; }
        public string NazwaTowaru { get; set; }
        public double? obrotyFV { get; set; }
        public double? obrotyFK { get; set; }
        public double? obrotySuma { get; set; }
        public double? obrotyFVWstecz { get; set; }
        public double? obrotyFKWstecz { get; set; }
        public double? obrotySumaWstecz { get; set; }
    }
}
