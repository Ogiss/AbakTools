using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms
{
    public interface IMenuItem : IComparable, IComparable<IMenuItem>
    {
        #region Properties

        IMenuItem Parent { get; set; }
        string Text { get; set; }
        MenuActionsType MenuAction { get; set; }
        Type FormType { get; set; }
        IList SubMenu { get; }
        MenuActionAttribute ActionAttribute { get; set; }
        event EventHandler Click;

        #endregion
    }
}
