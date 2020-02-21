using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public class ColumnCollection : IEnumerable, IEnumerable<Column>, ISupportInitialize
    {
        #region Fields

        private Dictionary<string, Column> byName;
        private SortedList<int, Column> byOrder;
        private bool initializing;

        #endregion

        #region Properties

        public Column this[string name]
        {
            get
            {
                if (this.byName.ContainsKey(name))
                    return this.byName[name];
                return null;
            }
        }

        #endregion

        #region Methods

        public ColumnCollection()
        {
            this.byName = new Dictionary<string, Column>();
            this.byOrder = new SortedList<int, Column>();
        }

        public void Add(Column column)
        {
            if (string.IsNullOrEmpty(column.Name))
                column.Name = "Column" + this.byName.Values.Count.ToString();
            if (!this.byName.ContainsKey(column.Name))
            {
                Column last = this.GetLast();
                if (last != null)
                    column.Order = last.Order + 1;
                this.byName.Add(column.Name, column);
                this.byOrder.Add(column.Order, column);
                column.PropertyChanged += new PropertyChangedEventHandler(column_PropertyChanged);
                this.OnColumnAdded(new ColumnEventArgs(column));
            }
        }

        public void Insert(int index, Column column)
        {
        }

        public Column GetLast()
        {
            return this.byOrder.LastOrDefault().Value;
        }

        public Column GetFirst()
        {
            return this.byOrder.FirstOrDefault().Value;
        }

        public bool Contains(string name)
        {
            return this.byName.ContainsKey(name);
        }

        public IEnumerator<Column> GetEnumerator()
        {
            return this.byOrder.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerable<Column> GetVisible()
        {
            foreach (var col in this)
            {
                if (!col.Visible)
                    continue;
                yield return col;
            }
        }

        public void ForeEach(ColumnAction action, bool onlyVisible = false)
        {
            foreach (var col in this)
            {
                if (!col.Visible && onlyVisible)
                    continue;
                action(col);
            }
        }

        public void BeginInit()
        {
            if (!this.initializing)
            {
                this.initializing = true;
                this.ForeEach((column) => {
                    column.BeginInit();
                });
            }
        }

        public void EndInit()
        {
            if (this.initializing)
            {
                this.ForeEach((column) => {
                    column.EndInit();
                });
                this.initializing = false;
            }
        }

        private void column_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnColumnChanged(new ColumnChangedEventArgs((Column)sender, e.PropertyName));
        }

        protected virtual void OnColumnChanged(ColumnChangedEventArgs e)
        {
            if (!this.initializing && this.ColumnChanged != null)
            {
                this.ColumnChanged(this, e);
            }
        }

        protected virtual void OnColumnAdded(ColumnEventArgs e)
        {
            if (!this.initializing && this.ColumnAdded != null)
                this.ColumnAdded(this, e);
        }

        #endregion

        #region Events

        public event EventHandler<ColumnChangedEventArgs> ColumnChanged;
        public event EventHandler<ColumnEventArgs> ColumnAdded;

        #endregion

        #region Nested Types

        public delegate void ColumnAction(Column column);

        #endregion

    }
}
