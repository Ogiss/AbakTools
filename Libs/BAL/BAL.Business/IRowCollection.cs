using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public interface IRowCollection : IEnumerable
    {
        IList Changed { get; }
    }
}
