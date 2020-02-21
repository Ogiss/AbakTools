using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms
{
    public class CanceledRowEventArgs : RowEventArgs
    {
        public bool Cancel;

        public CanceledRowEventArgs(object row) : base(row) { }
    }
}
