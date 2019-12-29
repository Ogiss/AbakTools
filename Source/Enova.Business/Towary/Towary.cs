using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using Enova.Business.Old;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB;

namespace Enova.Old.Towary
{
    public partial class Towary : EnovaGuidedTable<Towar>
    {
        #region Properties

        new public TowaryModule Module
        {
            get { return (TowaryModule)base.Module; }
        }

        #endregion

        #region Methods

        public Towary() { }

        public Towary(ObjectQuery<Towar> query)
            : base(query)
        {
        }

        public Towary(EnovaContext dc)
            : this(dc.Towary)
        {
            this.DataContext = dc;
        }

        protected override ObjectQuery<Towar> CreateQuery()
        {
            return Module.DataContext.Towary;
        }

        #endregion
    }
}
