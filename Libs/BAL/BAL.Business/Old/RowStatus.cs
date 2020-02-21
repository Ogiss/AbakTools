using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business.Old
{
    [Flags]
    public enum RowStatus
    {
        None = 0,
        Added = 1,
        Deleting = 2,
        Editing = 4,
        ReadOnly = 8,
        IsLive = 0x10,
        IsChanged = 0x20,
        UndoChanges = 0x40
        
        /*
        Added = 8,
        Cloned = 0x10,
        Collected = 0x100,
        Deleting = 1,
        ForceChangeInfo = 0x800,
        IsLive = 0x80,
        IsRowChanged = 0x1000,
        NoChangeInfo = 0x40,
        None = 0,
        NonSerialize = 2,
        NoTransacted = 0x1701,
        OptimisticLocked = 0x200,
        ReadOnly = 4,
        SpecialEdit = 0x20,
        SpecialGet = 0x400
         */
    }
}
