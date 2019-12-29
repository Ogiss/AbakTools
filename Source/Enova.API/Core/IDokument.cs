using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Business;

[assembly: Enova.API.TypeMap("Soneta.Core.IDokument, Soneta.Core", typeof(Enova.API.Core.IDokument), typeof(Enova.API.Connector.Core.IDokument))]

namespace Enova.API.Core
{
    public interface IDokument : IRow, ISessionable
    {
        // Properties
        Date Data { get; }
        IDefinicjaDokumentu Definicja { get; }
        NumerDokumentu Numer { get; }
    }
}
