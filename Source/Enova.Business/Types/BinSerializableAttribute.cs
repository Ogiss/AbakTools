using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Types
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
    public class BinSerializableAttribute : Attribute
    {
        // Properties
        public bool AffectDerived { get; set; }
    }
}
