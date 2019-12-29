using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Enova.Business.Old.Core
{
    public class TableBase<TEntity> : /*Component,*/ IEnumerator, IEnumerator<TEntity>, IEnumerable, IEnumerable<TEntity>,
        ICollection, ICollection<TEntity>, IList, IList<TEntity>, IBindingList, IBindingListView, ICancelAddNew, IElementType, IReadOnly,
        IFeatures, IReload, ICreateNewRecord, ITable
        where TEntity : class, /*EntityObject,*/ new()
    {
        private int addNewIndex = -1;
        private IContainer components = null;
        private IComparer sortComparer;

        private ObjectContext dataContext = null;
        public ObjectContext DataContext
        {
            get 
            {
                if (this.dataContext != null)
                    return dataContext;
                if (this.module != null)
                    return this.module.DataContext;
                return null;
            }
            set { this.dataContext = value; }
        }

        private MergeOption mergeOption = MergeOption.PreserveChanges;
        public MergeOption MergeOption
        {
            get { return this.mergeOption; }
            set { this.mergeOption = value; }
        }

        private string tableName = null;
        public virtual string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(this.tableName))
                    this.tableName = this.GetType().Name;
                return tableName;
            }
            set { tableName = value; }
        }

        private Module module;
        public Module Module
        {
            get { return this.module; }
            internal set { this.module = value; }
        }

        private ObjectQuery<TEntity> baseQuery;
        public virtual ObjectQuery<TEntity> BaseQuery
        {
            get
            {
                if (this.baseQuery == null)
                    this.baseQuery = CreateQuery();
                return this.baseQuery; 
            }
            set
            {
                this.baseQuery = value;
                if (baseQuery != null)
                    this.dataContext = value.Context;
            }
        }

        IQueryable ITable.BaseQuery
        {
            get { return this.BaseQuery; }
        }

        private List<TEntity> list = null;
        internal List<TEntity> List
        {
            get { return list; }
            set { list = value; }
        }

        private bool sortLocal = false;
        public bool SortLocal
        {
            get { return sortLocal; }
            set { sortLocal = value; }
        }

        private bool filterLocal = false;
        public bool FilterLocal
        {
            get { return filterLocal; }
            set { filterLocal = value; }
        }

        private bool refreshContext = false;
        public bool RefreshContext
        {
            get { return refreshContext; }
            set { refreshContext = value; }
        }

        public bool IsEmpty
        {
            get
            {
                if (this.BaseQuery != null)
                    return this.BaseQuery.Count() > 0;
                return this.rows.Count() > 0;
            }
        }

        public IKey Key { get; internal set; }
        private object[] root;


        private List<TEntity> rows = null;

        public List<TEntity> Rows
        {
            get
            {
                if (rows == null)
                    LoadData();
                return rows;
            }
            internal set
            {
                this.rows = value;
            }
        }

        public TableBase(ObjectQuery<TEntity> query, IContainer container)
        {
            this.BaseQuery = query;

            //this.dataContext = query.Context;
            /*
            if (container != null)
                container.Add(this);
            InitializeComponent();
             */
        }

        public TableBase(ObjectQuery<TEntity> query) : this(query, null) { }

        public TableBase() : this((ObjectQuery<TEntity>)null) { }

        public TableBase(ObjectContext dataContext, string tableName)
            : this((ObjectQuery<TEntity>)null)
        {
            this.dataContext = dataContext;
            this.tableName = tableName;
            if (dataContext != null && !string.IsNullOrEmpty(tableName))
            {
                PropertyInfo property = dataContext.GetType().GetProperty(tableName);
                if (property != null)
                {
                    this.BaseQuery = (ObjectQuery<TEntity>)property.GetValue(dataContext, null);
                }
            }
        }

        public TableBase(ICollection<TEntity> collection)
            : this((ObjectQuery<TEntity>)null)
        {
            this.rows = collection.ToList();
            filterLocal = true;
            sortLocal = true;
        }

        public TableBase<TEntity> ByKey(IKey key,params object[] data)
        {
            this.Key = key;
            this.root = data;
            return this;
        }

        protected virtual ObjectQuery<TEntity> CreateQuery()
        {
            return null;
        }

        public virtual void Refresh()
        {
            if (rows != null)
            {
                rows = null;
                this.LoadData();
                OnListChanged(resetEvent);
            }
        }

        public void ResetRows()
        {
            rows = null;
            list = null;
        }

        public virtual void Adding(Module module)
        {
            this.module = module;
        }

        public virtual void Reload()
        {
            list = null;
            Refresh();
        }

        public TableBase(IEnumerable list)
        {
            
            if (list != null)
            {
                rows = new List<TEntity>();

                foreach (var i in list)
                {
                    if (i is TEntity)
                    {
                        rows.Add((TEntity)i);
                    }
                }
            }
        }

        public TableBase(IEnumerable<TEntity> list) : this((IEnumerable)list) { }

        public Type GetRowType(int selector)
        {
            throw new NotImplementedException("TableBase<TEntity>.GetRowType(int selector)");
            /*
            if (rowProviders == null)
            {
                loadProviders();
            }
            ConstructorInfo info = (ConstructorInfo)rowProviders[new ProviderKey(getBaseType(this.GetRowType()), selector)];
            if (info == null)
            {
                throw new UnrecognizedRowException(string.Format("Selektor {0} w tabeli {1} nieznaleziony. Możliwe, że nie zostały poprawnie załadowane wszystkie pliki DLL.", selector, this.Name));
            }
            return info.DeclaringType;
             */
        }

        protected virtual void LoadData()
        {
            if (BaseQuery != null)
            {

                ObjectQuery<TEntity> query;

                if (sortLocal || filterLocal)
                {
                    if (list == null)
                    {
                        if (filterLocal)
                        {
                            query = BaseQuery;
                            //list = PostLoadProcess(BaseQuery.ToList());
                        }
                        else
                        {
                            if (filterInfo != null && !string.IsNullOrEmpty(filterInfo.Filter))
                            {
                                query = BaseQuery.Where(filterInfo.Filter, new ObjectParameter[] { });
                                //list = PostLoadProcess(BaseQuery.Where(filterInfo.Filter, new ObjectParameter[] { }).ToList());
                            }
                            else
                            {
                                query = BaseQuery;
                                //list = PostLoadProcess(BaseQuery.ToList());
                            }
                        }
                        query.MergeOption = this.mergeOption;
                        if (refreshContext && DataContext != null)
                            DataContext.Refresh(RefreshMode.StoreWins, query);
                        list = PostLoadProcess(query.ToList());
                    }

                    if (FilterLocal && filterInfo != null && !string.IsNullOrEmpty(filterInfo.Filter))
                    {
                        rows = PostLoadProcess(list.AsQueryable().Where((Expression<Func<TEntity, bool>>)filterInfo.Expression).ToList());
                    }
                    else
                    {
                        rows = PostLoadProcess(list);
                    }

                    if (isSorted)
                    {

                        if (sortDirection == ListSortDirection.Ascending)
                        {
                            rows = PostLoadProcess(rows.OrderBy((Func<TEntity, object>)keySelector).ToList());
                        }
                        else
                        {
                            rows = PostLoadProcess(rows.OrderByDescending((Func<TEntity, object>)keySelector).ToList());
                        }
                    }

                }
                else
                {

                    if (filterInfo == null || string.IsNullOrEmpty(filterInfo.Filter))
                    {
                        query = BaseQuery;
                    }
                    else
                    {
                        query = BaseQuery.Where(filterInfo.Filter, new ObjectParameter[] { });

                    }

                    query.MergeOption = this.mergeOption;
                    /*
                    if (refreshContext && DataContext != null)
                        DataContext.Refresh(RefreshMode.StoreWins, query);
                     */

                    if (isSorted)
                    {
                        /*
                        if (sortDirection == ListSortDirection.Ascending)
                        {
                            rows = PostLoadProcess(query.OrderBy((Func<TEntity, object>)keySelector).ToList());
                        }
                        else
                        {
                            rows = PostLoadProcess(query.OrderByDescending((Func<TEntity, object>)keySelector).ToList());
                        }
                         */
                        rows = PostLoadProcess(GetSortedRows(query).ToList());
                    }
                    else
                        rows = this.PostLoadProcess(query.ToList());
                }
                /*
                if (refreshContext && dataContext != null)
                    dataContext.Refresh(RefreshMode.StoreWins, rows);
                 */

            }
            else
            {
                this.rows = GetRows();
            }
            if (this.sortComparer != null)
            {
                this.Sort(this.sortComparer);
                this.sortComparer = null;
            }
            this.OnDataLoaded(new EventArgs());
        }

        /*
        protected virtual ObjectQuery<TEntity> GetSortedRows(ObjectQuery<TEntity> query)
        {
            if (((IBindingList)this).SortDirection == ListSortDirection.Ascending)
                return (ObjectQuery<TEntity>)query.OrderBy((Func<TEntity, object>)keySelector);
            return (ObjectQuery<TEntity>)query.OrderByDescending((Func<TEntity, object>)keySelector);
        }
         */

        protected virtual IEnumerable<TEntity> GetSortedRows(ObjectQuery<TEntity> query)
        {
            if (((IBindingList)this).SortDirection == ListSortDirection.Ascending)
                return query.OrderBy((Func<TEntity, object>)keySelector);
            return query.OrderByDescending((Func<TEntity, object>)keySelector);
        }


        public event EventHandler DataLoaded;
        protected virtual void OnDataLoaded(EventArgs e)
        {
            if (this.DataLoaded != null)
                this.DataLoaded(this, e);
        }

        protected virtual List<TEntity> GetRows()
        {
            return null;
        }

        public virtual void Sort(IComparer comparer)
        {
            if (this.rows != null)
            {
                ArrayList.Adapter(this.rows).Sort(comparer);
            }
            else
                this.sortComparer = comparer;
        }

        protected virtual List<TEntity> PostLoadProcess(List<TEntity> list)
        {
            return list;
        }

        private object keySelector(TEntity entity)
        {
            if (entity == null)
                return null;
            return (object)typeof(TEntity).GetProperty(sortProperty.DisplayName).GetValue(entity, null);
        }

        private void InitializeComponent()
        {
            components = new Container();
        }

        public List<TEntity> ToList()
        {
            if (rows == null)
                LoadData();

            return rows;
        }

        public virtual object CreateNewRecord()
        {
            var cinfo = typeof(TEntity).GetConstructor(new Type[] { });
            if (cinfo != null)
                return cinfo.Invoke(new object[] { });
            throw new Exception("Typ " + typeof(TEntity).Name + " nie posiada bezparametrowego konstructora");
        }

        protected virtual bool RemoveValid(TEntity entity)
        {
            return true;
        }

        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
         */

        public virtual void Dispose() { }

        public virtual void SaveChanges()
        {
            if (dataContext != null)
                dataContext.SaveChanges();
        }

        public virtual Type GetElementType()
        {
            return typeof(TEntity);
        }

        #region IFeatures Implementation

        public virtual bool SupportFeatures
        {
            get { return false; }
        }

        public virtual string FeaturesTableName
        {
            get { return null; }
        }

        public virtual ObjectQuery<Enova.Business.Old.DB.FeatureDef> FeatureDefs
        {
            get
            {
                if (DataContext != null && SupportFeatures && !string.IsNullOrEmpty(FeaturesTableName))
                {
                    return (ObjectQuery<Enova.Business.Old.DB.FeatureDef>)((Enova.Business.Old.DB.EnovaContext)DataContext).FeatureDefs
                        .Where(f => f.Name == FeaturesTableName);
                }
                return null;
            }
        }

        public virtual void ApplyFeatureFilter(Enova.Business.Old.DB.FeatureDef featureDef, string value)
        {
            Refresh();
        }

        public virtual void RemoveFeatureFilter()
        {
            Refresh();
        }

        #endregion


        #region IEnumerator Implementation

        int position = -1;

        public bool MoveNext()
        {
            position++;
            return (position < Rows.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        public TEntity Current
        {
            get
            {
                try
                {
                    return Rows[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion

        #region IEnumerable Implementation

        public TableBase<TEntity> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return (IEnumerator<TEntity>)GetEnumerator();
        }

        #endregion

        #region ICollection Implementation

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        public int Count
        {
            get
            {
                return Rows == null ? 0 : Rows.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo((TEntity[])array, index);
        }

        #endregion

        #region ICollection<TEntity> Implementation


        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(TEntity entity)
        {
            /*
            if (RemoveValid(entity) == true)
            {
                int index = IndexOf(entity);
                bool ret = Rows.Remove(entity);

                if (dataContext != null)
                {
                    if (entity.EntityState != EntityState.Detached && entity.EntityState != EntityState.Deleted)
                    {
                        if (entity is IEditableRecord)
                        {
                            ((IEditableRecord)entity).RemoveRecord();
                        }
                        else
                        {
                            dataContext.DeleteObject(entity);
                        }
                    }

                    SaveChanges();
                }

                OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
                return ret;
            }
             */
            return false;
        }

        public void CopyTo(TEntity[] array, int index)
        {
            Rows.CopyTo(array, index);
        }

        public bool Contains(TEntity entity)
        {
            return Rows.Contains(entity);
        }

        public void Clear()
        {
            Rows.Clear();
        }

        public virtual void Add(TEntity entity)
        {
            /*
            if (dataContext != null && tableName != null)
            {
                if (entity.EntityState == EntityState.Detached)
                    dataContext.AddObject(tableName, entity);
                if (entity is IValidator)
                {
                    var v = entity as IValidator;
                    v.Valid(RecordValidateType.SaveValidate);
                    if (v.IsValid == false)
                    {
                        if (!string.IsNullOrEmpty(v.ErrorMessage))
                            MessageBox.Show(v.ErrorMessage);
                        return;
                    }
                }
                if (entity.EntityState != EntityState.Unchanged)
                    SaveChanges();
                if (entity is IEditableRecord)
                {
                    if (!((IEditableRecord)entity).SaveRecord(dataContext))
                        SaveChanges();
                }
            }
            Rows.Add(entity);
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, Count - 1));
             */
        }

        #endregion

        #region IList Implementation

        public bool IsFixedSize
        {
            get { return true; }
        }

        public TEntity this[int index]
        {
            get
            {
                return Rows[index];
            }

            set
            {
                Rows[index] = value;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return (Object)this[index];
            }

            set
            {
                this[index] = (TEntity)value;
            }
        }

        public void RemoveAt(int index)
        {
            Remove(Rows[index]);
        }

        public void Remove(Object item)
        {
            Remove((TEntity)item);
        }

        public void Insert(int index, TEntity entity)
        {
            Rows.Insert(index, entity);
        }

        public void Insert(int index, Object item)
        {
            Insert(index, (TEntity)item);
        }

        public int IndexOf(TEntity entity)
        {
            return Rows.IndexOf(entity);
        }

        public int IndexOf(Object item)
        {
            return IndexOf((TEntity)item);
        }

        bool IList.Contains(Object item)
        {
            return this.Contains((TEntity)item);
        }


        public int Add(Object item)
        {
            this.Add((TEntity)item);
            return Count - 1;
        }

        #endregion

        #region IBindingList Implementation

        private ListChangedEventArgs resetEvent = new ListChangedEventArgs(ListChangedType.Reset, -1);
        private ListChangedEventHandler onListChanged;

        protected virtual void OnListChanged(ListChangedEventArgs e)
        {
            if (onListChanged != null)
            {
                onListChanged(this, e);
            }
        }

        public event ListChangedEventHandler ListChanged
        {
            add
            {
                onListChanged += value;
            }
            remove
            {
                onListChanged -= value;
            }
        }

        bool IBindingList.AllowEdit
        {
            get { return true; }
        }

        bool IBindingList.AllowNew
        {
            get { return true; }
        }

        bool IBindingList.AllowRemove
        {
            get { return true; }
        }

        bool IBindingList.SupportsChangeNotification
        {
            get { return true; }
        }

        bool IBindingList.SupportsSearching
        {
            get { return true; }
        }

        bool IBindingList.SupportsSorting
        {
            get { return true; }
        }

        public virtual TEntity AddNew()
        {
            /*
            TEntity entity = new TEntity();
            if (entity is IEditableRecord)
                ((IEditableRecord)entity).InitRecord();

            Type type = typeof(TEntity);

            var attributes = type.GetCustomAttributes(typeof(DataEditFormAttribute), false);
            if (attributes.Count() > 0)
            {
                Type formType = Type.GetType(((DataEditFormAttribute)attributes[0]).TypeName);
                var form = Activator.CreateInstance(formType);
                if (form != null)
                {
                    if (form is IDataSourceForm<TEntity>)
                        ((IDataSourceForm<TEntity>)form).DataSource = entity;
                    else if (form is IDataSourceForm)
                        ((IDataSourceForm)form).DataSource = entity;
                    DialogResult result = ((Form)form).ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        addNewIndex = Count;
                        Add(entity);
                        return entity;
                    }
                }
            }
            addNewIndex = -1;
            if (entity is IEditableRecord)
                ((IEditableRecord)entity).RemoveRecord();
             */
            //return null;
            throw new NotSupportedException();
        }

        object IBindingList.AddNew()
        {
            return this.AddNew();
        }

        private bool isSorted = false;
        bool IBindingList.IsSorted
        {
            get { return isSorted; }
        }

        private ListSortDirection sortDirection = ListSortDirection.Ascending;
        ListSortDirection IBindingList.SortDirection
        {
            get { return sortDirection; }
        }

        private PropertyDescriptor sortProperty = null;
        PropertyDescriptor IBindingList.SortProperty
        {
            get { return sortProperty; }
        }

        void IBindingList.AddIndex(PropertyDescriptor property)
        {
            throw new NotSupportedException();
        }

        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            sortProperty = property;
            sortDirection = direction;
            isSorted = true;
            Refresh();
        }

        public virtual int Find(PropertyDescriptor property, object key)
        {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveIndex(PropertyDescriptor property)
        {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveSort()
        {
            sortProperty = null;
            isSorted = false;
            Refresh();
        }

        #endregion

        #region IBindingListView Implementation

        bool IBindingListView.SupportsFiltering
        {
            get { return true; }
        }

        bool IBindingListView.SupportsAdvancedSorting
        {
            get { return true; }
        }

        internal FilterInfo  filterInfo = null;
        public string Filter
        {
            get { return filterInfo!=null ? filterInfo.Filter : null; }
            set
            {
                if (value == null)
                {
                    RemoveFilter();
                }
                else if (filterInfo == null || filterInfo.Filter != value)
                {
                    filterInfo = createFilterInfo(value);
                    Refresh();
                }
            }
        }

        public void RemoveFilter()
        {
            if (filterInfo != null)
            {
                filterInfo = null;
                Refresh();
            }
        }

        private FilterInfo createFilterInfo(string filter)
        {
            FilterInfo filterInfo = null;

            string[] andParts = filter.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);

            ParameterExpression param = Expression.Parameter(typeof(TEntity), "__record__");

            Expression exp;


            exp = null;

            foreach (var p in andParts)
            {

                Expression e = parseExpression(param, p);

                if (exp == null)
                {
                    exp = e;
                }
                else
                {
                    exp = Expression.And(exp, e);
                }
            }

            if (exp != null)
            {
                filterInfo = new FilterInfo() { Filter = filter };
                filterInfo.Expression = Expression.Lambda(exp, new ParameterExpression[] { param });
            }


            return filterInfo;
        }

        private Expression parseExpression(ParameterExpression param, string strExp)
        {
            List<string> pre_parts = strExp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> parts = new List<string>();
            bool startString = false;
            int stringIndex = 0;
            for (int i = 0; i < pre_parts.Count; i++)
            {
                string p = pre_parts[i];
                if (startString)
                {
                    parts[stringIndex] += " " + p;
                    if (p[p.Length - 1] == '\'')
                        startString = false;
                }
                else
                {
                    if (p[0] == '\'')
                    {
                        startString = true;
                        stringIndex = parts.Count;
                        parts.Add(p);
                        
                        if (p[p.Length - 1] == '\'')
                            startString = false;
                    }
                    else
                    {
                        parts.Add(p);
                    }
                }
            }

            Expression exp = null;

            while (parts != null && parts.Count() > 0)
            {
                exp = parsePart(ref parts, exp, param);
            }

            return exp;

        }

        private Expression parsePart(ref List<string> parts, Expression exp, ParameterExpression param)
        {
            string part = parts[0];
            parts.RemoveAt(0);

            // Member access
            if (part.ToLower() == "true")
            {
                return Expression.Constant(true);
            }
            else if (part.ToLower() == "false")
            {
                return Expression.Constant(false);
            }
            else if (Regex.IsMatch(part, @"^[a-zA-Z_]+[0-9a-zA-Z_ĄĆĘŁŃÓŚŹŻąćęłńóśźż]*$"))
            {
                PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(TEntity))[part];

                if (property != null)
                {
                    return Expression.Property(param, part);
                }
                return null;
            }
            else if (Regex.IsMatch(part, @"^[^\d'][^\.]+\.[^\d].*$"))
            {
                string[] memberParts = part.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                Expression memberExp = param;
                foreach (var p in memberParts)
                {
                    memberExp = Expression.Property(memberExp, p);
                }

                return memberExp;

            }
            // String
            else if (Regex.IsMatch(part, @"^'.*'$"))
            {
                if (exp != null && (exp.Type == typeof(DateTime) || exp.Type == typeof(DateTime?)))
                {
                    DateTime d = DateTime.Parse(part.Trim(new char[] { '\'' }));

                    if (exp.Type == typeof(DateTime?))
                    {
                        return Expression.Constant(new DateTime?(d), typeof(DateTime?));
                    }
                    return Expression.Constant(d, typeof(DateTime));
                }
                return Expression.Constant(part.Trim(new char[] { '\'' }), typeof(string));
            }
            // Integer
            else if (Regex.IsMatch(part, @"^\d+$"))
            {
                if (exp != null)
                    return parseConstant(part, exp.Type);
                return Expression.Constant(int.Parse(part));
            }
            // Double
            else if (Regex.IsMatch(part, @"^\d+\.\d+$"))
            {
                if (exp != null)
                    return parseConstant(part, exp.Type);
                return Expression.Constant(Double.Parse(part));
            }
            // Comparison
            else if (Regex.IsMatch(part, @"^[<=>!]+$"))
            {
                Expression right = parsePart(ref parts, exp, param);
                if (right != null)
                {
                    return createBinaryExpression(exp, right, part);
                }

            }

            return null;
        }

        private ConstantExpression parseConstant(string s, Type type)
        {
            if (type == typeof(byte) || type == typeof(byte?))
            {
                byte v = byte.Parse(s);
                if (type == typeof(byte?))
                    return Expression.Constant(new byte?(v), typeof(byte?));
                return Expression.Constant(v, typeof(byte));
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                int v = int.Parse(s);
                if (type == typeof(int?))
                    return Expression.Constant(new int?(v), typeof(int?));
                return Expression.Constant(v, typeof(int));
            }
            else if (type == typeof(Int64) || type == typeof(Int64?))
            {
                Int64 v = Int64.Parse(s);
                if (type == typeof(Int64?))
                    return Expression.Constant(new Int64?(v), typeof(Int64?));
                return Expression.Constant(v, typeof(Int64));
            }

            return null;
        }

        private BinaryExpression createBinaryExpression(Expression left, Expression right, string oper)
        {
            switch (oper.Trim())
            {
                case "==":
                    /*
                    if (left.Type.Name == "Nullable`1")
                    {
                        Expression notNull = Expression.NotEqual(left, Expression.Constant(null));
                        return Expression.And(notNull, Expression.Equal(Expression.Property(left, "Value"), right));
                    }
                     */

                    if (left.Type.Name != right.Type.Name)
                        left = Expression.Convert(left, right.Type);
                 
                    return Expression.Equal(left, right);
                case "!=":
                    return Expression.NotEqual(left, right);
                case "<":
                    return Expression.LessThan(left, right);
                case "<=":
                    return Expression.LessThanOrEqual(left, right);
                case ">":
                    return Expression.GreaterThan(left, right);
                case ">=":
                    return Expression.GreaterThanOrEqual(left, right);
                default:
                    return null;
            }
        }

        ListSortDescriptionCollection sortDescriptions = null;
        ListSortDescriptionCollection IBindingListView.SortDescriptions
        {
            get { return sortDescriptions; }
        }

        void IBindingListView.ApplySort(ListSortDescriptionCollection sorts)
        {
        }

        void IBindingListView.RemoveFilter()
        {
        }

        #endregion

        #region ICancelAddNew Implementation

        public virtual void CancelNew(int index)
        {
        }

        public virtual void EndNew(int index)
        {
            if (addNewIndex == index)
            {
                addNewIndex = -1;
            }
        }

        #endregion

        internal class FilterInfo
        {
            public Expression Expression;
            public string Filter;
        }

    }
}
