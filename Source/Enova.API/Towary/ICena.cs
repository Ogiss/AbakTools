using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;

namespace Enova.API.Towary
{
    public interface ICena 
    {
        DoubleCy Brutto { get; set; }
        DoubleCy Netto { get; set; }
        double StandardowaIlosc { get; }
    }
}
