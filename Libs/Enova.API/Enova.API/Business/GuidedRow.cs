using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Business.GuidedRow, Soneta.Business", typeof(Enova.API.Business.GuidedRow), typeof(Enova.API.Connector.Business.GuidedRow))]

namespace Enova.API.Business
{
    public interface GuidedRow : Row
    {
        Guid Guid { get; }
    }
}
