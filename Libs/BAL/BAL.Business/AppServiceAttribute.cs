using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Business
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class AppServiceAttribute : AttributeBase
    {
        #region Fields

        private Type serviceInterface;
        private Type serviceType;
        private static Dictionary<Type, Type> __cache__;

        #endregion

        #region Properties

        public Type ServiceInterface
        {
            get { return this.serviceInterface; }
        }

        public Type ServiceType
        {
            get { return this.serviceType; }
        }

        #endregion

        #region Methods

        public AppServiceAttribute(Type serviceInterface, Type serviceType, int priority = 10000)
        {
            this.serviceInterface = serviceInterface;
            this.serviceType = serviceType;
        }

        private static void checkCache()
        {
            if (__cache__ == null)
            {
                __cache__ = new Dictionary<Type, Type>();
                var attrs = AppController.Instance.AssemblyAttributes[typeof(AppServiceAttribute)];
                foreach (AppServiceAttribute attr in attrs)
                {
                    if (__cache__.ContainsKey(attr.ServiceInterface))
                    {
                        if (PriorityAttribute.GetPriority(attr) < PriorityAttribute.GetPriority(__cache__[attr.ServiceInterface]))
                            __cache__[attr.ServiceInterface] = attr.ServiceType;
                    }
                    else
                        __cache__.Add(attr.ServiceInterface, attr.ServiceType);
                }
            }
        }

        public static Type GetServiceType(Type serviceInterfaceType, Type defaultType = null)
        {
            checkCache();
            if (__cache__.ContainsKey(serviceInterfaceType))
                return __cache__[serviceInterfaceType];
            return defaultType;
        }

        #endregion
    }
}
