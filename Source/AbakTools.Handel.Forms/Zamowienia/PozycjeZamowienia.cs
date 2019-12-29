using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.Types;
using Enova.Business.Old.Core;

namespace EnovaTools.Forms.Web
{
    public class PozycjeZamowienia : TableBase<Enova.Business.Old.DB.Web.PozycjaZamowienia>
    {
        private Enova.Business.Old.DB.Web.Zamowienie zamowienie = null;

        public PozycjeZamowienia(Enova.Business.Old.DB.Web.Zamowienie zamowienie)
            : base((ICollection<Enova.Business.Old.DB.Web.PozycjaZamowienia>)zamowienie.PozycjeZamowienia)
        {
            this.zamowienie = zamowienie;
            this.BaseQuery = (ObjectQuery<Enova.Business.Old.DB.Web.PozycjaZamowienia>)zamowienie.PozycjeZamowienia.CreateSourceQuery();
            this.Filter = "Synchronizacja != " + RowSynchronizeOld.NotsynchronizedDelete;
        }
    }
}
