using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class RaportFormularzWgGrupViewRow
    {
        public int? TowarID { get; set; }
        public Guid? TowarGuid { get; set; }
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public string Info { get; set; }
        public double? ObrotFV { get; set; }
        public double? ObrotFK { get; set; }
        public double? ObrotSuma
        {
            get
            {
                if (ObrotFV == null && ObrotFK == null)
                    return null;
                return (ObrotFV == null ? 0 : ObrotFV.Value) + (ObrotFK == null ? 0 : ObrotFK.Value);
            }
        }
        public decimal? ObrotFVWartosc { get; set; }
        public decimal? ObrotSumaWartosc { get; set; }
        public int Kolejnosc { get; set; }
        public string Kolor { get; set; }

        private decimal? cennaNetto;
        public decimal? CenaNetto
        {
            get
            {
                if (this.cennaNetto != null)
                    return this.cennaNetto;
                if (this.ObrotFV != null && this.ObrotFV != 0 && this.ObrotFVWartosc != null)
                {
                    return decimal.Round((decimal)(ObrotFVWartosc / (decimal?)ObrotFV), 2);
                }
                return null;
            }
            set
            {
                this.cennaNetto = value;
            }
        }
        public double? ProcentZwrotu
        {
            get
            {
                if (ObrotFV != null)
                {
                    double obrotFK = ObrotFK == null ? 0 : (double)Math.Abs((decimal)ObrotFK);
                    return round(obrotFK / (double)ObrotFV);
                }
                return null;
            }
        }
        public int? IloscFV { get; set; }
        public string Grupa { get; set; }
        public string Kolumna1 { get; set; }
        public string Kolumna2 { get; set; }
        public string Kolumna3 { get; set; }
        public string Kolumna4 { get; set; }
        public string Kolumna5 { get; set; }
        public IEnumerable ObrotyWZ { get; set; }
        public float? IloscZam { get; set; }
        public string Opis { get; set; }
        public bool TowarIndywidualny { get; set; }
        public double? StandardowaIlosc { get; set; }
        public double? StanMagazynu { get; set; }

        private double? round(double? d)
        {
            try
            {
                if (d != null)
                {
                    var r = (double)(d - (int)d);
                    if (r != 0)
                        return (double)decimal.Round((decimal)d, 2);
                }
            }
            catch {}
            return d;

        }

    }
}
