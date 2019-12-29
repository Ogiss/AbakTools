using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;

namespace Enova.Business.Old.DB
{
    public partial class ZUS : Enova.Business.Old.Core.IPodmiot, Enova.Business.Old.Core.IRozrachunkiQuery
    {
        public ObjectQuery<RozrachunekIdx> RozrachunkiQuery
        {
            get
            {
                if (EntityState == EntityState.Modified || EntityState == EntityState.Unchanged)
                    return (ObjectQuery<RozrachunekIdx>)Enova.Business.Old.Core.ContextManager.DataContext.RozrachunkiIdx
                        .Where(r => r.PodmiotType == "ZUSY" && r.Podmiot == ID);
                return null;
            }
        }
    }
}
