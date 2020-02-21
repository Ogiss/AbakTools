using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Business
{
    public class RowAction
    {
        #region Fields

        private string name;
        private ActionTarget target;
        private IList rows;

        #endregion

        #region Properties

        public string Name
        {
            get { return this.name; }
        }

        public ActionTarget Target
        {
            get { return this.target; }
        }

        public virtual IList Rows
        {
            get { return this.rows; }
            set { this.rows = value; }
        }

        public virtual System.Windows.Forms.Control Control { get; set; }

        #endregion

        #region Methods

        public RowAction(string name, ActionTarget target)
        {
            this.name = name;
            this.target = target;
        }

        #endregion
    }
}
