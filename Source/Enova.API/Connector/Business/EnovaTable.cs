using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class EnovaTable : API.Types.ObjectBase
    {
        #region Methods

        public EnovaView CreateView()
        {
            var view = CallMethod("CreateView");
            if (view != null)
                return new EnovaView() { EnovaObject = view };
            return null;
        }

        #endregion
    }
}
