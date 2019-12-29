using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Forms
{
    public partial class DataBaseFormWithEnovaAPI : Enova.Business.Old.Forms.DataBaseForm, API.Business.ISessionable
    {
        private API.Business.Session session;

        public API.Business.Session Session
        {
            get
            {
                if(!this.DesignMode && this.session==null)
                    session = API.EnovaService.Instance.CreateSession();
                return session;
            }
        }

        public DataBaseFormWithEnovaAPI()
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
