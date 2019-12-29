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

//[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.ZamowieniaViewPrzedstawicielAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{

    [Priority(220), Caption("Przedstawiciel")]
    public class ZamowieniaViewPrzedstawicielAction : ZamowieniaViewActionBase
    {
        #region Fields

        #endregion

        #region Properties

        public override RodzajTransportu? DostepnyRodzajTransportu
        {
            get
            {
                return RodzajTransportu.NieWybrano;
            }
        }

        #endregion

        #region Methods

        public ZamowieniaViewPrzedstawicielAction()
        {
            AddStatus(TypStatusuZamowienia.NoweZamowienie);
        }

        public void OnAction()
        {
            if (StatusAction.CurrentRow != null)
            {
                StatusAction.CurrentRow.Zamowienie.RodzajTransportu = RodzajTransportu.Przedstawiciel;
                StatusAction.CurrentRow.ZmienStatusZamowienia(TypStatusuZamowienia.DoMagazynu, true);
                Refresh();
            }
        }

        #endregion
    }
}
