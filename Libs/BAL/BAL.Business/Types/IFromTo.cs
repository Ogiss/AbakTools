using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public interface IFromTo
    {
        object From { get; set; }
        object To { get; set; }
    }
}
