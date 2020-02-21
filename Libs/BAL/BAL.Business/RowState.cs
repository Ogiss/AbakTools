using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public enum RowState
    {
        Detached,
        Unchanged,
        Modified,
        Added,
        Deleted
    }
}
