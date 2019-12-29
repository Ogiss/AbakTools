using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AbakTools.Forms;
using Enova.Business.Old.Zwroty;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace AbakTools.Zwroty.Forms
{
    public partial class WystawKorektyForm : Form
    {
        public  AnalizaZwrotu AnalizaZwrotu;
        private bool closeForm;

        public WystawKorektyForm()
        {
            InitializeComponent();
            this.dataGridView.AutoGenerateColumns = false;
        }

        private void WystawKorektyForm_Load(object sender, EventArgs e)
        {
            if (AnalizaZwrotu != null)
            {
                bindingSource.DataSource = AnalizaZwrotu.Dokumenty.Where(d => d.WystawicKorekte);
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (closeForm)
            {
                this.Close();
                return;
            }

            try
            {

                this.Cursor = Cursors.WaitCursor;

                var service = Enova.API.EnovaService.Instance;

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    DokumentAnalizyZwrotu d = (DokumentAnalizyZwrotu)row.DataBoundItem;
                    if (d.WystawicKorekte && d.KorektaGuid == Guid.Empty)
                    {
                        Enova.API.Handel.DokumentHandlowy korekta = null;
                        korekta = d.WystawKorekte(d.DataKorekty, d.ZatwierdzicKorekte);
                        if (korekta != null)
                        {
                            d.KorektaGuid = korekta.Guid;
                            row.Cells[this.numerKorektyColumn.Index].Value = korekta.NumerPelny;
                            var korektaZwrot = new Enova.Business.Old.DB.Web.KorektaZwrot()
                            {
                                Zwrot = AnalizaZwrotu.Zwrot,
                                KorektaGuid = korekta.Guid,
                                KorektaNumerPelny = korekta.NumerPelny
                            };
                            Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
                            bool wCalosci = true;

                            foreach (var poz in d.PozycjeKorygowane)
                            {
                                var pozycjeZwrotu = AnalizaZwrotu.Zwrot.Pozycje.Where(p => p.Towar.EnovaGuid == poz.Towar.Guid).ToList();
                                double ilosc = d.IlosciKorygowane[poz.Guid];

                                foreach (var pozycjaZwrotu in pozycjeZwrotu)
                                {
                                    if (pozycjaZwrotu.IloscSkorygowana == null)
                                        pozycjaZwrotu.IloscSkorygowana = 0;

                                    var pozycjaKorektaZwrot = new Enova.Business.Old.DB.Web.KorektaPozycjaZwrotu()
                                    {
                                        Korekta = korektaZwrot,
                                        PozycjaIdent = pozycjaZwrotu.Ident,
                                        PozycjaZwrotu = pozycjaZwrotu
                                    };

                                    if (pozycjaZwrotu.Ilosc - pozycjaZwrotu.IloscSkorygowana >= ilosc)
                                    {
                                        pozycjaKorektaZwrot.Ilosc = ilosc;
                                        pozycjaZwrotu.IloscSkorygowana += ilosc;
                                        ilosc = 0;
                                    }
                                    else
                                    {
                                        pozycjaKorektaZwrot.Ilosc = pozycjaZwrotu.Ilosc - pozycjaZwrotu.IloscSkorygowana.Value;
                                        pozycjaZwrotu.IloscSkorygowana += pozycjaKorektaZwrot.Ilosc;
                                        ilosc -= pozycjaKorektaZwrot.Ilosc;
                                    }
                                    Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
                                    if (ilosc == 0)
                                        break;
                                }
                                if (wCalosci && ilosc > 0)
                                    wCalosci = false;
                            }

                            if (d.ZatwierdzicKorekte && !korekta.Magazyn.Firmowy)
                            {
                                if (BAL.Forms.FormManager.Confirm(
                                    string.Format("Korekta {0} została wystawiona z magazynu {1}.\r\nCzy wygenerować dokument przesunięcia towaru na główny magazyn?",
                                    korekta.NumerPelny, korekta.Magazyn.Nazwa)))
                                {
                                    Enova.API.Handel.DokumentHandlowy dh = null;
                                    using (var session = service.CreateSession())
                                    {
                                        var hm = session.GetModule<Enova.API.Handel.HandelModule>();
                                        var mm = session.GetModule<Enova.API.Magazyny.MagazynyModule>();
                                        var crm = session.GetModule<Enova.API.CRM.CRMModule>();
                                        var tm = session.GetModule<Enova.API.Towary.TowaryModule>();
                                        
                                        var def = hm.DefDokHandlowych.Przesuniecie;
                                        using (var t = session.CreateTransaction())
                                        {
                                            dh = service.CreateObject<Enova.API.Handel.DokumentHandlowy>();
                                            hm.DokHandlowe.AddRow(dh);
                                            dh.Definicja = def;
                                            dh.Magazyn = (Enova.API.Magazyny.Magazyn) mm.Magazyny[korekta.Magazyn.ID];
                                            dh.MagazynDo = mm.Magazyny.Firma;
                                            if ( korekta.Data >= DateTime.Now.Date)
                                                dh.Data = korekta.Data;

                                            dh.Opis = "Przesunięcie do korekty " + korekta.NumerPelny;

                                            foreach (var poz in korekta.Pozycje)
                                            {
                                                var ilosc = (int)poz.Ilosc;
                                                var iloscKor = poz.PozycjaKorygowana != null ? (int)poz.PozycjaKorygowana.Ilosc : 0;
                                                if (poz.PozycjaKorygowana != null && ilosc < iloscKor)
                                                {
                                                    //var pdh = dh.NowaPozycja((Enova.API.Towary.Towar)tm.Towary[poz.Towar.ID], poz.PozycjaKorygowana.Ilosc - poz.Ilosc);
                                                    var pdh = (Enova.API.Handel.PozycjaDokHandlowego)Enova.API.EnovaService.Instance.CreateObject(typeof(Enova.API.Handel.PozycjaDokHandlowego), null, new object[] { dh });
                                                    hm.PozycjeDokHan.AddRow(pdh);
                                                    

                                                    pdh.Towar = poz.Towar;
                                                    pdh.Ilosc = iloscKor - ilosc;
                                                    //pdh.Ilosc = poz.Ilosc;
                                                    //pdh.IloscMagazynu = poz.Ilosc;
                                                    var cena = poz.Towar.OstatniaCenaZakupu;
                                                    if (cena != null)
                                                        pdh.Cena = (decimal)cena.Netto.Value;
                                                    
                                                }
                                            }
                                            session.EventsInvoke();
                                            dh.Stan = Enova.API.Handel.StanDokumentuHandlowego.Zatwierdzony;
                                            //dh.Stan = Enova.API.Handel.StanDokumentuHandlowego.Bufor;
                                            t.Commit();
                                        }
                                        session.Save();
                                    }
                                }
                            }
                        }
                    }
                }

                var nieskor = this.AnalizaZwrotu.Zwrot.Pozycje.Where(p => p.Deleted == false && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete
                    && (p.IloscSkorygowana == null || p.IloscSkorygowana < p.Ilosc)).Count();
                AnalizaZwrotu.Zwrot.SkorygowanyWCalosci = nieskor == 0;
                AnalizaZwrotu.Zwrot.SetStatus(Enova.Business.Old.DB.Web.TypStatusuZwrotu.Załatwiony);
                Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
                Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, AnalizaZwrotu.Zwrot);


                this.closeForm = true;
                acceptButton.Text = "Zamknij";
                this.printButton.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            if (AnalizaZwrotu.Zwrot.SkorygowanyWCalosci != null && !AnalizaZwrotu.Zwrot.SkorygowanyWCalosci.Value)
            {
                if (MessageBox.Show("Zwrot nie został skorygowany w całości.\r\nCzy wygenerować zwrot z pozycji nieskorygowanych?", "AbakTools",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    generujZwrotZNieskorygowanych();
                }
            }
        }

        private void completedProcess()
        {
            if (this.InvokeRequired)
            {
                var d = new Action(this.completedProcess);
                this.Invoke(d);
            }
            else
            {
                this.closeForm = true;
                acceptButton.Text = "Zamknij";
                this.printButton.Visible = true;
            }
        }

        private void generujZwrotZNieskorygowanych()
        {
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;

            var dc = Enova.Business.Old.Core.ContextManager.WebContext;

            Zwrot nowyZwrot = new Zwrot()
            {
                Guid = Guid.NewGuid(),
                DataDodania = DateTime.Now,
                DataModyfikacji = DateTime.Now,
                Deleted = false,
                IloscPaczek = 0,
                SkorygowanyWCalosci = false,
                Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                Kontrahent = this.AnalizaZwrotu.Zwrot.Kontrahent,
                Opis = "Zwrot z pozycji nieskorygowanych ze zwrotu " + this.AnalizaZwrotu.Zwrot.ID,
                OstatniStatus = dc.StatusyZwrotow.Where(sz => sz.Typ == (int)TypStatusuZwrotu.ZwrotZNieskorygowanych).FirstOrDefault()
            };

            nowyZwrot.HistoriaZwrotu.Add(new HistoriaZwrotu()
            {
                Data = DateTime.Now,
                Deleted = false,
                Guid = Guid.NewGuid(),
                Status = dc.StatusyZwrotow.Where(sz => sz.Typ == (int)TypStatusuZwrotu.ZwrotZNieskorygowanych).FirstOrDefault(),
                Synchronize = (int)RowSynchronizeOld.NotsynchronizedNew,
                Uzytkownik = User.LoginedUser
            });

            dc.OptimisticSaveChanges();
            dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, nowyZwrot);

            var pozycje = this.AnalizaZwrotu.Zwrot.Pozycje.Where(p => p.Deleted == false && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete
                && (p.IloscSkorygowana == null || p.IloscSkorygowana < p.Ilosc)).ToList();

            foreach (var poz in pozycje)
            {
                var nowaPozycja = new PozycjaZwrotu()
                {
                    Cena = poz.Cena,
                    Guid = Guid.NewGuid(),
                    Deleted = false,
                    Ilosc = poz.IloscPozostaloDoSkorygowania,
                    IloscSkorygowana = 0,
                    Opis = poz.Opis,
                    Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                    Towar = poz.Towar,
                    TowarNazwa = poz.TowarNazwa
                };
                nowyZwrot.Pozycje.Add(nowaPozycja);
                if (poz.IloscOrg == null)
                    poz.IloscOrg = poz.Ilosc;
                poz.Ilosc -= nowaPozycja.Ilosc;
            }

            this.AnalizaZwrotu.Zwrot.SkorygowanyWCalosci = true;
            this.AnalizaZwrotu.Zwrot.PrzeliczWartosc();
            nowyZwrot.ReIdentPozycje();
            nowyZwrot.PrzeliczWartosc();
            dc.OptimisticSaveChanges();
            dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, this.AnalizaZwrotu.Zwrot);
            dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, nowyZwrot);

            this.Enabled = true;
            this.Cursor = Cursors.Default;

        }

        private void printButton_Click(object sender, EventArgs e)
        {
            if (this.dataGridView.CurrentRow != null)
            {
                DokumentAnalizyZwrotu dokument = (DokumentAnalizyZwrotu)this.dataGridView.CurrentRow.DataBoundItem;
                var template = Enova.Business.Old.Core.Configuration.GetSetting("EnovaFKReport");
                if (string.IsNullOrEmpty(template))
                    throw new Exception("Nie skonfigurowano wzorca wydruku dla korekty sprzedaży");
                template = Path.IsPathRooted(template) ? template : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aspx", template);
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    using (var session = Enova.API.EnovaService.Instance.CreateSession())
                        session.GetModule<Enova.API.Handel.HandelModule>().DrukujDokument(null, dokument.KorektaGuid, template);
                    //Enova.API.EnovaServiceAttribute.EnovaService.HandelModule.DrukujDokument(this, dokument.KorektaGuid, template);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
                
            }

        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
