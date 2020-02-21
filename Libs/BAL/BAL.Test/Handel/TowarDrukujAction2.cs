using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: BAL.Types.Action(
    ActionType = typeof(BAL.Test.Handel.TowarDrukujAction2),
    DataType = typeof(BAL.Test.Handel.Towar),
    Path = "Drukuj\\Kartoteka towaru 2",
    Description = "Wydruk kartoteki towarowej",
    Priority = 20,
    Target = BAL.Types.ActionTarget.FormMenu)]

namespace BAL.Test.Handel
{
    public class TowarDrukujAction2
    {

        public void Action()
        {
        }
    }
}
