using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Types.Time, Soneta.Types", null, typeof(Enova.API.Types.Time))]

namespace Enova.API.Types
{
    public class Time : ObjectBase
    {
        public double TotalHours
        {
            get { return (double)GetValue("TotalHours"); }
        }
        public int TotalMinutes
        {
            get { return (int)GetValue("TotalMinutes"); }
        }

        public override string ToString()
        {
            return (string)CallMethod("ToString");
        }
    }
}
