using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Business
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class AppModuleAttribute : AttributeBase
    {
        #region Fields

        private string moduleName;
        private Type moduleType;
        private Type dbContextType;
        private static Dictionary<string, AppModuleAttribute> __byName_cache;
        private static Dictionary<Type, AppModuleAttribute> __byType_cache;
        private static AppModuleAttributeCollection collection;

        #endregion

        #region Properties

        public string ModuleName
        {
            get { return this.moduleName; }
        }

        public Type ModuleType
        {
            get { return this.moduleType; }
        }

        public Type DbContextType
        {
            get
            {
                return dbContextType;
            }
            set
            {
                dbContextType = value;
            }
        }

        public static AppModuleAttributeCollection Collection
        {
            get
            {
                if (collection == null)
                    collection = new AppModuleAttributeCollection();
                return collection;
            }
        }

        #endregion

        #region Methods

        public AppModuleAttribute(string moduleName, Type moduleType, Type dbContextType = null)
        {
            this.moduleName = moduleName;
            this.moduleType = moduleType;
            this.dbContextType = dbContextType;
        }

        private static void checkChache()
        {
            if (__byName_cache == null)
            {
                __byName_cache = new Dictionary<string, AppModuleAttribute>();
                __byType_cache = new Dictionary<Type, AppModuleAttribute>();
                var attrs = AppController.Instance.AssemblyAttributes[typeof(AppModuleAttribute)];
                foreach (AppModuleAttribute attr in attrs)
                {
                    __byName_cache.Add(attr.ModuleName, attr);
                    __byType_cache.Add(attr.ModuleType, attr);
                }
            }
        }

        public static implicit operator Type(AppModuleAttribute attr)
        {
            return attr != null ? attr.ModuleType : null;
        }

        public static implicit operator string(AppModuleAttribute attr)
        {
            return attr != null ? attr.ModuleName : null;
        }

        #endregion

        #region Nested Types

        public class AppModuleAttributeCollection : IEnumerable, IEnumerable<AppModuleAttribute>
        {
            public AppModuleAttribute this[string name]
            {
                get
                {
                    if (AppModuleAttribute.__byName_cache.ContainsKey(name))
                        return AppModuleAttribute.__byName_cache[name];
                    return null;
                }
            }

            public AppModuleAttribute this[Type type]
            {
                get
                {
                    if (AppModuleAttribute.__byType_cache.ContainsKey(type))
                        return AppModuleAttribute.__byType_cache[type];
                    return null;
                }
            }

            internal AppModuleAttributeCollection()
            {
                AppModuleAttribute.checkChache();
            }

            public IEnumerator<AppModuleAttribute> GetEnumerator()
            {
                return AppModuleAttribute.__byType_cache.Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }

        #endregion
    }
}
