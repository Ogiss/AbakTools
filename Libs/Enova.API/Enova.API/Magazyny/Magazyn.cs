using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Magazyny.Magazyn, Soneta.Handel", typeof(Enova.API.Magazyny.Magazyn), typeof(Enova.API.Connector.Magazyny.Magazyn)),
           RowMap("Magazyny", typeof(Enova.API.Magazyny.Magazyn), typeof(Enova.API.Magazyny.MagazynyModule))]

namespace Enova.API.Magazyny
{
    public interface Magazyn : Business.GuidedRow
    {
        string Symbol { get; }
        string Nazwa { get; }
        bool Firmowy { get; }
    }
}
