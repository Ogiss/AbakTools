using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business.Old
{
    public interface ISession
    {
        DBContextBase GetDataContext(Type type);
        DBContextBase GetDataContext(string moduleName);
        DBContextBase GetDataContext(Module module);
        void Undo(Row row);
    }
}
