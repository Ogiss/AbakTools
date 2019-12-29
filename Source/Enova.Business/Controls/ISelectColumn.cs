using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Controls
{
    public interface ISelectColumn
    {
        Type SelectFormType { get; }
        bool FormatedValueIsValid(object formatedValue);
    }
}
