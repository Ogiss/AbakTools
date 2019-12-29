using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public partial class DefinicjaNumeracji : IEnumerable
    {
        #region Methods
        #endregion
        public IEnumerator GetEnumerator()
        {
            return this.Wzor.Split(new char[] { '/' }).GetEnumerator();
        }
    }
}
