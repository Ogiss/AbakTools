using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class AppServiceBase : IAppService
    {
        #region Fields

        private bool is_disposed;
        private Session session;

        #endregion

        #region Properties

        public Session Session
        {
            get { return this.session; }
        }

        #endregion

        #region Methods

        protected AppServiceBase(Session session)
        {
            this.is_disposed = false;
            this.session = session;
        }

        protected AppServiceBase() : this(null) { }

        internal void Assign(Session session)
        {
            this.session = session;
        }

        protected virtual void DisposeService(bool userCall)
        {
        }

        #endregion

        #region IDisposable Implementation

        private void Dispose(bool userCall)
        {
            if (!is_disposed)
            {
                DisposeService(userCall);
                is_disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AppServiceBase()
        {
            Dispose(false);
        }

        #endregion
    }
}
