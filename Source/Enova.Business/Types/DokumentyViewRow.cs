using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class DokumentyViewRow
    {
        public int ID { get; set; }
        public string NumerPelny { get; set; }
        public DateTime Data { get; set; }
        public string KonKontrahenta { get; set; }
        public decimal? SumaNetto { get; set; }
        public decimal? SumaBrutto { get; set; }
        public string Przedstawiciel { get; set; }
        public decimal? DoRozliczenia { get; set; }
    }
}
