using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class GuidedRow : Row, API.Business.GuidedRow
    {
        #region Properties

        public Guid Guid
        {
            get { return GetValue<Guid>("Guid"); }
        }

        #endregion
    }
}
