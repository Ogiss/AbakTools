using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API = Enova.API;

namespace AbakTools.Forms
{
    public abstract class GridViewWithEnovaApi<T> : BAL.Forms.GridViewContext
        where T : Enova.API.Business.Row
    {
        #region Fields

        private Enova.API.Business.Table<T> table;
        private Enova.API.Business.Session session;
        private bool disposeSession;


        #endregion

        #region Properties

        public API.EnovaService Servive
        {
            get
            {
                return Enova.API.EnovaService.Instance;
            }
        }

        new public Enova.API.Business.Session Session
        {
            get
            {
                if (session == null)
                {
                    session = Servive.CreateSession();
                    disposeSession = true;
                }
                return session;
            }
        }

        new public Enova.API.Business.Table<T> Table
        {
            get
            {
                if (table == null)
                    table = CreateTable();
                return table;
            }
        }

        #endregion

        #region Methods

        protected abstract Enova.API.Business.Table<T> CreateTable();
    

        protected override void Dispose(bool userCall)
        {
            base.Dispose(userCall);
            if (disposeSession && session != null)
            {
                session.Dispose();
                session = null;
            }
        }

        protected override System.Collections.IList GetRows()
        {
            if (Table != null)
                return Table.ToList();
            return base.GetRows();
        }

        public override Type GetDataType()
        {
            return typeof(T);
        }

        #endregion
    }
}
