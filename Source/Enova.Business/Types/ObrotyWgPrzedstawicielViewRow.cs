using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class ObrotyWgPrzedstawicielViewRow
    {
        public string Przedstawiciel { get; set; }
        public decimal? Sprzedaż { get; set; }
        public decimal? Korekty { get; set; }
        public decimal? Obrót
        {
            get
            {
                if (Sprzedaż != null && Korekty != null)
                {
                    return Sprzedaż + Korekty;
                }
                return null;
            }
        }
        public decimal? SprzedażProcent { get; set; }
        public decimal? KorektyProcent { get; set; }
        public decimal? ObrotyProcent { get; set; }
    }
}
