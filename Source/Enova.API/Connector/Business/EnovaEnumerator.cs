using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class EnovaEnumerator : API.Types.ObjectBase, IEnumerator
    {
        public object Current
        {
            get
            {
                var current = ((IEnumerator)EnovaObject).Current;
                if (current == null)
                    return null;
                return EnovaHelper.FromEnova(current);
            }
        }

        public bool MoveNext()
        {
            return ((IEnumerator)EnovaObject).MoveNext();
        }

        public void Reset()
        {
            ((IEnumerator)EnovaObject).Reset();
        }
    }
}
