using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB
{
    public partial class CenaGrupowa
    {
        public string RabatProcent
        {
            get
            {
                if (Rabat != null)
                {
                    return  decimal.Round((Rabat.Value * 100M),2).ToString() + "%";
                }
                return null;
            }
        }
    }
}
