using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enova.Forms
{
    public partial class DataGridFormWithEnovaAPI : AbakTools.Forms.DataGridForm, Enova.API.Business.ISessionable
    {

        private Enova.API.Business.Session session;

        public Enova.API.Business.Session Session
        {
            get
            {
                if (session == null)
                    session = Enova.API.EnovaService.Instance.CreateSession();
                return session;
            }
        }

        public DataGridFormWithEnovaAPI()
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
