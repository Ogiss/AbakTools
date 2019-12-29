using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Analizy.Forms
{
    public class AnalizaZwrotowWgGrupRow
    {
        public int KontrahentID { get; set; }
        public string KontrahentKod { get; set; }
        public string KontrahentNazwa { get; set; }
        public decimal? WartoscObrotuI { get; set; }
        public decimal? WartoscObrotuII { get; set; }
        public decimal? WartoscSprzedarzyII { get; set; }
        public decimal? WartoscZwrotuIIValue { get; set; }
        public decimal? WartoscZwrotuII
        {
            get
            {
                if (WartoscZwrotuIIValue != null)
                    return WartoscZwrotuIIValue;
                if (WartoscObrotuII != null && WartoscSprzedarzyII != null)
                    return decimal.Round(WartoscSprzedarzyII.Value - WartoscObrotuII.Value, 2);
                return null;
            }
        }
        public double? ProcentSprzedazy
        {
            get
            {
                if (WartoscObrotuI != null && WartoscObrotuI != 0 && WartoscSprzedarzyII != null && WartoscSprzedarzyII != 0)
                    return (double)decimal.Round(WartoscSprzedarzyII.Value / WartoscObrotuI.Value - 1, 4);
                return null;
            }
        }
        public double? ProcentZwrotu
        {
            get
            {
                if (WartoscZwrotuII != null && WartoscZwrotuII != 0 && WartoscSprzedarzyII != null && WartoscSprzedarzyII != 0)
                    return (double)decimal.Round((WartoscZwrotuII.Value / WartoscSprzedarzyII.Value), 4);
                return null;
            }
        }
        public double? TeoretycznyProcentZwrotu
        {
            get
            {
                if (WartoscZwrotuII != null && WartoscZwrotuII != 0 && WartoscObrotuI != null && WartoscObrotuI != 0)
                    return (double)decimal.Round(WartoscZwrotuII.Value / WartoscObrotuI.Value, 4);
                return null;
            }
        }
    }
}
