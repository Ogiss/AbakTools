using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbakTools.Towary;

namespace AbakTools.Handel.Forms
{
    public class PozycjeReklamacjiView : AbakTools.Forms.GridViewBaseWithDbContext<Enova.Business.Old.DB.Web.PozycjaReklamacji>
    {
        #region Fields

        private Enova.Business.Old.DB.Web.Reklamacja reklamacja;

        #endregion

        #region Methods

        public PozycjeReklamacjiView(Enova.Business.Old.DB.Web.Reklamacja reklamacja)
        {
            this.reklamacja = reklamacja;
        }

        protected override Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.PozycjaReklamacji> CreateTable()
        {
            return new Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.PozycjaReklamacji>(reklamacja.Pozycje);
        }

        public PozycjeReklamacjiView(BAL.Business.DataContext dc) : this((Enova.Business.Old.DB.Web.Reklamacja)dc.GetData()) { }

        public Enova.Business.Old.DB.Web.PozycjaReklamacji CreateData(Enova.Business.Old.DB.Web.Produkt towar)
        {
            var row = (Enova.Business.Old.DB.Web.PozycjaReklamacji)CreateData();
            row.Reklamacja = reklamacja;
            row.Towar = towar;
            row.Lp = reklamacja.GetMaxLp() + 1;
            return row;
        }

        public override int Add(object obj)
        {
            Enova.Business.Old.DB.Web.PozycjaReklamacji row = null;
            if (typeof(Enova.Business.Old.DB.Web.Produkt).IsAssignableFrom(obj.GetType()))
            {
                row = this.CreateData((Enova.Business.Old.DB.Web.Produkt)obj);
                this.reklamacja.Pozycje.Add(row);
            }
            else
                row = (Enova.Business.Old.DB.Web.PozycjaReklamacji)obj;

            return base.Add(row);
        }


        #endregion
    }
}
