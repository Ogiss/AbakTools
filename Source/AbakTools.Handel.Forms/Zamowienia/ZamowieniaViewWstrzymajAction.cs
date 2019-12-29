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

[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.ZamowieniaViewWstrzymajAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{
    [Priority(200),Caption("Wstrzymaj")]
    public class ZamowieniaViewWstrzymajAction : ZamowieniaViewActionBase
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods

        public ZamowieniaViewWstrzymajAction()
        {
            AddStatus(TypStatusuZamowienia.NoweZamowienie);
            AddStatus(TypStatusuZamowienia.DoMagazynu);
            AddStatus(TypStatusuZamowienia.DoDostawcy);
            AddStatus(TypStatusuZamowienia.Pakowanie);
            AddStatus(TypStatusuZamowienia.Spakowane);
        }

        public void OnAction()
        {
            if (StatusAction.CurrentRow != null)
            {
                StatusAction.CurrentRow.ZmienStatusZamowienia(TypStatusuZamowienia.Wstrzymane, true);
                Refresh();
            }
        }

        #endregion
    }
}
