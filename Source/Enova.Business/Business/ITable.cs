using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public interface ITable
    {
        IQueryable BaseQuery { get; }
        string TableName { get; }
        void Adding(Module module);
    }
}
