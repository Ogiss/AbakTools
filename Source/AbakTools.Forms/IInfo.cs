using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Forms
{
    public interface IInfo
    {
        string Info { get; }
        void AddInfo(string info);
    }
}
