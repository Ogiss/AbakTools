using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.DB;

namespace Enova.Old.Handel.Helpers
{
    internal sealed class DocumentPair
    {
        // Methods
        internal DocumentPair(DokumentHandlowy nadrzedny, DokumentHandlowy podrzedny)
        {
            this.Nadrzedny = nadrzedny;
            this.Podrzedny = podrzedny;
        }

        // Properties
        internal DokumentHandlowy Nadrzedny { get; private set; }

        internal DokumentHandlowy Podrzedny { get; private set; }
    }
}
