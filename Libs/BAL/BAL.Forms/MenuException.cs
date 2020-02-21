using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms
{
    public class MenuException : BALFormException
    {
        #region Fields

        private IMenuItem menuItem;

        #endregion

        public MenuException(IMenuItem menuItem, string message, Exception innerException)
            : base(message, innerException)
        {
            this.menuItem = menuItem;
        }

        public MenuException(IMenuItem menuItem, string message) : this(menuItem, message, null) { }

    }
}
