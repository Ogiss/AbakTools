using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.DB;

namespace Enova.Business.Old.Types
{
    public class DokHandloweComparer : IEqualityComparer<DokumentHandlowy>
    {
        public bool Equals(DokumentHandlowy a, DokumentHandlowy b)
        {
            return a.ID == b.ID;
        }

        public int GetHashCode(DokumentHandlowy obj)
        {
            return obj.ID.GetHashCode();
        }

    }
}
