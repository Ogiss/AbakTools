using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Core;
using BAL.Test.Core;

namespace BAL.Test.CRM
{
    public class CRMContext : CoreContext
    {
        public virtual DbSet<Kontrahent> Kontrahenci { get; set; }

        public CRMContext(BAL.Business.App.IDatabase database) : base(database) { }
    }
}
