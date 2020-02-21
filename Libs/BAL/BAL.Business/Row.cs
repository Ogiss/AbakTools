using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BAL.Types;

namespace BAL.Business
{
    public partial class Row : RowBase, IRow, ISessionable, INotifyPropertyChanging, INotifyPropertyChanged, /*IDeletable, IEditable,*/ Old.IRow
    {
        #region Fields

        private int id;
        private Dictionary<string, MethodInfo> propertiesChangedMethods;
     
        internal ITable table;
        internal Session session;
        internal DBContextBase dbContext;

        internal RowState state;
        internal RowStatus status;

        #endregion

        #region Properties

        [NotMapped, Hidden]
        public Session Session
        {
            get
            {
                if (this.session != null)
                    return this.session;
                if (this.Table != null)
                    return this.Table.Session;
                return null;
            }
        }

        [NotMapped, Hidden]
        public ITable Table
        {
            get { return this.table; }
        }

        [NotMapped, Hidden]
        public virtual Module Module
        {
            get
            {
                if (this.Table != null)
                    return this.Table.Module;
                return null;
            }
        }

        [NotMapped, Hidden]
        public DBContextBase DBContext
        {
            get
            {
                if (this.dbContext != null)
                    return this.dbContext;
                if (this.Session != null && this.Table != null)
                    return ((Old.ISession)this.Session).GetDataContext(this.Table.Module.Name);
                return null;
            }
        }

        [NotMapped, Hidden]
        public Row Root
        {
            get { return this; }
        }

        [Key]
        public int ID
        {
            get { return this.id; }
            set
            {
                if (this.id != value)
                {
                    SetValue(() => { this.id = value; }, "ID");
                }
            }
        }

        [NotMapped, Hidden]
        public RowState State
        {
            get { return this.state; }
        }

        [NotMapped, Hidden]
        internal RowStatus Status
        {
            get { return this.status; }
        }

        [NotMapped, Hidden]
        public bool IsLive
        {
            get { return (status & RowStatus.IsLive) == RowStatus.IsLive; }
            internal set
            {
                if (value)
                    this.status |= RowStatus.IsLive;
                else
                    this.status &= ~RowStatus.IsLive;
            }
        }

        [NotMapped, Hidden]
        public bool IsEditing
        {
            get { return (status & RowStatus.IsEditing) == RowStatus.IsEditing; }
            internal set
            {
                if (value)
                    this.status |= RowStatus.IsEditing;
                else
                    this.status &= ~RowStatus.IsEditing;
            }
        }

        [NotMapped, Hidden]
        public bool IsChanged
        {
            get
            {
                if (this.State != RowState.Added && this.State != RowState.Modified)
                    return this.State == RowState.Deleted;
                return true;
            }
        }


        #endregion

        #region Methods

        public virtual void SetState(RowState state)
        {
            this.state = state;

            if (this.IsChanged)
            {
                if ((this.Status & RowStatus.IsChanged) == RowStatus.IsChanged)
                {
                    int index = this.Session.changedRows.IndexOf(this);
                    if (index >= 0 && index < this.Session.changedRows.Count)
                    {
                        this.Session.changedRows.RemoveAt(index);
                        this.Session.changedRows.Add(this);
                    }

                    index = this.Table.RootTable.Rows.Changed.IndexOf(this);
                    if (index >= 0 && index < this.Table.RootTable.Rows.Changed.Count)
                    {
                        this.Table.RootTable.Rows.Changed.RemoveAt(index);
                        this.Table.RootTable.Rows.Changed.Add(this);
                    }
                }
                else
                {
                    this.Session.changedRows.Add(this);
                    this.Table.RootTable.Rows.Changed.Add(this);
                    this.status |= RowStatus.IsChanged;
                }
            }

            if (this.state == RowState.Added || this.state == RowState.Modified || this.state == RowState.Unchanged)
                this.IsLive = true;
            else
                this.IsLive = false;

        }

        #endregion

        #region Events



        #endregion


        #region Methods (Do Poprawienia)

        public virtual PropertyInfo GetPropertyInfo(string name)
        {
            return this.GetType().GetProperty(name);
        }

        public virtual object GetFieldValue(string name)
        {
            var pinfo = GetPropertyInfo(name);
            if (pinfo != null)
                return pinfo.GetValue(this, null);
            return null;
        }

        public virtual IRow GetInterfaceField(string typeField, string idField)
        {
            var id = (int)GetFieldValue(idField);
            if (id == 0)
                return null;
            var type = (string)GetFieldValue(typeField);
            if (string.IsNullOrEmpty(type))
                return null;

            var table = this.Session.Tables[type];
            if (table == null)
                return null;
            return table.GetByID(id);
        }

        public void SetInterfaceField(string typeField, string idField, IRow row)
        {
            this.GetPropertyInfo(typeField).SetValue(this, row.Table.TableName, null);
            this.GetPropertyInfo(idField).SetValue(this, row.ID, null);
        }

        protected virtual MethodInfo GetPropertyChangedMethod(string propertyName)
        {
            if (this.propertiesChangedMethods == null)
                this.propertiesChangedMethods = new Dictionary<string, MethodInfo>();

            if (this.propertiesChangedMethods.ContainsKey(propertyName))
                return this.propertiesChangedMethods[propertyName];

            MethodInfo minfo = this.GetType().GetMethod("On" + propertyName + "Changed", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (minfo != null)
            {
                this.propertiesChangedMethods.Add(propertyName, minfo);
                return minfo;
            }
            return null;
        }

        public void SetValue( MethodDelegate setter, params string[] propertyName) 
        {
            if (this.IsLive)
            {
                ((Old.IRow)this).BeginEdit();
                foreach(var p in propertyName)
                this.OnPropertyChanging(p);
            }
            setter();
            if (this.IsLive)
            {
                for (int i = propertyName.Length - 1; i >= 0; i--)
                    this.OnPropertyChanged(propertyName[i]);
                    /*
                foreach (var p in propertyName)
                    this.OnPropertyChanged(p);
                     */
            }
        }

        #endregion

        #region INotifyPropertyChanging And INotifyPropertyChanged Implementation

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanging(string propertyName)
        {
            if (this.IsLive && PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.IsLive && PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            var minfo = this.GetPropertyChangedMethod(propertyName);
            if (minfo != null)
                minfo.Invoke(this, new object[0]);
        }

        #endregion

        #region Nested Types

        public delegate void RowDelegate<T>(T row) where T : Row;
        public delegate void MethodDelegate();

        public class RowConfiguration<R> : RowBaseConfiguration<R>
            where R : Row
        {
            protected RowConfiguration()
                : base()
            {
                HasKey(r => r.ID);
                Ignore(r => r.Root);
                Ignore(r => r.Module);
                Ignore(r => r.State);
                Ignore(r => r.DBContext);
                Ignore(r => r.Session);
                Ignore(r => r.Table);
            }
        }

        #endregion

        #region To Remove

        [NotMapped, Hidden]
        bool Old.IRow.ReadOnly
        {
            get
            {
                return (this.Table != null && this.Table.Session.ReadOnly) || ((this.status & RowStatus.ReadOnly) != RowStatus.None);
            }
        }

        [NotMapped, Hidden]
        bool Old.IRow.IsChanged
        {
            get
            {
                if (this.State != RowState.Added && this.State != RowState.Modified)
                {
                    return this.State == RowState.Deleted;
                }
                return true;
            }
        }

        void Old.IRow.Delete()
        {
            if (this.Table != null)
                ((Old.ITable)this.Table).RemoveRow(this);
        }

        void Old.IRow.BeginEdit()
        {
            if (!this.IsEditing && this.IsLive)
            {
                if (((Old.IRow)this).ReadOnly)
                {
                    throw new ReadOnlyException(this);
                }

                if (this.State == RowState.Deleted)
                {
                    throw new DeletedRowInaccessibleException(this);
                }

                if (this.State != RowState.Detached && this.Table != null)
                {
                    if (this.State == RowState.Unchanged)
                    {
                        this.SetState(RowState.Modified);
                    }
                }
                this.IsEditing = true;
            }
        }

        void Old.IRow.EndEdit()
        {
            if (this.IsEditing && this.IsLive)
            {
                ((Old.IRow)this).AcceptChanges();
                this.SetState(RowState.Unchanged);
                this.IsEditing = false;
            }
        }

        void Old.IRow.CancelEdit()
        {
            if (this.IsEditing)
            {
                ((Old.IRow)this).CancelChanges();
                if (State == RowState.Added)
                    this.SetState(RowState.Detached);
                else if (this.State == RowState.Modified)
                    this.SetState(RowState.Unchanged);
                this.IsEditing = false;
            }
        }

        void Old.IRow.AcceptChanges()
        {
            this.Session.Save();
        }

        void Old.IRow.CancelChanges()
        {
            if (this.Session != null)
            {
                ((Old.ISession)this.Session).Undo(this);

                if ((this.Status & RowStatus.IsChanged) == RowStatus.IsChanged)
                {
                    int idx = this.Session.changedRows.IndexOf(this);
                    if (idx >= 0 && idx < this.Session.changedRows.Count)
                        this.Session.changedRows.RemoveAt(idx);
                    idx = this.Table.Rows.Changed.IndexOf(this);
                    if (idx >= 0 && idx < this.Table.Rows.Changed.Count)
                        this.Table.Rows.Changed.Remove(this);
                    this.status &= ~RowStatus.IsChanged;
                }
            }
        }

        void Old.IRow.Reload()
        {
            if (this.DBContext != null)
            {
                this.DBContext.Entry(this).Reload();
                this.SetState(RowState.Unchanged);
            }
        }


        #endregion

    }
}
