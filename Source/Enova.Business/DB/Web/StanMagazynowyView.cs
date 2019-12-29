using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    /*
    public partial class StanMagazynowyView
    {
        #region Methods

        public double? StanRazem
        {
            get
            {
                if (this.StanMagazynowy == 0 && this.IloscZam == 0)
                    return (double?)null;
                return this.StanMagazynowy + this.IloscZam;
            }
        }


        #endregion

        #region Methods

        public StanMagazynowy GetStanMagazynowy(WebContext dc)
        {
            if (this.StanMagazynowyID != null)
            {
                var row = dc.StanyMagazynowe.Where(sm => sm.ID == this.StanMagazynowyID).FirstOrDefault();
                dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, row);
                return row;
            }
            return null;
        }


        #endregion
    }
     */
}
