using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class DefRabatu
    {
        public int? RabatRodzaj { get; set; }
        public int? RabatWliczaj { get; set; }
        public int? RabatGrupa { get; set; }
        public string RabatGrupaNazwa { get; set; }
        public int? RabatGrupaTowarowa { get; set; }
        public string RabatGrupaTowarowaNazwa { get; set; }
    }

    public class DefRabatuComparer : IEqualityComparer<DefRabatu>
    {
        public bool Equals(DefRabatu a, DefRabatu b)
        {
            return a.RabatRodzaj == b.RabatRodzaj && a.RabatWliczaj == b.RabatWliczaj &&
                a.RabatGrupa == b.RabatGrupa && a.RabatGrupaTowarowa == b.RabatGrupaTowarowa;
        }

        public int GetHashCode(DefRabatu obj)
        {
            return (obj.RabatRodzaj.ToString() + obj.RabatWliczaj.ToString() + obj.RabatGrupa.ToString() + obj.RabatGrupaTowarowa.ToString()).GetHashCode();
        }
    }

    public class DefRabatuGrupaTowarowaComparer : IEqualityComparer<DefRabatu>
    {
        public bool Equals(DefRabatu a, DefRabatu b)
        {
            return a.RabatGrupaTowarowa == b.RabatGrupaTowarowa;
        }

        public int GetHashCode(DefRabatu obj)
        {
            return obj.RabatGrupaTowarowa.GetHashCode();
        }
    }
}
