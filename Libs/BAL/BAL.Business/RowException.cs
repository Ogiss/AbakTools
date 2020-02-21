using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class RowException : BusException
    {
        // Fields
        private IRow row;

        // Methods
        public RowException(IRow row, string comment)
            : base(Format(true, row, comment))
        {
            this.row = row;
        }

        public RowException(IRow row, string format, params object[] args)
            : this(row, string.Format(format, args))
        {
        }

        protected RowException(bool addRowToComment, IRow row, string comment)
            : base(Format(addRowToComment, row, comment))
        {
            this.row = row;
        }

        public RowException(Exception inner, IRow row, string comment)
            : base(inner, Format(true, row, comment))
        {
            this.row = row;
        }

        public RowException(Exception inner, IRow row, string format, params object[] args)
            : this(inner, row, string.Format(format, args))
        {
        }

        private static string Format(bool addRowToComment, IRow row, string comment)
        {
            if ((addRowToComment && (row != null)) /*&& (row.Root.AccessRight != AccessRights.Denied)*/)
            {
                try
                {
                    if (!comment.Contains(row.Root.ToString()))
                    {
                        return string.Concat(new object[] { comment, "\n(", row.Root, ")" });
                    }
                }
                catch
                {
                }
            }
            return comment;
        }

        public static string GetRowInfo(IRow row)
        {
            if (row == null)
            {
                return "";
            }
            string str = "ID=" + row.ID;
            /*
            GuidedRow root = row.Root as GuidedRow;
            if (root != null)
            {
                str = str + "; GUID=" + root.Guid;
            }
             */
            return str;
        }

        // Properties
        public IRow IRow
        {
            get
            {
                return this.row;
            }
        }
    }
}
