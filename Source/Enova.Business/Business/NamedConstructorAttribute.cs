using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class NamedConstructorAttribute : Attribute
    {
        // Fields
        public readonly string Name;

        // Methods
        public NamedConstructorAttribute(string name)
        {
            this.Name = name;
        }
    }

}
