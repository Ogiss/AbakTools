using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Types
{
    public class CurrencySymbolTooLongException : Exception
    {
        // Methods
        public CurrencySymbolTooLongException()
            : base("Symbol waluty może mieć nie więcej niż 3 znaki długości")
        {
        }
    }
}
