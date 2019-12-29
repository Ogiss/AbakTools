using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Enova.Business.Old
{
    public static class RowHelper
    {
        public static RowState GetRowState(this EntityObject entity)
        {
            switch (entity.EntityState)
            {
                case EntityState.Added:
                    return RowState.Added;
                case EntityState.Deleted:
                    return RowState.Deleted;
                case EntityState.Modified:
                    return RowState.Modified;
                case EntityState.Unchanged:
                    return RowState.Unchanged;
                default:
                    return RowState.Detached;
            }
        }

        public static bool GetIsLive(this EntityObject entity)
        {
            return (entity.EntityState == EntityState.Added) || (entity.EntityState == EntityState.Modified) || (entity.EntityState == EntityState.Unchanged);
        }

        public static void Delete(this IRow row)
        {
            row.DbContext.DeleteObject(row);
            row.DbContext.SaveChanges();
        }
    }
}
