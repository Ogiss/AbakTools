using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;

namespace Enova.Business.Old.Core
{
    public interface IRozrachunkiQuery
    {
        ObjectQuery<Enova.Business.Old.DB.RozrachunekIdx> RozrachunkiQuery { get; }
    }
}
