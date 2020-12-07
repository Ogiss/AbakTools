using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AbakTools.EnovaApi;
using AbakTools.Framework;
using Microsoft.Reporting.WinForms;
//using global::Enova.Business.Old.DB.Web;
using global::Enova.Business.Old.Types;
using DBWeb = global::Enova.Business.Old.DB.Web;
using DBEnova = global::Enova.Business.Old.DB;

namespace AbakTools.Zamowienia.Forms
{
    public partial class ZamowienieEditForm : global::Enova.Business.Old.Forms.DataEditForm
    {
        private bool cancelClose = false;
        private bool fireEvents = true;
        private AbakTools.Towary.Forms.WyborProduktuForm wyborProduktuForm = null;
        private bool zmienionoStatus = false;
        public bool SpakowanoZamowienie;

        new public global::Enova.Business.Old.DB.EnovaContext DataContext
        {
            get { return (global::Enova.Business.Old.DB.EnovaContext)base.DataContext; }
        }

        public global::Enova.Business.Old.DB.Web.WebContext DbContext
        {
            get
            {
                return global::Enova.Business.Old.Core.ContextManager.WebContext;
            }
        }

        public AbakTools.Towary.Forms.WyborProduktuForm WyborProduktuForm
        {
            get
            {
                if (wyborProduktuForm == null)
                    wyborProduktuForm = new AbakTools.Towary.Forms.WyborProduktuForm();
                return wyborProduktuForm;
            }
        }

        public global::Enova.Business.Old.DB.Web.Zamowienie Zamowienie
        {
            get
            {
                return (global::Enova.Business.Old.DB.Web.Zamowienie)DataSource;
            }
            set
            {
                DataSource = value;
            }
        }

        public ZamowienieEditForm()
        {
            InitializeComponent();
        }

        private bool blokadaEdycji = false;

        private void blokujEdycje()
        {
            blokadaEdycji = true;
            //kontrahentSelectTextBox.Enabled = false;
            pozycjeDataGrid.ReadOnly = true;
            addmessageButton.Enabled = false;
            editMessageButton.Enabled = false;
            delMessageButton.Enabled = false;
            bindingNavigatorAddNewItem.Enabled = false;
            bindingNavigatorDeleteItem.Enabled = false;
            opisToolStripButton.Enabled = false;
            zamFormButton.Enabled = false;
            rozdzielButton.Enabled = false;
        }


        private void ZamowienieEditForm_Load(object sender, EventArgs e)
        {
            if (((DBWeb.Zamowienie)DataSource).EntityState == System.Data.EntityState.Unchanged)
                DbContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, (DBWeb.Zamowienie)DataSource);

            if (Zamowienie != null)
            {
                if (Zamowienie.Kontrahent != null)
                {
                    /*
                    var enova_kontrahent = this.DataContext.Kontrahenci.Where(k => k.Guid == Zamowienie.Kontrahent.Guid).FirstOrDefault();
                    if (enova_kontrahent != null)
                    {
                        this.DataContext.Refresh(RefreshMode.StoreWins, enova_kontrahent);
                        if (enova_kontrahent.Blokada != null && enova_kontrahent.Blokada.Value ||
                            enova_kontrahent.BlokadaSprzedaży != null && enova_kontrahent.BlokadaSprzedaży.Value)
                            MessageBox.Show("UWAGA!!! KLIENT JEST ZABLOKOWANY.", "AbakTools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                     */

                    var service = global::Enova.API.EnovaService.Instance;
                    using (var session = service.CreateSession())
                    {
                        var ekontrahent = session.GetModule<global::Enova.API.CRM.CRMModule>().Kontrahenci[Zamowienie.Kontrahent.Guid.Value];
                        if (ekontrahent != null)
                        {
                            this.kontrahentEnovaSelect.SelectedItem = ekontrahent;
                            if (ekontrahent.Blokada || ekontrahent.BlokadaSprzedazy)
                                MessageBox.Show("UWAGA!!! KLIENT JEST ZABLOKOWANY.", "AbakTools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                }

                if (((global::Enova.Business.Old.DB.Web.Zamowienie)DataSource).EntityState == System.Data.EntityState.Unchanged)
                {
                    DbContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, ((global::Enova.Business.Old.DB.Web.Zamowienie)DataSource).PozycjeZamowienia);
                    DbContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, ((global::Enova.Business.Old.DB.Web.Zamowienie)DataSource).HistoriaZamowienia);
                }

                if (Zamowienie.StatusZamowienia != null && Zamowienie.StatusZamowienia.NoweZamowienie.Value && (Zamowienie.RodzajTransportu == RodzajTransportu.Kurier || Zamowienie.RodzajTransportu == RodzajTransportu.Przedstawiciel))
                    Zamowienie.Transport = (int)RodzajTransportu.NieWybrano;

                if (global::Enova.Business.Old.DB.Web.User.LoginedUser.IsAdmin.Value ||
                    (global::Enova.Business.Old.DB.Web.User.LoginedUser.IsSuperAdmin != null && global::Enova.Business.Old.DB.Web.User.LoginedUser.IsSuperAdmin.Value))
                {
                    pilneCheckBox.Visible = true;
                    blokadaCheckBox.Visible = true;
                }
                else
                {
                    pilneCheckBox.Visible = false;
                    blokadaCheckBox.Visible = false;
                }

                if (Zamowienie.BlokadaEdycji)
                {
                    blokujEdycje();
                    statusButton1.Enabled = false;
                    statusButton2.Enabled = false;
                    statusButton3.Enabled = false;
                    statusButton4.Enabled = false;
                    okButton.Enabled = false;
                    zatwierdzButton.Enabled = false;
                    naKiedyDateTimePicker.Enabled = false;
                }
                else if (Zamowienie.Pakowanie && !IsWarehouseman)
                {
                    MessageBox.Show("Zamówienie w trakcie pakowania, edycja zabroniona.", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    blokujEdycje();
                }
                else if (Zamowienie.Spakowane)
                {
                    blokujEdycje();
                }

                if (!Zamowienie.Spakowane && Zamowienie.RodzajTransportu != RodzajTransportu.DoDostawcy)
                {
                    kopiujButton.Visible = false;
                }

                if (Zamowienie.EntityState == System.Data.EntityState.Detached)
                {
                    Zamowienie.DataDodania = DateTime.Now;
                    Zamowienie.NaKiedy = DateTime.Now;
                    global::Enova.Business.Old.DB.Web.StatusZamowienia status = 
                        DbContext.StatusyZamowien.Where(s => s.NoweZamowienie == true).FirstOrDefault();
                    Zamowienie.HistoriaZamowienia.Add(new DBWeb.HistoriaZamowienia()
                    {
                        DataDodania = DateTime.Now,
                        Operator = LoginedOperator,
                        Status = status

                    });

                }
                fireEvents = false;
                transportTextBox.Text = Zamowienie.RodzajTransportuStr;
                //kontrahentSelectTextBox.SelectedItem = Zamowienie.Kontrahent;


                if (string.IsNullOrEmpty(Zamowienie.NaKiedyTyp))
                {
                    nakiedyN.Checked = true;
                }
                else
                {
                    switch (Zamowienie.NaKiedyTyp.Trim().ToLower())
                    {
                        case "r":
                            nakiedyR.Checked = true;
                            break;
                        case "p":
                            nakiedyP.Checked = true;
                            break;
                        case "w":
                            nakiedyW.Checked = true;
                            break;
                        default:
                            nakiedyN.Checked = true;
                            break;
                    }
                }

                terminTextBox.Text = Zamowienie.TerminPlatnosci.ToString();
                terminDataTimePicker.Value = Zamowienie.TerminData;

                fireEvents = true;

            }

            loadSezony();
            loadPozycje();
            loadWiadomości();
            loadStatusyZamowien();
            loadHistorieZamowienia();
            setStatusButtons();
        }

        private void loadSezony()
        {
            var service = global::Enova.API.EnovaService.Instance;
            sezonyComboBox.Items.Add("---------------");
            sezonDodatkowyComboBox.Items.Add("---------------");
            using (var session = service.CreateSession())
            {
                var bm = session.GetModule<global::Enova.API.Business.BusinessModule>();
                foreach (var s in bm.Dictionary["F.SEZON"].OrderBy(r => r.Value))
                {
                    sezonyComboBox.Items.Add(s.Value);
                    sezonDodatkowyComboBox.Items.Add(s.Value);
                }
                if (this.Zamowienie != null)
                {
                    if (!string.IsNullOrEmpty(this.Zamowienie.Sezon))
                        sezonyComboBox.SelectedItem = this.Zamowienie.Sezon;
                    else
                        sezonyComboBox.SelectedIndex = 0;

                    if (!string.IsNullOrEmpty(this.Zamowienie.SezonDodatkowy))
                        sezonDodatkowyComboBox.SelectedItem = this.Zamowienie.SezonDodatkowy;
                    else
                        sezonDodatkowyComboBox.SelectedIndex = 0;
                }
            }
        }

        private void loadPozycje()
        {
            loadPozycje(true);
        }

        private void loadPozycje(bool refresh)
        {
            if (DataSource != null)
            {
                if (Zamowienie.EntityState != System.Data.EntityState.Added && Zamowienie.EntityState != System.Data.EntityState.Detached)
                    Zamowienie.PozycjeZamowienia.Load();

                pozycjeBindingSource.DataSource = ((global::Enova.Business.Old.DB.Web.Zamowienie)DataSource).PozycjeZamowienia
                    .Where(p => p.Synchronizacja != (int)global::Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedDelete && p.Ilosc > 0).OrderBy(p => p.Ident).ToList();
                Zamowienie.PrzeliczZamowienie();
            }
        }


        private void loadWiadomości()
        {
            if (Zamowienie != null)
            {
                if (Zamowienie.EntityState != System.Data.EntityState.Added && Zamowienie.EntityState != System.Data.EntityState.Detached)
                    Zamowienie.Wiadomosci.Load();
                wiadomosciListBox.Items.Clear();
                foreach (DBWeb.Wiadomosc msg in Zamowienie.Wiadomosci.Where(w => w.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && w.Tekst != null && w.Tekst != "").OrderByDescending(m => m.Stamp))
                {
                    wiadomosciListBox.Items.Add(msg);
                }
            }
        }

        private void loadStatusyZamowien()
        {

            statusyBindingSource.DataSource = DbContext.StatusyZamowien.OrderBy(s => s.ID).ToList();
        }

        private void loadHistorieZamowienia()
        {
            if (Zamowienie != null)
            {
                if (Zamowienie.EntityState != System.Data.EntityState.Added && Zamowienie.EntityState != System.Data.EntityState.Detached)
                {
                    Zamowienie.HistoriaZamowienia.Load();
                }

                historiaListBox.Items.Clear();
                foreach (global::Enova.Business.Old.DB.Web.HistoriaZamowienia hist in Zamowienie.HistoriaZamowienia.Where(h => h.EntityState != System.Data.EntityState.Deleted
                    && h.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && h.Deleted == false).OrderByDescending(h => h.DataDodania))
                {
                    historiaListBox.Items.Add(hist.ToString());
                }
            }
        }

        protected override bool CancelValid()
        {
            if (Zamowienie.EntityState == System.Data.EntityState.Added || Zamowienie.Synchronizacja == (int)global::Enova.Business.Old.Types.RowSynchronizeOld.Notsaved)
            {
                DialogResult result = MessageBox.Show("Czy napewno chcesz zrezygnować z zamówienia?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                    return false;
            }
            return true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            drukujZamowienie(false);
        }

        private void drukujZamowienie(bool modal, bool wCenachZakupu = false, string fileName = null)
        {
            if (SaveChanges())
            {
                bool? preview = DbContext.GetConfigBool("PREVIEW_ORDER", global::Enova.Business.Old.DB.Web.User.LoginedUser);

                var query = DbContext.PozycjeZamowieniaView
                    .Where(p => p.ZamowienieID == Zamowienie.ID).OrderBy(p => p.Ident).ThenBy(p => p.ProduktKod).ThenBy(p => p.ProduktNazwa).ThenBy(p => p.AtrybutNazwa);

                var pozycje = query.ToList();
                if (wCenachZakupu)
                {
                    foreach (var pozycja in pozycje)
                    {
                        if (pozycja.ProductEnovaGuid != null)
                        {
                            var towar = DataContext.Towary.Where(t => t.Guid == pozycja.ProductEnovaGuid).FirstOrDefault();
                            var cena = towar.CenaPodstawowa != null && towar.CenaPodstawowa.NettoValue != null ? towar.CenaPodstawowa.NettoValue.Value : 0;
                            pozycja.Cena = (decimal)cena;
                        }
                    }
                }

                string wiadomosci = "";
                /*
                using (var session = BAL.Business.AppController.Instance.CurrentLogin.CreateSession(true, false, "Zamowienie.Reklamacje"))
                {
                    var kontr = AbakTools.CRM.CRMModule.GetInstance(session).Kontrahenci[this.Zamowienie.Kontrahent.ID];
                    var reklamacje = AbakTools.Handel.HandelModule.GetInstance(session).Reklamacje.WgKontrahent[kontr].WgOpcjiStatusu[Core.OpcjeStatusuDokumentu.Koncowy, true].ToList();
                    if (reklamacje.Count > 0)
                        wiadomosci += "UWAGA ISTNIEJĄ NIEZAŁATWIONE REKLAMACJE DLA TEGO KONTRAHENTA !!!";
                }
                 */
                var opt = (int)Core.OpcjeStatusuDokumentu.Koncowy;
                var status = Zamowienie.DbContext.StatusyDokumentow.Where(r => r.Deleted == false && r.Kategoria == "Reklamacje" && (r.OpcjeInt & opt) == opt).FirstOrDefault();
                if (status != null)
                {
                    if (Zamowienie.DbContext.Reklamacje.Where(r => r.Kontrahent.ID == Zamowienie.Kontrahent.ID && r.OstatniStatusID != null &&
                        r.OstatniStatusID != status.ID && r.Deleted == false).Count() > 0)
                        wiadomosci += "UWAGA ISTNIEJĄ NIEZAŁATWIONE REKLAMACJE DLA TEGO KONTRAHENTA !!!";
                }



                if (wiadomosci != "")
                    wiadomosci += "\r\n";
                wiadomosci += Zamowienie.WiadomosciText;

                if (fileName == null && (preview == null || preview.Value))
                {
                    AbakTools.Printer.ReportForm form = new AbakTools.Printer.ReportForm() { Text = "Zamówienie" };
                    form.ReportViewer.LocalReport.ReportPath = "Reports\\ZamowienieReport.rdlc";


                    form.ReportViewer.LocalReport.SetParameters(new List<ReportParameter>()
                    {
                        new ReportParameter("numer",Zamowienie.NumerPelny),
                        new ReportParameter("data",Zamowienie.DataDodania.Value.ToShortDateString()),
                        new ReportParameter("przedstawiciel",Zamowienie.PrzedstawicielKod),
                        new ReportParameter("kontrahentkod",Zamowienie.KontrahentKod),
                        new ReportParameter("adresfaktury",Zamowienie.AdresFakturyText+"\r\nNIP: "+Zamowienie.Kontrahent.Nip),
                        new ReportParameter("adreswysylki",Zamowienie.AdresWysylkiText),
                        new ReportParameter("transport",Zamowienie.Transport.ToString()),
                        new ReportParameter("drukujtermin",Zamowienie.DrukujTermin.ToString()),
                        new ReportParameter("termin",Zamowienie.TerminPlatnosci.ToString()),
                        new ReportParameter("termindata",Zamowienie.TerminData.ToShortDateString()),
                        new ReportParameter("wiadomosci", wiadomosci /*Zamowienie.WiadomosciText*/)
                    });

                    form.reportViewer.LocalReport.DataSources.Clear();
                    form.reportViewer.LocalReport.DataSources.Add(
                         new ReportDataSource("PozycjeZamowieniaView", pozycje)
                         );

                    if (modal)
                        form.ShowDialog();
                    else
                        form.Show();
                }
                else
                {
                    using (AbakTools.Printer.PrintingService ps = new AbakTools.Printer.PrintingService())
                    {
                        ps.ReportPath = "Reports\\ZamowienieReport.rdlc";
                        //ps.DataSources.Add(new ReportDataSource("Zamowienie", new List<global::Enova.Business.Old.DB.Web.Zamowienie>() { Zamowienie }));
                        ps.SetParameters(new List<ReportParameter>()
                    {
                        new ReportParameter("numer",Zamowienie.NumerPelny),
                        new ReportParameter("data",Zamowienie.DataDodania.Value.ToShortDateString()),
                        new ReportParameter("przedstawiciel",Zamowienie.PrzedstawicielKod),
                        new ReportParameter("kontrahentkod",Zamowienie.KontrahentKod),
                        new ReportParameter("adresfaktury",Zamowienie.AdresFakturyText),
                        new ReportParameter("adreswysylki",Zamowienie.AdresWysylkiText),
                        new ReportParameter("transport",Zamowienie.Transport.ToString()),
                        new ReportParameter("drukujtermin",Zamowienie.DrukujTermin.ToString()),
                        new ReportParameter("termin",Zamowienie.TerminPlatnosci.ToString()),
                        new ReportParameter("termindata",Zamowienie.TerminData.ToShortDateString()),
                        new ReportParameter("wiadomosci", wiadomosci /*Zamowienie.WiadomosciText*/)
                    });
                        ps.DataSources.Add(new ReportDataSource("PozycjeZamowieniaView", pozycje));
                        if (fileName != null)
                        {
                            ps.Export("PDF", fileName);
                        }
                        else
                            ps.Print();
                    }
                }
            }
        }

        private void pozycjeDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

            DBWeb.PozycjaZamowienia pozycja = (DBWeb.PozycjaZamowienia)pozycjeBindingSource.Current;
            if (pozycja != null)
            {
                DialogResult result = MessageBox.Show("Czy napewno chcesz usunąć pozycję?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    ((global::Enova.Business.Old.Core.IDeleteRecord)pozycja).DeleteRecord();
                    pozycjeBindingSource.Remove(pozycja);
                    Zamowienie.PrzeliczZamowienie();
                }
            }

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            AbakTools.Towary.Forms.WyborProduktuForm form = WyborProduktuForm;
            form.ShowDialog();
            if (form.SelectedItem != null)
            {
                //DBWeb.ProduktAtrybut produktAtrybut = null;
                DBWeb.TowarAtrybut towarAtrybut = null;
                DBWeb.Produkt produkt = null;
                DBWeb.AtrybutProduktu atrybut = null;

                /*
                if (form.SelectedItem is DBWeb.ProduktAtrybut)
                {
                    produktAtrybut = (DBWeb.ProduktAtrybut)form.SelectedItem;
                    produkt = produktAtrybut.Produkt;
                    atrybut = produktAtrybut.AtrybutProduktu;
                }
                 */
                if (form.SelectedItem is DBWeb.TowarAtrybut)
                {
                    towarAtrybut = (DBWeb.TowarAtrybut)form.SelectedItem;
                    produkt = towarAtrybut.Towar;
                    atrybut = towarAtrybut.AtrybutTowaru;
                }
                else if (form.SelectedItem is DBWeb.Produkt)
                {
                    produkt = (DBWeb.Produkt)form.SelectedItem;
                }
                int ilosc = 1;

                if (produkt.JednostkaMiary != null)
                {
                    ilosc = (int)produkt.JednostkaMiary.Mnoznik;
                }

                //DBWeb.Kontrahent kontrahent = (DBWeb.Kontrahent)kontrahentSelectTextBox.SelectedItem;
                DBWeb.Kontrahent kontrahent = Zamowienie.Kontrahent;
                decimal rabat = kontrahent != null ? kontrahent.GetRabat(produkt) : 0;

                if (produkt.GetRozdzielProduktNaZamowieniu(DataContext))
                {
                    var els = DataContext.ElemKompletow.Where(el => el.Komplet.Guid == produkt.EnovaGuid && el.Towar.Guid != produkt.EnovaGuid).ToList();

                    foreach (var el in els)
                    {
                        produkt = (DBWeb.Produkt)el.Towar;

                        if (produkt != null)
                        {
                            DBWeb.PozycjaZamowienia pozycja = CreatePozycjaZamowienia(produkt, null, el.IloscValue, rabat);
                            this.Zamowienie.PozycjeZamowienia.Add(pozycja);

                        }
                    }
                }

                else
                {
                    /*
                    DBWeb.PozycjaZamowienia pozycja = new DBWeb.PozycjaZamowienia()
                    {
                        GUID = Guid.NewGuid(),
                        ProduktIndywidualny = false,
                        Rabat = rabat,
                        ZmienionoRabat = false,
                        Ilosc = ilosc,
                        IloscOrg = ilosc,
                        Cena = produkt.Cena,
                        Stamp = DateTime.Now,
                        StawkaVatSymbol = produkt.StawkaVat.Nazwa,
                        StawkaVatValue = produkt.StawkaVat.Procent,
                        PSID = 0,
                        Synchronizacja = (int)global::Enova.Business.Old.Types.RowSynchronize.NotsynchronizedNew,
                        Ident = Zamowienie.MaxIdent == null ? 1 : Zamowienie.MaxIdent + 1,
                    };
                     */

                    DBWeb.PozycjaZamowienia pozycja = CreatePozycjaZamowienia(produkt, atrybut, ilosc, rabat);

                    //pozycja.Produkt = produkt;
                    //pozycja.AtrybutProduktu = atrybut;
                    this.Zamowienie.PozycjeZamowienia.Add(pozycja);
                }

                loadPozycje(false);
                pozycjeDataGrid.Focus();
                scrollToLastPosition();

            }
        }

        private DBWeb.PozycjaZamowienia CreatePozycjaZamowienia(DBWeb.Produkt produkt, DBWeb.AtrybutProduktu atrybut, double ilosc, decimal? rabat = null)
        {
            DBWeb.PozycjaZamowienia pozycja = new DBWeb.PozycjaZamowienia()
            {
                GUID = Guid.NewGuid(),
                ProduktIndywidualny = false,
                Rabat = rabat,
                ZmienionoRabat = false,
                Ilosc = ilosc,
                IloscOrg = ilosc,
                Cena = produkt.Cena,
                Stamp = DateTime.Now,
                StawkaVatSymbol = produkt.StawkaVat.Nazwa,
                StawkaVatValue = produkt.StawkaVat.Procent,
                PSID = 0,
                Synchronizacja = (int)global::Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew,
                Ident = Zamowienie.MaxIdent == null ? 1 : Zamowienie.MaxIdent + 1,
            };

            pozycja.Produkt = produkt;
            pozycja.AtrybutProduktu = atrybut;

            return pozycja;
        }



        private void scrollToLastPosition()
        {
            pozycjeBindingSource.Position = pozycjeDataGrid.RowCount - 2;
            pozycjeBindingSource.Position = pozycjeDataGrid.RowCount - 1;
            pozycjeDataGrid.CurrentCell = pozycjeDataGrid.CurrentRow.Cells[2];
        }

        private void pozycjeDataGrid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void pozycjeDataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 0 && string.IsNullOrEmpty((string)e.FormattedValue))
            {
                e.Cancel = true;
                new AbakTools.Towary.Forms.WyborProduktuForm().ShowDialog();
            }

        }

        private void pozycjeDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!blokadaEdycji)
            {
                if (e.Alt == false && e.Control == false && e.Shift == false)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Delete:
                            bindingNavigatorDeleteItem_Click(null, null);
                            break;
                        case Keys.Down:
                            if (pozycjeDataGrid.CurrentRow != null && pozycjeDataGrid.CurrentRow.Index == pozycjeDataGrid.Rows.Count - 1)
                            {
                                bindingNavigatorAddNewItem_Click(null, null);
                                e.Handled = true;
                            }
                            break;
                    }
                }
                else if (e.Control == true && e.Alt == false && e.Shift == false)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.O:
                            opisToolStripButton_Click(null, null);
                            break;
                    }
                }
            }
        }

        private void ZamowienieEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == false && e.Control == false && e.Shift == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.Insert:
                        if (!blokadaEdycji)
                            bindingNavigatorAddNewItem_Click(null, null);
                        break;
                }
            }
            else if (e.Alt == false && e.Control == true && e.Shift == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.P:
                        printToolStripButton_Click(null, null);
                        break;
                }
            }
        }

        private void ZamowienieEditForm_Shown(object sender, EventArgs e)
        {
            /*
            if (pozycjeDataGrid.Rows.Count > 0)
            {
                pozycjeDataGrid.CurrentCell = pozycjeDataGrid.Rows[0].Cells[0];
                pozycjeDataGrid.Rows[0].Selected = true;
            }
            pozycjeDataGrid.Focus();
             */
            //kontrahentSelectTextBox.Focus();
            kontrahentEnovaSelect.Focus();

        }


        private void ZamowienieEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cancelClose)
            {
                e.Cancel = true;
                cancelClose = false;
            }
        }

        private void addmessageButton_Click(object sender, EventArgs e)
        {
            AddMessageForm form = new AddMessageForm();
            form.DataSource = new DBWeb.Wiadomosc()
            {
                GUID = Guid.NewGuid(),
                Prywatna = false,
                Stamp = DateTime.Now,
                Synchronizacja = (int)global::Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                Zamowienie.Wiadomosci.Add(form.DataSource);
                loadWiadomości();
            }
        }

        private void editMessageButton_Click(object sender, EventArgs e)
        {
            DBWeb.Wiadomosc wiadomosc = (DBWeb.Wiadomosc)wiadomosciListBox.SelectedItem;
            if (wiadomosc != null)
            {
                DialogResult result = new AddMessageForm()
                {
                    DataSource = wiadomosc
                }.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (wiadomosc.Synchronizacja == (int)RowSynchronizeOld.Synchronized)
                        wiadomosc.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                    loadWiadomości();
                }
            }
        }

        private void delMessageButton_Click(object sender, EventArgs e)
        {
            DBWeb.Wiadomosc wiadomosc = (DBWeb.Wiadomosc)wiadomosciListBox.SelectedItem;
            if (wiadomosc != null)
            {
                DialogResult result = MessageBox.Show("Czy napedno chcesz usunąć wiadomość", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    wiadomosc.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                    loadWiadomości();
                }
            }
        }



        private void opisToolStripButton_Click(object sender, EventArgs e)
        {
            if (pozycjeDataGrid.CurrentRow != null)
            {
                DBWeb.PozycjaZamowienia pozycja = (DBWeb.PozycjaZamowienia)pozycjeDataGrid.CurrentRow.DataBoundItem;
                OpisPozycjiForm form = new OpisPozycjiForm() { Opis = pozycja.Opis };
                DialogResult result = form.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    pozycja.Opis = form.Opis;
                    pozycjeDataGrid.Refresh();
                }

            }
        }

        private void pozycjeDataGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
        }

        private bool IsAdmin
        {
            get
            {
                return global::Enova.Business.Old.DB.Web.User.LoginedUser.IsAdmin.Value;
            }
        }

        private bool IsSuperAdmin
        {
            get
            {
                return global::Enova.Business.Old.DB.Web.User.LoginedUser.IsSuperAdmin.Value;
            }
        }

        private bool IsWarehouseman
        {
            get
            {
                return global::Enova.Business.Old.DB.Web.User.LoginedUser.IsWarehouseman.Value;
            }
        }

        private bool IsAgetOrder
        {
            get
            {
                return this.Zamowienie != null && this.Zamowienie.Kontrahent != null && DBWeb.User.LoginedUser.CheckAgent(this.Zamowienie.PrzedstawicielKod);
            }
        }

        private DBWeb.Operator LoginedOperator
        {
            get { return DBWeb.Operator.CurrentOperator.GetOperator(DbContext); }
        }

        private void setStatusButtons()
        {
            if (Zamowienie.StatusZamowienia == null || (Zamowienie.StatusZamowienia.NoweZamowienie.Value && Zamowienie.RodzajTransportu != RodzajTransportu.DoDostawcy))
            {
                DBWeb.HistoriaZamowienia hist = Zamowienie.OstatniaHistoriaZamowienia;
                if (IsAdmin || IsWarehouseman || Zamowienie.ZamPrzedstawiciela || (hist?.Operator != null) ||
                    IsAgetOrder)
                {
                    statusButton1.Text = "Wstrzymane";
                    statusButton2.Text = "Kurier";
                    statusButton3.Text = "Przedstawiciel";
                    statusButton4.Text = "Do dostawcy";
                    statusButton1.Visible = true;
                    statusButton2.Visible = true;
                    statusButton3.Visible = true;
                    statusButton4.Visible = true;
                    return;
                }
            }
            else if (Zamowienie.StatusZamowienia.NoweZamowienie.Value && Zamowienie.RodzajTransportu == RodzajTransportu.DoDostawcy)
            {
                if (IsAdmin || IsWarehouseman || IsAgetOrder)
                {
                    statusButton1.Text = "U dostawcy";
                    statusButton1.Visible = true;
                    statusButton2.Text = "Cofnij";
                    statusButton2.Visible = true;
                }
                statusButton3.Visible = false;
                statusButton4.Visible = false;
                return;
            }
            else if (Zamowienie.StatusZamowienia.DoMagazynu.Value)
            {
                if (IsAdmin || IsWarehouseman || IsAgetOrder)
                {
                    statusButton1.Text = "Pakuj";
                    statusButton1.Visible = true;
                    statusButton2.Text = "Cofnij";
                    statusButton2.Visible = true;
                    statusButton3.Text = "Blokuj";
                    statusButton3.Visible = true;
                    statusButton4.Text = "Wstrzymaj";
                    statusButton4.Visible = true;
                    return;
                }
                else if (LoginedOperator != null && Zamowienie.OstatniaHistoriaZamowienia.Operator != null && 
                    LoginedOperator.ID == Zamowienie.OstatniaHistoriaZamowienia.Operator.ID)
                {
                    statusButton1.Visible = false;
                    statusButton2.Text = "Cofnij";
                    statusButton2.Visible = true;
                    statusButton3.Visible = false;
                    statusButton4.Text = "Wstrzymaj";
                    statusButton4.Visible = true;
                    return;
                }
            }
            else if (Zamowienie.StatusZamowienia.DoDostawcy.Value)
            {
                if (IsAdmin || IsWarehouseman)
                {
                    statusButton1.Text = "Załatwione";
                    statusButton1.Visible = true;
                    statusButton2.Text = "Cofnij";
                    statusButton2.Visible = true;
                    statusButton3.Text = "Anuluj";
                    statusButton3.Visible = true; ;
                }

                statusButton4.Visible = false;
                return;
            }
            else if (Zamowienie.StatusZamowienia.Pakowanie.Value)
            {
                if (IsAdmin || IsWarehouseman)
                {
                    statusButton1.Text = "Spakowane";
                    statusButton1.Visible = true;
                    statusButton2.Text = "Wstrzymaj";
                    statusButton2.Visible = true;
                    statusButton3.Text = "Cofnij";
                    statusButton3.Visible = true;
                    statusButton4.Text = "Blokuj";
                    statusButton4.Visible = true;
                }
                return;
            }
            else if (Zamowienie.StatusZamowienia.Spakowane.Value)
            {
                if (IsAdmin || IsWarehouseman)
                {
                    statusButton1.Text = "Gotowe";
                    statusButton1.Visible = true;
                    statusButton2.Text = "Cofnij";
                    statusButton2.Visible = true;
                    statusButton3.Text = "Pakowacz";
                    statusButton3.Visible = true;
                    statusButton4.Visible = false;
                }
                return;
            }
            else if (Zamowienie.StatusZamowienia.Kurier.Value || Zamowienie.StatusZamowienia.Przedstawiciel.Value)
            {
                if (IsAdmin || IsWarehouseman)
                {
                    statusButton1.Text = "Wysłane";
                    statusButton1.Visible = true;
                    statusButton2.Text = " Cofnij";
                    statusButton2.Visible = true;
                }
                statusButton3.Visible = false;
                statusButton4.Visible = false;
                return;
            }
            else if (Zamowienie.StatusZamowienia.Blokada.Value)
            {
                if (IsAdmin || IsWarehouseman)
                {
                    statusButton1.Text = "Odblokuj";
                    statusButton1.Visible = true;
                    statusButton2.Text = "Anuluj";
                    statusButton2.Visible = true;
                }
                statusButton3.Visible = false;
                statusButton4.Visible = false;
                return;
            }
            else if (Zamowienie.StatusZamowienia.Anulowane.Value || Zamowienie.StatusZamowienia.Wysłane.Value)
            {
                if (IsAdmin || IsWarehouseman)
                {
                    statusButton1.Text = "Cofnij";
                    statusButton1.Visible = true;
                }
                statusButton2.Visible = false;
                statusButton3.Visible = false;
                statusButton4.Visible = false;
                return;
            }
            else if (Zamowienie.StatusZamowienia.Wstrzymane.Value)
            {
                if (IsAdmin || IsWarehouseman || Zamowienie.StatusZmUzytkownik)
                {
                    statusButton1.Text = "Cofnij";
                    statusButton1.Visible = true;
                }
                statusButton2.Visible = false;
                statusButton3.Visible = false;
                statusButton4.Visible = false;
                return;
            }
            statusButton1.Visible = false;
            statusButton2.Visible = false;
            statusButton3.Visible = false;
            statusButton4.Visible = false;
        }

        private void cofnijStatus()
        {
            if (MessageBox.Show("Czy napewno chcesz cofnąć status zamówienia?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                DBWeb.HistoriaZamowienia hist = Zamowienie.OstatniaHistoriaZamowienia;
                if (hist != null)
                {
                    hist.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                    hist.Deleted = true;
                    loadHistorieZamowienia();
                    setStatusButtons();
                    zmienionoStatus = true;
                }
            }
        }


        private void statusButton1_Click(object sender, EventArgs e)
        {
            DBWeb.Operator @operator = LoginedOperator;
            DBWeb.StatusZamowienia status = null;
            if (Zamowienie.StatusZamowienia == null || (Zamowienie.StatusZamowienia.NoweZamowienie.Value && 
                Zamowienie.RodzajTransportu == RodzajTransportu.NieWybrano))
            {
                status = DbContext.StatusyZamowien
                    .Where(s => s.Wstrzymane.Value == true).FirstOrDefault();
            }
            else if (Zamowienie.StatusZamowienia.NoweZamowienie.Value && Zamowienie.RodzajTransportu == RodzajTransportu.DoDostawcy)
            {
                status = DbContext.StatusyZamowien
                    .Where(s => s.DoDostawcy.Value == true).FirstOrDefault();

            }
            else if (Zamowienie.StatusZamowienia.Wstrzymane.Value)
            {

                if (LoginedOperator != null)
                {
                    var hist = Zamowienie.OstatniaHistoriaZamowienia;
                    if (hist.Operator == null || hist.Operator.ID == LoginedOperator.ID || IsSuperAdmin)
                    {
                        cofnijStatus();
                    }
                    else
                    {
                        MessageBox.Show("Status \"Wstrzymane\" może cofnąć tylko użytkownik, który go ustawił!!!", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (Zamowienie.StatusZamowienia.DoMagazynu.Value)
            {
                var form = new WyborMagazynieraForm();
                DialogResult result = form.ShowDialog();

                if (result == DialogResult.OK && form.User != null)
                {
                    @operator = DBWeb.User.GetOperator(form.User);

                    status = DbContext.StatusyZamowien
                        .Where(s => s.Pakowanie.Value == true).FirstOrDefault();
                    if (@operator != null)
                        Zamowienie.Pakowacz = @operator.Nazwa;
                }
            }
            else if (Zamowienie.StatusZamowienia.Pakowanie.Value)
            {
                status = DbContext.StatusyZamowien
                    .Where(s => s.Spakowane.Value == true).FirstOrDefault();
                DBWeb.HistoriaZamowienia hist = Zamowienie.OstatniaHistoriaZamowienia;
                if (hist == null)
                {
                    var form = new WyborMagazynieraForm();
                    DialogResult result = form.ShowDialog();

                    if (result == DialogResult.OK && form.User != null)
                    {
                        @operator = DBWeb.User.GetOperator(form.User);
                        status = DbContext.StatusyZamowien
                            .Where(s => s.Spakowane.Value == true).FirstOrDefault();
                        if (@operator != null)
                            Zamowienie.Pakowacz = @operator.Nazwa;
                    }
                }
                else
                {
                    @operator = hist.Operator;
                }
                kopiujButton.Visible = true;

            }
            else if (Zamowienie.StatusZamowienia.Spakowane.Value || Zamowienie.StatusZamowienia.DoDostawcy.Value)
            {
                if (Zamowienie.BrakiDoZamowienia != null && (bool)Zamowienie.BrakiDoZamowienia)
                {
                    Zamowienie.FakturaNumer = "BRAKI";
                    if (Zamowienie.RodzajTransportu == RodzajTransportu.Kurier)
                    {
                        status = DbContext.StatusyZamowien
                            .Where(s => s.Kurier.Value == true).FirstOrDefault();
                    }
                    else if (Zamowienie.RodzajTransportu == RodzajTransportu.Przedstawiciel)
                    {
                        status = DbContext.StatusyZamowien
                            .Where(s => s.Przedstawiciel.Value == true).FirstOrDefault();
                    }
                    else if (Zamowienie.RodzajTransportu == RodzajTransportu.DoDostawcy)
                    {
                        status = DbContext.StatusyZamowien
                            .Where(s => s.Wysłane == true).FirstOrDefault();
                    }

                }
                else
                {
                    if (!this.Zamowienie.KopiowanoPozycje)
                    {
                        DialogResult result = MessageBox.Show("Z tego zamówienia nie kopiowano pozycji. Czy napewno chcesz zmienić status na \"GOTOWE\"",
                            "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (result == System.Windows.Forms.DialogResult.No)
                            return;
                    }

                    WyborFakturyForm form = new WyborFakturyForm() { Zamowienie = Zamowienie };

                    form.ShowDialog();
                    if (form.Faktura != null)
                    {
                        Zamowienie.FakturaGuid = form.Faktura.Guid;
                        Zamowienie.FakturaNumer = form.Faktura.NumerPelny;

                        if (Zamowienie.RodzajTransportu == RodzajTransportu.Kurier)
                        {
                            status = DbContext.StatusyZamowien
                                .Where(s => s.Kurier.Value == true).FirstOrDefault();
                        }
                        else if (Zamowienie.RodzajTransportu == RodzajTransportu.Przedstawiciel)
                        {
                            status = DbContext.StatusyZamowien
                                .Where(s => s.Przedstawiciel.Value == true).FirstOrDefault();
                        }
                        else if (Zamowienie.RodzajTransportu == RodzajTransportu.DoDostawcy)
                        {
                            status = DbContext.StatusyZamowien
                                .Where(s => s.Wysłane == true).FirstOrDefault();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nie wybrano faktury", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (Zamowienie.StatusZamowienia.Kurier.Value || Zamowienie.StatusZamowienia.Przedstawiciel.Value)
            {
                status = DbContext.StatusyZamowien
                    .Where(s => s.Wysłane.Value == true).FirstOrDefault();
            }
            else if (Zamowienie.StatusZamowienia.Blokada.Value)
            {
                Zamowienie.HistoriaZamowienia.Load();

                if (Zamowienie.OstatniaHistoriaZamowienia != null && Zamowienie.OstatniaHistoriaZamowienia.Operator != null && 
                    @operator.Nazwa != Zamowienie.OstatniaHistoriaZamowienia.Operator.Nazwa)
                {
                    MessageBox.Show("Nie masz uptawnień do odblokowania tego zamówienia!!!", "EnovaTools");
                    return;
                }
                cofnijStatus();
            }
            //else if (Zamowienie.StatusZamowienia.DoDostawcy.Value)
            //{
            //    status = DbContext.StatusyZamowien
            //        .Where(s => s.Wysłane.Value == true).FirstOrDefault();
            //}
            else if (Zamowienie.StatusZamowienia.Anulowane.Value
                || Zamowienie.StatusZamowienia.Wysłane.Value)
            {
                cofnijStatus();
            }

            if (@operator != null && status != null)
            {
                Zamowienie.ZmienStatus(@operator, status);
                zmienionoStatus = true;
                loadHistorieZamowienia();
                setStatusButtons();
                if (status.Pakowanie.Value)
                {
                    DialogResult result = MessageBox.Show("Czy chcesz wydrukować zamówienie?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                        drukujZamowienie(true);
                    this.ProgressOk();
                }
                else if (status.Spakowane.Value)
                {
                    this.SpakowanoZamowienie = true;
                    this.ProgressOk();
                }
            }

        }

        protected override void OnAfterSaveChanges(EventArgs e)
        {
            base.OnAfterSaveChanges(e);
            //this.Zamowienie.AktualizujStanMagazynu();
        }

        private void statusButton2_Click(object sender, EventArgs e)
        {
            DBWeb.Operator @operator = LoginedOperator;
            DBWeb.StatusZamowienia status = null;
            if (Zamowienie.StatusZamowienia == null || (Zamowienie.StatusZamowienia.NoweZamowienie.Value && Zamowienie.RodzajTransportu == RodzajTransportu.NieWybrano))
            {
                status = DbContext.StatusyZamowien
                    .Where(s => s.DoMagazynu.Value == true).FirstOrDefault();
                Zamowienie.RodzajTransportu = RodzajTransportu.Kurier;
                transportTextBox.Text = Zamowienie.RodzajTransportuStr;
            }
            else if (Zamowienie.StatusZamowienia.NoweZamowienie.Value && Zamowienie.RodzajTransportu == RodzajTransportu.DoDostawcy)
            {
                Zamowienie.RodzajTransportu = RodzajTransportu.NieWybrano;
                transportTextBox.Text = Zamowienie.RodzajTransportuStr;
                setStatusButtons();
            }
            else if (Zamowienie.StatusZamowienia.DoMagazynu.Value)
            {
                Zamowienie.RodzajTransportu = RodzajTransportu.NieWybrano;
                transportTextBox.Text = Zamowienie.RodzajTransportuStr;
                cofnijStatus();
            }
            else if (Zamowienie.StatusZamowienia.Pakowanie.Value)
            {
                status = DbContext.StatusyZamowien
                    .Where(s => s.Wstrzymane.Value == true).FirstOrDefault();
            }
            else if (Zamowienie.StatusZamowienia.Spakowane.Value)
            {
                cofnijStatus();
            }
            else if (Zamowienie.StatusZamowienia.Kurier.Value || Zamowienie.StatusZamowienia.Przedstawiciel.Value ||
                Zamowienie.StatusZamowienia.DoDostawcy.Value)
            {
                cofnijStatus();
            }
            else if (Zamowienie.StatusZamowienia.Blokada.Value)
            {
                /*
                status = DbContext.StatusyZamowien
                    .Where(s => s.Anulowane.Value == true).FirstOrDefault();
                 */
                cofnijStatus();
            }

            if (@operator != null && status != null)
            {
                Zamowienie.ZmienStatus(@operator, status);
                zmienionoStatus = true;
                loadHistorieZamowienia();
                setStatusButtons();
            }

        }

        private void statusButton3_Click(object sender, EventArgs e)
        {
            DBWeb.Operator @operator = LoginedOperator;
            DBWeb.StatusZamowienia status = null;
            if (Zamowienie.StatusZamowienia == null || Zamowienie.StatusZamowienia.NoweZamowienie.Value)
            {
                status = DbContext.StatusyZamowien
                    .Where(s => s.DoMagazynu.Value == true).FirstOrDefault();
                Zamowienie.RodzajTransportu = RodzajTransportu.Przedstawiciel;
                transportTextBox.Text = Zamowienie.RodzajTransportuStr;

            }
            else if (Zamowienie.StatusZamowienia.DoMagazynu.Value)
            {
                status = DbContext.StatusyZamowien
                    .Where(s => s.Blokada.Value == true).FirstOrDefault();
            }
            else if (Zamowienie.StatusZamowienia.Pakowanie.Value)
            {
                cofnijStatus();
            }
            else if (Zamowienie.StatusZamowienia.Spakowane.Value)
            {
                var form = new WyborMagazynieraForm();
                DialogResult result = form.ShowDialog();

                if (result == DialogResult.OK && form.User != null)
                {
                    @operator = DBWeb.User.GetOperator(form.User);
                    DBWeb.HistoriaZamowienia hist = Zamowienie.OstatniaHistoriaZamowienia;
                    if (hist != null)
                    {
                        hist.Operator = @operator;
                        loadHistorieZamowienia();
                    }
                }
            }
            else if (Zamowienie.StatusZamowienia.DoDostawcy.Value)
            {
                status = DbContext.StatusyZamowien
                    .Where(s => s.Anulowane.Value == true).FirstOrDefault();
            }

            if (@operator != null && status != null)
            {
                Zamowienie.ZmienStatus(@operator, status);
                zmienionoStatus = true;
                loadHistorieZamowienia();
                setStatusButtons();
            }

        }

        private void statusButton4_Click(object sender, EventArgs e)
        {
            DBWeb.Operator @operator = LoginedOperator;
            DBWeb.StatusZamowienia status = null;

            if (Zamowienie.StatusZamowienia == null || Zamowienie.StatusZamowienia.NoweZamowienie.Value)
            {
                Zamowienie.RodzajTransportu = RodzajTransportu.DoDostawcy;
                transportTextBox.Text = Zamowienie.RodzajTransportuStr;

                if (Zamowienie.StatusZamowienia == null)
                    status = DbContext.StatusyZamowien
                        .Where(s => s.NoweZamowienie.Value == true).FirstOrDefault();
                else
                    setStatusButtons();
            }
            else if (Zamowienie.StatusZamowienia.DoMagazynu.Value)
            {
                status = DbContext.StatusyZamowien
                    .Where(s => s.Wstrzymane.Value == true).FirstOrDefault();
            }
            else if (Zamowienie.StatusZamowienia.Blokada.Value)
            {
                status = DbContext.StatusyZamowien
                    .Where(s => s.Blokada.Value == true).FirstOrDefault();
            }
            else if (Zamowienie.StatusZamowienia.Spakowane.Value)
            {

            }

            if (@operator != null && status != null)
            {
                Zamowienie.ZmienStatus(@operator, status);
                zmienionoStatus = true;
                loadHistorieZamowienia();
                setStatusButtons();
            }

        }

        private void kopiujButton_Click(object sender, EventArgs e)
        {
            this.SaveChanges();
            if (Zamowienie.ZamPrzedstawiciela && Zamowienie.Kontrahent.Guid == null)
            {
                MessageBox.Show("Zamówienie stworzone przez przedstawiciela.\r\nMusisz wybrać kontrahenta!!!");
                return;
            }
            //string pozycjeText = Zamowienie.GetTextPozycjeOld();
            string pozycjeText = Zamowienie.GetTextPozycje();
            if (!string.IsNullOrEmpty(pozycjeText))
            {
                Clipboard.Clear();
                Clipboard.SetText(pozycjeText);
                this.Zamowienie.KopiowanoPozycje = true;
                this.SaveChanges();
                DbContext.Refresh(RefreshMode.StoreWins, this.Zamowienie);
                MessageBox.Show("POZYCJE ZOSTAŁY SKOPIOWANE", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void pozycjeDataGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < pozycjeDataGrid.Rows.Count)
            {
                DBWeb.PozycjaZamowienia pozycja = (DBWeb.PozycjaZamowienia)pozycjeDataGrid.Rows[e.RowIndex].DataBoundItem;
                if (pozycja.ProduktIndywidualny != null && pozycja.ProduktIndywidualny.Value)
                {
                    pozycjeDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                }
                if (pozycja.ZmienionoRabat != null && pozycja.ZmienionoRabat.Value)
                {
                    var cell = this.pozycjeDataGrid.Rows[e.RowIndex].Cells[RabatColumn.Index];
                    cell.Style = new DataGridViewCellStyle() { ForeColor = Color.Red };
                }
            }

            /*
            if (e.RowIndex > -1 && e.RowIndex < pozycjeDataGrid.Rows.Count)
            {
                PozycjaZamowienia pozycja = (PozycjaZamowienia)pozycjeDataGrid.Rows[e.RowIndex].DataBoundItem;
                if (!string.IsNullOrEmpty(pozycja.Opis))
                    pozycjeDataGrid.Rows[e.RowIndex].Height = 44;


            }
             */
        }

        private void nakiedyN_CheckedChanged(object sender, EventArgs e)
        {
            Zamowienie.NaKiedyTyp = null;
        }

        private void nakiedyR_CheckedChanged(object sender, EventArgs e)
        {
            Zamowienie.NaKiedyTyp = "R";
        }

        private void nakiedyP_CheckedChanged(object sender, EventArgs e)
        {
            Zamowienie.NaKiedyTyp = "P";
        }

        private void nakiedyW_CheckedChanged(object sender, EventArgs e)
        {
            Zamowienie.NaKiedyTyp = "W";
        }

        private void zamFormButton_Click(object sender, EventArgs e)
        {
            ZamowienieFormularzForm form = new ZamowienieFormularzForm();
            //if (kontrahentSelectTextBox.SelectedItem != null)
            if (Zamowienie.Kontrahent != null)
            {
                //global::Enova.Business.Old.DB.Web.Kontrahent kontrahent = (global::Enova.Business.Old.DB.Web.Kontrahent)kontrahentSelectTextBox.SelectedItem;
                global::Enova.Business.Old.DB.Web.Kontrahent kontrahent = Zamowienie.Kontrahent;
                form.KontrahentId = global::Enova.Business.Old.Core.ContextManager.DataContext.Kontrahenci.Where(k => k.Guid == kontrahent.Guid).Select(k => k.ID).FirstOrDefault();
            }
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK && form.DataSource != null)
            {
                var pozycje = form.DataSource.Where(p => p.IloscZam != null && p.IloscZam.Value > 0).OrderBy(p => p.Kod);
                int ident = 1;
                foreach (var p in pozycje)
                {

                    if (p.TowarIndywidualny)
                    {
                        Zamowienie.PozycjeZamowienia.Add(new DBWeb.PozycjaZamowienia()
                        {
                            ProduktIndywidualny = true,
                            ProduktNazwa = p.Nazwa,
                            Opis = p.Opis,
                            Ident = ident++,
                            Ilosc = p.IloscZam,
                            IloscOrg = p.IloscZam,
                            IloscObroty = p.ObrotSuma,
                            Stamp = DateTime.Now,
                            Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew
                        });
                    }
                    else
                    {
                        var produkt = webProdukt(p.TowarGuid.Value);
                        if (produkt != null)
                        {
                            var kontrahent = this.Zamowienie.Kontrahent;
                            decimal rabat = kontrahent != null ? kontrahent.GetRabat(produkt) : 0;

                            var pozycja = new DBWeb.PozycjaZamowienia()
                            {
                                ZmienionoRabat = false,
                                Rabat = rabat,
                                ProduktIndywidualny = false,
                                Cena = produkt.Cena,
                                Ident = ident++,
                                Ilosc = p.IloscZam,
                                IloscOrg = p.IloscZam,
                                IloscObroty = p.ObrotSuma,
                                Stamp = DateTime.Now,
                                StawkaVatSymbol = produkt.StawkaVat.Nazwa,
                                StawkaVatValue = produkt.StawkaVat.Procent,
                                Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                                Opis = p.Opis,
                            };

                            pozycja.Produkt = produkt;
                            Zamowienie.PozycjeZamowienia.Add(pozycja);

                        }
                    }
                }
                loadPozycje();
            }
            this.ActiveControl = pozycjeDataGrid;
        }

        private global::Enova.Business.Old.DB.Web.Produkt webProdukt(Guid guid)
        {
            global::Enova.Business.Old.DB.Web.Produkt produkt = DbContext.GetProduktByEnovaGuid(guid);
            if (produkt == null)
            {
                var ec = global::Enova.Business.Old.Core.ContextManager.DataContext;
                global::Enova.Business.Old.DB.Towar towar = global::Enova.Business.Old.Core.ContextManager.DataContext.Towary.Where(t => t.Guid == guid).FirstOrDefault();
                DateTime stamp = DateTime.Now;
                if (!towar.DefinicjaStawkiReference.IsLoaded)
                    towar.DefinicjaStawkiReference.Load();
                global::Enova.Business.Old.DB.Web.StawkaVat stawkaVat = (global::Enova.Business.Old.DB.Web.StawkaVat)towar.DefinicjaStawki;
                /*
                int ograniczenieSprzedarzy = 0;
                var fos = towar.GetFeatures(ec, "Ograniczenie sprzedaży").FirstOrDefault();
                if (fos != null)
                    int.TryParse(fos.Data, out ograniczenieSprzedarzy);
                 */
                if (stawkaVat != null)
                {
                    produkt = new global::Enova.Business.Old.DB.Web.Produkt()
                    {
                        EnovaGuid = towar.Guid,
                        GUID = Guid.NewGuid(),
                        Kod = towar.Kod,
                        Nazwa = towar.Nazwa,
                        Aktywny = false,
                        Cena = (decimal)towar.CenaHurtowaNetto,
                        DataAktualizacji = stamp,
                        DataDodania = stamp,
                        Gotowy = false,
                        Indexed = true,
                        KrotkiOpis = "",
                        LangID = 3,
                        LinkRewrite = global::Enova.Business.Old.Core.Tools.LinkRewrite(towar.Nazwa),
                        MetaOpis = "",
                        MetaTytul = "",
                        Opis = "",
                        Podprodukt = false,
                        ProduktGrupujacy = false,
                        PSID = 0,
                        Stamp = stamp,
                        Stan = 0,
                        Synchronizacja = (int)global::Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew,
                        Widoczny = true,
                        WlascicielID = 0,
                        StawkaVat = stawkaVat,
                        TowarEnova = true,
                        //     OgraniczenieSprzedazy = ograniczenieSprzedarzy==0 ? (int?)null : ograniczenieSprzedarzy
                    };

                    DbContext.AddToProdukty(produkt);

                }
            }

            return produkt;
        }

        private void terminDataTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (fireEvents)
            {
                DateTime dd = Zamowienie.DataDodania.Value;
                DateTime t = terminDataTimePicker.Value;
                DateTime data = new DateTime(dd.Year, dd.Month, dd.Day, 0, 0, 0);
                DateTime termin = new DateTime(t.Year, t.Month, t.Day, 0, 0, 0);
                if (data <= termin)
                {
                    Zamowienie.TerminPlatnosci = (termin - data).Days;
                    terminTextBox.Text = Zamowienie.TerminPlatnosci.ToString();
                }
            }

        }

        private void terminTextBox_Leave(object sender, EventArgs e)
        {
            if (fireEvents)
            {
                int termin = 0;
                if (!string.IsNullOrEmpty(terminTextBox.Text))
                {
                    if (int.TryParse(terminTextBox.Text, out termin) && (Zamowienie.TerminPlatnosci == null || Zamowienie.TerminPlatnosci != termin))
                    {
                        fireEvents = false;
                        Zamowienie.TerminPlatnosci = termin;
                        if (Zamowienie.DataDodania != null)
                            terminDataTimePicker.Value = Zamowienie.DataDodania.Value.AddDays(termin);
                        fireEvents = true;
                    }
                }
            }
        }

        private void pozycjeDataGrid_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void rozdzielButton_Click(object sender, EventArgs e)
        {
            if (pozycjeDataGrid.SelectedCells.Count > 0)
            {
                List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();

                foreach (DataGridViewCell cel in pozycjeDataGrid.SelectedCells)
                {
                    if (!selectedRows.Any(r => r.Index == cel.RowIndex))
                        selectedRows.Add(pozycjeDataGrid.Rows[cel.RowIndex]);
                }

                DialogResult result = MessageBox.Show("Czy napewno chcesz rozdzielić zamówienie?", "EnovaTools", MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                    return;

                List<DBWeb.PozycjaZamowienia> pozycje = new List<DBWeb.PozycjaZamowienia>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    pozycje.Add((DBWeb.PozycjaZamowienia)row.DataBoundItem);
                }

                DBWeb.Zamowienie zam = (DBWeb.Zamowienie)DataSource;

                DBWeb.Zamowienie noweZamowienie = new DBWeb.Zamowienie()
                {
                    AdresFaktury = zam.AdresFaktury,
                    AdresWysylki = zam.AdresWysylki,
                    AnulujBraki = false,
                    Blokada = false,
                    BlokadaEdycji = false,
                    DataDodania = DateTime.Now,
                    GUID = Guid.NewGuid(),
                    Kontrahent = zam.Kontrahent,
                    NaKiedy = DateTime.Now,
                    Pilne = false,
                    RodzajTransportu = RodzajTransportu.NieWybrano,
                    Stamp = DateTime.Now,
                    Synchronizacja = (int)RowSynchronizeOld.Notsaved,
                    ZamPrzedstawiciela = false
                };

                int ident = 1;

                foreach (var poz in pozycje)
                {
                    noweZamowienie.PozycjeZamowienia.Add(new DBWeb.PozycjaZamowienia()
                    {
                        AtrybutProduktu = poz.AtrybutProduktu,
                        Cena = poz.Cena,
                        Ident = ident++,
                        Ilosc = poz.Ilosc,
                        IloscOrg = poz.Ilosc,
                        Opis = poz.Opis,
                        Produkt = poz.Produkt,
                        ProduktIndywidualny = poz.ProduktIndywidualny,
                        ProduktNazwa = poz.ProduktNazwa,
                        Stamp = DateTime.Now,
                        Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                        StawkaVatSymbol = poz.StawkaVatSymbol,
                        StawkaVatValue = poz.StawkaVatValue
                    });
                }

                noweZamowienie.ZmienStatus(StatusyZamowieniaTyp.NoweZamowienie);

                try
                {
                    DbContext.SaveChanges();


                    foreach (var p in pozycje)
                        p.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;

                    noweZamowienie.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew;
                    DbContext.SaveChanges();
                    loadPozycje();
                }
                catch
                {
                    return;
                }

                MessageBox.Show("Stworzono nowe zamówienie o numerze " + noweZamowienie.NumerPelny, "EnovaTools");
            }
        }

        private void kopiujZamowienieButton_Click(object sender, EventArgs e)
        {
            NaglowekZamowieniaForm form = new NaglowekZamowieniaForm();
            form.AllowSaveChanges = false;
            form.DataSource = new DBWeb.Zamowienie()
            {

            };

            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                DBWeb.Zamowienie noweZam = (DBWeb.Zamowienie)form.DataSource;
                if (noweZam.Kontrahent == null || noweZam.RodzajTransportu == RodzajTransportu.NieWybrano)
                    return;
                DBWeb.Operator @operator = LoginedOperator;
                DBWeb.StatusZamowienia status = DbContext.StatusyZamowien.Where(s => s.NoweZamowienie.Value).FirstOrDefault();
                noweZam.HistoriaZamowienia.Add(new DBWeb.HistoriaZamowienia()
                {
                    GUID = Guid.NewGuid(),
                    DataDodania = DateTime.Now,
                    Operator = @operator,
                    Status = status,
                    PSID = 0,
                    Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew
                });

                int idend = 1;

                foreach (var pozycja in Zamowienie.PozycjeZamowienia.Where(p => p.Ilosc > 0 && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete))
                {
                    var pz = new DBWeb.PozycjaZamowienia()
                    {
                        AtrybutProduktu = pozycja.AtrybutProduktu,
                        Cena = pozycja.Cena,
                        Ident = idend++,
                        Ilosc = pozycja.Ilosc,
                        IloscOrg = pozycja.Ilosc,
                        Opis = pozycja.Opis,
                        ProductNazwaPelna = pozycja.ProductNazwaPelna,
                        Produkt = pozycja.Produkt,
                        ProduktIndywidualny = pozycja.ProduktIndywidualny,
                        PSID = 0,
                        Stamp = DateTime.Now,
                        StawkaVatSymbol = pozycja.StawkaVatSymbol,
                        StawkaVatValue = pozycja.StawkaVatValue,
                        Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew
                    };

                    if (form.KopijRabaty)
                    {
                        pz.Rabat = pozycja.Rabat;
                        pz.ZmienionoRabat = pozycja.ZmienionoRabat;
                    }

                    noweZam.PozycjeZamowienia.Add(pz);

                }

                noweZam.PrzeliczZamowienie();
                if (noweZam.SaveChanges())
                {
                    MessageBox.Show("Stworzono nowe zamówienie o numerze " + noweZam.ID.ToString(), "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd w trakcie tworzenia nowego zamówienia!!!", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (((DBWeb.Zamowienie)form.DataSource).EntityState == System.Data.EntityState.Added)
                    DbContext.DeleteObject(form.DataSource);
            }
        }

        private void blokadaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!blokadaCheckBox.Checked && !okButton.Enabled)
            {
                okButton.Enabled = true;
                statusButton1.Enabled = true;
                statusButton2.Enabled = true;
                statusButton3.Enabled = true;
                statusButton4.Enabled = true;
                setStatusButtons();

            }
        }


        bool fireCellValueChanged = true;
        private void pozycjeDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (fireCellValueChanged && e.ColumnIndex == 4 && e.RowIndex >= 0 && e.RowIndex < pozycjeDataGrid.Rows.Count)
            {
                fireCellValueChanged = false;
                DBWeb.PozycjaZamowienia pozycja = (DBWeb.PozycjaZamowienia)pozycjeDataGrid.Rows[e.RowIndex].DataBoundItem;
                if (pozycja != null)
                {
                    decimal val = (decimal)pozycjeDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    pozycjeDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = val / 100;
                    pozycja.ZmienionoRabat = true;
                }
                fireCellValueChanged = true;
            }
        }

        private void pozycjeDataGrid_CancelRowEdit(object sender, QuestionEventArgs e)
        {

        }

        private void pozycjeDataGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void pozycjeDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printCenyZakupu_Click(object sender, EventArgs e)
        {
            drukujZamowienie(false, true);
        }

        private void wystawFaktureButton_Click(object sender, EventArgs e)
        {
            if (!global::Enova.Business.Old.DB.Web.Operator.CurrentOperator.CheckPrawaDostepu(Business.OperatorPrawaDostepu.Magazynier))
            {
                BAL.Forms.FormManager.Alert("Nie posiadasz wystarczających uprawnień");
                return;
            }

            if (string.IsNullOrEmpty(this.Zamowienie.FakturaFirma) && this.Zamowienie.FakturaGuid == null)
            {
                var form = new WystawFaktureForm()
                {
                    Zamowienie = this.Zamowienie
                };
                var result = form.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && form.Magazyn != null)
                {
                    form.Hide();
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        //Enova.API.Handel.DokumentHandlowy dokument = null;
                        int? dokID = null;
                        using (var session = Enova.API.EnovaService.Instance.CreateSession())
                        {
                            var hm = session.GetModule<Enova.API.Handel.HandelModule>();
                            var tm = session.GetModule<Enova.API.Towary.TowaryModule>();
                            var cechy = form.GetCechy(session);
                            Enova.API.Towary.Towar towarKosztuWysylki = null;
                            decimal kostWysylki = 0;
                            System.Collections.ArrayList exPozycje = null;
                            if (form.DoliczKosztWysylki && form.KosztWysylki > 0)
                            {
                                string kod = DbContext.GetConfigString("KOD_TOWARU_USLUGI_TRANSPORTU");
                                if (!string.IsNullOrEmpty(kod))
                                {
                                    towarKosztuWysylki = tm.Towary[kod];
                                    kostWysylki = form.KosztWysylki;
                                }
                                else
                                    MessageBox.Show("Brak skonfigurowanego towaru usługi transportu");
                            }

                            var dokument = this.Zamowienie.FakturaDoZamowienia(
                                session,
                                form.Magazyn,
                                form.DataWystawienia,
                                form.DefDokHandlowego,
                                cechy,
                                form.ZatwierdzFakture,
                                towarKosztuWysylki,
                                kostWysylki,
                                form.Termin,
                                form.SposobZaplaty);

                            if (dokument != null)
                                dokID = dokument.ID;


                            /*
                            if (dokument != null)
                            {
                                this.Zamowienie.FakturaGuid = dokument.Guid;
                                this.Zamowienie.FakturaNumer = dokument.NumerPelny;
                                this.Zamowienie.SaveChanges();
                                if (Zamowienie.StatusZamowienia != null && Zamowienie.StatusZamowienia.Pakowanie == true)
                                {
                                    Zamowienie.ZmienStatus(Zamowienie.OstatniaHistoriaZamowienia.Pracownik, StatusyZamowieniaTyp.Spakowane);
                                    Zamowienie.SaveChanges();
                                    SpakowanoZamowienie = true;
                                }

                                if (this.Zamowienie.RodzajTransportu == RodzajTransportu.Kurier)
                                    this.Zamowienie.ZmienStatus(StatusyZamowieniaTyp.Kurier);
                                else if (this.Zamowienie.RodzajTransportu == RodzajTransportu.Przedstawiciel)
                                    this.Zamowienie.ZmienStatus(StatusyZamowieniaTyp.Przedstawiciel);
                                this.Zamowienie.SaveChanges();
                                DbContext.Refresh(RefreshMode.StoreWins, this.Zamowienie);

                                if (MessageBox.Show(this, "Czy chcesz wydrukować fakturę ?", "AbakTools",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    var template = global::Enova.Business.Old.Core.Configuration.GetSetting("EnovaFVReport");
                                    template = Path.IsPathRooted(template) ? template : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aspx", template);
                                    if (string.IsNullOrEmpty(template))
                                        throw new Exception("Nie skonfigurowano wzorca wydruku dla faktury sprzedaży");
                                    hm.DrukujDokument(null, dokument.Guid, template);
                                }
                            }
                             */
                        }
                        if (dokID != null)
                        {
                            using (var session = Enova.API.EnovaService.Instance.CreateSession())
                            {
                                var crm = session.GetModule<Enova.API.CRM.CRMModule>();
                                var hm = session.GetModule<Enova.API.Handel.HandelModule>();
                                using (var t = session.CreateTransaction())
                                {
                                    var tk = (Enova.API.CRM.Kontrahent)crm.Kontrahenci[927];
                                    var dokument = (Enova.API.Handel.DokumentHandlowy)hm.DokHandlowe[dokID.Value];
                                    var kontr = crm.Kontrahenci[dokument.Kontrahent.ID];
                                    dokument.Kontrahent = tk;
                                    dokument.Kontrahent = (Enova.API.CRM.Kontrahent)kontr;
                                    session.EventsInvoke();

                                    if (form.Termin != null)
                                        dokument.UstawTermin(form.Termin.Value);

                                    session.EventsInvoke();

                                    dokument.Stan = Enova.API.Handel.StanDokumentuHandlowego.Zatwierdzony;
                                    t.Commit();
                                }
                                session.Save();
                            }

                            using (var session = Enova.API.EnovaService.Instance.CreateSession())
                            {
                                var hm = session.GetModule<Enova.API.Handel.HandelModule>();
                                var dokument = (Enova.API.Handel.DokumentHandlowy)hm.DokHandlowe[dokID.Value];
                                this.Zamowienie.FakturaGuid = dokument.Guid;
                                this.Zamowienie.FakturaNumer = dokument.NumerPelny;
                                this.Zamowienie.SaveChanges();
                                if (Zamowienie.StatusZamowienia != null && Zamowienie.StatusZamowienia.Pakowanie == true)
                                {
                                    Zamowienie.ZmienStatus(Zamowienie.OstatniaHistoriaZamowienia.Operator, StatusyZamowieniaTyp.Spakowane);
                                    Zamowienie.SaveChanges();
                                    SpakowanoZamowienie = true;
                                }

                                if (this.Zamowienie.RodzajTransportu == RodzajTransportu.Kurier)
                                    this.Zamowienie.ZmienStatus(StatusyZamowieniaTyp.Kurier);
                                else if (this.Zamowienie.RodzajTransportu == RodzajTransportu.Przedstawiciel)
                                    this.Zamowienie.ZmienStatus(StatusyZamowieniaTyp.Przedstawiciel);
                                this.Zamowienie.SaveChanges();
                                DbContext.Refresh(RefreshMode.StoreWins, this.Zamowienie);

                                if (MessageBox.Show(this, "Czy chcesz wydrukować fakturę ?", "AbakTools",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    /*
                                    var template = global::Enova.Business.Old.Core.Configuration.GetSetting("EnovaFVReport");
                                    template = Path.IsPathRooted(template) ? template : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aspx", template);
                                    if (string.IsNullOrEmpty(template))
                                        throw new Exception("Nie skonfigurowano wzorca wydruku dla faktury sprzedaży");
                                    hm.DrukujDokument(null, dokument.Guid, template);
                                    */

                                    DependencyProvider.Resolve<IEnovaService>().PrintDocument(dokument.Guid);
                                }
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        BAL.Business.AppController.ThrowException(ex);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }

                }
            }
            else
                throw new Exception("Do tego zamówienia wystawiono już fakturę");
        }

        private void drukujFaktureToolStripButton_Click(object sender, EventArgs e)
        {
            if (this.Zamowienie.FakturaGuid.HasValue)
            {
                DependencyProvider.Resolve<IEnovaService>().PrintDocument(Zamowienie.FakturaGuid.Value);
            }
        }

        #region Nested Types

        public class TowaryTools : Enova.API.ITowaryTools
        {
            public Guid GetMapGuid(Guid guid)
            {
                var dc = global::Enova.Business.Old.Core.ContextManager.WebContext;
                var cel = dc.GuidMaps.Where(g => g.Tabela == "Towary" && g.Zrodlo == guid).Select(g => g.Cel).FirstOrDefault();
                if (cel == null)
                    return guid;
                return cel.Value;
            }
        }

        #endregion

        private void sortujButton_Click(object sender, EventArgs e)
        {
            if (this.Zamowienie != null)
            {
                this.Zamowienie.SortujPozycje();
                this.loadPozycje();
            }
        }

        private void emailToolStripButton_Click(object sender, EventArgs e)
        {

            bool wCenachZakupu = false;
            var result = MessageBox.Show("Czy wysłać zamówienie w cenach zakupu?", "AbakTools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;

            if (result == System.Windows.Forms.DialogResult.Yes)
                wCenachZakupu = true;

            var form = new AbakTools.Web.EmailSendForm();

            form.MailSubject = "ABAK - ZAMÓWIENIE";
            string fileName = "tmp\\" + Guid.NewGuid().ToString() + ".pdf";
            this.drukujZamowienie(false, wCenachZakupu, fileName);
            form.Attachments.Add(new System.Net.Mail.Attachment(fileName)
            {
                Name = "Zamowienie_" + this.Zamowienie.ID.ToString() + ".pdf"
            });
            form.ShowDialog();

        }

        private void ZamowienieEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.SpakowanoZamowienie && this.Zamowienie.CheckCzyZmienicDostepnosci())
            {
                var view = new AbakTools.Zamowienia.Forms.ZamowienieMagazynAVView(this.Zamowienie);
                BAL.Forms.FormManager.Instance.OpenView(view, true);
            }
        }

        private void sezonyComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (sezonyComboBox.SelectedIndex == 0)
            {
                this.Zamowienie.Sezon = null;
            }
            else
            {
                this.Zamowienie.Sezon = (string)sezonyComboBox.SelectedItem;
            }
        }

        private void sezonDodatkowyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sezonDodatkowyComboBox.SelectedIndex == 0)
            {
                this.Zamowienie.SezonDodatkowy = null;
            }
            else
            {
                this.Zamowienie.SezonDodatkowy = (string)sezonDodatkowyComboBox.SelectedItem;
            }

        }

        private void sortujAZbutton_Click(object sender, EventArgs e)
        {
            if (this.Zamowienie != null)
            {
                this.Zamowienie.SortujPozycjeAZ();
                this.loadPozycje();
            }
        }

        /*
        private void kontrahentSelectTextBox_SelectionChanged(object sender, EventArgs e)
        {
            global::Enova.Business.Old.DB.Web.Kontrahent kontrahent;
            if (kontrahentSelectTextBox.SelectedItem is global::Enova.Business.Old.DB.Kontrahent)
            {
                kontrahent = ((global::Enova.Business.Old.DB.Kontrahent)kontrahentSelectTextBox.SelectedItem).GetWebKontrahent();
            }
            else
            {
                kontrahent = (global::Enova.Business.Old.DB.Web.Kontrahent)kontrahentSelectTextBox.SelectedItem;
            }
            if (kontrahent != null && (Zamowienie.Kontrahent == null || Zamowienie.Kontrahent.ID != kontrahent.ID))
            {
                Zamowienie.Kontrahent = kontrahent;
                if (kontrahent != null)
                {
                    Zamowienie.AdresFaktury = kontrahent.DomyslnyAdresFaktury;
                    Zamowienie.AdresWysylki = kontrahent.DomyslnyAdresWysylki;
                    if (Zamowienie.ZamPrzedstawiciela != null && Zamowienie.ZamPrzedstawiciela.Value && (kontrahent.CzyAgent == null || !kontrahent.CzyAgent.Value))
                        Zamowienie.ZamPrzedstawiciela = false;
                    fireEvents = false;
                    terminTextBox.Text = Zamowienie.TerminPlatnosci.ToString();
                    terminDataTimePicker.Value = Zamowienie.TerminData;
                    fireEvents = true;
                }
                Zamowienie.PrzeliczRabaty();
                Zamowienie.PrzeliczZamowienie();
                pozycjeDataGrid.Refresh();
            }
        }
         */

        private void kontrahentEnovaSelect_ValueChanging(object sender, BAL.Forms.CancelWithDataEventArgs e)
        {
            var kontrahent = e.Data as Enova.API.CRM.Kontrahent;
            if (kontrahent != null)
            {
                if (kontrahent.BlokadaSprzedazy)
                {
                    e.Cancel = true;
                    BAL.Forms.FormManager.Alert(string.Format("Kontrahent {0} jest zablokowany i nie może być wybrany na formularzu zamówienia", kontrahent.Kod));
                }
            }
            else
                e.Cancel = true;

        }

        private void kontrahentEnovaSelect_ValueChanged(object sender, EventArgs e)
        {
            var enovaKontrahent = (Enova.API.CRM.Kontrahent)this.kontrahentEnovaSelect.SelectedItem;
            var kontrahent = global::Enova.Business.Old.DB.Web.Kontrahent.GetKontrahent(DbContext, enovaKontrahent);
            if (kontrahent == null && enovaKontrahent != null)
            {
                kontrahent = new DBWeb.Kontrahent(enovaKontrahent);
                (kontrahent as global::Enova.Business.Old.Core.ISaveChanges).SaveChanges();
            }
            if (kontrahent != null && (Zamowienie.Kontrahent == null || Zamowienie.Kontrahent.Guid != kontrahent.Guid))
            {
                Zamowienie.Kontrahent = kontrahent;
                if (kontrahent != null)
                {
                    Zamowienie.AdresFaktury = kontrahent.DomyslnyAdresFaktury;
                    Zamowienie.AdresWysylki = kontrahent.DomyslnyAdresWysylki;
                    if (Zamowienie.ZamPrzedstawiciela && (kontrahent.CzyAgent == null || !kontrahent.CzyAgent.Value))
                        Zamowienie.ZamPrzedstawiciela = false;
                    fireEvents = false;
                    terminTextBox.Text = Zamowienie.TerminPlatnosci.ToString();
                    terminDataTimePicker.Value = Zamowienie.TerminData;
                    fireEvents = true;
                }
                Zamowienie.PrzeliczRabaty();
                Zamowienie.PrzeliczZamowienie();
                pozycjeDataGrid.Refresh();

            }
        }

        private void kontrahentEnovaSelect_Load(object sender, EventArgs e)
        {

        }

    }
}
