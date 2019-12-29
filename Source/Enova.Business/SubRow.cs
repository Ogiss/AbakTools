using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Data.Objects;

namespace Enova.Business.Old
{
    public class SubRow : IRow
    {
        #region Fields

        string name;
        string fullName;
        IRow parent;
        IRow root;

        #endregion

        #region Properies

        public int ID
        {
            get { return this.root.ID; }
        }

        public IRow Parent
        {
            get { return this.parent; }
        }

        public IRow Root
        {
            get { return this.root; }
        }

        public string Prefix
        {
            get
            {
                string prefix = this.parent.Prefix;
                if (prefix == "")
                {
                    return this.name;
                }
                return (prefix + "." + this.name);
            }
        }

        public string FullName
        {
            get
            {
                if(string.IsNullOrEmpty(this.fullName))
                    this.fullName = this.Prefix.Replace(".", "");
                return this.fullName;
            }
        }

        public ITable Table
        {
            get { return this.root.Table; }
        }

        public RowState State
        {
            get
            {
                if (this.root != null)
                    return this.root.State;
                return RowState.Detached;
            }
        }

        public bool IsLive
        {
            get
            {
                if (this.root != null)
                    return this.root.IsLive;
                return false;
            }
        }

        protected bool ReadOnly
        {
            get
            {
                if (this.root == null)
                {
                    return false;
                }
                //return this.root.ReadOnly;
                return this.root.IsReadOnly();
            }
            set
            {
                //this.root.ReadOnly = value;
            }
        }

        #endregion

        #region Methods

        public SubRow() { }
        public SubRow(IRow parent, string name)
        {
            this.AssignParent(parent, name);
        }

        public bool IsReadOnly()
        {
            if (this.root == null)
            {
                return false;
            }
            return this.root.IsReadOnly();
        }

        public void AssignParent(IRow parent, string name)
        {
            this.parent = parent;
            if (parent != null)
                this.root = parent.Root;
            this.name = name;
        }

        public PropertyInfo GetPropertyInfo(string propertyName)
        {
            return this.parent.GetType().GetProperty(this.FullName + propertyName);
        }

        public T GetFieldValue<T>(string propertyName)
        {
            return (T)this.GetPropertyInfo(propertyName).GetValue(this.parent, null);
        }

        public object GetFieldValue(string propertyName)
        {
            return this.GetPropertyInfo(name).GetValue(this.parent, null);
        }

        public void SetFieldValue(string propertyName, object value)
        {
            this.GetPropertyInfo(propertyName).SetValue(this.parent, value, null);
        }

        protected static T GetParent<T>(IRow obj) where T : class
        {
            if (obj != null)
            {
                T local1 = obj as T;
                if (local1 != null)
                {
                    return local1;
                }
                return GetParent<T>(obj.Parent);
            }
            return default(T);
        }

        #endregion

        #region IDataContext Implementation

        ObjectContext IDbContext.DbContext
        {
            get { return this.root.DbContext; }
            set { this.root.DbContext = value; }
        }

        #endregion
    }
}
