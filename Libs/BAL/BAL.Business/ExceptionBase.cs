using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class ExceptionBase : Exception
    {
        public ExceptionBase(string message) : base(message) { }
    }
}
