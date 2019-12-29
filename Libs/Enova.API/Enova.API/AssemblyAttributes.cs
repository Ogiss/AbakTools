using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Enova.API
{
    public class AssemblyAttributes
    {
        #region Fields

        private static List<Assembly> assemblies;
        private static Dictionary<Type, List<Attribute>> attributes;

        #endregion

        #region Properties
        #endregion

        #region Methods

        public static List<Attribute> GetAttributes(Type attributeType)
        {
            if (assemblies == null)
                assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.FullName.StartsWith("System") && !a.FullName.StartsWith("Microsoft") && !a.FullName.StartsWith("Soneta")).ToList();
            if (attributes == null)
                attributes = new Dictionary<Type, List<Attribute>>();
            if (attributes.ContainsKey(attributeType))
                return attributes[attributeType];
            attributes[attributeType] = new List<Attribute>();
            foreach (var asm in assemblies)
            {
                var attrs = asm.GetCustomAttributes(attributeType, true);
                foreach (Attribute attr in attrs)
                    attributes[attributeType].Add(attr);
            }
            return attributes[attributeType];
        }

        #endregion
    }
}
