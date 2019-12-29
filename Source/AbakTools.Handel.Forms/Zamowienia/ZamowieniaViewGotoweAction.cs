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

[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.ZamowieniaViewGotoweAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{

    [Priority(260), Caption("Gotowe")]
    public class ZamowieniaViewGotoweAction : ZamowieniaViewActionBase
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

        public ZamowieniaViewGotoweAction()
        {
            AddStatus(TypStatusuZamowienia.Spakowane);
            AddTransport(RodzajTransportu.Kurier);
            AddTransport(RodzajTransportu.Przedstawiciel);
        }

        public void OnAction()
        {
            if (StatusAction.CurrentRow != null)
            {
                if (!StatusAction.CurrentRow.Zamowienie.KopiowanoPozycje)
                {
                    if (!FormManager.Confirm("Z tego zamówienia nie kopiowano pozycji czy napewno chcesz zmienić jego status na \"Gotowe\"?"))
                        return;
                }

                var form = new WyborFakturyForm() { Zamowienie = StatusAction.CurrentRow.Zamowienie };
                form.ShowDialog();
                if (form.Faktura != null)
                {

                    StatusAction.CurrentRow.Zamowienie.FakturaGuid = form.Faktura.Guid;
                    StatusAction.CurrentRow.Zamowienie.FakturaNumer = form.Faktura.NumerPelny;
                    if (StatusAction.CurrentRow.RodzajTransportu == RodzajTransportu.Kurier)
                        StatusAction.CurrentRow.ZmienStatusZamowienia(TypStatusuZamowienia.Kurier, true);
                    else if (StatusAction.CurrentRow.RodzajTransportu == RodzajTransportu.Przedstawiciel)
                        StatusAction.CurrentRow.ZmienStatusZamowienia(TypStatusuZamowienia.Przedstawiciel, true);
                    Refresh();
                }

            }
        }

        #endregion
    }
}
