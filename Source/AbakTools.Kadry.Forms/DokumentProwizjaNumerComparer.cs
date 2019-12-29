using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Kadry.Forms
{
    internal class DokumentProwizjaNumerComparer : Comparer<DokumentProwizjaRow>
    {
        private bool grupujKontrahentami;

        public DokumentProwizjaNumerComparer(bool grupujKontrahentami)
        {
            this.grupujKontrahentami = grupujKontrahentami;
        }

        public DokumentProwizjaNumerComparer() : this(false) { }

        public override int Compare(DokumentProwizjaRow x, DokumentProwizjaRow y)
        {
            int cmp = 0;
            if (grupujKontrahentami)
                cmp = x.KodKontrahenta.CompareTo(y.KodKontrahenta);
            if (cmp == 0)
                cmp = x.NumerPelny.CompareTo(y.NumerPelny);
            return cmp;
        }
    }
}
