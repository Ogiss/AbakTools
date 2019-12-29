using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Core
{
    public interface IPodmiot
    {
        int ID { get; }
        string Kod { get; }
        string Nazwa { get; }
    }
}
