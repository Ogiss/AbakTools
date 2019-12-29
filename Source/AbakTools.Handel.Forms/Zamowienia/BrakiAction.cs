using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.Types;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;

[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.BrakiAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{
    [Priority(110), Caption("Braki")]
    public class BrakiAction : ZamowieniaViewActionBase
    {
        #region Properties

        protected override bool ZawszeWidoczne
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        public void OnAction()
        {
            if (StatusAction.CurrentRow != null)
            {
                new BrakiForm() { Zamowienie = StatusAction.CurrentRow.Zamowienie }.ShowDialog();
                Reload();
            }

        }

        #endregion
    }
}
