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
                try
                {
                    var current = ((IEnumerator)EnovaObject).Current;
                    if (current == null)
                        return null;
                    return EnovaHelper.FromEnova(current);
                }catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool MoveNext()
        {
            try
            {
                return ((IEnumerator)EnovaObject).MoveNext();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Reset()
        {
            ((IEnumerator)EnovaObject).Reset();
        }
    }
}
