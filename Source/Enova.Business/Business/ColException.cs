using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Old.Types;
using Enova.Old.Tools;

namespace Enova.Business.Old
{
    public abstract class ColException : RowException
    {
        // Fields
        private string column;

        // Methods
        protected ColException(IRow row, string column, string comment)
            : base(row, comment)
        {
            this.column = column;
        }

        protected static string makeFieldName(IRow row, string name)
        {
            string prefix = row.Prefix;
            if (prefix == "")
            {
                return name;
            }
            return CaptionAttribute.ConvertName(prefix + '.' + name);
        }

        // Properties
        public string Column
        {
            get
            {
                string prefix = base.IRow.Prefix;
                if (prefix == "")
                {
                    return this.column;
                }
                return (prefix + '.' + this.column);
            }
        }
    }

}
