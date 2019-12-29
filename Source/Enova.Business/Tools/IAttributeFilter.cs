using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Tools
{
    public interface IAttributeFilter
    {
        // Methods
        bool Filter(Type dataType, IIdentAttribute ident);
    }
}
