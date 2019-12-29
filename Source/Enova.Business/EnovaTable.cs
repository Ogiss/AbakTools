using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using Enova.Business.Old.Core;

namespace Enova.Business.Old
{
    public class EnovaTable<TEntity> : TableBase<TEntity>
        where TEntity :class, IRow, new()
    {
        #region Properties

        public TEntity this[int id]
        {
            get { return this.BaseQuery.Where(r => r.ID == id).FirstOrDefault(); }
        }

        #endregion

        #region Methods

        public EnovaTable() { }

        public EnovaTable(ObjectQuery<TEntity> query)
            : base(query)
        {
        }

        #endregion

        #region Nested Types
        #endregion
    }
}
