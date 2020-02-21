using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using BAL.Types;

namespace BAL.Business
{
    public class View : DataContext, IEnumerable, ICollection, IList, IBindingList
    {
        #region Fields

        internal ITable table;
        internal IList rows;
        private object syncRoot;
        private DataContext childContext;
        private ListChangedEventArgs resetArgs = new ListChangedEventArgs(ListChangedType.Reset, -1);
        private bool isNewRow;
        private ListSortDirection sortDirection;
        private PropertyDescriptor sortProperty;
        private bool isSorted;
        private int position;
        private bool selectionMode;
        private IComparer sortComparer;
        private bool isLoaded;
        //private PropertyPath sortPropertyPath;

        #endregion

        #region Properties

        public override string Key
        {
            get
            {
                return base.Key + (this.SelectionMode ? "Select" : "");
            }
        }

        public virtual IList Rows
        {
            get
            {
                if (this.rows == null)
                    //this.initRows();
                    this.rows = this.GetRows();
                return this.rows;
            }
        }

        public virtual ITable Table
        {
            get
            {
                return this.table;
            }
        }

        public virtual bool IsSynchronized
        {
            get { return true; }
        }

        public virtual object SyncRoot
        {
            get
            {
                if (this.syncRoot == null)
                    this.syncRoot = new object();
                return this.syncRoot;
            }
        }

        public virtual int Count
        {
            get
            {
                return this.Rows.Count;
            }
        }

        public virtual int Position
        {
            get { return this.position; }
            set
            {
                if (this.position != value)
                {
                    this.position = value;
                    this.OnPositionChanged(new EventArgs());
                }
            }
        }

        public virtual object Current
        {
            get { return this.Rows.Count > 0 ? this.Rows[this.Position] : null; }
        }

        public virtual bool IsFixedSize
        {
            get { return true; }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                if (this.Session != null)
                    return this.Session.ReadOnly;
                return true;
            }
        }

        public virtual bool IsNewRow
        {
            get { return this.isNewRow; }
        }

        public override bool IsEditing
        {
            get
            {
                if (this.childContext != null)
                    return this.childContext.IsEditing;
                return base.IsEditing;
            }
        }

        public bool IsLoaded
        {
            get { return this.isLoaded; }
        }

        public virtual object this[int index]
        {
            get { return this.Rows[index]; }
            set { this.Rows[index] = value; }
        }

        public virtual bool AllowEdit
        {
            get
            {
                return true;
            }
        }

        public virtual bool AllowNew
        {
            get { return true; }
        }

        public virtual bool AllowRemove
        {
            get { return true; }
        }

        public virtual bool IsSorted
        {
            get { return this.isSorted; }
        }

        public virtual ListSortDirection SortDirection
        {
            get { return this.sortDirection; }
        }

        public virtual PropertyDescriptor SortProperty
        {
            get
            {
                return this.sortProperty;
            }
        }

        public virtual bool SupportsChangeNotification
        {
            get { return true; }
        }

        public virtual bool SupportsSearching
        {
            get { return true; }
        }

        public virtual bool SupportsSorting
        {
            get { return true; }
        }

        public virtual bool SelectionMode
        {
            get { return this.selectionMode; }
            set { this.selectionMode = value; }
        }

        #endregion

        #region Methods

        public View(string key) : base(null, false, key) { }

        public View() : this((string)null) { }

        public View(ITable table, string key)
            : base(table == null ? null : table.Session, false, key)
        {
            this.table = table;
        }

        public View(ITable table) : this((ITable)table, null) { }

        public View(Session session, string key)
            : base(session, false, key)
        {
        }

        public View(Session session) : this(session, null) { }

        public View(IEnumerable list)
            : base(list)
        {
        }

        public virtual void SetTable(ITable table)
        {
            this.table = table;
            this[typeof(Session)] = table.Session;
        }

        protected virtual IList GetRows()
        {
            var r = new ArrayList();
            if (this.Table != null)
            {
                foreach (var row in this.Table)
                {
                    r.Add(row);
                }
            }
            else if (typeof(IEnumerable).IsAssignableFrom(this.GetDataType()))
            {
                foreach (var row in (IEnumerable)this.GetData())
                {
                    r.Add(row);
                }
            }
            if (this.IsSorted && this.SortProperty != null)
            {
                r.Sort(GetSortComparer());
            }
            return r;
        }

        public override object GetData(Type type)
        {
            if (type == this.GetDataType())
                return this.Current;
            return base.GetData(type);
        }

        public IEnumerator GetEnumerator()
        {
            return this.Rows.GetEnumerator();
        }

        public override Type GetDataType()
        {
            if(this.table!=null)
                return this.Table.GetRowType();
            return base.GetDataType();
        }

        public override string GetTitle()
        {
            string title = BAL.Types.CaptionAttribute.GetCaption(this);
            if (string.IsNullOrEmpty(title) && this.Table != null)
                title = BAL.Types.CaptionAttribute.GetCaption(this.Table, this.Table.TableName);
            if (string.IsNullOrEmpty(title))
                title = this.Key;
            return title;
        }

        public override IDataContextSerializer GetSerializer()
        {
            return new ViewSerializer();
        }

        public virtual void CopyTo(Array array, int index)
        {
        }

        public override object CreateData()
        {
            if (this.Table != null)
            {
                var row = (IRow)this.Table.GetRowType().GetConstructor(new Type[0]).Invoke(new object[0]);
                this.Table.AddRow(row);
                return row;
            }
            return base.CreateData();
        }

        public override DataContext CreateContext(object data)
        {
            this.childContext = base.CreateContext(data);
            return this.childContext;
        }

        public override bool Load()
        {
            if (isLoaded)
                return true;
            isLoaded = true;
            try
            {
                string name = this.Key + ".grid.xml";
                var service = AppController.Instance.FileStorageService.GetFileStorageService(name);
                if (service != null && service.Exist(name))
                {
                    this.VisibleColumns.BeginInit();
                    var serializer = this.GetSerializer();
                    serializer.Deserialize(service.GetStreamReader(name), this);
                    this.VisibleColumns.EndInit();
                    return true;
                }
                else if (service != null)
                {
                    string defaultDefinition = this.GetDefaultXmlDefinition();
                    if (!string.IsNullOrEmpty(defaultDefinition))
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(defaultDefinition);
                        using (var writer = service.GetStreamWriter(name))
                            writer.Write(buffer, 0, buffer.Length);
                        isLoaded = false;
                        return this.Load();
                    }
                    return false;
                }
                else
                    return base.Load();
            }
            catch(Exception ex)
            {
                throw ex;
                //return false;
            }
        }
        

        public override void Save()
        {
            string name = this.Key + ".grid.xml";
            var service = AppController.Instance.FileStorageService.GetFileStorageService(name);
            if (service != null)
            {
                var serializer = this.GetSerializer();
                serializer.Serialize(service.GetStreamWriter(name), this);
            }
            else
                base.Save();
        }

        public virtual DataContext BeginEdit(object row)
        {
            if (!this.IsEditing)
            {
                base.BeginEdit();
                if (this.IsEditing)
                {
                    if (row == null)
                    {
                        row = this.CreateData();
                        this.isNewRow = true;
                    }
                    var type = DataContextAttribute.GetDataContextType(row, typeof(DataContext));
                    bool flag = this.Session != null;
                    ConstructorInfo cinfo = null;
                    if (flag)
                        cinfo = type.GetConstructor(new Type[] { typeof(ISessionable) });

                    if (cinfo == null)
                    {
                        cinfo = type.GetConstructor(new Type[0]);
                        flag = false;
                    }

                    if (cinfo != null)
                    {
                        this.childContext = (DataContext)(flag ? cinfo.Invoke(new object[] { this }) : cinfo.Invoke(new object[0]));
                        this.childContext.parent = this;
                        this.childContext.SetData(row);
                        this.childContext.BeginEdit();
                        return this.childContext;
                    }
                }
            }
            return null;
        }

        public override void BeginEdit()
        {
            if (this.childContext != null)
                this.childContext.BeginEdit();
            else
                base.BeginEdit();
        }

        public override void EndEdit()
        {
            if (this.childContext != null)
            {
                this.childContext.EndEdit();
                if (this.childContext.IsModified && !this.IsNewRow)
                {
                    int idx = this.IndexOf(this.childContext.GetData());
                    this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, idx));
                }
                else if (this.IsNewRow)
                {
                    this.Add(this.childContext.GetData());
                }
                //this.childContext.SaveChanges();
                this.childContext.Dispose();
                this.childContext = null;
                this.isNewRow = false;
            }
            else
                base.EndEdit();
        }

        public override void CancelEdit()
        {
            if (this.childContext != null)
            {
                this.childContext.CancelEdit();
                this.childContext.Dispose();
                this.childContext = null;
                this.isNewRow = false;
            }
            else
                base.CancelEdit();
        }

        public virtual void EditCurrent()
        {
            if (this.Current != null)
                this.EditRow(this.Current);
        }

        public virtual bool EditRow(object row)
        {
            return false;
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
        }

        public virtual int Add(object obj)
        {
            int idx = this.Rows.Add(obj);
            if (this.IsSorted && this.SortProperty != null)
            {
                this.Sort(GetSortComparer());
                idx = this.IndexOf(obj);
            }
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, idx));
            this.Position = idx;
            return idx;
        }

        public virtual void Clear()
        {
            this.Rows.Clear();
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        public virtual bool Contains(object obj)
        {
            return this.Rows.Contains(obj);
        }

        public virtual int IndexOf(object obj)
        {
            return this.Rows.IndexOf(obj);
        }

        public virtual void Insert(int index, object obj)
        {
            this.Rows.Insert(index, obj);
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
        }

        public virtual void Remove(object obj)
        {
            int idx = this.Rows.IndexOf(obj);
            if (idx >= 0)
            {
                if (this.Table != null && obj is IRow)
                    ((BAL.Business.Old.ITable)this.Table).RemoveRow((IRow)obj);

                this.Rows.Remove(obj);
                if (this.Session != null)
                    this.Session.Save();

                this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, idx));
            }
        }

        public virtual void RemoveAt(int index)
        {
            this.Remove(this.Rows[index]);
        }

        public override void Reset()
        {
            this.OnListChanged(this.resetArgs);
        }

        public override void Reload()
        {
            OnBeforeReload(new EventArgs());
            this.rows = null;
            this.Reset();
            OnAfterReload(new EventArgs());
        }

        protected virtual void OnBeforeReload(EventArgs e)
        {
            if (this.BeforeReload != null)
                this.BeforeReload(this, e);

        }

        protected virtual void OnAfterReload(EventArgs e)
        {
            if (this.AfterReload != null)
                this.AfterReload(this, e);
        }

        protected virtual void OnListChanged(ListChangedEventArgs e)
        {
            if (this.ListChanged != null)
            {
                this.ListChanged(this, e);
            }
        }

        public virtual void AddIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException("BAL.Business.View.AddIndex(PropertyDescriptor)");
        }

        public virtual object AddNew()
        {
            var row = this.CreateData();
            this.isNewRow = true;
            if (this.EditRow(row))
            {
                /*
                if ( row is IRow && ((IRow)row).State == RowState.Unchanged)
                    return row;
                 */
                return row;
            }
            return null;
        }

        public virtual void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            this.isSorted = false;
            this.sortProperty = property;
            this.sortDirection = direction;
            this.Sort(GetSortComparer());
            this.isSorted = true;
        }

        public virtual void Sort(IComparer comparer)
        {
            if (this.rows != null && this.rows is IList)
                ArrayList.Adapter(this.rows).Sort(GetSortComparer());
        }

        public virtual int Find(PropertyDescriptor property, Object key)
        {
            if (property != null && key != null)
            {
                string s = ((string)key).ToLower();
                if (!string.IsNullOrEmpty(s))
                {
                    int l = s.Length;
                    for (int i = 0; i < this.Rows.Count; i++)
                    {
                        var row = this.Rows[i];
                        string val = property.GetValue(row).ToString().ToLower();
                        if (!string.IsNullOrEmpty(val) && val.Length >= l && val.StartsWith(s))
                            return i;
                    }
                }
            }
            return -1;
        }

        public virtual void RemoveIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException("BAL.Business.View.RemoveIndex(PropertyDescriptor)");
        }

        public virtual void RemoveSort()
        {
            throw new NotImplementedException("BAL.Business.View.RemoveSort()");
        }

        protected virtual IComparer GetSortComparer()
        {
            if (sortComparer == null)
                sortComparer = new SortComparer();
            /*
            if (this.sortPropertyPath != null)
                ((SortComparer)sortComparer).Init(SortPropertyPath, SortDirection);
            else*/
            
            ((SortComparer)sortComparer).Init(this.SortProperty, this.SortDirection);
             
            return sortComparer;
        }

        #endregion

        #region Events

        public event ListChangedEventHandler ListChanged;
        public event EventHandler BeforeReload;
        public event EventHandler AfterReload;


        #endregion

        #region Nested Types

        private class SortComparer : IComparer
        {
            #region Fields

            private PropertyDescriptor property;
            //private BAL.Types.PropertyPath propertyPath;
            private ListSortDirection direction;
            private IComparer defaultComparer;

            #endregion

            #region Methods
            /*
            private SortComparer(View view, PropertyDescriptor property, ListSortDirection direction)
            {
                this.view = view;
                if (property != null)
                {
                    this.property = property;
                    this.direction = direction;

                    var type = typeof(Comparer<>).MakeGenericType(this.property.PropertyType);
                    var pinfo = type.GetProperty("Default", BindingFlags.Static | BindingFlags.Public);
                    this.defaultComparer = (IComparer)pinfo.GetValue(null, null);
                }
            }
             */

            public void Init(PropertyDescriptor property, ListSortDirection direction)
            {
                if (property != null)
                {
                    this.property = property;
                    this.direction = direction;

                    var type = typeof(Comparer<>).MakeGenericType(this.property.PropertyType);
                    var pinfo = type.GetProperty("Default", BindingFlags.Static | BindingFlags.Public);
                    this.defaultComparer = (IComparer)pinfo.GetValue(null, null);
                }
            }

            /*
            public void Init(BAL.Types.PropertyPath propertyPath, ListSortDirection direction)
            {
                if (propertyPath != null)
                {
                    this.propertyPath = propertyPath;
                    this.direction = direction;

                    var type = typeof(Comparer<>).MakeGenericType(this.propertyPath.Last.PropertyType);
                    var pinfo = type.GetProperty("Default", BindingFlags.Static | BindingFlags.Public);
                    this.defaultComparer = (IComparer)pinfo.GetValue(null, null);
                }
            }
             */

            public int Compare(object x, object y)
            {
                object val1 = null;
                object val2 = null;

                /*if (this.propertyPath != null)
                {
                    val1 = this.propertyPath.GetValue(x);
                    val2 = this.propertyPath.GetValue(y);
                }
                else */if (this.property != null)
                {
                    val1 = this.property.GetValue(x);
                    val2 = this.property.GetValue(y);
                }

                if (val1 == null && val2 != null)
                    return this.direction == ListSortDirection.Ascending ? -1 : 1;
                if (val1 != null && val2 == null)
                    return this.direction == ListSortDirection.Ascending ? 1 : -1;
                if (val1 == null && val2 == null)
                    return 0;

                try
                {

                    if (val1 is IComparable)
                        return this.direction == ListSortDirection.Ascending ? ((IComparable)val1).CompareTo(val2) : ((IComparable)val2).CompareTo(val1);
                    return this.direction == ListSortDirection.Ascending ? this.defaultComparer.Compare(val1, val2) : this.defaultComparer.Compare(val2, val1);
                }
                catch
                {
                    return val1.ToString().CompareTo(val2.ToString());
                }

            }

            #endregion
        }

        #endregion
    }
}
