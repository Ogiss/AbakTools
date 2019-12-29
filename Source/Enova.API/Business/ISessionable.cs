using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface ISessionable
    {
        Session Session { get; }
    }
}
