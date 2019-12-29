using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Text;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;

namespace Enova.Business.Old.Web
{
    public class RejestrKorespondencji : TableBase<Korespondencja>
    {
        public RejestrKorespondencji(ObjectQuery<Korespondencja> query) : base(query) { }

        public RejestrKorespondencji() : this(ContextManager.WebContext.RejestrKorespondencji) { }

        public RejestrKorespondencji(string kod) : base(ContextManager.WebContext.RejestrKorespondencji.Where(r => kod == null || r.Kod == kod)) { }
    }
}
