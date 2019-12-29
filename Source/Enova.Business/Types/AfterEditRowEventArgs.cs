using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class AfterEditRowEventArgs : EventArgs
    {
        private object editedRow = null;

        public AfterEditRowEventArgs(object editedRow)
        {
            this.editedRow = editedRow;
        }

        public object EditedRow
        {
            get { return editedRow; }
        }
    }

    public delegate void AfterEditRowEventHandler(object sender, AfterEditRowEventArgs e);
}
