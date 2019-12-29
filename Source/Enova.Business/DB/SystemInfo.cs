using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Enova.Business.Old.DB
{
    public partial class SystemInfo : IRow, ISetSession, ISessionable, IDbContext
    {
        #region Properties

        public BusinessModule Module
        {
            get { return BusinessModule.GetInstance(this.Session); }
        }

        #endregion

        #region ISessionable, ISetSession

        private Session session;

        Session ISetSession.Session
        {
            set { this.session = value; }
        }

        public Session Session
        {
            get { return this.session; }
        }

        ObjectContext IDbContext.DbContext
        {
            get
            {
                if (this.session != null)
                    return this.session.DataContext;
                return null;
            }
            set { }
        }

        #endregion

        #region IRow Implementation

        IRow IRow.Parent
        {
            get { return null; }
        }
        IRow IRow.Root
        {
            get { return this; }
        }
        string IRow.Prefix
        {
            get { return ""; }
        }

        public SystemInfos Table
        {
            get
            {
                return this.Module.SystemInfos;
            }
        }

        ITable IRow.Table
        {
            get
            {
                return this.Table;
            }
        }
        public RowState State
        {
            get { return this.GetRowState(); }
        }

        public bool IsLive
        {
            get { return this.GetIsLive(); }
        }

        public bool IsReadOnly()
        {
            return false;
        }

        #endregion

    }
}
