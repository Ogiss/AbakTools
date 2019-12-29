using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;

namespace Enova.Old.Core
{
    public interface IDokument : IRow
    {
        // Properties
        DateTime Data { get; }
        IDefinicjaDokumentu Definicja { get; }
        NumerDokumentu Numer { get; }
    }


}
