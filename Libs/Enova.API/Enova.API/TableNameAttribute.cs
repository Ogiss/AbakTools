using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Interface, AllowMultiple=true,Inherited=true)]
    public class TableNameAttribute : Attribute
    {
        private string tableName;

        public string TableName
        {
            get { return tableName; }
        }

        public TableNameAttribute(string tableName)
        {
            this.tableName = tableName;
        }
    }
}
