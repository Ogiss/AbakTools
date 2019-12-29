using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;

namespace Enova.Business.Old.DB
{
    public partial class Platnosc : Enova.Business.Old.Core.INumerDokumentu
    {
        public IQueryable<RozliczenieSP> Rozliczenia
        {
            get
            {
                Enova.Business.Old.DB.EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;
                return dc.RozliczeniaSP.Where(r => r.DokumentType == "Platnosci" && r.DokumentID == this.ID);
            }
        }

        public override string ToString()
        {
            return NumerDokumentu;
        }

        private object dokument = null;
        public object Dokument
        {
            get
            {
                if (dokument == null)
                {
                    if (DokumentType == "DokHandlowe")
                    {
                        dokument = Enova.Business.Old.Core.ContextManager.DataContext.DokHandlowe
                            .Where(d => d.ID == DokumentID).FirstOrDefault();
                    }
                    else if (DokumentType == "DokRozliczeniowe")
                    {
                        dokument = Enova.Business.Old.Core.ContextManager.DataContext.DokRozliczeniowe
                            .Where(d => d.ID == DokumentID).FirstOrDefault();
                    }
                }
                return dokument;
            }
        }

        public ObjectQuery<RozliczenieSP> RozliczeniaSPZapłatyQuery
        {
            get
            {
                if (EntityState == EntityState.Modified || EntityState == EntityState.Unchanged)
                {
                    return (ObjectQuery<RozliczenieSP>)Enova.Business.Old.Core.ContextManager.DataContext.RozliczeniaSP
                        .Where(r => r.ZaplataType == "Platnosci" && r.ZaplataID == ID);
                }
                return null;
            }
        }
        
    }
}
