using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enova.Forms.Controls
{
    public partial class BALControlWithEnovaAPI : BAL.Forms.Controls.BALControl, API.Business.ISessionable
    {

        #region Fields

        private API.Business.Session enovaSession;

        #endregion

        #region Properties

        [Browsable(false)]
        public API.Business.Session Session
        {
            get
            {
                if (enovaSession == null)
                {
                    var form = this.FindForm();
                    if (form != null && form is API.Business.ISessionable)
                        enovaSession = ((API.Business.ISessionable)form).Session;
                }
                return enovaSession;
            }
        }

        #endregion


        public BALControlWithEnovaAPI()
        {
            InitializeComponent();
        }
    }
}
