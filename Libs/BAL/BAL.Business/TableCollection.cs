using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class TableCollection : IEnumerable, IEnumerable<ITable>, ISessionable , IDisposable
    {
        #region Fields

        private bool is_disposed;
        private Session session;
        private ArrayList array;
        private HybridDictionary byName;
        private Dictionary<Type, ITable> byRowType;

        private static Dictionary<Type, string> tableNameByRowType;
        private static Dictionary<string, string> moduleNameByTableName;

        #endregion

        #region Properties

        public Session Session
        {
            get { return this.session; }
        }

        public ITable this[string tableName]
        {
            get
            {
                if (this.byName[tableName] == null)
                {
                    checkCache();
                    if (moduleNameByTableName.ContainsKey(tableName))
                        session.Modules.LoadModule(moduleNameByTableName[tableName]);
                }
                return (ITable)this.byName[tableName];
            }
        }

        public ITable this[Type rowType]
        {
            get
            {
                var table = getByRowType(rowType);

                if (table == null)
                {
                    checkCache();
                    if (tableNameByRowType.ContainsKey(rowType))
                        return this[tableNameByRowType[rowType]];
                    else if (tableNameByRowType.ContainsKey(rowType.BaseType))
                        return this[tableNameByRowType[rowType.BaseType]];
                }

                return table;
            }
        }

        #endregion

        #region Methods

        public TableCollection(Session session)
        {
            this.is_disposed = false;
            this.session = session;
            byName = new HybridDictionary(true);
            byRowType = new Dictionary<Type, ITable>();
            array = new ArrayList();
        }

        private ITable getByRowType(Type rowType)
        {
            if (this.byRowType.ContainsKey(rowType))
                return this.byRowType[rowType];
            if (this.byRowType.ContainsKey(rowType.BaseType))
                return this.byRowType[rowType.BaseType];
            return null;
        }

        private static void checkCache()
        {
            if (tableNameByRowType == null)
            {
                tableNameByRowType = new Dictionary<Type, string>();
                moduleNameByTableName = new Dictionary<string, string>();

                foreach (AppModuleAttribute attr in AppModuleAttribute.Collection)
                {
                    var nestedTypes = attr.ModuleType.GetNestedTypes();
                    foreach (var type in nestedTypes)
                    {
                        if (typeof(ITable).IsAssignableFrom(type))
                        {
                            string tableName = null;
                            Type rowType = null;

                            var attribute = TableNameAttribute.GetAttribute(type);
                            if (attribute != null)
                            {
                                tableName = attribute.TableName;
                                rowType = attribute.RowType;
                            }
                            else
                            {
                                var tableType = getTableBaseType(type);
                                if (tableType != null)
                                {
                                    rowType = tableType.GetGenericArguments().First();
                                    attribute = TableNameAttribute.GetAttribute(rowType);
                                    if (attribute != null)
                                    {
                                        tableName = attribute.TableName;
                                    }
                                    else
                                    {
                                        var attributes = rowType.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), true);
                                        if (attributes.Length > 0)
                                            tableName = ((System.ComponentModel.DataAnnotations.Schema.TableAttribute)attributes[0]).Name;
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(tableName))
                                tableName = type.Name;

                            moduleNameByTableName.Add(tableName, attr.ModuleName);
                            if (rowType != null)
                                tableNameByRowType.Add(rowType, tableName);
                        }
                    }

                }
            }
        }

        private static Type getTableBaseType(Type type)
        {
            var table = typeof(Table<>);
            while (type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == table)
                    return type;
                type = type.BaseType;
            }
            return null;
        }

        public void Add(ITable table)
        {
            if(table is ISessionSetter)
                ((ISessionSetter)table).Session = this.session;
            this.byName.Add(table.TableName, table);
            this.byRowType.Add(table.GetRowType(), table);
            this.array.Add(table);
        }

        public IEnumerator<ITable> GetEnumerator()
        {
            return this.byRowType.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        internal ArrayList ToArray()
        {
            return new ArrayList(this.array);
        }

        #endregion

        #region IDisposable Implementation

        private void Dispose(bool userCall)
        {
            if (!is_disposed)
            {
                if (userCall)
                {
                }
                is_disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TableCollection()
        {
            this.Dispose(false);
        }

        #endregion

    }
}
