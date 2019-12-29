using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Types
{
    public class VariousCurrencyException : ArgumentException
    {
        // Methods
        public VariousCurrencyException(string symbol1, string symbol2)
            : base(string.Format("Nie można wykonywać tej operacji na wartościach w r\x00f3żnych walutach '{0}' i '{1}'.", symbol1, symbol2))
        {
        }
    }

}
