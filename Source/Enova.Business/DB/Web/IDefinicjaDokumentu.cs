using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public interface IDefinicjaDokumentu
    {
        DefinicjaNumeracji Numeracja { get; }
        string Symbol { get; }
    }
}
