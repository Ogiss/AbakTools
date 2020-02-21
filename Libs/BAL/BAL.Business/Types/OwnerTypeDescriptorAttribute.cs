using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    [AttributeUsage( AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple=true, Inherited=true )]
    public class OwnerTypeDescriptorAttribute : Attribute
    {
        private Type type;

        public Type ProviderType
        {
            get { return type; }
        }

        public OwnerTypeDescriptorAttribute(Type providerType)
        {
            this.type = providerType;
        }
    }
}
