using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Forms
{
    public class GridViewWithDbContext : GridViewBase, Enova.Business.Old.Core.IContexable
    {
        #region Fields

        private Enova.Business.Old.DB.Web.WebContext dbContext;

        #endregion

        #region Properties

        public Enova.Business.Old.DB.Web.WebContext DbContext
        {
            get
            {
                if (dbContext == null)
                    dbContext = new Enova.Business.Old.DB.Web.WebContext();
                return dbContext;
            }
        }

        System.Data.Objects.ObjectContext Enova.Business.Old.Core.IContexable.DbContext
        {
            get
            {
                return this.DbContext;
            }
        }
        
        #endregion

        #region Methods

        public override object CreateData()
        {
            var data = base.CreateData();
            if (data != null && data is Enova.Business.Old.IDbContext)
                ((Enova.Business.Old.IDbContext)data).DbContext = dbContext;
            return data;
        }

        protected override void OnDisposed(EventArgs e)
        {
            base.OnDisposed(e);
            if (dbContext != null)
            {
                dbContext.Dispose();
                dbContext = null;
            }
        }

        #endregion
    }
}
