using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business.Old
{
    public interface ITable
    {
        void AcceptChanges();
        void RemoveRow(BAL.Business.IRow row);
    }
}
