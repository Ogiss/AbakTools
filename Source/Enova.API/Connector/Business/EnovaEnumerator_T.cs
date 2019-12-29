using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class EnovaEnumerator<T> : API.Types.ObjectBase, IEnumerator<T>
        where T : class, API.Business.Row
    {
        #region Fields
        #endregion

        #region Properties

        public T Current
        {
            get
            {
                var current = ((IEnumerator)EnovaObject).Current;
                if (current != null)
                    return (T)(object)EnovaHelper.FromEnova(current);
                return null;
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.Current; }
        }

        #endregion

        #region Methods

        public EnovaEnumerator(object enumerator)
        {
            this.EnovaObject = enumerator;
        }

        public void Dispose()
        {
            try
            {
                if (EnovaObject is IDisposable)
                    ((IDisposable)EnovaObject).Dispose();
                
            }
            catch { }
        }

        public bool MoveNext()
        {
            try
            {
                return ((IEnumerator)EnovaObject).MoveNext();
            }
            catch
            {
                return false;
            }
        }

        public void Reset()
        {
            try
            {
                ((IEnumerator)EnovaObject).Reset();
            }
            catch { }
        }

        #endregion

    }
}
