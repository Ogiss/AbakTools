using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: BAL.Business.DataContext(typeof(Enova.API.CRM.Kontrahent), typeof(Enova.Forms.CRM.KontrahentContext))]

namespace Enova.Forms.CRM
{
    public class KontrahentContext : DataContextWithEnovaApi
    {
        #region Methods

        public override string GetTitle()
        {
            var row = this.GetData() as Enova.API.CRM.Kontrahent;
            if (row != null)
                return "Kontrahent: " + row.Kod;
            return base.GetTitle();
        }

        #endregion
    }
}
