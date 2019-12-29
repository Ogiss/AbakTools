using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Core
{
    public interface IValidation
    {
        bool IsValid { get; }
        string ValidationError { get; }
        object ValidationInfo { get; }
    }
}
