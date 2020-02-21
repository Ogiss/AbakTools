using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class ObjectDisposingEventArgs
    {
        private bool userCall;

        public bool UserCall
        {
            get { return userCall; }
        }

        public ObjectDisposingEventArgs(bool userCall)
        {
            this.userCall = userCall;
        }
    }

    public delegate void ObjectDisposingEventHandler(object sender, ObjectDisposingEventArgs e);
}
