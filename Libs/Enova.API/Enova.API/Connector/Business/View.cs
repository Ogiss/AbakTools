using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class View : API.Types.ObjectBase, API.Business.View
    {
        public string Filter
        {
            get { return (string)GetValue("Filter"); }
            set { SetValue("Filter", value); }
        }

        public API.Business.RowCondition Condition
        {
            get
            {
                return new API.Business.RowCondition() { EnovaObject = GetValue("Condition") };
            }
            set
            {
                SetValue("Condition", value.EnovaObject);
            }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return new EnovaEnumerator() { EnovaObject = ((IEnumerable)EnovaObject).GetEnumerator() };

        }

        public API.Business.View SetFilter(string filter)
        {
            this.Filter = filter;
            return this;
        }
    }
}
