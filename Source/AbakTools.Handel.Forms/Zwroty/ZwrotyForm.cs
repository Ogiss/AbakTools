using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;
using Enova.Business.Old.Zwroty;
using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;
using AbakTools.Printer;

[assembly: BAL.Forms.MenuAction("Zwroty", typeof(AbakTools.Zwroty.Forms.ZwrotyForm), Priority = 900)]

namespace AbakTools.Zwroty.Forms
{
    public partial class ZwrotyForm : Enova.Business.Old.Forms.DataGridFormWithEnovaAPI
    {
        public ZwrotyForm()
        {
            InitializeComponent();
        }

        private void ZwrotyForm_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                okresSpan.SetSpan(Enova.Business.Old.Controls.DateTimeSpanControl.SpanType.Month, DateTime.Now.Year, DateTime.Now.Month, null);
                loadSezony();
            }
        }

        private void loadSezony()
        {
            var service = Enova.API.EnovaService.Instance;
            sezonComboBox.Items.Add("---------------");

            using (var session = service.CreateSession())
            {
                var bm = session.GetModule<Enova.API.Business.BusinessModule>();
                foreach (var s in bm.Dictionary["F.SEZON"].OrderBy(r => r.Value))
                {
                    sezonComboBox.Items.Add(s.Value);
                }
                sezonComboBox.SelectedIndex = 0;
            }
        }

        protected override void LoadData()
        {
            DateTime dataOd = okresSpan.DateFrom.Date;
            DateTime dataDo = okresSpan.DateTo.Date.AddDays(1).AddSeconds(-1);
            Enova.API.CRM.Kontrahent kontrahent = null;
            string przedstawiciel = kontrahentSelectControl.Przedstawiciel;
            string trasa = kontrahentSelectControl.Trasa;
            string sezon = sezonComboBox.SelectedIndex > 0 ? (string)sezonComboBox.SelectedItem : null;
            Guid[] guids = null;
            if (kontrahentSelectControl.Kontrahent != null)
            {
                //kontrahent = kontrahentSelectControl.Kontrahent.GetWebKontrahent();
                kontrahent = Enova.Business.Old.DB.Web.Kontrahent.GetWebKontrahent(kontrahentSelectControl.Kontrahent);
            }
            else if (!string.IsNullOrEmpty(przedstawiciel) && !string.IsNullOrEmpty(trasa))
            {
                trasa = @"\" + przedstawiciel + @"\" + trasa + @"\";
                var dc = Enova.Business.Old.Core.ContextManager.DataContext;
                guids = (from k in dc.Kontrahenci
                         join f in dc.Features on
                         new { ParentType = "Kontrahenci", Parent = k.ID, Name = "TRASY" } equals
                         new { ParentType = f.ParentType, Parent = f.Parent, Name = f.Name }
                         where f.Data == trasa
                         select k.Guid).ToArray();
            }

            var zm = ZwrotyModule.GetInstance(Enova.Business.Old.Core.ContextManager.WebContext);

            var ds = zm.Zwroty.WgDatyDodania[okresSpan.DateFrom, okresSpan.DateTo, true].WgPrzedstawiciel[przedstawiciel].WgSezon[sezon];

            if (guids != null)
                this.DataSource = ds.WgKontrahent[guids];
            else
                this.DataSource = kontrahent == null ? ds : ds.WgKontrahent[new Guid[] { kontrahent.Guid }];
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void okresSpan_Changed(object sender, EventArgs e)
        {
            this.Reload();
        }

        private void kontrahentSelectControl_KontrahentChanged(object sender, EventArgs e)
        {
            if (this.IsLoaded)
                this.Reload();
        }

        private void DataGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
        }

        private void rejestrujButton_Click(object sender, EventArgs e)
        {
            RejestracjaZwrotuForm form = new RejestracjaZwrotuForm();

            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK && form.Kontrahent != null)
            {
                var zwrot = new Zwrot()
                {
                    Guid = Guid.NewGuid(),
                    DataDodania = DateTime.Now,
                    DataModyfikacji = DateTime.Now,
                    Deleted = false,
                    Kontrahent = form.Kontrahent,
                    IloscPaczek = form.IloscPaczek,
                    Opis = form.Opis,
                    Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                    WartoscNetto = 0
                };

                zwrot.SetStatus(TypStatusuZwrotu.Zarejestrowany);

                ContextManager.WebContext.AddToZwroty(zwrot);
                ContextManager.WebContext.OptimisticSaveChanges();

                this.Reload();
            }
        }

        private void DataGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

            if (e.RowIndex > -1 && e.RowIndex < this.DataGrid.Rows.Count)
            {
                DataGridViewRow row = this.DataGrid.Rows[e.RowIndex];
                if (row.DataBoundItem != null)
                {
                    Zwrot zwrot = (Zwrot)row.DataBoundItem;
                    StatusZwrotu s = zwrot.OstatniStatus;
                    if (s != null)
                    {
                        Color color = ColorTranslator.FromHtml(s.Kolor);
                        row.DefaultCellStyle.BackColor = color;
                        row.HeaderCell.Style.BackColor = color;
                        row.HeaderCell.Style.SelectionBackColor = color;
                    }
                }
            }
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DataGrid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void ZwrotyForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ZwrotyForm_PrintItemClick(object sender, EventArgs e)
        {
            if (DataGrid.CurrentRow != null && DataGrid.CurrentRow.DataBoundItem != null)
            {
                Zwrot zwrot = (Zwrot)DataGrid.CurrentRow.DataBoundItem;

                ReportForm form = new ReportForm();

                form.ReportPath = "Reports\\ZwrotReport.rdlc";
                form.LocalReport.SetParameters(new ReportParameter("kontrahent", zwrot.Kontrahent.Kod + " - " + zwrot.Kontrahent.Nazwa));
                form.AddDataSource("Zwrot", new object[] { zwrot });
                form.AddDataSource("PozycjeZwrotu", zwrot.Pozycje.OrderBy(p => p.Ident).ToList());
                form.ShowDialog();
            }
        }

        private void analizujButton_Click(object sender, EventArgs e)
        {
            var login = Enova.Business.Old.DB.Web.User.LoginedUser.LoginedEnova;

            if (login == null)
            {
                MessageBox.Show("Nie jesteś zalogowany do bazy Enova. Skontaktuj się z Administratorem");
                return;
            }

            if (DataGrid.CurrentRow != null && DataGrid.CurrentRow.DataBoundItem != null)
            {
                this.Cursor = Cursors.WaitCursor;
                this.Enabled = false;

                using (Session session = login.CreateSession(true, false, "Zwrot.Analiza"))
                {

                    Zwrot zwrot = (Zwrot)DataGrid.CurrentRow.DataBoundItem;

                    ZwrotAnaliza analiza = zwrot.AnalizujZwrot(session);

                    ReportForm form = new ReportForm();
                    form.ReportPath = "ZwrotAnalizaSimpleReport.rdlc";

                    form.Title = "Analiza zwrotu";

                    form.LocalReport.SetParameters(new ReportParameter[] { 
                    new ReportParameter("numer",zwrot.ID.ToString()),
                    new ReportParameter("dataDodania",zwrot.DataDodania.ToShortDateString()),
                    new ReportParameter("dataModyfikacji",zwrot.DataModyfikacji.ToShortDateString()),
                    new ReportParameter("kontrahent", zwrot.Kontrahent.ToString()),
                    new ReportParameter("opis",zwrot.OpisLine)
                });

                    int idx = 0;
                    foreach (ZwrotAnalizaDokHandlowyOld dh in analiza.Dokumenty)
                    {
                        if (idx <= 9)
                            form.LocalReport.SetParameters(new ReportParameter("dokument" + idx.ToString(), dh.NumerPelny));
                        idx++;
                    }

                    if (idx < 9)
                    {
                        for (int i = idx; i <= 9; i++)
                            form.LocalReport.SetParameters(new ReportParameter("dokument" + i.ToString(), string.Empty));
                    }

                    form.AddDataSource("Pozycje", analiza.Pozycje);

                    form.ShowDialog();
                }
                this.Enabled = true;
                this.Cursor = Cursors.Default;

            }
        }

        private void korygujButton_Click(object sender, EventArgs e)
        {

            var login = Enova.Business.Old.DB.Web.User.LoginedUser.LoginedEnova;

            if (!Enova.Business.Old.DB.Web.User.LoginedUser.Administrator)
            {
                MessageBox.Show("!!! BRAK UPRAWNIEŃ !!!");
                return;
            }

            if (login == null)
            {
                MessageBox.Show("Nie jesteś zalogowany do bazy Enova. Skontaktuj się z Administratorem");
                return;
            }


            if (DataGrid.CurrentRow != null && DataGrid.CurrentRow.DataBoundItem != null)
            {
                this.Cursor = Cursors.WaitCursor;
                this.Enabled = false;

                using (Session session = login.CreateSession(false, false, "AnalizaZwrotu.Koryguj"))
                {

                    Zwrot zwrot = (Zwrot)DataGrid.CurrentRow.DataBoundItem;

                    AnalizaZwrotu analiza = new AnalizaZwrotu(session, zwrot);

                    if (analiza != null)
                    {
                        analiza.Koryguj();
                        var form = new AnalizaZwrotuEditForm(analiza);
                        DialogResult result = form.ShowDialog();
                    }
                }

                this.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void kontrahentSelectControl_Changed(object sender, EventArgs e)
        {
            if (this.IsLoaded)
                this.Reload();
        }

        private void połaczButton_Click(object sender, EventArgs e)
        {
            if (this.DataGrid.SelectedRows.Count > 1 &&
                MessageBox.Show("czy napewno chcesz połaczyć zwroty?", "EnovaTools", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                Kontrahent kontrahent = null;
                StatusZwrotu status = null;
                List<Zwrot> zwroty = new List<Zwrot>();

                foreach (DataGridViewRow row in this.DataGrid.SelectedRows)
                {
                    if (row.DataBoundItem != null)
                    {
                        Zwrot zwrot = (Zwrot)row.DataBoundItem;
                        if (kontrahent == null)
                            kontrahent = zwrot.Kontrahent;
                        else if (kontrahent.ID != zwrot.KontrahentID)
                        {
                            MessageBox.Show("Nie można łaczyć zwrotów dla różnych kontrahentów");
                            return;
                        }

                        if (status == null)
                            status = zwrot.OstHistoriaZwrotu.Status;
                        else if (status.ID != zwrot.OstHistoriaZwrotu.StatusID)
                        {
                            MessageBox.Show("Nie można łaczyć zwrotów o różnym statusie");
                            return;
                        }

                        zwroty.Add(zwrot);
                    }
                }

                if (zwroty.Count > 1)
                {
                    string opis = "Połączone zwroty: ";
                    
                    Zwrot nowyZwrot = new Zwrot()
                    {
                        DataDodania = DateTime.Now,
                        DataModyfikacji = DateTime.Now,
                        Deleted = false,
                        Guid = Guid.NewGuid(),
                        IloscPaczek = zwroty.Sum(z=>z.IloscPaczek),
                        Kontrahent = kontrahent,
                        Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew
                    };



                    nowyZwrot.HistoriaZwrotu.Add(new HistoriaZwrotu()
                    {
                        Data = DateTime.Now,
                        Deleted = false,
                        Guid = Guid.NewGuid(),
                        Status = status,
                        Synchronize = (int)RowSynchronizeOld.NotsynchronizedNew,
                        Uzytkownik = User.LoginedUser
                    });

                    foreach (var zwrot in zwroty)
                    {
                        opis += zwrot.ID + ", ";

                        if (!string.IsNullOrEmpty(zwrot.Opis))
                        {
                            if (nowyZwrot.Opis == null)
                                nowyZwrot.Opis = "";
                            if (nowyZwrot.Opis != "")
                                nowyZwrot.Opis += "\r\n";
                            nowyZwrot.Opis += zwrot.Opis;
                        }

                        foreach (var poz in zwrot.Pozycje.Where(p=>p.Deleted == false && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).ToList())
                        {
                            nowyZwrot.Pozycje.Add(new PozycjaZwrotu()
                            {
                                Cena = poz.Cena,
                                Deleted = false,
                                Guid = Guid.NewGuid(),
                                Ident = nowyZwrot.GetMaxIdent()+1,
                                IlocsDeklarowana = poz.IlocsDeklarowana,
                                Ilosc = poz.Ilosc,
                                Opis = poz.Opis,
                                Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                                Towar = poz.Towar,
                                TowarNazwa = poz.TowarNazwa
                            });
                            poz.Deleted = true;
                        }

                        zwrot.Deleted = true;
                    }

                    if (!string.IsNullOrEmpty(nowyZwrot.Opis))
                        nowyZwrot.Opis += "\r\n";
                    nowyZwrot.Opis += opis;

                    nowyZwrot.PrzeliczWartosc();

                    Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();
                    Enova.Business.Old.Core.ContextManager.WebContext.Refresh(RefreshMode.StoreWins, nowyZwrot);

                    MessageBox.Show("Został stworzony nowy zwrot o numerze " + nowyZwrot.ID);
                    this.Reload();
                }
                
                
            }
        }

        private void towaryDoSkorygowaniaButton_Click(object sender, EventArgs e)
        {

            new TowaryNieskorygowaneForm().ShowDialog();
        }

        private void sezonComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.IsLoaded)
                Reload();
        }

    }
}
