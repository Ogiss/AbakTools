using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Enova.Old.Types
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public class MaxLengthAttribute : Attribute
    {
        // Fields
        public readonly int MaxLength;

        // Methods
        public MaxLengthAttribute(int maxLength)
        {
            this.MaxLength = maxLength;
        }

        public static int Get(MemberInfo mi, int defaultValue)
        {
            MaxLengthAttribute customAttribute = (MaxLengthAttribute)Attribute.GetCustomAttribute(mi, typeof(MaxLengthAttribute));
            if (customAttribute != null)
            {
                return customAttribute.MaxLength;
            }
            return 0;
        }
    }

}
