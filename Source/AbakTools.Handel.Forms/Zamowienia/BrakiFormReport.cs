using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Enova.Business.Old.DB;

[assembly: BAL.Forms.MenuAction("Narzędzia\\Raport braków", typeof(AbakTools.Zamowienia.Forms.BrakiForm), Priority = 620)]

namespace AbakTools.Zamowienia.Forms
{
    public partial class BrakiFormReport : Form
    {
        public BrakiFormReport()
        {
            InitializeComponent();
        }

        private void BrakiForm_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                featureSelect.LoadGrupy();
                this.reportViewer.RefreshReport();
            }
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            CreateReport();
        }

        private void CreateReport()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            DateTime dataOd = dataOdDateTimePicker.Value.Date;
            DateTime dataDo = dataDoDateTimePicker.Value.Date.AddDays(1);
            string przedstawiciel = przedstawicielSelect.Przedstawiciel;
            FeatureDef fd = featureSelect.FeatureDef;
            Dictionary disc = featureSelect.Dictionary;

            List<BrakiRow> ds;

            if (fd != null && fd.ID > 0 && disc != null && disc.ID > 0)
            {
                var dc = Enova.Business.Old.Core.ContextManager.WebContext;
                var ec = Enova.Business.Old.Core.ContextManager.DataContext;
                var guids = (from t in ec.Towary
                             join f in ec.Features on
                             new { ParentType = "Towary", Parent = t.ID, Name = fd.Name } equals
                             new { ParentType = f.ParentType, Parent = f.Parent, Name = f.Name }
                             where f.Data == disc.Value
                             select t.Guid).ToArray();

                ds = (from p in Enova.Business.Old.Core.ContextManager.WebContext.PozycjeZamByPrzedstawiciel(przedstawiciel)
                      where p.Zamowienie.DataDodania >= dataOd && p.Zamowienie.DataDodania < dataDo && p.Ilosc != p.IloscOrg && p.IloscOrg != null
                      && p.Produkt!= null && p.Produkt.EnovaGuid != null && guids.Contains(p.Produkt.EnovaGuid.Value)
                      select new BrakiRow()
                      {
                          KontrahentKod = p.Zamowienie.Kontrahent.Kod,
                          KontrahentNazwa = p.Zamowienie.Kontrahent.Nazwa,
                          TowarKod = p.Produkt != null ? p.Produkt.Kod : "",
                          TowarNazwa = p.Produkt != null ? p.Produkt.Nazwa : p.ProduktNazwa,
                          Opis = p.Opis,
                          AtrybutProduktu = p.AtrybutProduktu,
                          Ilosc = (double)(p.Ilosc - p.IloscOrg),
                          Cena = (decimal)(p.Cena != null ? p.Cena.Value : 0M)
                      }
                               ).ToList();

            }
            else
            {

                ds = (from p in Enova.Business.Old.Core.ContextManager.WebContext.PozycjeZamByPrzedstawiciel(przedstawiciel)
                               where p.Zamowienie.DataDodania >= dataOd && p.Zamowienie.DataDodania < dataDo && p.Ilosc != p.IloscOrg && p.IloscOrg != null
                               select new BrakiRow()
                               {
                                   KontrahentKod = p.Zamowienie.Kontrahent.Kod,
                                   KontrahentNazwa = p.Zamowienie.Kontrahent.Nazwa,
                                   TowarKod = p.Produkt != null ? p.Produkt.Kod : "",
                                   TowarNazwa = p.Produkt != null ? p.Produkt.Nazwa : p.ProduktNazwa,
                                   Opis = p.Opis,
                                   AtrybutProduktu = p.AtrybutProduktu,
                                   Ilosc = (double)(p.Ilosc - p.IloscOrg),
                                   Cena = (decimal)(p.Cena != null ? p.Cena.Value : 0M)
                               }
                               ).ToList();
            }

            this.reportViewer.LocalReport.ReportPath = "BrakiReport.rdlc";
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("BrakiRow", ds));
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer.RefreshReport();

            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }

        private void featureSelect_Changed(object sender, EventArgs e)
        {

        }
    }
}
