using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enova.Forms
{
    public partial class DataGridFormWithEnovaAPIOld : AbakTools.Forms.DataGridFormOld, API.Business.ISessionable
    {
        private API.Business.Session session;

        public API.Business.Session Session
        {
            get
            {
                if (!this.DesignMode && session == null)
                    session = API.EnovaService.Instance.CreateSession();
                return session;
            }
        }

        public DataGridFormWithEnovaAPIOld()
        {
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
