using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Core
{
    public interface IContextSaveChanges
    {
        bool SaveChanges(System.Data.Objects.ObjectContext dataContext);
    }
}
