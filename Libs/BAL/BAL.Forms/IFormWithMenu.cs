using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms
{
    public interface IFormWithMenu
    {
        IList GetMenuItemCollection();
    }
}
