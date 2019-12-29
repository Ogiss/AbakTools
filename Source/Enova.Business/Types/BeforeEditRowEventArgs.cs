using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class BeforeEditRowEventArgs : EventCancelableArgs
    {
        private object editingRow = null;

        public BeforeEditRowEventArgs(object editingRow)
        {
            this.editingRow = editingRow;
        }

        public object EditingRow
        {
            get { return editingRow; }
            set { editingRow = value; }
        }
    }

    public delegate void BeforeEditRowEventHandler(object sender, BeforeEditRowEventArgs e);
}
