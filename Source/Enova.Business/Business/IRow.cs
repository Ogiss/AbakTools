using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public interface IRow : IDbContext
    {
        int ID { get; }
        IRow Parent { get; }
        IRow Root { get; }
        string Prefix { get; }
        ITable Table { get; }
        RowState State { get; }
        bool IsLive { get; }
        bool IsReadOnly();
    }
}
