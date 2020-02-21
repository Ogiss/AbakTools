using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms
{
    public class WaitCursor : IDisposable
    {
        #region Fields

        private Control form;
        private bool disposed;

        #endregion

        #region Methods

        public WaitCursor(Control form)
        {
            this.form = form != null ? form : Form.ActiveForm;
            if (this.form == null)
                this.form =(Form)FormManager.MainForm;
            this.form.Enabled = false;
            this.form.Cursor = Cursors.WaitCursor;
        }

        public WaitCursor() : this(null) { }

        private void Dispose(bool userCall)
        {
            if (!this.disposed)
            {
                this.form.Enabled = true;
                this.form.Cursor = Cursors.Default;
                this.form.Select();
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~WaitCursor()
        {
            this.Dispose(false);
        }

        #endregion
    }
}
