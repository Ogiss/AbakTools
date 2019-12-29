using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public class RequiredException : ColException
    {
        // Methods
        public RequiredException(IRow row, string fieldName)
            : base(row, fieldName, "Wymagane jest wprowadzenie wartości pola '" + ColException.makeFieldName(row, fieldName) + "'")
        {
        }
    }
}
