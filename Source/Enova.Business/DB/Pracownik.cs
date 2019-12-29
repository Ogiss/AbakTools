using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.Core;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;

namespace Enova.Business.Old.DB
{
    public partial class Pracownik : IPodmiot, IRozrachunkiQuery
    {
        public string Nazwa
        {
            get { return Nazwisko + " " + Imie; }
        }

        public ObjectQuery<RozrachunekIdx> RozrachunkiQuery
        {
            get
            {
                if (EntityState == EntityState.Modified || EntityState == EntityState.Unchanged)
                {
                    return (ObjectQuery<RozrachunekIdx>)Enova.Business.Old.Core.ContextManager.DataContext.RozrachunkiIdx
                        .Where(r => r.PodmiotType == "Pracownicy" && r.Podmiot == ID);
                }
                return null;
            }
        }
        
    }
}
