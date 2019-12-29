using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class EventCancelableArgs : EventArgs
    {
        private bool cancel = false;

        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }
    }

    public delegate void EventCancelableHandler(object sender, EventCancelableArgs e);
}
