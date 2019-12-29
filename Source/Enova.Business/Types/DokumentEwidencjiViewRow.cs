using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Enova.Business.Old.Types
{
    public class DokumentEwidencjiViewRow
    {
        public int ID { get; set; }
        public int? Stan { get; set; }
        public string KodKontrahenta { get; set; }
        public string NazwaKontrahenta { get; set; }
        public Enova.Business.Old.DB.DokumentHandlowy DokHandlowy { get; set; }
        public string NumerDokumentu { get; set; }
        public DateTime DataDokumentu { get; set; }
        public decimal? WartośćDokumentu { get; set; }
        public decimal? WartośćNetto { get; set; }
        public DateTime DataRozliczenia { get; set; }
        public string Przewoźnik { get; set; }
    }
}
