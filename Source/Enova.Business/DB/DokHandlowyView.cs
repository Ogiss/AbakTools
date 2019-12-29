using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using Enova.Old.Handel;

namespace Enova.Business.Old.DB
{
    public class DokHandlowyView
    {
        public int ID { get; set; }
        public Guid Guid { get; set; }
        public string BazaDanych { get; set; }
        public DateTime Data { get; set; }
        public DateTime Termin { get; set; }
        public string NumerPelny { get; set; }
        public int Definicja { get; set; }
        public int? Stan { get; set; }
        public int KontrahentID { get; set; }
        public string KontrahentKod { get; set; }
        public string KontrahentNazwa { get; set; }
        public decimal? SumaNetto { get; set; }
        public decimal? SumaVat { get; set; }
        public decimal? SumaBrutto { get; set; }

        public bool Zatwierdzony
        {
            get
            {
                return Stan == (int)StanDokumentuHandlowego.Zablokowany || Stan == (int)StanDokumentuHandlowego.Zatwierdzony;
            }
        }
    }
}
