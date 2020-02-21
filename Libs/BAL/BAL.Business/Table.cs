using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using BAL.Types;

namespace BAL.Business
{
    public class Table<T> : ITable, IModuleSetter, IEnumerable<T>, IEnumerable, ISessionSetter, Old.ITable
        where T : Row
    {
        #region Fields

        internal Session session;
        internal Module module;
        internal Table<T> parentTable;
        internal Key<T> key;
        private string tableName;
        private RowCollection<T> rows;
        
        #endregion

        #region Properties

        [Hidden]
        public virtual Session Session
        {
            get
            {
                //return this.module.Session;
                if (this.parentTable != null)
                    return this.parentTable.Session;
                return this.session;
            }
        }

        Session ISessionSetter.Session
        {
            set { this.session = value; }
        }

        [Hidden]
        public virtual Module Module
        {
            get
            {
                if (this.parentTable != null)
                    return this.parentTable.Module;
                return this.module;
            }
        }

        Module IModuleSetter.Module
        {
            set { this.module = value; }
        }

        [Hidden]
        public virtual string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(tableName))
                {
                    this.tableName = TableNameAttribute.GetTableName(this.GetType());
                    if (string.IsNullOrEmpty(this.tableName))
                    {
                        this.tableName = TableNameAttribute.GetTableName(typeof(T));
                        if (string.IsNullOrEmpty(this.tableName))
                        {
                            var attributes = typeof(T).GetCustomAttributes(typeof(TableAttribute), true);
                            if (attributes.Length > 0)
                                this.tableName = ((TableAttribute)attributes[0]).Name;
                            else
                                this.tableName = this.GetType().Name;
                        }
                    }
                }

                return this.tableName;
            }
        }

        public Table<T> ParentTable
        {
            get
            {
                return this.parentTable;
            }
        }

        public Table<T> RootTable
        {
            get
            {
                if (this.ParentTable != null)
                    return this.ParentTable.RootTable;
                return this;
            }
        }

        ITable ITable.RootTable
        {
            get { return this.RootTable; }
        }

        [Hidden]
        public virtual IQueryable<T> Query
        {
            get
            {
                IQueryable<T> query = null;
                if (this.parentTable != null)
                    query = this.parentTable.Query;
                else
                    query = Module.DataContext.Set<T>();

                if (this.key != null)
                    query = key.AddWhere(query);

                return query;
            }
        }

        IQueryable ITable.Query
        {
            get { return this.Query; }
        }

        public T this[int id]
        {
            get
            {
                return this.GetByID(id);
            }
        }

        public virtual int Count
        {
            get
            {
                return this.Rows.Count();
            }
        }

        [Hidden]
        public virtual RowCollection<T> Rows
        {
            get
            {
                if (this.rows == null)
                    this.rows = new RowCollection<T>(this);
                return this.rows;
            }
        }

        IRowCollection ITable.Rows
        {
            get
            {
                return this.Rows;
            }
        }

        #endregion

        #region Methods

        public Table() :this(null) { }

        public Table(Table<T> parentTable)
        {
            this.parentTable = parentTable;
            this.key = this.CreateDefaultKey();
        }

        public Type GetRowType()
        {
            return typeof(T);
        }

        public virtual T GetByID(int id)
        {
            if (this.Query == null)
                return null;
            return this.Query.Where(r => r.ID == id).FirstOrDefault();
        }

        IRow ITable.GetByID(int id)
        {
            return this.GetByID(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual V CreateView<V>()
            where V : View, new()
        {
            V view = new V();
            view.table = this;
            view[typeof(Session)] = this.Session;
            return view;
        }

        public virtual View CreateView()
        {
            /*
            var view = new View(this);
            //view.Load();
            return view;
             */
            return this.CreateView<View>();
        }

        protected virtual Key<T> CreateDefaultKey()
        {
            return new Key<T>(this).InitField(r => r.ID);
        }

        public void AddRow(T row)
        {
            if (row.State == RowState.Detached)
            {
                row.session = this.Session;
                //row.table = this.Session.Tables[row.GetType()];
                row.table = this;
                if ((row.Status & RowStatus.IsChanged) == RowStatus.None)
                {
                    this.Session.changedRows.Add(row);
                    this.RootTable.Rows.Changed.Add(row);
                    row.status |= RowStatus.IsChanged;
                }
                row.state |= RowState.Added;
                row.IsLive = true;
                row.Invoke(RowInvokeType.Added, new EventArgs());

            }
        }

        public void AddRow(IRow row)
        {
            this.AddRow((T)row);
        }

        public virtual void AcceptChanges()
        {
            if (this.Rows.Changed != null && this.Rows.Changed.Count > 0)
            {
                foreach (var row in this.Rows.Changed)
                {
                    row.status = row.status & ~RowStatus.IsChanged;
                    row.SetState(RowState.Unchanged);
                    row.Invoke(RowInvokeType.Saved, new EventArgs());
                }
                this.Rows.Changed.Clear();
            }
        }


        #endregion

        #region Do Usunięcia


        public virtual void RemoveRowOld(T row)
        {
            row.status |= RowStatus.Deleting;
            CancelEventArgs e = new CancelEventArgs(false);
            row.Invoke(RowInvokeType.Deleting, e);
            if (e.Cancel)
            {
                row.status &= ~RowStatus.Deleting;
                return;
            }

            row.SetState(RowState.Deleted);

            row.Invoke(RowInvokeType.Deleted, new EventArgs());
        }

        void Old.ITable.RemoveRow(IRow row)
        {
            this.RemoveRowOld((T)row);
        }

        #endregion
    }
}
