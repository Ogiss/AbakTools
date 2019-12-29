using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public interface IKey : ISubTable
    {
        ITable Table { get; }
        ISubTable CreateSubTable(params object[] values);
    }
}
