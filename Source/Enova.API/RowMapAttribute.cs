using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true, Inherited=true)]
    public class RowMapAttribute : Attribute
    {
        private static Hashtable byTableName;
        private static Hashtable byRowType;
        private string tableName;
        private Type rowType;
        private Type moduleType;

        public string TableName
        {
            get { return tableName; }
        }

        public Type RowType
        {
            get { return rowType; }
        }

        public Type ModuleType
        {
            get { return moduleType; }
        }

        public RowMapAttribute(string tableName, Type rowType, Type moduleType)
        {
            this.tableName = tableName;
            this.rowType = rowType;
            this.moduleType = moduleType;
        }

        private static void checkCache()
        {
            if (byTableName == null)
            {
                byTableName = new Hashtable();
                byRowType = new Hashtable();
                var attrs = AssemblyAttributes.GetAttributes(typeof(RowMapAttribute));
                foreach (RowMapAttribute attr in attrs)
                {
                    byTableName[attr.TableName] = attr;
                    byRowType[attr.RowType] = attr;
                }

            }
        }

        public static RowMapAttribute GetByTableName(string tableName)
        {
            checkCache();
            return (RowMapAttribute)byTableName[tableName];
        }

        public static RowMapAttribute GetByRowType(Type rowType)
        {
            checkCache();
            return (RowMapAttribute)byRowType[rowType];
        }
    }
}
