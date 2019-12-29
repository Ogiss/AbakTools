using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public interface ISubTable
    {
        object GetPrev(params object[] data);
    }
}
