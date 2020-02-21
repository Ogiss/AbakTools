using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Test.Handel
{
    [Table("Kategorie"), DefaultProperty("Nazwa")]
    public class Kategoria : HandelModule.KategoriaRow
    {
    }
}
