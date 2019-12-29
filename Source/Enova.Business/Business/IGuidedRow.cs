using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public interface IGuidedRow : IRow
    {
        Guid Guid { get; }
    }
}
