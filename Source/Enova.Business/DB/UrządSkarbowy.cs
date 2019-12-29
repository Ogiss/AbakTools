using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;

namespace Enova.Business.Old.DB
{
    public partial class UrządSkarbowy : Enova.Business.Old.Core.IPodmiot, Enova.Business.Old.Core.IRozrachunkiQuery
    {
        public ObjectQuery<RozrachunekIdx> RozrachunkiQuery
        {
            get
            {
                if (EntityState == EntityState.Modified || EntityState == EntityState.Unchanged)
                {
                    return (ObjectQuery<RozrachunekIdx>)Enova.Business.Old.Core.ContextManager.DataContext.RozrachunkiIdx
                        .Where(r => r.PodmiotType == "UrzedySkarbowe" && r.Podmiot == ID);
                }
                return null;
            }
        }
    }
}
