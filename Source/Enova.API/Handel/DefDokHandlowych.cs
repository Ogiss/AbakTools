using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Handel
{
    public interface DefDokHandlowych : Business.GuidedTable<DefDokHandlowego>
    {
        DefDokHandlowego FakturaSprzedazy { get; }
        DefDokHandlowego FakturaProforma { get; }
        DefDokHandlowego Paragon { get; }
        DefDokHandlowego KorektaSprzedazy { get; }
        DefDokHandlowego KorektaWZ { get; }
        DefDokHandlowego ZamowienieOdbiorcy { get; }
        DefDokHandlowego Przesuniecie { get; }
        DefDokHandlowego WydanieMagazynowe { get; }

        IEnumerable<DefDokHandlowego> this[API.Handel.KategoriaHandlowa kategoria] { get; }
    }
}
