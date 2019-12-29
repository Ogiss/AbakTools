using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;


namespace Enova.Business.Old.DB
{
    public partial class Zaplata : Enova.Business.Old.Core.INumerDokumentu
    {
        public override string ToString()
        {
            return NumerDokumentu;
        }

        public ObjectQuery<RozliczenieSP> RozliczeniaSPQuery
        {
            get
            {
                if (EntityState == EntityState.Modified || EntityState == EntityState.Unchanged)
                {
                    return (ObjectQuery<RozliczenieSP>)Enova.Business.Old.Core.ContextManager.DataContext.RozliczeniaSP
                        .Where(r => r.ZaplataType == "Zaplaty" && r.ZaplataID == ID);
                }
                return null;
            }
        }

  
    }
}
