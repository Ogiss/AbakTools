using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    [Flags, Obsolete("Część flag do usuniecia")]
    internal enum RowStatus
    {
        None = 0,
        IsLive = 1,
        IsEditing = 2,
        IsChanged = 4,
        

        Deleting = 0x00010000,
        ReadOnly = 0x00020000
    }
}
