using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Forms
{
    public partial class DataGridFormWithEnovaAPI : Enova.Business.Old.Forms.DataGridForm, API.Business.ISessionable
    {
        private API.Business.Session session;

        public API.Business.Session Session
        {
            get { return session; }
        }

        public DataGridFormWithEnovaAPI()
        {
            if (!this.DesignMode)
                session = API.EnovaService.Instance.CreateSession();
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (session != null)
                session.Dispose();
            session = null;
        }

    }
}
