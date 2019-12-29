using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Types
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
    public class CompoundNamesAttribute : Attribute
    {
        // Fields
        public readonly string[] DbNames;
        public readonly string[] Names;

        // Methods
        public CompoundNamesAttribute(string[] dbnames, string[] names)
        {
            this.Names = names;
            this.DbNames = dbnames;
        }

        public static CompoundNamesAttribute Get(Type t)
        {
            return (CompoundNamesAttribute)Attribute.GetCustomAttribute(t, typeof(CompoundNamesAttribute));
        }

        public Type[] GetTypes(Type valueType)
        {
            Type[] typeArray = new Type[this.DbNames.Length];
            int length = this.DbNames.Length;
            while (--length >= 0)
            {
                typeArray[length] = valueType.GetProperty(this.DbNames[length]).PropertyType;
            }
            return typeArray;
        }
    }

}
