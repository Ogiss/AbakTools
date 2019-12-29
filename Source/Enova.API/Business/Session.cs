using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface Session : IDisposable
    {
        T GetModule<T>() where T : class, Module;
        Transaction CreateTransaction();
        Table GetTable(string tableName);
        void Save();
    }
}
