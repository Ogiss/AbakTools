using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB
{
    public class TowarRow
    {
        
        public int ID { get; set; }
        public int MagazynID { get; set; }
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public string FullName { get { return this.Kod + " - " + this.Nazwa; } }
        public double? CenaHurtowaNetto { get; set; }
        public double? CenaHurtowaBrutto { get; set; }
        public double? CenaPodstawowaNetto { get; set; }
        public double? CenaPodstawowaBrutto { get; set; }
        public double? Ilość { get; set; }
        public string IlośćStr
        {
            get
            {
                return Ilość == null ? null : decimal.Round((decimal)Ilość, 2).ToString();
            }
        }
        public decimal? Marża
        {
            get
            {
                if (CenaHurtowaNetto != null && CenaPodstawowaNetto != null)
                {
                    return decimal.Round((decimal)(CenaHurtowaNetto - CenaPodstawowaNetto) / (decimal)CenaHurtowaNetto * 100M, 2);
                }
                return null;
            }
        }
        public string MarżaStr
        {
            get
            {
                if (Marża != null)
                {
                    return Marża.ToString() + "%";
                }
                return null;
            }
        }

        /*
        public static explicit operator Towar(TowarRow towar)
        {
            return Enova.Business.Core.ContextManager.DataContext.Towary.Where(t => t.ID == towar.ID).FirstOrDefault();
        }
         */

        public static implicit operator Towar(TowarRow towar)
        {
            return Enova.Business.Old.Core.ContextManager.DataContext.Towary.Where(t => t.ID == towar.ID).FirstOrDefault();
        }

        public Towar GetTowar()
        {
            return Enova.Business.Old.Core.ContextManager.DataContext.Towary.Where(t => t.ID == this.ID).FirstOrDefault();
        }

        internal bool blokada;

        public bool Blokada
        {
            get
            {
                return this.blokada;
            }
            set
            {
                this.blokada = value;
                Towar towar = this.GetTowar();
                towar.Blokada = value;
                Enova.Business.Old.Core.ContextManager.DataContext.OptimisticSaveChanges();
            }
        }
    }
}
