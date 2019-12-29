using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;

namespace Enova.Old.Core
{
    public interface IDaneKontrahentaHost : IRow
    {
        // Methods
        bool IsReadOnlyDaneKontrahenta(int typ);
        void Update(int typ, DaneKontrahentaUpdate pola);
    }
}
