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

[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.ZamowieniaViewBlokujAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{

    [Priority(280), Caption("Blokuj")]
    public class ZamowieniaViewBlokujAction : ZamowieniaViewActionBase
    {
        #region Fields

        #endregion

        #region Properties

        public override bool TylkoAdmin
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        public ZamowieniaViewBlokujAction()
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
                StatusAction.CurrentRow.ZmienStatusZamowienia(TypStatusuZamowienia.Blokada, true);
                Refresh();
            }
        }

        #endregion
    }
}
