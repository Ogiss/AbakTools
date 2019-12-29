using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface View : IEnumerable
    {
        string Filter { get; set; }
        RowCondition Condition { get; set; }
        View SetFilter(string filter);
    }
}
