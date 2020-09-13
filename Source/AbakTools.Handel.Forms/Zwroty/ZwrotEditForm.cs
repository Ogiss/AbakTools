using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Data.Objects;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;
using AbakTools.Towary.Forms;

namespace AbakTools.Zwroty.Forms
{
    public partial class ZwrotEditForm : Enova.Business.Old.Forms.DataEditForm
    {

        private WyborTowaruEnovaForm wyborTowaruForm = null;
        private WyborTowaruEnovaForm WyborForm
        {
            get
            {
                if (wyborTowaruForm == null)
                    wyborTowaruForm = new WyborTowaruEnovaForm();
                return wyborTowaruForm;
            }
        }

        private Enova.Business.Old.DB.Web.Zwrot Zwrot
        {
            get { return (Enova.Business.Old.DB.Web.Zwrot)this.DataSource; }
        }

        public WebContext DbContext
        {
            get
            {
                return Enova.Business.Old.Core.ContextManager.WebContext;
            }
        }

        public ZwrotEditForm()
        {
            InitializeComponent();
        }

        private void ZwrotEditForm_Load(object sender, EventArgs e)
        {

            if (this.DataSource != null)
            {

                if (this.Zwrot.EntityState == EntityState.Detached)
                    this.DbContext.Zwroty.AddObject(this.Zwrot);
                if (this.Zwrot.EntityState == EntityState.Added || this.Zwrot.EntityState == EntityState.Detached)
                {
                    this.Zwrot.DataDodania = DateTime.Now.Date;
                    this.Zwrot.DataModyfikacji = this.Zwrot.DataDodania;
                }
                else if(this.Zwrot.EntityState != EntityState.Deleted)
                {
                    this.DbContext.Refresh(RefreshMode.StoreWins, this.Zwrot);
                    this.Zwrot.Pozycje.Load();
                    this.Zwrot.HistoriaZwrotu.Load();
                    this.Zwrot.OstatniaHistoriaUserReference.Load();
                    this.Zwrot.OstatniStatusReference.Load();
                }

                if (this.Zwrot.Kontrahent != null)
                {
                    var service = Enova.API.EnovaService.Instance;
                    using (var session = service.CreateSession())
                        kontrahentEnovaSelect.SelectedItem = session.GetModule<Enova.API.CRM.CRMModule>().Kontrahenci[this.Zwrot.Kontrahent.Guid.Value];
                }


                statusyBindingSource.DataSource = DbContext.StatusyZwrotow
                    .Where(s => s.Deleted == false && s.Synchronize != (int)RowSynchronizeOld.NotsynchronizedDelete).OrderBy(s => s.ID).ToList();

                if (Zwrot.Status == null)
                {
                    Zwrot.SetStatus(TypStatusuZwrotu.Zarejestrowany);
                    DataSourceBinding.ResetCurrentItem();
                }

                loadSezony();
                loadPozycje();
            }
        }

        private void loadSezony()
        {
            var service = Enova.API.EnovaService.Instance;
            sezon1ComboBox.Items.Add("---------------");
            sezon2ComboBox.Items.Add("---------------");
            sezon3ComboBox.Items.Add("---------------");
            sezon4ComboBox.Items.Add("---------------");

            using (var session = service.CreateSession())
            {
                var bm = session.GetModule<Enova.API.Business.BusinessModule>();
                foreach (var s in bm.Dictionary["F.SEZON"].OrderBy(r => r.Value))
                {
                    sezon1ComboBox.Items.Add(s.Value);
                    sezon2ComboBox.Items.Add(s.Value);
                    sezon3ComboBox.Items.Add(s.Value);
                    sezon4ComboBox.Items.Add(s.Value);
                }
                if (this.Zwrot != null)
                {
                    if (!string.IsNullOrEmpty(this.Zwrot.Sezon))
                        sezon1ComboBox.SelectedItem = this.Zwrot.Sezon;
                    else
                        sezon1ComboBox.SelectedIndex = 0;

                    if (!string.IsNullOrEmpty(this.Zwrot.Sezon2))
                        sezon2ComboBox.SelectedItem = this.Zwrot.Sezon2;
                    else
                        sezon4ComboBox.SelectedIndex = 0;

                    if (!string.IsNullOrEmpty(this.Zwrot.Sezon3))
                        sezon3ComboBox.SelectedItem = this.Zwrot.Sezon3;
                    else
                        sezon4ComboBox.SelectedIndex = 0;

                    if (!string.IsNullOrEmpty(this.Zwrot.Sezon4))
                        sezon4ComboBox.SelectedItem = this.Zwrot.Sezon4;
                    else
                        sezon4ComboBox.SelectedIndex = 0;

                }
            }
        }

        private void PozycjaAdding()
        {
            if (Zwrot.Status != null && Zwrot.Status.Typ == (int)TypStatusuZwrotu.Zarejestrowany)
            {
                Zwrot.SetStatus(TypStatusuZwrotu.Liczony);
                DbContext.OptimisticSaveChanges();
                DbContext.Refresh(RefreshMode.StoreWins, Zwrot);
                Zwrot.HistoriaZwrotu.Load();
                DataSourceBinding.ResetCurrentItem();
            }
        }

        private void loadPozycje()
        {
            
            this.pozycjeBindingSource.DataSource = Zwrot.Pozycje.OrderBy(p=>p.Ident).ToList()
                .Where(p=>p.ToRemove == false && p.Deleted == false && p.Synchronizacja!=(int)RowSynchronizeOld.NotsynchronizedDelete).ToList();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            dodajPozycje();
        }

        private void goToLastRow()
        {
            pozycjeZwrotuGrid.CurrentCell = pozycjeZwrotuGrid.Rows[pozycjeZwrotuGrid.Rows.Count - 1].Cells[2];
        }

        private void dodajPozycje(Enova.Business.Old.DB.Towar towar = null)
        {
            Produkt local = null;
            if (towar == null)
            {
                var form = WyborForm;
                form.ShowDialog();
                this.Activate();

                
                local = (Produkt)form.SelectedItem;
            }
            else
                local = (Produkt)towar;



            if (local != null && checkObrot(local))
            {
                PozycjaAdding();

                var pozycja = new Enova.Business.Old.DB.Web.PozycjaZwrotu()
                {
                    Towar = local,
                    Guid = Guid.NewGuid(),
                    Ident = Zwrot.GetMaxIdent() + 1,
                    TowarNazwa = local.Kod + " - " + local.Nazwa,
                    Cena = local.Cena == null ? 0 : local.Cena.Value,
                    Ilosc = 1,
                    Deleted = false,
                    Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew
                };

                Zwrot.Pozycje.Add(pozycja);
                pozycjeZwrotuGrid.Focus();
                loadPozycje();
                Zwrot.PrzeliczWartosc();
                goToLastRow();
            }
        }

        private bool checkObrot(Produkt produkt)
        {
            if (produkt != null && produkt.EnovaGuid != null && produkt.EnovaGuid.Value != Guid.Empty && Zwrot.Kontrahent != null)
            {
                DateTime from = DateTime.Now.Date.AddDays(-1 * Enova.Business.Old.Zwroty.AnalizaZwrotu.OkresAnalizy);
                var dc = Enova.Business.Old.Core.ContextManager.DataContext;
                if (!dc.PozycjeDokHan.Any(p => p.Dokument.Kontrahent.Guid == Zwrot.Kontrahent.Guid && p.Dokument.KategoriaInt == (int)Enova.Old.Handel.KategoriaHandlowa.Sprzedaż && p.Dokument.Data >= from && p.Towar.Guid == produkt.EnovaGuid.Value))
                {
                    if (!BAL.Forms.FormManager.Confirm("Wybrany towar nie wystepuje na dokumentach sprzedaży\r\nCzy mimo to chcesz go dodać do zwrotu?"))
                        return false;
                }
            }
            return true;
        }

        private void pozycjeZwrotuGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < pozycjeZwrotuGrid.Rows.Count)
            {
                if (e.ColumnIndex == 2)
                {
                    Zwrot.PrzeliczWartosc();
                }
            }
        }

        BarCodeForm barCodeForm = null;

        private void ZwrotEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                if (e.KeyCode == Keys.Insert)
                {
                    dodajPozycje();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.F12)
                {
                    if (barCodeForm == null)
                        barCodeForm = new BarCodeForm();

                    DialogResult result = barCodeForm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        /*
                        Enova.Business.Old.DB.Towar towar = this.DataContext.GetTowarByBarCode(barCodeForm.BarCode);
                        if (towar != null)
                            this.dodajPozycje(towar);
                        else
                            MessageBox.Show("Nieznany kod kreskowy");
                         */

                        var towary = this.DataContext.GetTowaryByBarCode(barCodeForm.BarCode);
                        if (towary.Count > 0)
                        {
                            if (towary.Count == 1)
                            {
                                this.dodajPozycje(towary.First());
                            }
                            else
                            {
                                var form = new WyborTowaruForm();
                                form.Towary = towary;
                                form.ShowDialog();
                                if (form.WybranyTowar != null)
                                    this.dodajPozycje(form.WybranyTowar);
                            }
                        }
                        else
                            MessageBox.Show("Nieznany kod kreskowy");
                    }
                }
            }
        }

        private void pozycjeZwrotuGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && !e.Alt && !e.Shift)
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (pozycjeZwrotuGrid.CurrentRow.Index == pozycjeZwrotuGrid.Rows.Count - 1)
                    {
                        dodajPozycje();
                        e.Handled = true;
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    usunPozycje();
                    e.Handled = true;
                }
            }
        }

        private void ZwrotEditForm_Shown(object sender, EventArgs e)
        {
            /*
            if (kontrahentSelectTextBox.SelectedItem == null)
                kontrahentSelectTextBox.Focus();
             */
            if (kontrahentEnovaSelect.SelectedItem == null)
                kontrahentEnovaSelect.Focus();
            else
                pozycjeZwrotuGrid.Focus();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            usunPozycje();
        }

        private void usunPozycje()
        {
            if (pozycjeZwrotuGrid.CurrentRow != null)
            {
                DialogResult result = MessageBox.Show("Czy napewno chcesz usunąć pozycję?","EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    PozycjaZwrotu poz = (PozycjaZwrotu)pozycjeZwrotuGrid.CurrentRow.DataBoundItem;
                    poz.ToRemove = true;
                    pozycjeZwrotuGrid.Rows.Remove(pozycjeZwrotuGrid.CurrentRow);
                }
                Zwrot.ReIdentPozycje();
            }
        }

        protected override bool OkValid()
        {
            if (Zwrot.Kontrahent == null)
            {
                MessageBox.Show("Brak wybranego kontrahenta!!!");
                return false;
            }
            return true;
        }

        protected override bool CancelValid()
        {
            DialogResult result = MessageBox.Show("czy napewno chcesz opuścić dokument zwrotu?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == System.Windows.Forms.DialogResult.Yes)
                return true;
            return false;
        }

        private Enova.Business.Old.DB.Web.Produkt webProdukt(Guid guid)
        {
            Enova.Business.Old.DB.Web.Produkt produkt = DbContext.GetProduktByEnovaGuid(guid);
            if (produkt == null)
            {
                Enova.Business.Old.DB.Towar towar = Enova.Business.Old.Core.ContextManager.DataContext.Towary.Where(t => t.Guid == guid).FirstOrDefault();
                DateTime stamp = DateTime.Now;
                if (!towar.DefinicjaStawkiReference.IsLoaded)
                    towar.DefinicjaStawkiReference.Load();
                Enova.Business.Old.DB.Web.StawkaVat stawkaVat = (Enova.Business.Old.DB.Web.StawkaVat)towar.DefinicjaStawki;
                if (stawkaVat != null)
                {
                    produkt = new Enova.Business.Old.DB.Web.Produkt()
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
                        LinkRewrite = Enova.Business.Old.Core.Tools.LinkRewrite(towar.Nazwa),
                        MetaOpis = "",
                        MetaTytul = "",
                        Opis = "",
                        Podprodukt = false,
                        ProduktGrupujacy = false,
                        PSID = 0,
                        Stamp = stamp,
                        Stan = 0,
                        Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew,
                        Widoczny = true,
                        WlascicielID = 0,
                        StawkaVat = stawkaVat,
                        TowarEnova = true
                    };

                    DbContext.AddToProdukty(produkt);
                }
            }

            return produkt;
        }


        private void zformularzaButton_Click(object sender, EventArgs e)
        {
            var form = new ZwrotFormularzForm();

            if(Zwrot.Kontrahent != null)
            {
                Enova.Business.Old.DB.Web.Kontrahent kontrahent = Zwrot.Kontrahent;
                form.KontrahentId = Enova.Business.Old.Core.ContextManager.DataContext.Kontrahenci.Where(k => k.Guid == kontrahent.Guid).Select(k => k.ID).FirstOrDefault();
            }

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK && form.DataSource != null)
            {
                var pozycje = form.DataSource.Where(p => p.IloscZam != null && p.IloscZam.Value > 0).OrderBy(p => p.Kod);

                if (pozycje.Count() > 0)
                    PozycjaAdding();

                foreach (var p in pozycje)
                {
                    var produkt = webProdukt(p.TowarGuid.Value);
                    if (produkt != null)
                    {
                        Zwrot zwrot = (Zwrot)this.DataSource;

                        zwrot.Pozycje.Add(new PozycjaZwrotu()
                        {
                            Cena = produkt.Cena == null ? 0 : produkt.Cena.Value,
                            Deleted = false,
                            Guid = Guid.NewGuid(),
                            Ident = Zwrot.GetMaxIdent() + 1,
                            Ilosc = p.IloscZam.Value,
                            Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew,
                            Towar = produkt,
                            TowarNazwa = produkt.Nazwa
                        });
                    }
                }

                loadPozycje();
                Zwrot.PrzeliczWartosc();
                goToLastRow();
                
            }
        }

        private void zmienButton_Click(object sender, EventArgs e)
        {
            if (statusComboBox.SelectedItem != null)
            {
                var status = (StatusZwrotu)statusComboBox.SelectedItem;
                if(Zwrot.Status == null || Zwrot.Status.ID != status.ID)
                {
                    Zwrot.SetStatus(status);
                    DbContext.OptimisticSaveChanges();
                    DbContext.Refresh(RefreshMode.StoreWins, Zwrot);
                    Zwrot.HistoriaZwrotu.Load();
                    DataSourceBinding.ResetCurrentItem();
                }
                else if (Zwrot.OstatniStatusID == null || Zwrot.OstatniStatusID.Value != status.ID)
                {
                    Zwrot.OstatniStatusID = status.ID;
                    Zwrot.OstatniaHistoriaUserID = User.LoginedUser.ID;
                    DbContext.OptimisticSaveChanges();
                    DbContext.Refresh(RefreshMode.StoreWins, Zwrot);
                }
            }
        }

        private void ZwrotEditForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void rozdzielButton_Click(object sender, EventArgs e)
        {
            if (this.pozycjeZwrotuGrid.SelectedRows.Count > 0 && 
                MessageBox.Show("Czy napewno chcesz rozdzielić zwrot?","EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes )
            {
                List<PozycjaZwrotu> pozycje = new List<PozycjaZwrotu>();
                foreach (DataGridViewRow row in this.pozycjeZwrotuGrid.SelectedRows)
                {
                    if (row.DataBoundItem != null)
                        pozycje.Add((PozycjaZwrotu)row.DataBoundItem);
                }

                Zwrot nowyZwrot = new Zwrot()
                {
                    DataDodania = DateTime.Now,
                    DataModyfikacji = DateTime.Now,
                    Deleted = false,
                    Guid = Guid.NewGuid(),
                    IloscPaczek = 0,
                    Kontrahent = this.Zwrot.Kontrahent,
                    Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                  
                };

                DbContext.SaveChanges();
                DbContext.Refresh(RefreshMode.StoreWins, nowyZwrot);

                nowyZwrot.HistoriaZwrotu.Add(new HistoriaZwrotu()
                {
                    Data = DateTime.Now,
                    Deleted = false,
                    Guid = Guid.NewGuid(),
                    Status = this.Zwrot.OstHistoriaZwrotu.Status,
                    Synchronize = (int)RowSynchronizeOld.NotsynchronizedNew,
                    Uzytkownik = User.LoginedUser,
                });

                foreach (var pozycja in pozycje)
                {
                    nowyZwrot.Pozycje.Add(new PozycjaZwrotu()
                    {
                        Cena = pozycja.Cena,
                        Deleted = false,
                        Guid = Guid.NewGuid(),
                        Ident = nowyZwrot.GetMaxIdent()+1,
                        Ilosc = pozycja.Ilosc,
                        Opis = pozycja.Opis,
                        IlocsDeklarowana = pozycja.IlocsDeklarowana,
                        Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                        Towar = pozycja.Towar,
                        TowarNazwa = pozycja.TowarNazwa
                    });
                    pozycja.Deleted = true;
                }

                this.Zwrot.ReIdentPozycje();
                this.Zwrot.PrzeliczWartosc();

                nowyZwrot.ReIdentPozycje();
                nowyZwrot.PrzeliczWartosc();

                DbContext.SaveChanges();
                DbContext.Refresh(RefreshMode.StoreWins, nowyZwrot);

                this.loadPozycje();

                MessageBox.Show("Stworzono nowy zwrot o numerze " + nowyZwrot.ID);
            }
        }

        private void historiaButton_Click(object sender, EventArgs e)
        {
            var form = new HistoriaZwrotuForm();
            form.Zwrot = this.Zwrot;
            form.ShowDialog();
        }

        private void pozycjeNieskorygowaneButton_Click(object sender, EventArgs e)
        {
            new PozycjeNieskorygowaneForm()
            {
                Zwrot = this.Zwrot
            }.ShowDialog();
        }

        private void sezonyComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (sezon1ComboBox.SelectedIndex == 0)
            {
                this.Zwrot.Sezon = null;
            }
            else
            {
                this.Zwrot.Sezon = (string)sezon1ComboBox.SelectedItem;
            }
        }

        private void sezon2ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (sezon2ComboBox.SelectedIndex == 0)
            {
                this.Zwrot.Sezon2 = null;
            }
            else
            {
                this.Zwrot.Sezon2 = (string)sezon2ComboBox.SelectedItem;
            }
        }

        private void sezon3ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (sezon3ComboBox.SelectedIndex == 0)
            {
                this.Zwrot.Sezon3 = null;
            }
            else
            {
                this.Zwrot.Sezon3 = (string)sezon3ComboBox.SelectedItem;
            }
        }

        private void sezon4ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (sezon4ComboBox.SelectedIndex == 0)
            {
                this.Zwrot.Sezon4 = null;
            }
            else
            {
                this.Zwrot.Sezon4 = (string)sezon4ComboBox.SelectedItem;
            }
        }

        private void kontrahentEnovaSelect_ValueChanged(object sender, EventArgs e)
        {
            var kontrahent = Kontrahent.GetKontrahent(DbContext, (Enova.API.CRM.Kontrahent)kontrahentEnovaSelect.SelectedItem);
            if (kontrahent != null)
                Zwrot.Kontrahent = kontrahent;
        }

    }
}
