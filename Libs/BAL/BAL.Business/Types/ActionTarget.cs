using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    [Flags]
    public enum ActionTarget
    {
        None = 0,
        MainMenu = 1,
        MainToolbar = 2,
        GridHeader = 4,
        GridToolbar = 8,
        FormMenu = 16,
        FormToolbar = 32
    }
}
