using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RightsSourceAttribute : Attribute
    {
        // Fields
        public readonly bool Enabled;

        // Methods
        public RightsSourceAttribute()
            : this(true)
        {
        }

        public RightsSourceAttribute(bool enabled)
        {
            this.Enabled = enabled;
        }
    }

}
