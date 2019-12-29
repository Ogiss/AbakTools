using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;

namespace Enova.Business.Old.DB
{
    public partial class Bank : Enova.Business.Old.Core.IPodmiot, Enova.Business.Old.Core.IRozrachunkiQuery
    {
        public ObjectQuery<RozrachunekIdx> RozrachunkiQuery
        {
            get
            {
                if (EntityState == EntityState.Modified || EntityState == EntityState.Unchanged)
                {
                    return (ObjectQuery<RozrachunekIdx>)Enova.Business.Old.Core.ContextManager.DataContext.RozrachunkiIdx
                        .Where(r => r.PodmiotType == "Banki" && r.Podmiot == ID);
                }
                return null;
            }
        }

        public ObjectQuery<RozrachunekIdx> RozrachunkiWpłaty
        {
            get
            {
                return (ObjectQuery<RozrachunekIdx>)RozrachunkiQuery.Where(r => r.Typ == 20);
            }
        }

        public ObjectQuery<RozrachunekIdx> RozrachunkiWypłaty
        {
            get
            {
                return (ObjectQuery<RozrachunekIdx>)RozrachunkiQuery.Where(r => r.Typ == 21);
            }
        }

        public decimal Saldo
        {
            get
            {
                decimal? wp = RozrachunkiWpłaty.Sum(r => r.KwotaValue);
                decimal? wy = RozrachunkiWypłaty.Sum(r => r.KwotaValue);
                return (wp == null ? 0 : wp.Value) - (wy == null ? 0 : wy.Value);
            }
        }

    }
}
