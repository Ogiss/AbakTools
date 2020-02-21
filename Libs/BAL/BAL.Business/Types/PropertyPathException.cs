using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Types
{
    public class PropertyPathException : ExceptionBase
    {
        public PropertyPathException(Type type, string path)
            : base(string.Format("\"{0}\" jest nieprawidłową ścieżką właściwości dla typu {1}", path, type.Name))
        {
        }
    }
}
