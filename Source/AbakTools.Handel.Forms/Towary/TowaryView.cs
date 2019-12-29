using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Towary.Forms
{
    public class TowaryView : AbakTools.Forms.GridViewBase
    {
        #region Methods

        public TowaryView(BAL.Business.Session session)
            : base(session.Tables["Towary"])
        {
        }

        #endregion
    }
}
