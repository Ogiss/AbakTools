using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Core;
using BAL.Test.Business;

namespace BAL.Test.Core
{
    public class CoreContext : BusinessContext
    {

        public virtual DbSet<Adres> Adresy { get; set; }

        public CoreContext(BAL.Business.App.IDatabase database) : base(database) { }
    }
}
