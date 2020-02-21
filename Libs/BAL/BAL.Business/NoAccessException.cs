using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class NoAccessException : ExceptionBase
    {
        public NoAccessException(string message) : base(message) { }
    }
}
