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

[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.ZamowieniaViewWyslaneAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{

    [Priority(270), Caption("Wyslane")]
    public class ZamowieniaViewWyslaneAction : ZamowieniaViewActionBase
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

        protected override bool TylkoPojedynczyWiersz
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Methods

        public ZamowieniaViewWyslaneAction()
        {
            AddStatus(TypStatusuZamowienia.Kurier);
            AddStatus(TypStatusuZamowienia.Przedstawiciel);
        }

        public void OnAction()
        {
            if (StatusAction.SelectedRows != null && StatusAction.SelectedRows.Count > 0)
            {
                foreach (ZamowienieView row in StatusAction.SelectedRows)
                {
                    row.ZmienStatusZamowienia(TypStatusuZamowienia.Wyslane, false);
                }
                SaveChanges();
                RefreshSelectedRows();
                Refresh();
            }
        }

        #endregion
    }
}
