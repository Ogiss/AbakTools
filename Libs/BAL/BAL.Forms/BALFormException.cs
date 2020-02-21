using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms
{
    public class BALFormException : Exception
    {
        public BALFormException(string message) : base(message) { }
        public BALFormException(string message, Exception innerException) : base(message, innerException) { }
    }
}
