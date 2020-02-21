using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms
{
    public class CancelWithDataEventArgs : EventArgs
    {
        private bool cancel;
        private object data;

        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }

        public object Data
        {
            get { return data; }
        }

        public CancelWithDataEventArgs(object data, bool cancel = false)
        {
            this.cancel = cancel;
            this.data = data;
        }
    }
}
