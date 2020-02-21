using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BAL.Forms
{
    public interface IControlContainer
    {
        IList GetControlCollection();
        Rectangle GetClientRectagle();
    }
}
