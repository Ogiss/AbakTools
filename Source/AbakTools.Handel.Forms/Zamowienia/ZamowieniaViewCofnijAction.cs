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

[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.ZamowieniaViewCofnijAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{

    [Priority(310), Caption("Cofnij")]
    public class ZamowieniaViewCofnijAction : ZamowieniaViewActionBase
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

        public ZamowieniaViewCofnijAction()
        {
            AddStatus(TypStatusuZamowienia.DoMagazynu);
            AddStatus(TypStatusuZamowienia.DoDostawcy);
            AddStatus(TypStatusuZamowienia.Pakowanie);
            AddStatus(TypStatusuZamowienia.Spakowane);
            AddStatus(TypStatusuZamowienia.Kurier);
            AddStatus(TypStatusuZamowienia.Przedstawiciel);
            AddStatus(TypStatusuZamowienia.Wstrzymane);
            AddStatus(TypStatusuZamowienia.Anulowane);
            AddStatus(TypStatusuZamowienia.Wyslane);
        }

        public void OnAction()
        {
            if (StatusAction.CurrentRow != null)
            {
                if (FormManager.Confirm("Czy napewno chcesz cofnąć status zamówienia?"))
                {
                    StatusAction.CurrentRow.CofnijStatusZamowienia(true);
                    Refresh();
                }
            }
        }

        #endregion
    }
}
