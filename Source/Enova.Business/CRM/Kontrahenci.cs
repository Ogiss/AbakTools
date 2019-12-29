using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Enova.Business.Old;
using Enova.Business.Old.DB;

namespace Enova.Old.CRM
{
    public class Kontrahenci : EnovaGuidedTable<Kontrahent>
    {

        #region Methods

        protected override ObjectQuery<Kontrahent> CreateQuery()
        {
            return ((CRMModule)this.Module).DataContext.Kontrahenci;
        }

        #endregion
    }
}
