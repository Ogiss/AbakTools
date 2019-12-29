using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.DB.Web;

namespace AbakTools.Zamowienia.Forms
{
    public class BrakiRow
    {
        public string KontrahentKod { get; set; }
        public string KontrahentNazwa { get; set; }
        public string TowarKod { get; set; }
        public string TowarNazwa { get; set; }
        public string Opis { get; set; }
        public double Ilosc { get; set; }
        public decimal Cena { get; set; }
        public decimal Wartosc
        {
            get
            {
                return decimal.Round(Cena * (decimal)Ilosc, 2);
            }
        }
        public AtrybutProduktu AtrybutProduktu;
        public string AtrybutNazwa
        {
            get
            {
                if (AtrybutProduktu != null)
                {
                    return AtrybutProduktu.AtrybutNazwa;
                }
                return null;
            }
        }
    }
}
