using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Tools
{
    public interface IIdentAttribute
    {
        // Properties
        string Caption { get; }
        Type DataType { get; }
        bool IsRightsSource { get; }
        string Name { get; }
        string Type { get; }
    }
}
