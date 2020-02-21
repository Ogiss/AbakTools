using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited=true)]
    public class HiddenAttribute : Attribute
    {
    }
}
