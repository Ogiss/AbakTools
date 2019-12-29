using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class EnovaEnumerable : API.Types.ObjectBase, IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new EnovaEnumerator() { EnovaObject = ((IEnumerable)EnovaObject).GetEnumerator() };
        }
    }
}
