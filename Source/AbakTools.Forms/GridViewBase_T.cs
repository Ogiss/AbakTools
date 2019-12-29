using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Forms
{
    public class GridViewBase<T> : AbakTools.Forms.GridViewBase
        where T: class, new()
    {
        #region Fields

        private Enova.Business.Old.Core.TableBase<T> table;
        private object reloadLock = new object();

        #endregion

        #region Properties

        public override System.Collections.IList Rows
        {
            get
            {
                if (this.table == null)
                    this.initTable();
                return this.table;
            }
        }

        new public Enova.Business.Old.Core.TableBase<T> Table
        {
            get
            {
                return this.table;
            }
        }

        #endregion

        #region Methods

        private void initTable()
        {
            this.table = this.CreateTable();
            if (table != null && this.SupportsSorting && IsSorted)
                this.table.Sort(this.GetSortComparer());
        }

        protected virtual Enova.Business.Old.Core.TableBase<T> CreateTable()
        {
            return null;
        }

        public override Type GetDataType()
        {
            return typeof(T);
        }

        public override void Reload()
        {
            lock (reloadLock)
            {
                if (this.table != null)
                {
                    this.table.Reload();
                    if (SupportsSorting && IsSorted)
                        this.table.Sort(GetSortComparer());
                }
                base.Reload();
            }
        }

        public override void Sort(System.Collections.IComparer comparer)
        {
            if (this.table != null && this.SupportsSorting)
                this.table.Sort(comparer);
        }

        public override void EndEdit()
        {
            base.EndEdit();
            this.table = null;
            this.Reload();
        }


        #endregion
    }
}
