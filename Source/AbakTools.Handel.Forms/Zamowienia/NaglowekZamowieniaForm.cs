using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DBWeb = Enova.Business.Old.DB.Web;

namespace AbakTools.Zamowienia.Forms
{
    public partial class NaglowekZamowieniaForm : Enova.Business.Old.Forms.DataEditForm
    {
        private bool fireEvents = true;

        public DBWeb.Zamowienie Zamowienie
        {
            get { return (DBWeb.Zamowienie)this.DataSource; }
            set { this.DataSource = value; }
        }

        public bool KopijRabaty
        {
            get { return this.kopiujRabatyCheckBox.Checked; }
        }

        public NaglowekZamowieniaForm()
        {
            InitializeComponent();
        }

        private void NaglowekZamowieniaForm_Load(object sender, EventArgs e)
        {
            if (this.Zamowienie != null)
            {
                this.UndoChangesOnClose = false;

                if(Zamowienie.Kontrahent != null)
                using (var s = Enova.API.EnovaService.Instance.CreateSession())
                    kontrahentEnovaSelect.SelectedItem = s.GetModule<Enova.API.CRM.CRMModule>().Kontrahenci[Zamowienie.Kontrahent.Guid.Value];

                transportComboBox.SelectedIndex = (int)Zamowienie.Transport;
                if (string.IsNullOrEmpty(Zamowienie.NaKiedyTyp))
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    switch (Zamowienie.NaKiedyTyp.ToLower())
                    {
                        case "r":
                            radioButton2.Checked = true;
                            break;
                        case "p":
                            radioButton3.Checked = true;
                            break;
                        case "w":
                            radioButton4.Checked = true;
                            break;
                        default:
                            radioButton1.Checked = true;
                            break;
                    }
                }

            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (Zamowienie.Kontrahent == null)
            {
                MessageBox.Show("Nie wybrano kontrahenta");
                CancelClose = true;
            }
            else if (Zamowienie.RodzajTransportu == Enova.Business.Old.Types.RodzajTransportu.NieWybrano)
            {
                MessageBox.Show("Nie wybrano transportu");
                CancelClose = true;
            }

            if (radioButton2.Checked)
            {
                Zamowienie.NaKiedyTyp = "R";
            }
            else if (radioButton3.Checked)
            {
                Zamowienie.NaKiedyTyp = "P";
            }
            else if (radioButton4.Checked)
            {
                Zamowienie.NaKiedyTyp = "W";
            }
        }

        private void NaglowekZamowieniaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = this.CancelClose;
            this.CancelClose = false;
        }

        private void transportComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Zamowienie.Transport = transportComboBox.SelectedIndex;
        }

        private void kontrahentEnovaSelect_ValueChanged(object sender, EventArgs e)
        {
            var kontrahent = DBWeb.Kontrahent.GetKontrahent(Enova.Business.Old.Core.ContextManager.WebContext, (Enova.API.CRM.Kontrahent)kontrahentEnovaSelect.SelectedItem);
            Zamowienie.Kontrahent = kontrahent;
            if (kontrahent != null)
            {
                Zamowienie.AdresFaktury = kontrahent.DomyslnyAdresFaktury;
                Zamowienie.AdresWysylki = kontrahent.DomyslnyAdresWysylki;
                fireEvents = false;
                terminTextBox.Text = Zamowienie.TerminPlatnosci.ToString();
                terminDataTimePicker.Value = Zamowienie.TerminData;
                fireEvents = true;
            }
        }
    }
}
