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

[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.ZamowieniaViewPakujAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{

    [Priority(240), Caption("Pakuj")]
    public class ZamowieniaViewPakujAction : ZamowieniaViewActionBase
    {
        #region Fields

        #endregion

        #region Properties

        public override bool TylkoMagazynier
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        public ZamowieniaViewPakujAction()
        {
            AddStatus(TypStatusuZamowienia.DoMagazynu);
        }

        public void OnAction()
        {
            if (StatusAction.CurrentRow != null)
            {
                var form = new WyborMagazynieraForm();
                if (form.ShowDialog() == DialogResult.OK && form.User != null)
                {
                    StatusAction.CurrentRow.ZmienStatusZamowienia(TypStatusuZamowienia.Pakowanie, form.User, true);
                    Refresh();
                }
            }
        }

        #endregion
    }
}
