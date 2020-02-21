using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace BAL.Business
{
    internal class Saver
    {
        #region Fields

        private Session session;

        #endregion

        #region Methods

        public Saver(Session session)
        {
            this.session = session;
        }

        public void Run()
        {
            if (this.session.changedRows != null && this.session.changedRows.Count > 0)
            {
                this.saveRows(RowState.Deleted);
                this.saveRows(RowState.Added);
                this.saveRows(RowState.Modified);
                this.session.changedRows.Clear();
            }
        }

        private void saveRows(RowState state)
        {
            foreach (var row in this.session.changedRows.Where(r=>r.State == state))
            {
                switch (state)
                {
                    case RowState.Deleted:
                        deleteRow(row);
                        break;
                    case RowState.Added:
                        addRow(row);
                        break;
                    case RowState.Modified:
                        updateRow(row);
                        break;
                }
            }
        }

        private void deleteRow(IRow row)
        {
            if (row.DBContext != null)
            {
                var state = row.DBContext.Entry(row).State;
                if (state != System.Data.Entity.EntityState.Detached && state != System.Data.Entity.EntityState.Deleted)
                    row.DBContext.Set(row.GetType()).Remove(row);
            }
                
        }

        private void addRow(IRow row)
        {
            if (row.Table != null)
            {
                var dc = row.Table.Module.DataContext;
                if (dc.Entry(row).State != System.Data.Entity.EntityState.Added)
                        dc.Set(row.GetType()).Add(row);
                session.toRefreshOld.Add(row);
            }
        }

        private void updateRow(IRow row)
        {
            if (row.Table != null)
            {
                var state = row.Table.Module.DataContext.Entry(row).State;
                if (state == EntityState.Detached)
                {
                    this.addRow(row);
                }
                session.toRefreshOld.Add(row);
            }
        }

        #endregion
    }
}
