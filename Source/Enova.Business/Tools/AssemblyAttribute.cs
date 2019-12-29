using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Enova.Old.Tools
{
    [Serializable]
    public class AssemblyAttribute : Attribute
    {
        // Fields
        private Assembly assembly;

        // Methods
        internal void SetAssembly(Assembly assembly)
        {
            this.assembly = assembly;
        }

        // Properties
        public Assembly Assembly
        {
            get
            {
                return this.assembly;
            }
        }
    }
}
