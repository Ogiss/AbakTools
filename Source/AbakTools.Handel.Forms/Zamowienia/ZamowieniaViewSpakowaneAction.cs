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

//[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.ZamowieniaViewSpakowaneAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{

    [Priority(250), Caption("Spakowane")]
    public class ZamowieniaViewSpakowaneAction : ZamowieniaViewActionBase
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

        public ZamowieniaViewSpakowaneAction()
        {
            AddStatus(TypStatusuZamowienia.Pakowanie);
        }

        public void OnAction()
        {
            if (StatusAction.CurrentRow != null)
            {
                var @operator = StatusAction.CurrentRow.Zamowienie.OstatniaHistoriaZamowienia.Operator;
                if (@operator == null)
                    @operator = User.GetOperator(User.LoginedUser);
                StatusAction.CurrentRow.Zamowienie.ZmienStatus(@operator, StatusyZamowieniaTyp.Spakowane);
                StatusAction.CurrentRow.SaveChanges();
                Refresh();
            }
        }

        #endregion
    }
}
