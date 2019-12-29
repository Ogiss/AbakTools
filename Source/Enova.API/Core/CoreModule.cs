using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Core
{
    public interface CoreModule : Business.Module
    {
        DokEwidencja DokEwidencja { get; }
    }
}
