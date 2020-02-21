using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Business
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class DataContextAttribute : AttributeBase
    {
        #region Fields

        private Type dataType;
        private Type dataContextType;
        private static Dictionary<Type, Type> __cache__;

        #endregion

        #region Properties

        public Type DataType
        {
            get { return this.dataType; }
        }

        public Type DataContextType
        {
            get { return this.dataContextType; }
        }

        #endregion

        #region Methods

        public DataContextAttribute(Type dataType, Type dataContextType)
        {
            this.dataType = dataType;
            this.dataContextType = dataContextType;
        }

        private static void checkCache()
        {
            if (__cache__ == null)
            {
                __cache__ = new Dictionary<Type, Type>();
                var attrs = AppController.Instance.AssemblyAttributes[typeof(DataContextAttribute)];
                if (attrs != null)
                {
                    foreach (DataContextAttribute attr in attrs)
                    {
                        if (!__cache__.ContainsKey(attr.DataType))
                            __cache__.Add(attr.DataType, attr.DataContextType);
                        else
                            throw new AppControllerException("Zdublowana rejestracja kontekstu danych dla typu " + attr.DataType.FullName);
                    }
                }
            }
        }

        public static Type GetDataContextType(object obj, Type defaultType = null)
        {
            checkCache();
            var type = CoreTools.GetObjectType(obj);
            if (__cache__.ContainsKey(type))
                return __cache__[type];
            return defaultType;
        }

        #endregion
    }
}
