using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface Login: IDisposable
    {
        string OperatorName { get; }
        Session CreateSession(bool readOnly, bool config, string name);
        Session CreateSession(bool readOnly, bool config);
    }
}
