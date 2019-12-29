using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DBWeb = Enova.Business.Old.DB.Web;

[assembly: BAL.Forms.DataPanel("Ogólne", typeof(Enova.Business.Old.DB.Web.Korespondencja), typeof(AbakTools.CRM.Forms.KorespondencjaOgolnePanel))]

namespace AbakTools.CRM.Forms
{
    [ToolboxItem(false)]
    public partial class KorespondencjaOgolnePanel : BAL.Forms.DataPanel
    {
        #region Methods

        public KorespondencjaOgolnePanel()
        {
            InitializeComponent();
        }

        protected override void OnBeforeBinding(EventArgs e)
        {
            this.loadRodzaje();
            base.OnBeforeBinding(e);
        }

        protected override void OnBindingComplete(EventArgs e)
        {
            base.OnBindingComplete(e);
            if (DataContext.GetValue("RodzajKorespondencji") == null)
            {
                rodzajComboBox.SelectedIndex = 0;
                DataContext.SetValue("RodzajKorespondencji", (Enova.Business.Old.DB.Web.RodzajKorespondencji)rodzajComboBox.SelectedItem);
            }

        }

        private void loadRodzaje()
        {
            rodzajBindingSource.DataSource = Enova.Business.Old.Core.ContextManager.WebContext
                .RodzajeKorespondencji.OrderBy(r => r.ID).ToList();
            rodzajComboBox.SelectedIndex = 0;
        }

        private void kontrahentSelect_ValueChanged(object sender, EventArgs e)
        {
            Enova.API.CRM.Kontrahent kontrahent = (Enova.API.CRM.Kontrahent)kontrahentSelect.SelectedItem;
            if (kontrahent != null)
            {
                adresKoresCheckBox.Checked = !string.IsNullOrEmpty(kontrahent.AdresDoKorespondencji.KodPocztowy);
                setAdres();
            }
        }

        private void adresKoresCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setAdres();
        }

        private void setAdres()
        {
            Enova.API.CRM.Kontrahent kontrahent = (Enova.API.CRM.Kontrahent)kontrahentSelect.SelectedItem; 
            if (kontrahent != null && kontrahent.ID != 0)
            {
                //Enova.Business.Old.DB.Adres adres = adresKoresCheckBox.Checked ? kontrahent.AdresKorespondencyjny : kontrahent.Adres;
                var adres = adresKoresCheckBox.Checked ? kontrahent.AdresDoKorespondencji : kontrahent.Adres;
                if (adres != null && this.DataContext!= null)
                {
                    if (DataContext != null)
                    {
                        DataContext.SetValue("Kod", kontrahent.Kod);
                        DataContext.SetValue("Nazwa", kontrahent.Nazwa);
                        DataContext.SetValue("Adres",adres.Ulica + " " + adres.NrDomu + (string.IsNullOrEmpty(adres.NrLokalu) ? "" : "/" + adres.NrLokalu));
                        DataContext.SetValue("KodPocztowy", adres.KodPocztowy);
                        DataContext.SetValue("Miejscowosc", adres.Miejscowosc);
                        this.Refresh();
                    }
                }
            }
        }

        private void dodajRodzajButton_Click(object sender, EventArgs e)
        {
            RodzajKorespondencjiEditForm form = new RodzajKorespondencjiEditForm();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                loadRodzaje();
                rodzajComboBox.SelectedValue = form.RodzajKorespondencji.ID;
            }
        }

        #endregion

    }
}
