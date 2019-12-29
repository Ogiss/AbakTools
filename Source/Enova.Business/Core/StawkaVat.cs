using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Old.Types;
using Enova.Business.Old;

namespace Enova.Old.Core
{
    public class StawkaVat : SubRow
    {
        #region Properties

        public Percent Procent
        {
            get
            {
                decimal? procent = GetFieldValue<decimal?>("Procent");
                if (procent == null)
                    return Percent.Zero;
                return new Percent(procent.Value);
            }
        }

        #endregion

        #region Methods

        public StawkaVat(IRow parent, string name) : base(parent, name) { }

        public StawkaVat() : this(null, null) { }

        public decimal VatOdBrutto(decimal kwota)
        {
            throw new NotImplementedException("Enova.Core.StawkaVat.VatOdBrutto(decimal kwota)");
            //return Enova.Tools.Math.RoundCy((decimal)((kwota * this.Procent) / (this.Procent++));
        }

        public decimal VatOdNetto(decimal kwota)
        {
            return Enova.Old.Tools.Math.RoundCy((decimal)(kwota * this.Procent));
        }

        #endregion
    }
}
