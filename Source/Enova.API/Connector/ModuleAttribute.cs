using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class ModuleAttribute : Attribute
    {
        #region Fields

        private static Dictionary<string, ModuleAttribute> byName;
        private static Dictionary<Type, ModuleAttribute> byType;
        private static Dictionary<Type, ModuleAttribute> byInterfaceType;
        private string name;
        private Type type;
        private Type interfaceType;

        #endregion

        #region Properties

        public string Name
        {
            get { return this.name; }
        }

        public Type Type
        {
            get { return this.type; }
        }

        public Type InterfaceType
        {
            get { return this.interfaceType; }
        }

        #endregion

        #region Methods

        public ModuleAttribute(string name, Type type, Type interfaceType)
        {
            this.name = name;
            this.type = type;
            this.interfaceType = interfaceType;
        }

        private static void checkCache()
        {
            if (byName == null)
            {
                byName = new Dictionary<string, ModuleAttribute>();
                byType = new Dictionary<Type, ModuleAttribute>();
                byInterfaceType = new Dictionary<Type, ModuleAttribute>();
                var attrs = AssemblyAttributes.GetAttributes(typeof(ModuleAttribute));
                foreach (ModuleAttribute attr in attrs)
                {
                    byName.Add(attr.Name, attr);
                    byType.Add(attr.Type, attr);
                    byInterfaceType.Add(attr.InterfaceType, attr);
                }
            }
        }

        public static ModuleAttribute GetAttributeByName(string name)
        {
            checkCache();
            if (byName.ContainsKey(name))
                return byName[name];
            return null;
        }

        public static ModuleAttribute GetAttributeByInterfaceType(Type type)
        {
            checkCache();
            if (byInterfaceType.ContainsKey(type))
                return byInterfaceType[type];
            return null;
        }

        #endregion
    }
}
