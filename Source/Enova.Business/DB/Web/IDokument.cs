using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public interface IDokument
    {
        DateTime Data { get; }
        DefinicjaDokumentu Definicja { get; }
    }
}
