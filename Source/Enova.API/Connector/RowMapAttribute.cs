using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector
{
    /*
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class RowMapAttribute : Attribute
    {
        private string enovaType;
        private string tableName;
        private Type connectorType;
        private Type apiType;
        private Type moduleType;
        private static Dictionary<string, RowMapAttribute> byEnovaCache;
        private static Dictionary<Type, RowMapAttribute> byConnectorCache;
        private static Dictionary<Type, RowMapAttribute> byApiCache;
        private static Dictionary<string, RowMapAttribute> byTableName;

        public string EnovaType
        {
            get { return enovaType; }
        }

        public Type ConnectorType
        {
            get { return connectorType; }
        }

        public Type ApiType
        {
            get { return apiType; }
        }

        public string TableName
        {
            get { return tableName; }
        }

        public Type ModuleType
        {
            get { return moduleType; }
        }

        public RowMapAttribute(string enovaType, string tableName, Type apiType, Type connectorType, Type moduleType = null)
        {
            this.enovaType = enovaType;
            this.connectorType = connectorType;
            this.apiType = apiType;
            this.moduleType = moduleType;
            this.tableName = tableName;
        }

        private static void checkCache()
        {
            if (byEnovaCache == null)
            {
                byEnovaCache = new Dictionary<string, RowMapAttribute>();
                byConnectorCache = new Dictionary<Type, RowMapAttribute>();
                byApiCache = new Dictionary<Type, RowMapAttribute>();
                byTableName = new Dictionary<string, RowMapAttribute>();

                var attrs = AssemblyAttributes.GetAttributes(typeof(RowMapAttribute));
                foreach (RowMapAttribute attr in attrs)
                {
                    if (!string.IsNullOrEmpty(attr.EnovaType) && !byEnovaCache.ContainsKey(attr.EnovaType))
                        byEnovaCache.Add(attr.EnovaType, attr);

                    if (attr.ConnectorType!=null && !byConnectorCache.ContainsKey(attr.ConnectorType))
                        byConnectorCache.Add(attr.ConnectorType, attr);

                    if (attr.ApiType != null && !byApiCache.ContainsKey(attr.apiType))
                        byApiCache.Add(attr.apiType, attr);

                    if (!string.IsNullOrEmpty(attr.TableName) && !byTableName.ContainsKey(attr.TableName))
                        byTableName.Add(attr.TableName, attr);
                }
            }
        }

        public static Type GetConnectorType(string enovaType)
        {
            checkCache();
            if (byEnovaCache.ContainsKey(enovaType))
                return byEnovaCache[enovaType].ConnectorType;
            return null;
        }

        public static string GetEnovaType(Type connectorType)
        {
            checkCache();
            if (byConnectorCache.ContainsKey(connectorType))
                return byConnectorCache[connectorType].EnovaType;
            return null;
        }

        public static RowMapAttribute GetByApiType(Type apiType)
        {
            checkCache();
            if (byApiCache.ContainsKey(apiType))
                return byApiCache[apiType];
            return null;
        }

        public static RowMapAttribute GetByEnovaType(string enovaType)
        {
            checkCache();
            if (byEnovaCache.ContainsKey(enovaType))
                return byEnovaCache[enovaType];
            return null;
        }

        public static RowMapAttribute GetByConnectorType(Type type)
        {
            checkCache();
            if (byConnectorCache.ContainsKey(type))
                return byConnectorCache[type];
            return null;
        }

        public static RowMapAttribute GetByTableName(string tableName)
        {
            checkCache();
            if (byTableName.ContainsKey(tableName))
                return byTableName[tableName];
            return null;
        }
    }
    */
}
