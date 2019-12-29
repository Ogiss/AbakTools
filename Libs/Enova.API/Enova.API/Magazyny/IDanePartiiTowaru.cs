using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Towary;

namespace Enova.API.Magazyny
{
    public interface IDanePartiiTowaru
    {
        // Methods
        void ZmianaDokumentu(PartiaTowaru pt);

        // Properties
        Quantity Ilosc { get; set; }
        Magazyn Magazyn { get; }
        OkresMagazynowy Okres { get; }
        Towar Towar { get; }
    }
}
