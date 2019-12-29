using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public class ZwrotAnaliza
    {
        public ZwrotAnalizaDokHandlowyCollectionOld Dokumenty { get; set; }
        public IEnumerable<PozycjaZwrotuAnalizaOld> Pozycje { get; set; }
    }
}
