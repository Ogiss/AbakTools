using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Business
{
    public class DataContext : ISessionable, IDisposable, ICustomTypeDescriptor, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region Fields

        private bool disposed;
        private bool singleRow;
        private Type dataType;
        private Dictionary<Type, object> data;
        private ColumnCollection columns;
        private ColumnCollection visibleColumns;
        private Dictionary<string, object> storedProperties;
        internal DataContext parent;
        private bool isModified;
        private bool isEditing;
        private bool readOnly;
        private string key;

        #endregion

        #region Properties

        public object this[Type dataType]
        {
            get
            {
                return this.GetData(dataType);
            }
            set
            {
                this.AddData(dataType, value);
            }
        }

        public Session Session
        {
            get
            {
                return (Session)this[typeof(Session)];
            }
        }

        public App.ILogin Login
        {
            get
            {
                return (App.ILogin)this[typeof(App.ILogin)];
            }
        }

        public virtual string Key
        {
            get
            {
                if(key == null)
                    key = DataContextKeyAttribute.GetKey(this.GetDataType());
                if (string.IsNullOrEmpty(key))
                    key = this.GetType().Name;
                return key;
            }
        }

        public ColumnCollection Columns
        {
            get
            {
                if (this.columns == null)
                    this.InitColumns();
                return this.columns;
            }
        }

        public ColumnCollection VisibleColumns
        {
            get
            {
                if (this.visibleColumns == null)
                    this.InitVisibleColumns();
                return this.visibleColumns;
            }
        }

        public DataContext Parent
        {
            get { return this.parent; }
        }

        public virtual bool IsModified
        {
            get { return this.isModified; }
        }

        public virtual bool IsEditing
        {
            get { return this.isEditing; }
        }

        public virtual bool ReadOnly
        {
            get { return this.readOnly; }
        }

        public virtual int Position
        {
            get { return 0; }
        }

        public virtual object Current
        {
            get { return this.GetData(); }
        }

        #endregion

        #region Methods

        public DataContext(ISessionable session, bool singleRow, string key)
        {
            this.key = key;
            this.data = new Dictionary<Type, object>();
            this.storedProperties = new Dictionary<string, object>();
            this.singleRow = singleRow;
            this.AddData(typeof(App.ILogin), AppController.Instance.CurrentLogin);
            this.AddData(typeof(App.IDatabase), AppController.Instance.CurrentLogin.Database);
            if (session != null)
                this.AddData(typeof(Session), session.Session);

        }

        public DataContext(ISessionable session, bool singleRow) : this(session, singleRow, null) { }

        public DataContext(ISessionable session) : this(session, true) { }

        public DataContext() : this(null, true) { }

        public virtual void AddData(Type dataType, object data)
        {
            this.data[dataType] = data;
        }

        public DataContext(IEnumerable list)
            : this(null, false)
        {
            this.SetData(list);
        }

        

        public void AddData(object data)
        {
            this.AddData(CoreTools.GetObjectType(data), data);
        }

        public virtual void SetData(Type dataType, object data)
        {
            /*
            if (data != null)
            {
                this.dataType = dataType;
                if (data is IEnumerable)
                    this.singleRow = false;
                this.data[dataType] = data;
                if (data is INotifyPropertyChanging)
                    ((INotifyPropertyChanging)data).PropertyChanging += (sender, e) => { this.OnPropertyChanging(e); };
                if (data is INotifyPropertyChanged)
                    ((INotifyPropertyChanged)data).PropertyChanged += (sender, e) => { this.OnPropertyChanged(e); };
            }
             */
            if (dataType != null)
            {
                this.dataType = dataType;
                this.data[dataType] = data;
                if (typeof(IEnumerable).IsAssignableFrom(dataType))
                    this.singleRow = false;
                else
                    this.singleRow = true;
                if (data != null)
                {
                    if (data is INotifyPropertyChanging)
                        ((INotifyPropertyChanging)data).PropertyChanging += (sender, e) => { this.OnPropertyChanging(e); };
                    if (data is INotifyPropertyChanged)
                        ((INotifyPropertyChanged)data).PropertyChanged += (sender, e) => { this.OnPropertyChanged(e); };
                }
            }
        }

        public void SetData(object data)
        {
            this.SetData(CoreTools.GetObjectType(data), data);
        }

        public virtual void RemoveData(Type dataType)
        {
            if (dataType != null)
            {
                if (this.dataType == dataType)
                {
                    var data = this[dataType];
                    this.dataType = null;
                    if (data is INotifyPropertyChanging)
                        ((INotifyPropertyChanging)data).PropertyChanging -= (sender, e) => { this.OnPropertyChanging(e); };
                    if (data is INotifyPropertyChanged)
                        ((INotifyPropertyChanged)data).PropertyChanged -= (sender, e) => { this.OnPropertyChanged(e); };
                }
                this.data.Remove(dataType);
            }
        }

        public virtual object GetData(Type type)
        {
            if (type != null)
            {
                if (this.data.ContainsKey(type))
                    return this.data[type];
            }
            return null;
        }

        public object GetData()
        {
            return this.GetData(this.GetDataType());
        }

        public T GetData<T>()
        {
            return (T)this.GetData(typeof(T));
        }

        public virtual Dictionary<string, object> GetStoredProperties()
        {
            return this.storedProperties;
        }

        public virtual object GetStoredProperty(string name)
        {
            var properties = this.GetStoredProperties();
            if (properties.ContainsKey(name))
                return properties[name];
            return null;
        }

        public virtual void SetStoredProperties(Dictionary<string, object> properties)
        {
            this.storedProperties = properties;
        }

        public virtual void SetStoredProperty(string name, object value)
        {
            this.storedProperties[name] = value;
        }

        public virtual object GetValue(string propertyName)
        {
            if (Current != null)
            {
                var pinfo = Current.GetType().GetProperty(propertyName);
                if (pinfo != null)
                    return pinfo.GetValue(Current, null);
            }
            return null;
        }

        public virtual void SetValue(string propertyName, object value)
        {
            if (Current != null)
            {
                var pinfo = Current.GetType().GetProperty(propertyName);
                if (pinfo != null)
                    pinfo.SetValue(Current, value, null);
            }
        }

        protected virtual void InitColumns()
        {
            var type = this.GetDataType();
            this.columns = new ColumnCollection();
            this.columns.BeginInit();
            foreach (var pinfo in type.GetProperties() )
            {
                if (pinfo.GetCustomAttributes(typeof(HiddenAttribute), true).Length > 0)
                    continue;
                Column column = new Column();
                column.HeaderText = CaptionAttribute.GetCaption(pinfo, pinfo.Name);
                column.TextAlign = TextAlign.Left;
                column.Width = 100;
                column.Visible = true;
                column.PropertyPath = new PropertyPath(pinfo);
                this.columns.Add(column);
            }
            this.columns.EndInit();
        }

        protected virtual void InitVisibleColumns()
        {
            this.visibleColumns = new ColumnCollection();
            this.visibleColumns.BeginInit();
            if (!this.Load())
            {
                foreach (Column col in this.Columns.GetVisible())
                {
                    this.visibleColumns.Add(col.Clone());
                }
            }
            this.visibleColumns.ColumnChanged += new EventHandler<ColumnChangedEventArgs>(columns_ColumnChanged);
            this.visibleColumns.ColumnAdded+=new EventHandler<ColumnEventArgs>(visibleColumns_ColumnAdded);
            this.visibleColumns.EndInit();
        }

        public virtual IEnumerable<Type> GetRowActionsTypes()
        {
            return RowActionAttribute.GetActionsTypes(this);
        }

        public virtual Type GetDataType()
        {
            return this.dataType;
        }

        public virtual string GetTitle()
        {
            var type = this.GetDataType();
            var attrs = type.GetCustomAttributes(typeof(CaptionAttribute), true);
            if (attrs.Length > 0)
                return ((CaptionAttribute)attrs[0]).Caption;
            return type.Name;
        }

        public virtual bool Load()
        {
            return false;
        }

        public virtual void Save()
        {
        }

        public virtual string GetDefaultXmlDefinition()
        {
            return null;
        }

        public virtual IDataContextSerializer GetSerializer()
        {
            return null;
        }

        public virtual IEnumerable<DataContextParam> GetParams()
        {
            return null;
        }

        public virtual DataContext CreateContext(object data)
        {
            //var type = DataContextAttribute.GetDataContextType(data, typeof(DataContext));
            var type = DataContextAttribute.GetDataContextType(this.GetDataType(), typeof(DataContext));
            bool flag = this.Session != null;
            ConstructorInfo cinfo = null;
            DataContext context = null;
            if (flag)
                cinfo = type.GetConstructor(new Type[] { typeof(ISessionable) });

            if (cinfo == null)
            {
                cinfo = type.GetConstructor(new Type[0]);
                flag = false;
            }

            if (cinfo != null)
            {
                context = (DataContext)(flag ? cinfo.Invoke(new object[] { this }) : cinfo.Invoke(new object[0]));
                context.parent = this;
                Type type1 = this.GetDataType();
                Type type2 = CoreTools.GetObjectType(data);
                context.SetData(type1, data);
                if (type1 != type2)
                    context.AddData(type2, data);
            }
            return context;
        }

        public virtual void Reset()
        {
            if (this.Parent != null)
                this.Parent.Reset();
        }

        public virtual void Reload()
        {
            if (this.Parent != null)
                this.Parent.Reload();
        }

        public virtual object CreateData()
        {
            var t = this.GetDataType();
            return t.GetConstructor(new Type[0]).Invoke(new object[0]);
        }

        private void columns_ColumnChanged(object sender, ColumnChangedEventArgs e)
        {
            this.OnColumnChanged(e);
        }

        private void visibleColumns_ColumnAdded(object sender, ColumnEventArgs e)
        {
            this.OnColumnAdded(e);
        }

        protected virtual void OnColumnChanged(ColumnChangedEventArgs e)
        {
            if (this.ColumnChanged != null)
            {
                this.ColumnChanged(this, new ColumnChangedEventArgs(e.Column, e.PropertyName));
            }
            if (this.Parent != null)
                this.Parent.OnColumnChanged(e);
        }

        protected virtual void OnColumnAdded(ColumnEventArgs e)
        {
            if (this.ColumnAdded != null)
                this.ColumnAdded(this, e);
            if (this.Parent != null)
                this.Parent.OnColumnAdded(e);
        }

        protected virtual void OnInitParam(DataContextParamEventArgs e)
        {
            if (this.Session != null && e.Param.PropertyPath != null)
            {
                var paramDataType = e.Param.PropertyPath.Last.PropertyType;
                if (typeof(Row).IsAssignableFrom(paramDataType) && e.Control is IDataContexable)
                {
                    var table = this.Session.Tables[paramDataType];
                    if (table != null)
                    {
                        ((IDataContexable)e.Control).DataContext = table.CreateView();
                    }
                }
            }

            if (this.InitParam != null)
                this.InitParam(this, e);
            if (this.Parent != null)
                this.Parent.OnInitParam(e);

        }

        protected virtual void OnBeforeEdit(CancelEventArgs e)
        {

            if (this.BeforeEdit != null)
                this.BeforeEdit(this, e);
        }

        protected virtual void OnAfterEdit(EventArgs e)
        {
            if (this.AfterEdit != null)
                this.AfterEdit(this, e);
        }

        private void dataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.isModified = true;
        }

        protected virtual void OnParamValueChanged(DataContextParamEventArgs e)
        {
            if (this.ParamValueChanged != null)
                this.ParamValueChanged(this, e);
            if (this.Parent != null)
                this.Parent.OnParamValueChanged(e);
        }

        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            if (this.PropertyChanging != null)
                this.PropertyChanging(this, e);
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, e);
            this.isModified = true;
        }

        protected virtual void OnPositionChanged(EventArgs e)
        {
            if (this.PositionChanged != null)
                this.PositionChanged(this, e);
        }

        protected virtual void OnDisposed(EventArgs e)
        {
            if (this.Disposed != null)
                this.Disposed(this, e);
        }

        public void FireInitParam(DataContextParam dataContectParam, System.Windows.Forms.Control control)
        {
            this.OnInitParam(new DataContextParamEventArgs(dataContectParam, control));
        }

        public void FireParamValueChanged(DataContextParam param, System.Windows.Forms.Control control)
        {
            this.OnParamValueChanged(new DataContextParamEventArgs(param, control));
        }

        public void FireBeforeEdit(out bool cancel)
        {
            var e = new CancelEventArgs(false);
            this.OnBeforeEdit(e);
            cancel = e.Cancel;
        }

        public void FireAfterEdit()
        {
            this.OnAfterEdit(new EventArgs());
        }

        public virtual void BeginEdit()
        {
            if (!this.isEditing)
            {
                var e = new CancelEventArgs(false);
                this.OnBeforeEdit(e);
                if (e.Cancel) return;
                if (this.Current != null /*&& this.Current is IEditable*/ && this.Current is Old.IRow)
                {
                    try
                    {
                        //((IEditable)this.Current).BeginEdit();
                        ((Old.IRow)this.Current).BeginEdit();
                    }
                    catch (ReadOnlyException)
                    {
                        this.readOnly = true;
                    }
                    catch
                    {
                        throw;
                    }
                }
                this.isEditing = true;
            }
        }

        public virtual void EndEdit()
        {
            if (this.IsEditing)
            {
                if (this.Current != null && /*this.Current is IEditable*/ this.Current is Old.IRow)
                    //((IEditable)this.Current).EndEdit();
                    ((Old.IRow)this.Current).EndEdit();
                this.isEditing = false;
                this.OnAfterEdit(new EventArgs());
            }
        }

        public virtual void CancelEdit()
        {
            if (this.IsEditing)
            {
                if (this.Current != null && /*this.Current is IEditable*/ this.Current is Old.IRow)
                    //((IEditable)this.Current).CancelEdit();
                    ((Old.IRow)this.Current).CancelEdit();
                this.isEditing = false;
                this.OnAfterEdit(new EventArgs());
            }
        }

        public virtual void SaveChanges()
        {
            if (this.Session != null)
            {
               this.Session.Save();
            }
        }

        /*
        public virtual void UndoChanges()
        {
            if (this.Current is IRow)
                ((IRow)this.Current).UndoChanges();

            if (this.Session != null)
                this.Session.Save();
        }
         */

        protected virtual void Dispose(bool userCall)
        {
            if (!this.disposed)
            {
                /*
                Session session = this.GetData<Session>();
                if (session != null && this.parent == null)
                {
                    this.RemoveData(typeof(Session));
                    session.Dispose();
                }
                 */
                OnDisposed(new EventArgs());
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DataContext()
        {
            this.Dispose(false);
        }
        
        #endregion

        #region Events

        public event EventHandler<ColumnChangedEventArgs> ColumnChanged;
        public event EventHandler<ColumnEventArgs> ColumnAdded;
        public event EventHandler<DataContextParamEventArgs> InitParam;
        public event EventHandler<DataContextParamEventArgs> ParamValueChanged;
        public event CancelEventHandler BeforeEdit;
        public event EventHandler AfterEdit;
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler PositionChanged;
        public event EventHandler Disposed;

        #endregion

        #region ICustomTypedescription Implementation

        public AttributeCollection GetAttributes()
        {
            throw new NotImplementedException();
        }

        public string GetClassName()
        {
            throw new NotImplementedException();
        }

        public string GetComponentName()
        {
            throw new NotImplementedException();
        }

        public TypeConverter GetConverter()
        {
            throw new NotImplementedException();
        }

        public EventDescriptor GetDefaultEvent()
        {
            throw new NotImplementedException();
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            throw new NotImplementedException();
        }

        public object GetEditor(Type editorBaseType)
        {
            throw new NotImplementedException();
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            throw new NotImplementedException();
        }

        public EventDescriptorCollection GetEvents()
        {
            throw new NotImplementedException();
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(this.GetDataType(), attributes);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            throw new NotImplementedException();
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this.Current;
        }

        #endregion

    }
}
