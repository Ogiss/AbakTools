using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public interface IRowInvoker
    {
        void OnAdded();
        void OnDeleted();
        void OnLoaded();
    }
}
