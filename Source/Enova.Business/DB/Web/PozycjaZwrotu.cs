using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public partial class PozycjaZwrotu
    {
        private bool toRemove = false;
        public bool ToRemove
        {
            get { return toRemove; }
            set { toRemove = value; }
        }
                            
        public decimal WartoscNetto
        {
            get { return this.Cena * (decimal)this.Ilosc; }
        }

        public double IloscPozostaloDoSkorygowania
        {
            get
            {
                return this.Ilosc - (this.IloscSkorygowana == null ? 0 : this.IloscSkorygowana.Value);
            }
        }
    }
}
