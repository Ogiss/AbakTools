using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class EnovaEnumerable<T> : API.Types.ObjectBase, IEnumerable<T>
        where T : class, API.Business.Row
    {
        #region Methods

        public EnovaEnumerable(object enumerable)
        {
            this.EnovaObject = enumerable;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EnovaEnumerator<T>(((IEnumerable)EnovaObject).GetEnumerator());
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
