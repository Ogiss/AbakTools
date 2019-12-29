using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Controls
{
    public class KontrahentEnovaSelect : BAL.Forms.Controls.SelectBox
    {
        #region Properties

        #endregion

        #region Methods

        protected override BAL.Business.DataContext CreateDataContext()
        {
            return new CRM.KontrahenciView() { SelectionMode = true };
        }

        #endregion


    }
}
