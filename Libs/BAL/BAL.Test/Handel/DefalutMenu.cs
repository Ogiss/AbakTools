using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;

[assembly: MenuAction("Dokumenty", MenuAction= MenuActionsType.OpenView, DataType=typeof(BAL.Test.Handel.Dokument), Priority=200)]

namespace BAL.Test.Handel
{
}
