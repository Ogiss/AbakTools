using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true, Inherited=true)]
    internal class TypeMapAttribute : Attribute
    {
        private string enovaType;
        private Type apiType;
        private Type connectorType;
        private static Hashtable byEnova;
        private static Hashtable byApi;
        private static Hashtable byConn;

        public string EnovaType
        {
            get { return enovaType; }
        }

        public Type ApiType
        {
            get { return apiType; }
        }

        public Type ConnectorType
        {
            get { return connectorType; }
        }

        public TypeMapAttribute(string enovaType, Type apiType) : this(enovaType, apiType, null) { }

        public TypeMapAttribute(string enovaType, Type apiType, Type connectorType)
        {
            this.enovaType = enovaType;
            this.apiType = apiType;
            this.connectorType = connectorType;
        }

        private static void checkCache()
        {
            if (byEnova == null)
            {
                byEnova = new Hashtable();
                byApi = new Hashtable();
                byConn = new Hashtable();

                var attrs = AssemblyAttributes.GetAttributes(typeof(TypeMapAttribute)).Cast<TypeMapAttribute>();
                foreach (var attr in attrs)
                {
                    var enovaType = string.IsNullOrEmpty(attr.EnovaType) ? null : attr.EnovaType.Split(',')[0].Trim();
                    if (!string.IsNullOrEmpty(enovaType))
                        byEnova[enovaType] = attr;
                    if (attr.ApiType != null)
                        byApi[attr.ApiType] = attr;
                    if (attr.ConnectorType != null)
                        byConn[attr.ConnectorType] = attr;
                }

            }
        }

        public static TypeMapAttribute GetByEnova(object obj)
        {
            if (obj != null)
            {
                checkCache();
                string enovaType = null;
                if (obj.GetType() == typeof(string))
                    enovaType = (string)obj;
                else if (obj.GetType() == typeof(Type))
                    enovaType = ((Type)obj).FullName;
                else
                    enovaType = obj.GetType().FullName;
                if (byEnova.ContainsKey(enovaType))
                    return (TypeMapAttribute)byEnova[enovaType];
            }
            return null;
        }

        public static TypeMapAttribute GetByApiType(Type type)
        {
            if (type != null)
            {
                checkCache();
                if (byApi.ContainsKey(type))
                    return (TypeMapAttribute)byApi[type];
            }
            return null;
        }

        public static TypeMapAttribute GetByConnectorType(Type type)
        {
            if (type != null)
            {
                checkCache();
                if (byConn.ContainsKey(type))
                    return (TypeMapAttribute)byConn[type];
            }
            return null;
        }
    
    }

}
