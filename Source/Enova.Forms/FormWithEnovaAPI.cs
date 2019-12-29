using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Enova.Forms
{
    public partial class FormWithEnovaAPI : Form, API.Business.ISessionable
    {
        private API.Business.Session session;

        public API.Business.Session Session
        {
            get
            {
                if (session == null && !this.DesignMode)
                    session = API.EnovaService.Instance.CreateSession();
                return session;
            }
        }

        public FormWithEnovaAPI()
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
