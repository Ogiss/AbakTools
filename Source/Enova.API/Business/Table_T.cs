using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface Table<T> : IEnumerable<T>, Table
        where T : Row
    {
        /*
        IRow this[int id] { get; }
        */
    }
}
