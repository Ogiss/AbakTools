using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class EnovaView : API.Types.ObjectBase, IEnumerable
    {
        #region Properties

        public string Filter
        {
            get { return GetValue<string>("Filter"); }
            set { SetValue("Filter", value); }
        }

        #endregion

        #region Methods

        public IEnumerator GetEnumerator()
        {
            foreach (var row in ((IEnumerable)EnovaObject))
                yield return row;
        }

        #endregion
    }
}
