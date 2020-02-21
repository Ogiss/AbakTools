using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using System.Data.Entity;
using System.Data.Entity.Core;
using BAL.Test.CRM;

namespace BAL.Test.Handel
{
    public class HandelContext : CRMContext
    {
        public virtual DbSet<Towar> Towary { get; set; }
        public virtual DbSet<Kategoria> Kategorie { get; set; }
        public virtual DbSet<Dokument> Dokumenty { get; set; }

        public HandelContext(BAL.Business.App.IDatabase database) : base(database)
        {
            Database.SetInitializer<HandelContext>(new DropCreateDatabaseIfModelChanges<HandelContext>());
        }
    }
}
