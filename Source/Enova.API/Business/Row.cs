using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Business.Row, Soneta.Business", typeof(Enova.API.Business.Row), typeof(Enova.API.Connector.Business.Row))]

namespace Enova.API.Business
{
    public interface Row : IRow, ISessionable, API.Types.IObjectBase
    {
        int ID { get; }
        FeatureCollection Features { get; }

        void Refresh();
    }
}
