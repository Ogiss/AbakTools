using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms
{
    public class DuplicatedMenuItemException : MenuException
    {
        public DuplicatedMenuItemException(IMenuItem menuItem) : base(menuItem, string.Format("Istnieje już w kolekcji element IMenuItem o nazwie {0}", menuItem.Text)) { }
    }
}
