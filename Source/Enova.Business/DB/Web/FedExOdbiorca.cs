using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public partial class FedExOdbiorca : Enova.Business.Old.Core.IContextSaveChanges
    {
        #region Properties

        public string Adres
        {
            get
            {
                string str = "";
                string ul = Ulica.ToLower();
                if (!ul.StartsWith("ul.") && !ul.StartsWith("al.") && !ul.StartsWith("aleje ") && !ul.StartsWith("os.") && !ul.StartsWith("pl.") && !ul.StartsWith("pl "))
                    str += "ul. ";
                str += Ulica + " ";
                if (NrDomu != "" && NrDomu != "-")
                    str += NrDomu;
                if (NrLokalu != "" && NrLokalu != "-")
                    str += "/" + NrLokalu;
                str += ",  " + KodPoczt.Trim() + " " + Miasto.Trim();
                return str;
            }
        }

        #endregion

        #region Methods

        public bool SaveChanges(System.Data.Objects.ObjectContext dataContext)
        {
            var dc = (WebContext)dataContext;
            if (this.EntityState == System.Data.EntityState.Detached)
                dc.AddToFedExOdbiorcy(this);
            dc.OptimisticSaveChanges();
            dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
            return true;
        }

        public override string ToString()
        {
            return this.Nazwa;
        }

        #endregion

    }
}
