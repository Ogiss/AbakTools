using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Business
{
    public class TableNameAttribute : AttributeBase
    {
        #region Fields

        private string tableName;
        private Type rowType;

        #endregion

        #region Properties

        public string TableName
        {
            get { return tableName; }
        }

        public Type RowType
        {
            get { return rowType; }
            set { this.rowType = value; }
        }

        #endregion

        #region Methods

        public TableNameAttribute(string tableName, Type rowType)
        {
            this.tableName = tableName;
            this.rowType = rowType;
        }

        public TableNameAttribute(string tableName) : this(tableName, null) { }

        public static TableNameAttribute GetAttribute(Type classType)
        {
            var attributes = classType.GetCustomAttributes(typeof(TableNameAttribute), true);
            if (attributes.Length > 0)
                return (TableNameAttribute)attributes[0];
            return null;
        }

        public static string GetTableName(Type classType)
        {
            TableNameAttribute a = GetAttribute(classType);
            if (a != null)
                return a.tableName;
            return null;
        }

        #endregion
    }
}
