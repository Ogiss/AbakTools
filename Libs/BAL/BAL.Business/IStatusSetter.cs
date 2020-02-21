using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    internal interface IStatusSetter
    {
        Old.RowStatus Status { set; }
    }
}
