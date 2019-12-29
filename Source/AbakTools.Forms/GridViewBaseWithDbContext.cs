using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using DBWeb = Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace AbakTools.Forms
{
    public class GridViewBaseWithDbContext<T> : GridViewBase<T>, Enova.Business.Old.Core.IContexable
        where T : class, new()
    {
        #region Fields

        private bool disposeContext;
        private DBWeb.WebContext dbContext;

        #endregion

        #region Properties

        public virtual DBWeb.WebContext DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    dbContext = new DBWeb.WebContext();
                    disposeContext = true;
                }
                return dbContext;
            }
        }

        System.Data.Objects.ObjectContext Enova.Business.Old.Core.IContexable.DbContext
        {
            get { return this.DbContext; }
        }

        #endregion

        #region Methods

        public GridViewBaseWithDbContext() { }

        public GridViewBaseWithDbContext(DBWeb.WebContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override object CreateData()
        {
            var data = base.CreateData();
            if (data != null)
            {
                if (data is Enova.Business.Old.IDbContext)
                    ((Enova.Business.Old.IDbContext)data).DbContext = DbContext;
                if (data is Enova.Business.Old.IIsLive)
                    ((Enova.Business.Old.IIsLive)data).IsLive = true;
            }
            return data;
        }

        public override void Remove(object obj)
        {
            if (obj is Enova.Business.Old.Core.IDeleteRecord)
                ((Enova.Business.Old.Core.IDeleteRecord)obj).DeleteRecord();
            base.Remove(obj);
            this.Reload();
        }
    

        protected override void Dispose(bool userCall)
        {
            base.Dispose(userCall);
            if (this.dbContext != null && disposeContext)
            {
                this.dbContext.Dispose();
                this.dbContext = null;
            }
        }

        #endregion

    }
}
