using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class ObrotyWgGrupTowarowych
    {
        public int IDKontrahenta { get; set; }
        public string KodKontrahenta { get; set; }
        public string NazwaKontrahenta { get; set; }
        public string GrupaTowarowa { get; set; }
        public decimal? ObrótNettoI { get; set; }
        public decimal? ObrótVatI { get; set; }
        public decimal? ObrótNettoII { get; set; }
        public decimal? ObrótVatII { get; set; }
        public decimal? RóżnicaNetto 
        {
            get
            {
                return ObrótNettoII - ObrótNettoI;
            }
        }
        public decimal? RóżnicaProcent
        {
            get
            {
                if (ObrótNettoI != null && ObrótNettoI.Value != 0)
                {
                    return decimal.Round(RóżnicaNetto.Value / ObrótNettoI.Value * 100M, 2);
                }
                return 100;
            }
        }
    }
}
