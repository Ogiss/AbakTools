using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Core
{
    public interface INipHost : Business.IRow, Business.ISessionable
    {
        string NIP { get; }
    }
}
