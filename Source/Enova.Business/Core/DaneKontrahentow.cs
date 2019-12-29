using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using Enova.Business.Old;
using Enova.Business.Old.DB;
using Enova.Business.Old.Core;

namespace Enova.Old.Core
{
    public partial class DaneKontrahentow : EnovaTable<DaneKontrahenta>
    {
        #region Fields

        private HostRelation relationHost;

        #endregion

        #region Properties

        public HostRelation WgHost
        {
            get
            {
                return this.relationHost;
            }
        }

        #endregion

        #region Methods

        public DaneKontrahentow(ObjectQuery<DaneKontrahenta> query)
            : base(query)
        {
            this.relationHost = new HostRelation(this);
        }

        public DaneKontrahentow(EnovaContext dc) : this(dc.DaneKontrahentow) { }

        #endregion

        #region Nested Types

        public class HostRelation : Key<DaneKontrahenta>
        {
            public HostRelation(TableBase<DaneKontrahenta> table)
                : base(table)
            {
            }

            public DaneKontrahenta this[IDaneKontrahentaHost host, int typ]
            {
                get
                {
                    var tableName = host.Table.TableName;
                    var id = host.ID;
                    return BaseQuery.Where(r => r.HostType == tableName && r.Host == id && r.Typ == typ).FirstOrDefault();
                    
                }
            }
        }

        #endregion

    }
}
