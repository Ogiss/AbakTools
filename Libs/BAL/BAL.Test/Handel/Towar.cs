using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BAL.Types;
using BAL.Test.Core;
using BAL.Forms;

namespace BAL.Test.Handel
{
    [Table("Towary")]
    public class Towar : HandelModule.TowarRow
    {
        [NotMapped]
        public StawkaVat StawkaVat
        {
            get { return new StawkaVat() { Kod = "23%", Stawka = 0.23M }; }
        }
    }
}
