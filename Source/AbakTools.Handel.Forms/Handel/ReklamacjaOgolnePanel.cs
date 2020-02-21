using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Forms;
using AbakTools.Handel;
using AbakTools.Towary;

using OldDBWeb = Enova.Business.Old.DB.Web;

[assembly: DataPanel("Ogólne", typeof(Enova.Business.Old.DB.Web.Reklamacja), typeof(AbakTools.Handel.Forms.ReklamacjaOgolnePanel))]

namespace AbakTools.Handel.Forms
{
    public partial class ReklamacjaOgolnePanel : BAL.Forms.DataPanel
    {
        private AbakTools.Towary.Forms.TowaryEnovaView towarySelectView;
        private Form towarySelectForm;

        private List<Enova.Business.Old.DB.Web.PozycjaReklamacji> pozycje;

        private Enova.Business.Old.DB.Web.Reklamacja Reklamacja
        {
            get
            {
                return (Enova.Business.Old.DB.Web.Reklamacja)this.DataContext.GetData();
            }
        }

        public Enova.Business.Old.DB.Web.WebContext DbContext
        {
            get
            {
                if (this.DataContext != null && this.DataContext is Enova.Business.Old.Core.IContexable)
                    return ((Enova.Business.Old.Core.IContexable)this.DataContext).DbContext as Enova.Business.Old.DB.Web.WebContext;
                return null;
            }
        }

        public ReklamacjaOgolnePanel()
        {
            InitializeComponent();
        }

        protected override void OnBindingComplete(EventArgs e)
        {
            historiaDokumentuControl.DataContext = this.DataContext;
            if (this.DataContext is Enova.Business.Old.Core.IContexable)
            {
                var dc = ((Enova.Business.Old.Core.IContexable)this.DataContext).DbContext as Enova.Business.Old.DB.Web.WebContext;
                kontrahentSelect.DataContext = new AbakTools.CRM.Forms.KontrahenciView(dc);
            }
            pozycje = Reklamacja.Pozycje.Where(r=>r.Deleted == false).ToList();
            this.loadPozycje();
            this.ActiveControl = pozycjeReklamacjiGrid;
            if (Reklamacja.ID == 0)
            {
                generujZwrotButton.Enabled = false;
                rozdzielButton.Enabled = false;
            }
            else
            {
                if (Reklamacja.Pozycje.Where(r => r.Indywidualny == false && r.IloscBraki > 0).Count() == 0)
                {
                    generujZwrotButton.Enabled = false;
                }

                if ((Reklamacja.OstatniStatusDokumentu.Opcje & Core.OpcjeStatusuDokumentu.Koncowy) == Core.OpcjeStatusuDokumentu.Koncowy)
                {
                    generujZwrotButton.Enabled = false;
                }
            }
            base.OnBindingComplete(e);
        }

        private void loadPozycje()
        {
            
            this.pozycjeBindingSource.DataSource = pozycje;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.dodajPozycje();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            usunPozycje();
        }

        private void dodajPozycje()
        {
            if (towarySelectView == null)
            {
                towarySelectView = new AbakTools.Towary.Forms.TowaryEnovaView(this.DbContext);
                towarySelectView.SelectionMode = true;
                towarySelectView.ParentFormHideOnClose = true;
            }

            if (this.towarySelectForm == null)
            {
                this.towarySelectForm = FormManager.Instance.GetDefaultGridForm(towarySelectView);
                if(typeof(BAL.Forms.DataGridFormOld).IsAssignableFrom(this.towarySelectForm.GetType()))
                    ((BAL.Forms.DataGridFormOld)this.towarySelectForm).AfterSelectRow+=new EventHandler<RowEventArgs>(ReklamacjaOgolnePanel_AfterSelectRow);
                this.towarySelectForm.Deactivate += (sender, e) => { this.towarySelectForm.WindowState = FormWindowState.Minimized; };
                this.towarySelectForm.StartPosition = FormStartPosition.CenterScreen;
            }

            if (!this.towarySelectForm.Visible)
            {
                this.towarySelectForm.Show();
            }

            if (this.towarySelectForm.WindowState == FormWindowState.Minimized)
                this.towarySelectForm.WindowState = FormWindowState.Normal;

            this.towarySelectForm.Activate();


            /*
            if (FormManager.Instance.ShowGridFormDialog(towarySelectView) == DialogResult.OK)
            {
                Towar towar = (Towar)towarySelectView.Current;
                int maxLp = this.pozycje.Count == 0 ? 0 : this.pozycje.Max(r => r.Lp);

                var pozycja = new PozycjaReklamacji()
                {
                    Reklamacja = this.Reklamacja,
                    Towar = towar,
                    TowarNazwa = towar.Kod + " - " + towar.Nazwa,
                    Cena = towar.CenaNetto,
                    Lp = maxLp + 1
                };
                this.Reklamacja.Pozycje.AddRow(pozycja);
                int idx = this.pozycjeBindingSource.Add(pozycja);
                this.pozycjeReklamacjiGrid.CurrentCell = this.pozycjeReklamacjiGrid.Rows[idx].Cells[lloscBrakiColumn.Index];
                this.pozycjeReklamacjiGrid.Focus();
                //this.loadPozycje();
            }
             */
        }

        private OldDBWeb.PozycjaReklamacji dodajPozycje(OldDBWeb.Produkt towar, decimal? cena = null, double? ilosc = null)
        {
            int maxLp = this.pozycje.Count == 0 ? 0 : this.pozycje.Max(r => r.Lp);
            var pozycja = new Enova.Business.Old.DB.Web.PozycjaReklamacji()
            {
                Towar = towar,
                TowarNazwa = towar.Kod + " - " + towar.Nazwa,
                Cena = cena != null ? cena.Value : (towar.Cena == null ? 0 : towar.Cena.Value),
                Lp = maxLp + 1
            };
            if (ilosc != null)
            {
                if (ilosc.Value < 0)
                    pozycja.IloscBraki = -ilosc.Value;
                else if (ilosc > 0)
                    pozycja.IloscNadwyzki = ilosc.Value;
            }
            this.Reklamacja.Pozycje.Add(pozycja);
            int idx = this.pozycjeBindingSource.Add(pozycja);
            this.pozycjeReklamacjiGrid.CurrentCell = this.pozycjeReklamacjiGrid.Rows[idx].Cells[lloscBrakiColumn.Index];
            this.pozycjeReklamacjiGrid.Focus();
            return pozycja;
        }

        private void ReklamacjaOgolnePanel_AfterSelectRow(object sender, RowEventArgs e)
        {
            if (e.Row != null)
            {
                Enova.Business.Old.DB.Web.Produkt towar = (Enova.Business.Old.DB.Web.Produkt)towarySelectView.Current;
                dodajPozycje(towar);
                /*
                int maxLp = this.pozycje.Count == 0 ? 0 : this.pozycje.Max(r => r.Lp);
                var pozycja = new Enova.Business.Old.DB.Web.PozycjaReklamacji()
                {
                    Towar = towar,
                    TowarNazwa = towar.Kod + " - " + towar.Nazwa,
                    Cena = towar.Cena == null ? 0 : towar.Cena.Value,
                    Lp = maxLp + 1
                };

                this.Reklamacja.Pozycje.Add(pozycja);
                int idx = this.pozycjeBindingSource.Add(pozycja);
                this.pozycjeReklamacjiGrid.CurrentCell = this.pozycjeReklamacjiGrid.Rows[idx].Cells[lloscBrakiColumn.Index];
                this.pozycjeReklamacjiGrid.Focus();
                 */
            }
        }

        private void usunPozycje()
        {
            if (pozycjeReklamacjiGrid.CurrentRow != null)
            {
                if (FormManager.Confirm("Czy napewno chcesz usunąć pozycję?"))
                {
                    Enova.Business.Old.DB.Web.PozycjaReklamacji pozycja = (Enova.Business.Old.DB.Web.PozycjaReklamacji)pozycjeReklamacjiGrid.CurrentRow.DataBoundItem;
                    if (pozycja.EntityState == EntityState.Added)
                        DbContext.DeleteObject(pozycja);
                    else if (pozycja.EntityState == EntityState.Modified || pozycja.EntityState == EntityState.Unchanged)
                        pozycja.Deleted = true;
                    int i = 1;
                    foreach (var poz in this.Reklamacja.Pozycje.Where(r => r.Deleted == false).OrderBy(r => r.Lp))
                        poz.Lp = i++;
                    pozycjeBindingSource.Remove(pozycja);
                }
            }
        }

        private void pozycjeReklamacjiGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                if (e.KeyCode == Keys.Insert)
                {
                    dodajPozycje();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    usunPozycje();
                }
                else if (e.KeyCode == Keys.Down && pozycjeReklamacjiGrid.CurrentRow != null && pozycjeReklamacjiGrid.CurrentRow.Index == pozycjeReklamacjiGrid.Rows.Count - 1)
                {
                    dodajPozycje();
                    e.Handled = true;
                    e.SuppressKeyPress = false;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void generujZwrotButton_Click(object sender, EventArgs e)
        {
            if (this.Reklamacja.ID == 0)
            {
                BAL.Forms.FormManager.Alert("Musisz najpierw zapisać zwrot");
                return;
            }

            if (!BAL.Forms.FormManager.Confirm("Czy napewno chcesz wygenerować zwrot z reklamacji?"))
            {
                return;
            }

            var dc = (Enova.Business.Old.DB.Web.WebContext)Reklamacja.DbContext;

            var zwrot = new OldDBWeb.Zwrot();
            dc.Zwroty.AddObject(zwrot);
            var kontrahent = dc.Kontrahenci.Where(r => r.ID == this.Reklamacja.Kontrahent.ID).FirstOrDefault();
            zwrot.Kontrahent = kontrahent;
            zwrot.DataDodania = DateTime.Now;
            zwrot.DataModyfikacji = zwrot.DataDodania;
            zwrot.Deleted = false;
            zwrot.Guid = Guid.NewGuid();
            zwrot.IloscPaczek = 0;
            zwrot.Opis = "Zwrot do reklamacji " + this.Reklamacja.Numer.NumerPelny;
            zwrot.SkorygowanyWCalosci = false;
            zwrot.Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew;
            zwrot.SetStatus(OldDBWeb.TypStatusuZwrotu.Sprawdzony);

            int ident = 1;
            foreach (var poz in this.Reklamacja.Pozycje.Where(r=>r.Indywidualny == false && r.IloscBraki > 0))
            {
                var towar = dc.Produkty.Where(r => r.ID == poz.TowarID).First();
                var pozycja = new OldDBWeb.PozycjaZwrotu()
                {
                    Cena = towar.Cena == null ? 0 : towar.Cena.Value,
                    Deleted = false,
                    Guid = Guid.NewGuid(),
                    Ident = ident++,
                    IlocsDeklarowana = 0,
                    Ilosc = poz.IloscBraki,
                    IloscOrg = poz.IloscBraki,
                    IloscSkorygowana = 0,
                    Opis = "",
                    Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew,
                    Towar = towar,
                    TowarNazwa = towar.Nazwa,
                    Zwrot = zwrot
                };
            }

            var form = new AbakTools.Zwroty.Forms.ZwrotEditForm();
            form.DataSource = zwrot;
            if (form.ShowDialog() == DialogResult.OK)
            {
                int opt = (int)Core.OpcjeStatusuDokumentu.Koncowy;
                //var statusy = Core.CoreModule.GetInstance(this.DataContext).StatusyDokumentow.WgOpcji[Core.OpcjeStatusuDokumentu.Koncowy].ToList();
                var statusy = dc.StatusyDokumentow.Where(r=> r.Kategoria == "Reklamacje" && (r.OpcjeInt & opt) == opt).ToList();
                if (statusy.Count == 0)
                {
                    BAL.Forms.FormManager.Alert("UWAGA !!! Brak zdefiniowanego statusu końcowego dla reklamacji");
                    return;
                }
                //Core.StatusDokumentu status = null;
                Enova.Business.Old.DB.Web.StatusDokumentu status = null;
                if (statusy.Count > 1)
                {
                    var form2 = new Core.Forms.WyborStatusuForm()
                    {
                        Statusy = statusy
                    };

                    if (form2.ShowDialog() == DialogResult.OK)
                    {
                        status = form2.WybranyStatus;
                    }

                }
                else
                    status = statusy.First();

                if (status != null)
                {
                    this.Reklamacja.ZmienStatus(status, "Wygenerowano zwrot nr: " + zwrot.ID);
                }

            }
        }

        private void rozdzielButton_Click(object sender, EventArgs e)
        {
            
            List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();

            foreach (DataGridViewCell cell in pozycjeReklamacjiGrid.SelectedCells)
            {
                if (!selectedRows.Any(r => r.Index == cell.RowIndex))
                    selectedRows.Add(cell.OwningRow);
            }


            if (selectedRows.Count > 0 && BAL.Forms.FormManager.Confirm("Czy napewno chcesz rodzielić reklamację"))
            {
                //var hm = HandelModule.GetInstance(this.DataContext);
                var dc = (Enova.Business.Old.DB.Web.WebContext)((Enova.Business.Old.Core.IContexable)this.DataContext);
                var view = (ReklamacjeView)this.DataContext.Parent;

                //var rkl = new Enova.Business.Old.DB.Web.Reklamacja();
                var rkl = (Enova.Business.Old.DB.Web.Reklamacja)view.CreateData();
                rkl.Kontrahent = this.Reklamacja.Kontrahent;
                //hm.Reklamacje.AddRow(rkl);

                int i = 1;

                foreach (DataGridViewRow row in selectedRows)
                {
                    var poz = (Enova.Business.Old.DB.Web.PozycjaReklamacji)row.DataBoundItem;

                    var newPoz = new Enova.Business.Old.DB.Web.PozycjaReklamacji();
                    //hm.Reklamacje.AddRow(newPoz);

                    newPoz.Reklamacja = rkl;
                    newPoz.Towar = poz.Towar;
                    newPoz.TowarNazwa = poz.TowarNazwa;
                    newPoz.Cena = poz.Cena;
                    newPoz.Indywidualny = poz.Indywidualny;
                    newPoz.Korekta = poz.Korekta;
                    newPoz.Lp = i++;
                    newPoz.Opis = poz.Opis;
                    newPoz.IloscBraki = poz.IloscBraki;
                    newPoz.IloscNadwyzki = poz.IloscNadwyzki;

                    poz.Deleted = true;
                }

                ((ReklamacjeEditContext)this.DataContext).RozdzielonaReklamacja = rkl;
                ((BAL.Forms.DataEditForm)this.ParentForm).ProcessOKButton();
                
            }
        }

        private void wgDokSprzButton_Click(object sender, EventArgs e)
        {
            /*
            Enova.API.CRM.Kontrahent kontrahent = null;
            Enova.API.Handel.DefDokHandlowego definicja;
            using(var session = Enova.API.EnovaService.Instance.CreateSession())
            {
                definicja = session.GetModule<Enova.API.Handel.HandelModule>().DefDokHandlowych.FakturaSprzedazy;
                if(this.Reklamacja.Kontrahent != null)
                {
                    kontrahent = (Enova.API.CRM.Kontrahent)session.GetModule<Enova.API.CRM.CRMModule>().Kontrahenci[this.Reklamacja.Kontrahent.Guid.Value];
                }
            }
            var dokView = new Enova.Forms.Handel.DokumentyView("ReklamacjeDokumentyEnovaView", kontrahent, definicja);
            dokView.SelectionMode = true;
            if(BAL.Forms.FormManager.Instance.OpenView(dokView,true) == DialogResult.OK)
            {
                var dokContext = new ReklamacjaDokHandlowyContext();
                dokContext.SetData(typeof(Enova.API.Handel.DokumentHandlowy), dokView.Current);
                if(BAL.Forms.FormManager.Instance.OpenView(dokContext,true) == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(Reklamacja.Opis))
                        Reklamacja.Opis += "\r\n";
                    Reklamacja.Opis += dokContext.Dokument.NumerPelny + " ";
                    foreach (var rinfo in dokContext.Pozycje)
                    {
                        if (rinfo.Roznica != 0)
                        {
                            var towar = ((OldDBWeb.WebContext)Reklamacja.DbContext).Produkty
                                .Where(r => r.TowarEnova == true && r.EnovaGuid == rinfo.Pozycja.Towar.Guid).FirstOrDefault();

                            if (towar != null)
                            {
                                dodajPozycje(towar, rinfo.Pozycja.Cena, rinfo.Roznica);
                            }
                        }
                    }
                }
            }
            */
        }


    }
}
