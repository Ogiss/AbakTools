using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface Module : API.Types.IObjectBase, ISessionable
    {
 
        string Name { get; }
    }
}
