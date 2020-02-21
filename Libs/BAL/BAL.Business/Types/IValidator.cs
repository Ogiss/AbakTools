using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public interface IValidator
    {
        bool IsValid(out string error);
    }
}
