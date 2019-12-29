using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB
{
    public partial class Magazyn
    {
        public override string ToString()
        {
            return Nazwa;
        }

        public decimal WartośćNetto
        {
            get
            {
                return (decimal)Zasoby.Where(z => z.Okres == 1 && z.Towar.Typ == 1 && z.PartiaTyp == 1).Sum(z => z.PartiaWartosc);
            }
        }

        public decimal WartoscBrutto
        {
            get
            {
                return decimal.Round((decimal)(from z in Zasoby.CreateSourceQuery()
                                               where z.Okres == 1 && z.Towar.Typ == 1 && z.PartiaTyp == 1
                                               select z.PartiaWartosc * (1 + z.Towar.DefinicjaStawki.StawkaProcent)).Sum(), 2);
            }
        }

        public static Magazyn GetFirmowy(EnovaContext ec)
        {
            Guid g = new Guid("00000000-0011-0004-0001-000000000000");
            return ec.Magazyny.Where(m => m.Guid == g).FirstOrDefault();
        }


    }
}
