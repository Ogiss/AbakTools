using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Handel
{
    public class DokumentHandlowyView
    {
        private API.Handel.DokumentHandlowy dokument;

        public API.Handel.DokumentHandlowy Dokument
        {
            get { return dokument; }
        }

        public bool Zatwierdzony
        {
            get { return Dokument.Zatwierdzony; }
        }

        public Guid Guid
        {
            get { return Dokument.Guid; }
        }

        public DateTime Data
        {
            get { return Dokument.Data; }
        }

        public string NumerPelny
        {
            get { return Dokument.Numer.NumerPelny; }
        }

        public string KontrahentKod
        {
            get { return Dokument.Kontrahent.Kod; }
        }

        public decimal SumaNetto
        {
            get { return Dokument.Suma.Netto; }
        }

        public decimal SumaBrutto
        {
            get { return Dokument.Suma.Brutto; }
        }

        public decimal SumaVAT
        {
            get { return Dokument.Suma.VAT; }
        }

        public DokumentHandlowyView(API.Handel.DokumentHandlowy dokument)
        {
            this.dokument = dokument;
        }
    }
}
