using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: BAL.Types.Action(
    ActionType = typeof(BAL.Test.Handel.TowarDrukujAction),
    DataType = typeof(BAL.Test.Handel.Towar),
    Path = "Drukuj\\Kartoteka towaru",
    Description = "Wydruk kartoteki towarowej",
    Priority = 10,
    Target = BAL.Types.ActionTarget.FormMenu)]

namespace BAL.Test.Handel
{
    public class TowarDrukujAction
    {

        public void Action()
        {
            BAL.Forms.FormManager.Alert("Run action Drukuj kartoteka towarowa");
        }
    }
}
