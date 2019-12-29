using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.Core;

namespace Enova.Business.Old.DB.Web
{
    [DataEditForm("AbakTools.Zwroty.Forms.StatusZwrotuEditForm, AbakTools.Handel.Forms")]
    public partial class StatusZwrotu : ISaveChanges
    {
        /*
        public static StatusZwrotu GetByTyp(TypStatusuZwrotu typ)
        {
            return ContextManager.WebContext.StatusyZwrotow.Where(sz => sz.Typ == (int)typ).FirstOrDefault();
        }
         */

        public static StatusZwrotu GetByTyp(WebContext dc, TypStatusuZwrotu typ)
        {
            return dc.StatusyZwrotow.Where(sz => sz.Typ == (int)typ).FirstOrDefault();
        }


        public bool SaveChanges()
        {
            if (string.IsNullOrEmpty(this.Nazwa))
                return false;

            if (this.Guid == System.Guid.Empty)
                this.Guid = System.Guid.NewGuid();

            if (this.EntityState == System.Data.EntityState.Detached)
                ContextManager.WebContext.AddToStatusyZwrotow(this);


            ContextManager.WebContext.OptimisticSaveChanges();


            return true;
        }

        public override string ToString()
        {
            return this.Nazwa;
        }

    }
}
