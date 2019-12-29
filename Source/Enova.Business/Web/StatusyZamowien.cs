using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.Web
{
    public class StatusyZamowien : TableBase<StatusZamowienia>
    {
        public StatusyZamowien(ObjectQuery<StatusZamowienia> query) : base(query) { }

        public StatusyZamowien() : 
            this((ObjectQuery<StatusZamowienia>)ContextManager.WebContext.StatusyZamowien.Where(s=>s.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete)) { }

        public static implicit operator StatusyZamowien(ObjectQuery<StatusZamowienia> query)
        {
            return new StatusyZamowien(query);
        }
    }
}
