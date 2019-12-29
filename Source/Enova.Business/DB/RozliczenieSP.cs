using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;

namespace Enova.Business.Old.DB
{
    public partial class RozliczenieSP : Enova.Business.Old.Core.INumerDokumentu
    {
        private object dokument = null;
        public object Dokument
        {
            get
            {
                if (dokument == null && EntityState != EntityState.Added && EntityState != EntityState.Detached)
                {
                    if (DokumentType == "Platnosci")
                    {
                        dokument = Enova.Business.Old.Core.ContextManager.DataContext.Platnosci
                            .Where(p => p.ID == DokumentID).FirstOrDefault();
                    }
                    else if (DokumentType == "Zaplaty")
                    {
                        dokument = Enova.Business.Old.Core.ContextManager.DataContext.Zaplaty.Where(z => z.ID == DokumentID).FirstOrDefault();
                    }
                }
                else if (dokument == null)
                {
                    if (DokumentType == "Platnosci")
                    {
                        dokument = new Platnosc();
                    }
                    else if (DokumentType == "Zaplaty")
                    {
                        dokument = new Zaplata();
                    }
                }
                return dokument;
            }
        }

        private object zaplata = null;
        public object Zapłata
        {
            get
            {
                if (zaplata == null && EntityState != EntityState.Added && EntityState != EntityState.Detached)
                {
                    if (ZaplataType == "Platnosci")
                    {
                        zaplata = Enova.Business.Old.Core.ContextManager.DataContext.Platnosci
                            .Where(p => p.ID == ZaplataID).FirstOrDefault();
                    }
                    else if (ZaplataType == "Zaplaty")
                    {
                        zaplata = Enova.Business.Old.Core.ContextManager.DataContext.Zaplaty.Where(z => z.ID == ZaplataID).FirstOrDefault();
                    }
                }
                else if (zaplata == null)
                {
                    if (ZaplataType == "Platnosci")
                    {
                        zaplata = new Platnosc();
                    }
                    else if (ZaplataType == "Zaplaty")
                    {
                        zaplata = new Zaplata();
                    }
                }
                return zaplata;
            }
        }

        public string NumerDokumentu
        {
            get
            {
                if (this.Dokument is Enova.Business.Old.Core.INumerDokumentu)
                    return ((Enova.Business.Old.Core.INumerDokumentu)Dokument).NumerDokumentu;
                return null;
            }
        }
    }
}
