using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

namespace BAL.Business
{
    public class SubTable<T> : ITable, ISessionable
        where T : Row
    {
        #region Fields

        private Table<T> table;

        #endregion

        #region Properties

        public string TableName
        {
            get { return this.table.TableName; }
        }

        public IRowCollection Rows
        {
            get { return this.Table.Rows; }
        }

        public Module Module
        {
            get { return this.table.Module; }
        }

        public Session Session
        {
            get { return this.table.Session; }
        }

        public virtual IQueryable<T> Query
        {
            get { return this.table.AsQueryable<T>(); }
        }

        public Table<T> Table
        {
            get { return this.table; }
        }

        IQueryable ITable.Query
        {
            get { return this.table.Query; }
        }

        public int Count
        {
            get { return this.table.Count; }
        }

        public ITable RootTable
        {
            get { return this.table.RootTable; }
        }

        #endregion

        #region Methods

        public SubTable(Table<T> table)
        {
            this.table = table;
        }

        public Type GetRowType()
        {
            return typeof(T);
        }

        public virtual void AddRow(IRow row)
        {
            this.table.AddRow(row);
        }

        public virtual void RemoveRow(IRow row)
        {
            ((Old.ITable)this.table).RemoveRow(row);
        }

        public IRow GetByID(int id)
        {
            return this.table.GetByID(id);
        }

        public void AcceptChanges()
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Query.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public View CreateView()
        {
            throw new NotImplementedException("Not implemented BAL.Business.SubTable.CreateView()");
        }



        #endregion
    }
}
