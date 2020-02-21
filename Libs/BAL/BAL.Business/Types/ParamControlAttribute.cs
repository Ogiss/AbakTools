using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Types
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true , Inherited = true)]
    public class ParamControlAttribute : AttributeBase
    {
        private Type dataType;
        private Type controlType;
        private static Dictionary<Type, Type> __cache__;

        public Type DataType
        {
            get { return this.dataType; }
            set { this.dataType = value; }
        }

        public Type ControlType
        {
            get { return this.controlType; }
        }

        public ParamControlAttribute(Type controlType)
        {
            this.dataType = dataType;
            this.controlType = controlType;
        }

        private static void checkCache()
        {
            if (__cache__ == null)
            {
                __cache__ = new Dictionary<Type, Type>();
                var attrs = BAL.Business.AppController.Instance.AssemblyAttributes[typeof(ParamControlAttribute)];
                if (attrs != null)
                {
                    foreach (ParamControlAttribute attr in attrs)
                        __cache__.Add(attr.DataType, attr.ControlType);
                }
            }
        }

        public static Type GetParamControlType(object obj)
        {
            Type type = obj is Type ? (Type)obj : obj.GetType();
            var attrs = type.GetCustomAttributes(typeof(ParamControlAttribute), true);
            if (attrs != null && attrs.Length > 0)
                return ((ParamControlAttribute)attrs[0]).ControlType;
            checkCache();
            if (__cache__.ContainsKey(type))
                return __cache__[type];
            return null;
        }
    }
}
