using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class SaveRowErrorException : RowException
    {
        public SaveRowErrorException(IRow row) : base(row, "") { }
    }
}
