using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using Enova.Old.Types;
using Enova.Business.Old.Core;

namespace Enova.Business.Old
{
    public class Key<TEntity> : SubTable<TEntity>, IKey
        where TEntity : class,new()
    {
        #region Fields

        private PropertyPath[] propertyPaths;
        private Type[] propertyTypes;
        public bool Unique = false;

        #endregion

        #region Properties

        ITable IKey.Table
        {
            get { return this.Table; }
        }


        #endregion

        #region Methods

        public Key(TableBase<TEntity> table)
            : base(table)
        {
        }

        public ObjectQuery<TEntity> GetQuery(IQueryable<TEntity> query)
        {
            return (ObjectQuery<TEntity>) query;
        }

        public void InitFields(params string[] fields)
        {
            var rowType = this.Table.GetElementType();
            this.propertyPaths = new PropertyPath[fields.Length];
            this.propertyTypes = new Type[fields.Length];

            for (var i = 0; i < fields.Length; i++)
            {
                var p = new PropertyPath(rowType, fields[i]);
                this.propertyPaths[i] = p;
                this.propertyTypes[i] = p.GetLastType();
            }

        }

        public SubTable<TEntity> CreateSubTable(params object[] values)
        {
            throw new NotImplementedException();
        }

        ISubTable IKey.CreateSubTable(params object[] values)
        {
            return this.CreateSubTable(values);
        }

        #endregion
    }
}
