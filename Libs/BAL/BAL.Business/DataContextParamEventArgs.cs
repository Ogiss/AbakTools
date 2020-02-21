using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class DataContextParamEventArgs : EventArgs
    {
        private DataContextParam dcParam;
        private System.Windows.Forms.Control control;

        public DataContextParam Param
        {
            get { return this.dcParam; }
        }

        public System.Windows.Forms.Control Control
        {
            get { return this.control; }
        }

        public DataContextParamEventArgs(DataContextParam dataContextParam, System.Windows.Forms.Control control = null)
        {
            this.dcParam = dataContextParam;
            this.control = control;
        }
    }
}
