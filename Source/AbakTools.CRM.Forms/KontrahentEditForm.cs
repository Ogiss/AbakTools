using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.CRM.Forms
{
    public partial class KontrahentEditForm : Enova.Business.Old.Forms.DataEditTabForm
    {
        public KontrahentEditForm()
        {
            InitializeComponent();
        }

        protected override void OnBeforeBinding(Enova.Business.Old.Forms.BindingEventArgs e)
        {
            przedstawicieleBindingSource.DataSource = Enova.Business.Old.Core.ContextManager.WebContext.Kontrahenci.Where(k => k.CzyAgent == true).OrderBy(k => k.Kod).ToList();
            base.OnBeforeBinding(e);
        }

        private void KontrahentEditForm_Load(object sender, EventArgs e)
        {
            if (this.DataSource != null)
            {
                setEnovaTextBox();

                if (((Enova.Business.Old.DB.Web.Kontrahent)this.DataSource).EntityState != System.Data.EntityState.Detached &&
                    ((Enova.Business.Old.DB.Web.Kontrahent)this.DataSource).EntityState != System.Data.EntityState.Added)
                {
                    Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this.DataSource);
                    ((Enova.Business.Old.DB.Web.Kontrahent)this.DataSource).Adresy.Load();
                }

                Enova.Business.Old.DB.Web.Adres adres = ((Enova.Business.Old.DB.Web.Kontrahent)this.DataSource).DomyslnyAdresFaktury;
                if (adres == null)
                    adres = new Enova.Business.Old.DB.Web.Adres()
                    {
                        Domyslny = true,
                        DomyslnyAdresFaktury = true,
                        DomyslnyAdresWysylki = false,
                        Kontrahent = (Enova.Business.Old.DB.Web.Kontrahent)this.DataSource
                    };

                adresBindingSource.DataSource = adres;
                Enova.Business.Old.DB.Web.Adres adresKor = ((Enova.Business.Old.DB.Web.Kontrahent)this.DataSource)
                    .Adresy.Where(a => a.DomyslnyAdresWysylki == true && (a.Usuniety == null || a.Usuniety == false)
                        && (a.DomyslnyAdresFaktury == null || a.DomyslnyAdresFaktury == false)).FirstOrDefault();


                if (adresKor == null)
                {
                    adresKor = new Enova.Business.Old.DB.Web.Adres()
                    {
                        Kontrahent = (Enova.Business.Old.DB.Web.Kontrahent)this.DataSource,
                        DomyslnyAdresFaktury = false,
                        DomyslnyAdresWysylki = true
                    };
                }
                else
                {
                    checkBox1.Checked = false;
                }
                

                
                adresKorBindingSource.DataSource = adresKor;
            }
        }

        private void setEnovaTextBox()
        {
            /*
            Enova.Business.Old.DB.Kontrahent enova = ((Enova.Business.Old.DB.Web.Kontrahent)this.DataSource).EnovaKontrahent;
            if (enova != null)
                enovaTextBox.Text = "(" + enova.Kod + ") " + enova.Nazwa;
             */
            if (((Enova.Business.Old.DB.Web.Kontrahent)this.DataSource).Guid != null)
            {
                using (var s = Enova.API.EnovaService.Instance.CreateSession())
                {
                    var enova = s.GetModule<Enova.API.CRM.CRMModule>().Kontrahenci[((Enova.Business.Old.DB.Web.Kontrahent)this.DataSource).Guid.Value];
                    if(enova != null)
                        enovaTextBox.Text = "(" + enova.Kod + ") " + enova.Nazwa;
                }
            }
        }

        private void enovaButton_Click(object sender, EventArgs e)
        {
            if (((Enova.Business.Old.DB.Web.Kontrahent)this.DataSource).Guid != null)
            {
                DialogResult result = MessageBox.Show("Czy napewno chcesz zmienić kontrahenta ?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.No)
                    return;
            }
            var view = new Enova.Forms.CRM.KontrahenciView() { SelectionMode = true };
            if(BAL.Forms.FormManager.Instance.ShowGridFormDialog(view)== System.Windows.Forms.DialogResult.OK)
            //if (form.SelectedItem != null)
            {
                //Enova.Business.Old.DB.Kontrahent enovaKontrahent = (Enova.Business.Old.DB.Kontrahent)form.SelectedItem;
                Enova.API.CRM.Kontrahent enovaKontrahent = (Enova.API.CRM.Kontrahent)view.Current;
                if (Enova.Business.Old.Core.ContextManager.WebContext.Kontrahenci.Any(k => k.Guid == enovaKontrahent.Guid))
                {
                    MessageBox.Show("Istnieje już konto dla tego kontrahenta", "EnovaTools", MessageBoxButtons.OK);
                    return;
                }
                Enova.Business.Old.DB.Web.Kontrahent kontrahent = (Enova.Business.Old.DB.Web.Kontrahent)this.DataSource;
                kontrahent.Guid = enovaKontrahent.Guid;
                kontrahent.Kod = enovaKontrahent.Kod;
                kontrahent.Nazwa = enovaKontrahent.Nazwa;
                kontrahent.Nip = enovaKontrahent.NIP;

                string prKod = enovaKontrahent.Features["przedstawiciel"].ToString();

                Enova.Business.Old.DB.Web.Kontrahent przedstawiciel = Enova.Business.Old.Core.ContextManager.WebContext.Kontrahenci
                    .Where(k => k.CzyAgent == true && k.Kod == prKod).FirstOrDefault();

                kontrahent.Przedstawiciel = przedstawiciel;
                przedstawicielComboBox.DataBindings["SelectedItem"].ReadValue();
                

                var enovaAdres = enovaKontrahent.Adres;
                Enova.Business.Old.DB.Web.Adres adres = kontrahent.DomyslnyAdresFaktury;
                adres.KodPocztowy = enovaAdres.KodPocztowy;
                adres.Miasto = enovaAdres.Miejscowosc;
                adres.Adres1 = enovaAdres.Ulica + (enovaAdres.NrDomu != "" ? (" " + enovaAdres.NrDomu + (enovaAdres.NrLokalu != "" ? "/" + enovaAdres.NrLokalu : "")) : "");
                adres.Alias = enovaKontrahent.Kod;
                adres.Firma = enovaKontrahent.Nazwa;
                adres.Telefon = enovaAdres.Telefon;

                var enovaAdresKor = enovaKontrahent.AdresDoKorespondencji;
                if (enovaAdresKor != null && !string.IsNullOrEmpty(enovaAdresKor.KodPocztowy) && enovaAdresKor.KodPocztowy != "0")
                {
                    Enova.Business.Old.DB.Web.Adres adresKor = kontrahent.Adresy.Where(a => a.DomyslnyAdresFaktury == false && a.DomyslnyAdresWysylki == true).FirstOrDefault();
                    adresKor.Firma = enovaKontrahent.Nazwa;
                    adresKor.Alias = enovaKontrahent.Kod;
                    adresKor.Adres1 = enovaAdresKor.Ulica + (enovaAdresKor.NrDomu != "" ? (" " + enovaAdresKor.NrDomu + (enovaAdresKor.NrLokalu != "" ? "/" + enovaAdresKor.NrLokalu : "")) : "");
                    adresKor.KodPocztowy = enovaAdresKor.KodPocztowy;
                    adresKor.Miasto = enovaAdresKor.Miejscowosc;
                    adresKor.Telefon = enovaAdresKor.Telefon;
                }

                setEnovaTextBox();
            }
        }

        private void synchronizujButton_Click(object sender, EventArgs e)
        {
        }

        private void mainTabPage_Click(object sender, EventArgs e)
        {

        }
    }
}
