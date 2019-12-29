using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.DB;

namespace Enova.Business.Old.Types
{
    public class DokumentyProwizjeViewRow
    {
        public static decimal ProcentProwizji = 50;
        public static decimal WspółczynnikKary = 1.25M;
        public static bool KaryWgCechy = false;
        public static string CechaKary = "KARY";


        public int ID { get; set; }
        public bool Korekta { get; set; }
        public int Magazyn { get; set; }
        public int? PodrzednyDokumentID { get; set; }
        public Enova.API.Handel.DokumentHandlowy PodrzednyDokument { get; set; }
        public DateTime? Data { get; set; }
        public string NumerPelny { get; set; }
        public String KodKontrahenta { get; set; }
        public decimal? WartoscSprzedazyNetto { get; set; }
        public decimal? WartoscSprzedazyBrutto { get; set; }
        public decimal? WartoscZakupuNetto { get; set; }
        public decimal? WartoscCenaAbak { get; set; }
        public DateTime? DataRozliczenia { get; set; }
        public decimal? KwotaOstatniegoRozliczenia { get; set; }
        public DateTime? Termin { get; set; }
        public DateTime TerminGraniczny { get; set; }
        public decimal? DoRozliczenia { get; set; } 
        public decimal? PotrącenieProwizji { get; set; }
        public decimal? Kara { get; set; }
        public IEnumerable<Obrot> Obroty { get; set; }
        public IEnumerable<PozycjaDokHandlowego> Pozycje { get; set; }
        public IEnumerable<RozliczenieSP> RozliczeniaSp { get; set; }
        public bool RozliczanaChanged = false;
        private bool? rozliczana = null;
        public bool? Rozliczana {
            get
            {
                return rozliczana;
            }
            set
            {
                if (rozliczana != null && value != rozliczana)
                    RozliczanaChanged = true;
                rozliczana = value;
            }
        }
        public string GroupName { get; set; }
        public decimal? DochodValue { get; set; }
        public decimal? ProwizjaValue { get; set; } 
        public decimal? Dochod
        {
            get
            {
                if (this.WartoscSprzedazyNetto != null && this.WartoscCenaAbak != null)
                    return this.WartoscSprzedazyNetto - this.WartoscCenaAbak;
                return null;
            }
        }
        public decimal? Prowizja
        {
            get
            {
                if (this.Dochod != null)
                {
                    if (KaryWgCechy && this.Korekta)
                    {
                        //decimal? d = this.Dochod;
                        //decimal prowizja = d == null ? 0 : d.Value * ProcentProwizji / 100M;
                        return this.ProwizjaValue + this.Kara;
                    }
                    else
                    {
                        return this.Dochod * ProcentProwizji / 100 * (this.Korekta ? WspółczynnikKary : 1M);
                    }
                }
                return null;
            }
        }

        public decimal? GetProwizja(decimal prowizja, decimal współczynnik)
        {
            if (this.Dochod != null)
                return this.Dochod * prowizja / 100M * (this.Korekta ? współczynnik : 1M);
            return null;
        }

        public decimal? Marza
        {
            get
            {
                if (Dochod != null)
                {
                    return Dochod / WartoscSprzedazyNetto;
                }
                return null;
            }
        }
        public override bool Equals(object obj)
        {
            return this.ID == ((DokumentyProwizjeViewRow)obj).ID;
        }
        public override int GetHashCode()
        {
            return this.ID;
        }
    }

    public class DokumentyProwizjeComparer : IEqualityComparer<DokumentyProwizjeViewRow>
    {
        public bool Equals(DokumentyProwizjeViewRow a, DokumentyProwizjeViewRow b)
        {
            return a.ID == b.ID;
        }

        public int GetHashCode(DokumentyProwizjeViewRow obj)
        {
            return obj.ID.GetHashCode();
        }
    }


}
