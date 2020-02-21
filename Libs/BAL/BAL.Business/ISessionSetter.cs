using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    internal interface ISessionSetter
    {
        Session Session { set; }
    }
}
