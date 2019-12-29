using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Controls
{
    public interface ISelectForm
    {
        object SelectedItem { get; set; }
        bool SelectMode { get; set; }
        bool HideOnSelect { get; }
    }
}
