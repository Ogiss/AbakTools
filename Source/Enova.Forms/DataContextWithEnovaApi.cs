using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms
{
    public class DataContextWithEnovaApi : BAL.Business.DataContext
    {
        #region Properties

        public Enova.API.EnovaService Service
        {
            get { return Enova.API.EnovaService.Instance; }
        }

        #endregion

        #region Methods

        protected DataContextWithEnovaApi() : base(null, true) { }

        #endregion

    }
}
