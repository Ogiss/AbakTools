using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Core
{
    public interface IEuVatHost : Business.IRow, Business.ISessionable
    {
        // Properties
        string EuVAT { get; }
    }
}
