using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class RozliczenieRow
    {
        public string KodKontrahenta { get; set; }
        public string NazwaKontrahenta { get; set; }
        public string Numer { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Termin { get; set; }
        public decimal? Należność { get; set; }
        public decimal? Zobowiązanie { get; set; }
        public decimal? KwotaDokumenty { get; set; }
        public decimal? KwotaZapłaty { get; set; }
        public string NumerDokumentu { get; set; }
        public string Opis { get; set; }
        public string Info { get; set; }
    }
}
