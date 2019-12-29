using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Old.Types;

namespace Enova.Business.Old
{
    public enum AccessRights
    {
        [Caption("Zakaz dostępu")]
        Denied = 0,
        [Caption("Pełne prawo")]
        Granted = 2,
        //[Hidden, Caption("Niezainicjowane")]
        NoInit = 3,
        [Caption("Tylko odczyt")]
        ReadOnly = 1
    }
}
