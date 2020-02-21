using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class AccessException : RowException
    {
        // Methods
        public AccessException(IRow row, string comment)
            : base(false, row, comment)
        {
        }
    }
}
