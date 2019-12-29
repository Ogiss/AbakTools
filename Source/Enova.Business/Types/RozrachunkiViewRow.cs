using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class RozrachunkiViewRow
    {
        public int IDRozrachunku { get; set; }
        public int? Kierunek { get; set; }
        public int IDKontrahenta { get; set; }
        public string KodKontrahenta { get; set; }
        public string NazwaKontrahenta { get; set; }
        public string NumerDokumentu { get; set; }
        public DateTime? DataDokumentu { get; set; }
        public string PrzedstawicielDokumentu { get; set; }
        private Decimal? kwota;
        public Decimal? Kwota {
            get { return this.Zapłata ? 0 : this.kwota; }
            set { this.kwota = value; }
        }
        public Decimal? KwotaRozliczona { get; set; }
        public Decimal? Pozostało
        {
            get
            {
                if (kwota != null && KwotaRozliczona != null)
                {
                    return kwota - KwotaRozliczona;
                }
                return null;
            }
        }
        public DateTime? Termin { get; set; }
        public DateTime? DataRozliczenia { get; set; }
        public bool Zapłata { get; set; }
    }
}
