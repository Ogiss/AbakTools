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
    public class StatusyZwrotow : TableBase<StatusZwrotu>
    {
        public StatusyZwrotow(ObjectQuery<StatusZwrotu> query) : base(query) { }

        public StatusyZwrotow() :
            this((ObjectQuery<StatusZwrotu>)ContextManager.WebContext.StatusyZwrotow.Where(sz => sz.Deleted == false && sz.Synchronize != (int)RowSynchronizeOld.NotsynchronizedDelete))
        {
        }

        public override object CreateNewRecord()
        {
            StatusZwrotu status = new StatusZwrotu()
            {
                Guid = Guid.NewGuid(),
                Deleted = false,
                Synchronize = (int)RowSynchronizeOld.NotsynchronizedNew,
                Kolor = "white",
                Typ = (int)TypStatusuZwrotu.Nieznany
            };

            return status;
        }

    }
}
