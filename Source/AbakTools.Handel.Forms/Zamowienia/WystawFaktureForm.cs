using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Zamowienia.Forms
{
    public partial class WystawFaktureForm : Form
    {
        private bool cancelClosing;

        public Enova.Business.Old.DB.Web.Zamowienie Zamowienie;

        public DateTime DataWystawienia
        {
            get { return dataDateTimePicker.Value.Date; }
        }

        public Enova.API.Magazyny.Magazyn Magazyn
        {
            get { return (Enova.API.Magazyny.Magazyn)magazynyComboBox.SelectedItem; }
        }

        public bool ZatwierdzFakture
        {
            get { return this.zatwierdzCheckBox.Checked; }
        }

        public bool DoliczKosztWysylki
        {
            get
            {
                return this.doliczWysylkeCheckBox.Checked;
            }
        }

        public decimal KosztWysylki
        {
            get
            {
                decimal d;
                if (decimal.TryParse(this.kosztWysyłkiTextBox.Text, out d))
                    return d;
                return 0;
            }
        }

        public int? Termin
        {
            get
            {
                int i;
                if (int.TryParse(terminTextBox.Text, out i))
                    return i;
                return null;
            }
        }

        public string Sezon
        {
            get { return ((Enova.API.Business.DictionaryItem)sezonyComboBox.SelectedItem).Value; }
        }

        public Enova.API.Handel.DefDokHandlowego DefDokHandlowego
        {
            get { return (Enova.API.Handel.DefDokHandlowego)this.definicjeDokComboBox.SelectedItem; }
        }

        public Enova.API.Kasa.FormaPlatnosci SposobZaplaty
        {
            get
            {
                if (this.sposobyZaplatyComboBox.SelectedItem is Enova.API.Kasa.FormaPlatnosci)
                    return (Enova.API.Kasa.FormaPlatnosci)this.sposobyZaplatyComboBox.SelectedItem;
                return null;
            }
        }

        public WystawFaktureForm()
        {
            InitializeComponent();
        }

        private void WystawFaktureFormcs_Load(object sender, EventArgs e)
        {
            this.cancelClosing = false;
            this.dataDateTimePicker.Value = DateTime.Now.Date;
            var kostWysylki = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDecimal("KOSZT_WYSYLKI");
            if (kostWysylki != null)
                this.kosztWysyłkiTextBox.Text = kostWysylki.ToString();

            var service = Enova.API.EnovaService.Instance;
            using (var session = service.CreateSession())
            {
                var hm = session.GetModule<Enova.API.Handel.HandelModule>();
                var bm = session.GetModule<Enova.API.Business.BusinessModule>();
                var km = session.GetModule<Enova.API.Kasa.KasaModule>();
                magazynyBindingSource.DataSource = session.GetModule<Enova.API.Magazyny.MagazynyModule>().Magazyny;

                sposobyZaplatyComboBox.DisplayMember = "Nazwa";
                sposobyZaplatyComboBox.ValueMember = "ID";
                sposobyZaplatyComboBox.Items.Add(new { ID = 0, Nazwa = "----------" });
                sposobyZaplatyComboBox.SelectedIndex = 0;

                foreach (var fp in km.FormyPlatnosci)
                {
                    sposobyZaplatyComboBox.Items.Add(fp);
                }

                /*
                sezonBindingSource.DataSource = bm.Dictionary["F.SEZON"];
                if (!string.IsNullOrEmpty(this.Zamowienie.Sezon))
                    sezonyComboBox.SelectedText = this.Zamowienie.Sezon;
                 */

                foreach (var s in bm.Dictionary["F.SEZON"])
                {
                    sezonyComboBox.Items.Add(s.Value);
                }

                if (!string.IsNullOrEmpty(this.Zamowienie.Sezon))
                    sezonyComboBox.SelectedItem = this.Zamowienie.Sezon;
                else
                    sezonyComboBox.SelectedIndex = 0;

                var przedstawiciele = bm.Dictionary["F.PRZEDSTAWICIEL"];


                List<Enova.API.Handel.DefDokHandlowego> defs = new List<Enova.API.Handel.DefDokHandlowego>();
                defs.Add(hm.DefDokHandlowych.FakturaSprzedazy);
                defs.Add(hm.DefDokHandlowych.Paragon);
                defs.Add(hm.DefDokHandlowych.FakturaProforma);

                this.defDokBindingSource.DataSource = defs;

                placiWysylkeComboBox.Items.Add("Nie wybrano");

                foreach (var prz in przedstawiciele)
                {
                    przedstawicielComboBox.Items.Add(prz.Value);
                    placiWysylkeComboBox.Items.Add(prz.Value);
                }
                if (this.Zamowienie.Kontrahent.Przedstawiciel != null)
                    przedstawicielComboBox.SelectedItem = this.Zamowienie.Kontrahent.Przedstawiciel.Kod;
                magazynyComboBox.SelectedIndex = 0;
                //sezonyComboBox.SelectedIndex = 0;
                placiWysylkeComboBox.SelectedIndex = 0;

                if (this.Zamowienie != null)
                {
                    this.kontrahentTextBox.Text = this.Zamowienie.KontrahentKod + " - " + this.Zamowienie.KontrahentNazwa;

                    string komunikat = null;
                    komunikat = session.GetModule<Enova.API.CRM.CRMModule>().Kontrahenci[this.Zamowienie.Kontrahent.Guid.Value].Komunikat;


                    if (!string.IsNullOrEmpty(komunikat))
                    {
                        this.komunikatTextBox.BackColor = Color.Yellow;
                        this.komunikatTextBox.Text = komunikat;
                    }
                    if (Zamowienie.Termin != null)
                        this.terminTextBox.Text = Zamowienie.Termin.Value.ToString();

                    if (this.Zamowienie.Paragon)
                        this.definicjeDokComboBox.SelectedIndex = 1;
                    else
                        this.definicjeDokComboBox.SelectedIndex = 0;

                }
            }
        }

        private void doliczWysylkeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (doliczWysylkeCheckBox.Checked)
                kosztWysyłkiTextBox.Enabled = true;
            else
                kosztWysyłkiTextBox.Enabled = false;
        }

        public Dictionary<string, object> GetCechy(Enova.API.Business.Session session)
        {
            Dictionary<string, object> cechy = new Dictionary<string, object>();

            //cechy.Add("KONTRAHENT", this.Zamowienie.Kontrahent);
            
            //using (var session = Enova.API.EnovaService.Instance.CreateSession())
            //{
                cechy.Add("KONTRAHENT", session.GetModule<Enova.API.CRM.CRMModule>().Kontrahenci[this.Zamowienie.Kontrahent.Guid.Value]);
            //}
            if (!string.IsNullOrEmpty(this.Zamowienie.Pakowacz))
                cechy.Add("PAKOWACZ", this.Zamowienie.Pakowacz);

            if (!string.IsNullOrEmpty(sezonyComboBox.Text))
                cechy.Add("SEZON", sezonyComboBox.Text);

            if (!string.IsNullOrEmpty(przedstawicielComboBox.Text))
                cechy.Add("PRZEDSTAWICIEL", przedstawicielComboBox.Text);

            if (przewoznikComboBox.SelectedIndex > 0)
                cechy.Add("PRZEWOŻNIK", przewoznikComboBox.Text);

            if (!string.IsNullOrEmpty(nrListuTextBox.Text))
                cechy.Add("NR LISTU", nrListuTextBox.Text);

            if (!string.IsNullOrEmpty(iloscPaczekTextBox.Text))
                cechy.Add("ILOSC PACZEK", iloscPaczekTextBox.Text);

            if (placiWysylkeComboBox.SelectedIndex > 0)
                cechy.Add("PŁACI WYSYŁKĘ", placiWysylkeComboBox.Text);

            if (zaPobraniemCheckBox.Checked)
                cechy.Add("ZA POBRANIEM", true);

            return cechy;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (this.Zamowienie.RodzajTransportu == Enova.Business.Old.Types.RodzajTransportu.Kurier)
            {
                if (this.przewoznikComboBox.SelectedIndex <= 0 || string.IsNullOrEmpty(iloscPaczekTextBox.Text))
                {
                    cancelClosing = true;
                    BAL.Forms.FormManager.Alert("!!! UWAGA !!! - Musisz wybrać przewoźnika i podać ilość paczek");
                }
            }

        }

        private void przewoznikComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((string)przewoznikComboBox.SelectedItem == "OPEK" || (string)przewoznikComboBox.SelectedItem == "DHL"
                || (string)przewoznikComboBox.SelectedItem == "TBA")
            {
                if (!string.IsNullOrEmpty(przedstawicielComboBox.Text))
                    this.placiWysylkeComboBox.SelectedItem = przedstawicielComboBox.Text;
                if (string.IsNullOrEmpty(this.iloscPaczekTextBox.Text))
                    this.iloscPaczekTextBox.Text = "1";
            }
        }

        private void WystawFaktureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = cancelClosing;
            cancelClosing = false;
        }

        private void sposobyZaplatyComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (SposobZaplaty!=null && SposobZaplaty.Nazwa.ToLower() == "pobranie")
            {
                zaPobraniemCheckBox.Checked = true;
            }
            else
            {
                zaPobraniemCheckBox.Checked = false;
            }
        }
    }
}
