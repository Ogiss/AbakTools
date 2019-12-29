using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    [Obsolete("Przy modyfikacji należy też zmodyfikować AbalTools.Handel.TypStatusuZwrotu")]
    public enum TypStatusuZwrotu
    {
        Nieznany = 0,
        Zarejestrowany = 1,
        Liczony = 2,
        Sprawdzony = 3,
        Załatwiony = 4,
        ZwrotZNieskorygowanych = 5
    }
}
