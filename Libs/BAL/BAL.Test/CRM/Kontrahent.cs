using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

[assembly: BAL.Forms.MenuAction("Kartoteki\\Kontrahenci", MenuAction= BAL.Forms.MenuActionsType.OpenView, DataType = typeof(BAL.Test.CRM.Kontrahent))]

namespace BAL.Test.CRM
{
    [Table("Kontrahenci", Schema="CRM")]
    public partial class Kontrahent : CRMModule.KontrahentRow
    {


        public override string ToString()
        {
            return base.Kod + " - " + base.Nazwa;
        }
    }
}
