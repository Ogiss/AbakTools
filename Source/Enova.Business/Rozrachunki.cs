using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;

namespace Enova.Business.Old
{
    public class Rozrachunki : Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.RozrachunekIdx>
    {
        public Rozrachunki(ObjectQuery<Enova.Business.Old.DB.RozrachunekIdx> query) : base(query) { }
        public Rozrachunki() : base(Enova.Business.Old.Core.ContextManager.DataContext, "RozrachunkiIdx") { }

        public static implicit operator Rozrachunki(ObjectQuery<Enova.Business.Old.DB.RozrachunekIdx> query)
        {
            return new Rozrachunki(query);
        }
    }
}
