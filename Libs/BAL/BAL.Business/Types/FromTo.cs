using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public class FromTo : IFromTo
    {
        #region Fields

        private object from;
        private object to;

        #endregion

        #region Properties

        public object From
        {
            get { return this.GetFrom(); }
            set { this.SetFrom(value); }
        }

        public object To
        {
            get { return this.GetTo(); }
            set { this.SetTo(value); }
        }

        #endregion

        #region Methods

        public FromTo(object from, object to)
        {
            this.SetFrom(from);
            this.SetTo(to);
        }

        public FromTo(object obj) : this(obj, obj) { }

        protected virtual object GetFrom()
        {
            return this.from;
        }

        protected virtual void SetFrom(object value)
        {
            this.from = value;
        }

        protected virtual object GetTo()
        {
            return this.to;
        }

        protected virtual void SetTo(object value)
        {
            this.to = value;
        }

        #endregion
    }
}
