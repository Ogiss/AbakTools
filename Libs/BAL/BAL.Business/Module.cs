using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public abstract class Module : ISessionable, IDisposable
    {
        #region Fields

        private bool is_disposed = false;
        private string name;
        private Session session;

        #endregion

        #region Properties

        public string Name
        {
            get { return this.name; }
        }

        public Session Session
        {
            get { return this.session; }
        }

        public DBContextBase DataContext
        {
            get
            {
                return ((Old.ISession)this.Session).GetDataContext(this);
            }
        }

        #endregion

        #region Methods

        public Module(Session session, string name)
        {
            this.session = session;
            this.name = name;
        }

        protected virtual void OnDisposing(ObjectDisposingEventArgs e)
        {
            if (Disposing != null)
                Disposing(this, e);
        }

        public void AddTable(ITable table)
        {
            if (table is IModuleSetter)
                ((IModuleSetter)table).Module = this;
            this.Session.Tables.Add(table);
        }

        public virtual int SaveChanges()
        {
            return this.DataContext.SaveChanges();
        }

        #endregion

        #region Events

        public event ObjectDisposingEventHandler Disposing;

        #endregion

        #region IDisposable Implementation

        private void Dispose(bool userCall)
        {
            if (!is_disposed)
            {
                OnDisposing(new ObjectDisposingEventArgs(userCall));
                is_disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Module()
        {
            Dispose(false);
        }


        #endregion

        #region Nested types

       

        #endregion

    }
}
