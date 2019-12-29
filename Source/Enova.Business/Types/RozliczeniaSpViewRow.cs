using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class RozliczeniaSpViewRow
    {
        public int ID { get; set; }
        public int? Kierunek { get; set; }
        public DateTime Data { get; set; }
        public string KodKontrahenta { get; set; }
        public decimal? KwotaZaplaty { get; set; }
        public string NumerDokumentu { get; set; }
        public decimal? KwotaDokumentu { get; set; }
        public decimal? KwotaRozliczona { get; set; }
        public DateTime DataRozliczenia { get; set; }
        public string GroupName { get; set; }
    }
}
