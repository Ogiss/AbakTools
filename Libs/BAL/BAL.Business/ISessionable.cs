using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public interface ISessionable
    {
        Session Session { get; }
    }
}
