using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Business.SubRow, Soneta.Business", typeof(Enova.API.Business.SubRow), typeof(Enova.API.Connector.Business.SubRow))]

namespace Enova.API.Business
{
    public interface SubRow : API.Types.IObjectBase
    {
        Row Parent { get; }
        Row Root { get; }
    }
}
