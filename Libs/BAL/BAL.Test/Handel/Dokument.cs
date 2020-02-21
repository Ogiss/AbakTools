using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Test.Handel
{
    [Table("Dokumenty")]
    public class Dokument : HandelModule.DokumentRow
    {
    }
}
