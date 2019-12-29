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
    public class Kontrahenci : TableBase<Kontrahent>
    {

        public string PrzedstawicielKod;

        public override ObjectQuery<Kontrahent> BaseQuery
        {
            get
            {
                var q = base.BaseQuery.Where(r => r.CzyAgent == false);
                if (!string.IsNullOrEmpty(PrzedstawicielKod))
                    q = q.Where(r => r.Przedstawiciel.Kod == PrzedstawicielKod);
                return (ObjectQuery<Kontrahent>)q;
            }
            set
            {
                base.BaseQuery = value;
            }
        }

        public Kontrahenci(ObjectQuery<Kontrahent> query)
            : base(query)
        {
            DataContext = ContextManager.WebContext;
        }

        public Kontrahenci() : this((ObjectQuery<Kontrahent>) ContextManager.WebContext.Kontrahenci.Where(k=>k.CzyAgent == false)) { }

        public Kontrahenci(DB.Web.WebContext dbContext) : base(dbContext, "Kontrahenci") { }
    }
}
