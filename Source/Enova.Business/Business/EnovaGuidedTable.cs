using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;

namespace Enova.Business.Old
{
    public partial class EnovaGuidedTable<T> : EnovaTable<T>
        where T : class,IGuidedRow, new()
    {
        #region Properties

        public T this[Guid guid]
        {
            get
            {

                return (T)this.BaseQuery.Where("it.Guid = @guid", new ObjectParameter("guid",guid)).FirstOrDefault();
            }
        }

        #endregion

        #region Methods

        public EnovaGuidedTable() { }

        public EnovaGuidedTable(ObjectQuery<T> query)
            : base(query)
        {
        }

        #endregion
    }
}
