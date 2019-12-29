using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Core
{
    public interface IReadOnly
    {
        bool IsReadOnly { get; }
    }
}
