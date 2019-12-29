using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class GuidedTable<T> : Table<T>, API.Business.GuidedTable<T>
        where T : class,API.Business.GuidedRow
    {
        public T this[Guid guid]
        {
            get
            {
                return (T)(API.Business.GuidedRow)EnovaHelper.FromEnova(GetObjValue(EnovaObject, "Item", new Type[] { typeof(Guid) }, new object[] { guid }));
            }
        }
    }
}
