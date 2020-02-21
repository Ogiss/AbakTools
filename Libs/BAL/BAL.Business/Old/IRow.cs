using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business.Old
{
    [Obsolete("Interface do usunięcia i wszystkie jego implementacje")]
    public interface IRow
    {
        bool ReadOnly { get; }
        bool IsChanged { get; }

        void Delete();
        void BeginEdit();
        void EndEdit();
        void CancelEdit();
        void AcceptChanges();
        void CancelChanges();
        void Reload();
    }
}
