using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.Core;

namespace Enova.Business.Old
{
    public class SubTable<TEntity> : ISubTable
        where TEntity : class,new()
    {
        #region Fields

        private TableBase<TEntity> table;
        private Key<TEntity> key;
        private object[] root;

        #endregion

        #region Properties

        public TableBase<TEntity> Table
        {
            get { return this.table; }
        }

        public ObjectQuery<TEntity> BaseQuery
        {
            get { return this.table.BaseQuery; }
        }

        #endregion

        #region Methods

        protected SubTable(TableBase<TEntity> table)
        {
            this.table = table;
        }

        public SubTable(SubTable<TEntity> table, object value)
        {
            this.table = table.table;
            this.key = table.key;
            this.root = new object[] { value };
        }

        public SubTable(SubTable<TEntity> table, params object[] values)
        {
            this.table = table.Table;
            this.key = table.key;
            this.root = values;
        }

        public SubTable(IKey key, object value)
        {
              this.table = (TableBase<TEntity>)key.Table;
              this.key = (Key<TEntity>)key;
              this.root = new object[] { value };
        }


        public SubTable(IKey key, params object[] values)
        {
            this.table = (TableBase<TEntity>)key.Table;
            this.key = (Key<TEntity>)key;
            this.root = values;
        }

        public TEntity Find(object data)
        {
            throw new NotImplementedException();
        }

        public TEntity GetPrev(params object[] data)
        {
            throw new NotImplementedException();
        }

        object ISubTable.GetPrev(params object[] data)
        {
            return this.GetPrev(data);
        }

        #endregion
    }
}
