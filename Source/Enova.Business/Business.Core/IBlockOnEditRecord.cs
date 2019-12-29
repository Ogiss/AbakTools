using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Core
{
    public interface IBlockOnEditRecord
    {
        bool Block { get; set; }
        string BlockInfo { get; set; }
        DateTime BlockStamp { get; set; }
    }
}
