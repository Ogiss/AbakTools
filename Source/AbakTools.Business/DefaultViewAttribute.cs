using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Business
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true, Inherited=true)]
    public class DefaultViewAttribute : BAL.Types.AttributeBase
    {
        private static List<DefaultViewAttribute> __cache__;
        private static Dictionary<Type, DefaultViewAttribute> __byrowtype_cache;

        private Type rowType;
        private Type viewType;

        public Type RowType
        {
            get
            {
                return this.rowType;
            }
        }

        public Type ViewType
        {
            get { return viewType; }
        }

        public DefaultViewAttribute(Type rowType, Type viewType)
        {
            this.rowType = rowType;
            this.viewType = viewType;
        }

        private static void checkCache()
        {
            if (__cache__ == null)
            {
                __cache__ = new List<DefaultViewAttribute>();
                __byrowtype_cache = new Dictionary<Type, DefaultViewAttribute>();

                var attrs = BAL.Business.AppController.Instance.AssemblyAttributes[typeof(DefaultViewAttribute)];
                if (attrs != null)
                {
                    foreach (DefaultViewAttribute attr in attrs)
                    {
                        __cache__.Add(attr);
                        __byrowtype_cache.Add(attr.RowType, attr);
                    }
                }
            }
        }

        public static Type GetDefaultViewType(Type rowType)
        {
            checkCache();
            if (__byrowtype_cache.ContainsKey(rowType))
                return __byrowtype_cache[rowType].ViewType;
            return null;
        }
    }
}
