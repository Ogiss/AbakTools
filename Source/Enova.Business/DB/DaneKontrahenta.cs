using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Enova.Old.Core;
using Enova.Old.Handel;

namespace Enova.Business.Old.DB
{
    public partial class DaneKontrahenta : IRow, IDbContext
    {
        #region IRow Implementation

        IRow IRow.Parent
        {
            get { return null; }
        }
        IRow IRow.Root
        {
            get { return this; }
        }
        string IRow.Prefix
        {
            get { return ""; }
        }
        RowState IRow.State
        {
            get { return this.GetRowState(); }
        }

        public DaneKontrahentow Table
        {
            get { return CoreModule.GetInstance(this.DataContext).DaneKontrahentow; }
        }

        ITable IRow.Table
        {
            get { return this.Table; }
        }

        public bool IsLive
        {
            get { return this.GetIsLive(); }
        }

        public bool IsReadOnly()
        {
            return false;
        }

        #endregion

        #region IDataContext Implementation

        ObjectContext IDbContext.DbContext { get; set; }
        public EnovaContext DataContext
        {
            get { return (EnovaContext)((IDbContext)this).DbContext; }
        }

        #endregion

    }
}
