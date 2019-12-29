using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Core
{
    public interface INumerDokumentuHost
    {
        void Update();
        NumerDokumentu Numer { get; }
    }

}
