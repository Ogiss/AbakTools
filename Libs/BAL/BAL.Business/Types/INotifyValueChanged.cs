﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public interface INotifyValueChanged
    {
        event EventHandler ValueChanged;
    }
}
