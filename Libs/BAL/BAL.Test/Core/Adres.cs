using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using BAL.Forms;

[assembly: MenuAction("Kartoteki\\Adresy", MenuAction = MenuActionsType.OpenView, DataType = typeof(BAL.Test.Core.Adres))]

namespace BAL.Test.Core
{
    [Table("Adresy", Schema = "Core")]
    public class Adres : CoreModule.AdresRow
    {
    }
}
