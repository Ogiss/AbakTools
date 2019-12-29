using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public partial class Atrybut
    {
        /*
        public GrupaAtrybutow GrupaAtrybutow
        {
            get
            {
                if (!GrupaAtrybutowRefReference.IsLoaded)
                    GrupaAtrybutowRefReference.Load();
                return GrupaAtrybutowRef;
            }
        }
        */
        public override string ToString()
        {

            return (GrupaAtrybutow != null ? GrupaAtrybutow.NazwaPubliczna + ": " : "") + Nazwa;
        }
    }
}
