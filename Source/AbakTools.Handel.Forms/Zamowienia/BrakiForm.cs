using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;
using Microsoft.Reporting.WinForms;
using DBWeb = Enova.Business.Old.DB.Web;

namespace AbakTools.Zamowienia.Forms
{
    public partial class BrakiForm : Form
    {

        public DBWeb.Zamowienie Zamowienie { get; set; }

        public BrakiForm()
        {
            InitializeComponent();
        }

        private void BrakiForm_Load(object sender, EventArgs e)
        {
            if (Zamowienie != null)
            {
                bindingSource.DataSource = Zamowienie;
                CreateReport();
            }
        }

        private void CreateReport()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {

                var pozycje = (from p in Zamowienie.PozycjeZamowienia
                               where p.Ilosc != p.IloscOrg && p.IloscOrg != null
                               select new BrakiRow()
                               {
                                   KontrahentKod = p.Zamowienie.Kontrahent.Kod,
                                   KontrahentNazwa = p.Zamowienie.Kontrahent.Nazwa,
                                   TowarKod = p.Produkt != null ? p.Produkt.Kod : "",
                                   TowarNazwa = p.Produkt != null ? p.Produkt.Nazwa : p.ProduktNazwa,
                                   Opis = p.Opis,
                                   AtrybutProduktu = p.AtrybutProduktu,
                                   Ilosc = (double)(p.Ilosc - p.IloscOrg),
                                   Cena = p.Cena == null ? 0M : p.Cena.Value
                               }
                               ).ToList();


                //this.reportViewer.LocalReport.ReportPath = "Reports\\BrakiReport.rdlc";
                this.reportViewer.LocalReport.ReportEmbeddedResource = "AbakTools.Handel.Forms.Reports.BrakiReport.rdlc";
                this.reportViewer.LocalReport.DataSources.Clear();
                this.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("BrakiRow", pozycje));
                this.reportViewer.LocalReport.SetParameters(new ReportParameter("ind", "true"));
                this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                //this.reportViewer.ZoomMode = ZoomMode.Percent;
                //this.reportViewer.ZoomPercent = 100;
                this.reportViewer.ZoomMode = ZoomMode.PageWidth;
                this.reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }

        private void zamowienieButton_Click(object sender, EventArgs e)
        {
            if (Zamowienie != null)
            {
                if (Zamowienie.ZamowionoBraki != null && Zamowienie.ZamowionoBraki.Value)
                {
                    DialogResult result = MessageBox.Show("Do tego zamówienia wygenerowano już zamówienie braków o numerze " + Zamowienie.ZamowienieBrakow.ToString()+
                        "\r\nCzy napewno chcesz wygenerować nowe zamówienie?",
                        "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if(result == DialogResult.No)
                        return;
                }

                var kontr = Enova.Business.Old.Core.ContextManager.DataContext.Kontrahenci.Where(r => r.Guid == Zamowienie.Kontrahent.Guid).FirstOrDefault();
                if(kontr.BlokadaSprzedazy != null && kontr.BlokadaSprzedazy.Value)
                {
                    BAL.Forms.FormManager.Alert(string.Format("Kontrahent {0} jest zablokowany i nie może być wybrany na formularzu zamówienia", kontr.Kod));
                    return;
                }

                Enabled = false;
                Cursor = Cursors.WaitCursor;
                try
                {
                    Zamowienie.PrzeliczZamowienie();
                    var pozycjeBrakow = Zamowienie.PozycjeZamowienia.Where(p => p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && p.IloscOrg != null && p.IloscOrg > p.Ilosc).ToList();
                    if(pozycjeBrakow.Count > 0)
                    //if (Zamowienie.WartoscBraki != null && Zamowienie.WartoscBraki < 0)
                    {
                        DBWeb.Zamowienie noweZam = new DBWeb.Zamowienie()
                        {
                            DataDodania = DateTime.Now,
                            AdresFaktury = Zamowienie.AdresFaktury,
                            AdresWysylki = Zamowienie.AdresWysylki,
                            Blokada = false,
                            BlokadaEdycji = false,
                            GUID = Guid.NewGuid(),
                            Kontrahent = Zamowienie.Kontrahent,
                            RodzajTransportu = Enova.Business.Old.Types.RodzajTransportu.NieWybrano,
                            NaKiedy = DateTime.Now,
                            Stamp = DateTime.Now,
                            Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.Notsaved
                        };

                        int ident = 1;

                        foreach (var poz in pozycjeBrakow)
                        {
                            noweZam.PozycjeZamowienia.Add(new DBWeb.PozycjaZamowienia()
                            {
                                AtrybutProduktu = poz.AtrybutProduktu,
                                PSID = 0,
                                Cena = poz.Cena,
                                Ident = ident++,
                                Ilosc = poz.IloscOrg - poz.Ilosc,
                                IloscOrg = poz.IloscOrg - poz.Ilosc,
                                Opis = poz.Opis,
                                Produkt = poz.Produkt,
                                ProduktIndywidualny = poz.ProduktIndywidualny,
                                ProduktNazwa = poz.ProduktNazwa,
                                Stamp = DateTime.Now,
                                StawkaVatSymbol = poz.StawkaVatSymbol,
                                StawkaVatValue = poz.StawkaVatValue,
                                Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew
                            });
                        }

                        /*
                        Pracownik pracownik = Enova.Business.Old.Core.ContextManager.WebContext.Pracownicy
                            .Where(p => p.Kod == Enova.Business.Old.DB.Web.User.LoginedUser.Login || p.Kod == Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperator).FirstOrDefault();
                        StatusZamowienia status = Enova.Business.Old.Core.ContextManager.WebContext.StatusyZamowien
                            .Where(s => s.NoweZamowienie == true).FirstOrDefault();
                        noweZam.HistoriaZamowienia.Add(new HistoriaZamowienia()
                        {
                            DataDodania = DateTime.Now,
                            Pracownik = pracownik,
                            Status = status,
                            Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronize.NotsynchronizedNew
                        });
                         */

                        noweZam.ZmienStatus(Enova.Business.Old.Types.StatusyZamowieniaTyp.NoweZamowienie);

                        Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();
                        noweZam.Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew;
                        Zamowienie.ZamowionoBraki = true;
                        Zamowienie.ZamowienieBrakow = noweZam.ID;
                        Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();

                        MessageBox.Show("Stworzono nowe zamówienie o numerze " + noweZam.NumerPelny, "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    Cursor = Cursors.Default;
                    Enabled = true;
                }
            }
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            if (Zamowienie != null)
            {
                Zamowienie.AnulujBraki = true;
                Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();
            }
            this.Close();
        }

    }
}
