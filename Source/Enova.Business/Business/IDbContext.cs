using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Enova.Business.Old
{
    public interface IDbContext
    {
        ObjectContext DbContext { get; set; }
    }
}
