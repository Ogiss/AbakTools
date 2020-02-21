using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public class FromTo<T> : FromTo
    {
        #region Properties

        new public T From
        {
            get { return (T)this.GetFrom(); }
            set { this.SetFrom(value); }
        }

        new public T To
        {
            get { return (T)this.GetTo(); }
            set { this.SetTo(value); }
        }

        #endregion

        #region Methods

        public FromTo(T from, T to) : base(from, to) { }

        #endregion
    }
}
