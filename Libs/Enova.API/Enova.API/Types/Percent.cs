using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Types.Percent, Soneta.types", null, typeof(Enova.API.Types.Percent))]

namespace Enova.API.Types
{
    public class Percent : ObjectBase
    {
        public override string ToString()
        {
            return (string)CallMethod("ToString");
        }

        public static implicit operator decimal(Percent p)
        {
            return EnovaService.FromEnova<decimal>(p);
        }

        public static explicit operator double(Percent p)
        {
            return EnovaService.FromEnova<double>(p);
        }


    }
}
