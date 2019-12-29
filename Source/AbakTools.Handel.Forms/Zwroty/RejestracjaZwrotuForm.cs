using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB.Web;

namespace AbakTools.Zwroty.Forms
{
    public partial class RejestracjaZwrotuForm : Enova.Business.Old.Forms.DataEditForm
    {

        private Kontrahent kontrahent;

        public Kontrahent Kontrahent
        {
            get
            {
                /*
                if (kontrahentSelect.SelectedItem != null)
                    return (Kontrahent)kontrahentSelect.SelectedItem;
                return null;
                 */
                return kontrahent;
            }
        }

        public int IloscPaczek
        {
            get
            {
                int i;
                if (int.TryParse(iloscPaczekTextBox.Text, out i))
                    return i;
                return 0;
            }
        }

        public string Opis
        {
            get
            {
                return opisTextBox.Text;
            }
        }


        public RejestracjaZwrotuForm()
        {
            InitializeComponent();
        }

        private void kontrahentEnovaSelect_ValueChanged(object sender, EventArgs e)
        {
            kontrahent = Kontrahent.GetKontrahent(Enova.Business.Old.Core.ContextManager.WebContext, (Enova.API.CRM.Kontrahent)kontrahentEnovaSelect.SelectedItem);
            if (Kontrahent != null)
            {
                int? defaultStatusDopPrzj = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigInt("ZWROT_STATUS_DOPIERO_PRZYJEDZIE");
                if (defaultStatusDopPrzj != null)
                {
                    WebContext dc = this.Kontrahent.DbContext != null ? (WebContext)this.Kontrahent.DbContext : Enova.Business.Old.Core.ContextManager.WebContext;
                    if (dc.Zwroty.Any(z => z.KontrahentID == this.Kontrahent.ID && z.OstatniStatusID == defaultStatusDopPrzj.Value))
                        BAL.Forms.FormManager.Alert("!!! UWAGA !!! - DLA TEGO KONTRAHENTA ISTNIEJE ZWROT Z USTAWIONYM STATUSEM \"DOPIERO PRZYJEDZIE\"");

                }
            }
        }

    }
}
