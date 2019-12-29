using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data.Entity;
using Enova.Business.Old.DB;
using Enova.Business.Old.Types;
using DBWeb = Enova.Business.Old.DB.Web;

[assembly: BAL.Forms.MenuAction("Formularze\\Analiza wg grup towarowych", typeof(AbakTools.Analizy.Forms.AnalizaWgGrupTowarowychForm), Priority = 170)]

namespace AbakTools.Analizy.Forms
{
    public partial class AnalizaWgGrupTowarowychForm : Enova.Business.Old.Forms.DataBaseForm, Enova.API.Business.ISessionable
    {
        #region Fields

        private Enova.API.Business.Session session;

        #endregion

        public Enova.API.Business.Session Session
        {
            get { return session; }
        }

        new public Enova.Business.Old.DB.EnovaContext DataContext
        {
            get
            {
                return (Enova.Business.Old.DB.EnovaContext)base.DataContext;
            }
        }

        public AnalizaWgGrupTowarowychForm()
        {
            this.session = Enova.API.EnovaService.Instance.CreateSession();
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.reportViewer.RefreshReport();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (session != null)
                session.Dispose();
            session = null;
            base.OnClosed(e);
        }

        private void zatwierdźButton_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Cursor = Cursors.WaitCursor;

            string przedstawiciel = kontrahentSelect.Przedstawiciel;
            string trasa = kontrahentSelect.Trasa;
            Enova.API.CRM.Kontrahent kontrahent = kontrahentSelect.Kontrahent;
            FeatureDef featureDef = featureGroupSelect.DefinicjaCechy;
            string grupa = featureDef.Name;
            string słownik = featureDef.Dictionary;
            DateTime dateFrom1 = okres1dateSpan.DateFrom;
            DateTime dateTo1 = okres1dateSpan.DateTo;
            DateTime dateFrom2 = okres2DateSpan.DateFrom;
            DateTime dateTo2 = okres2DateSpan.DateTo;

            List<Enova.Business.Old.Types.ObrotyWgGrupTowarowych> reportSource = null;

            
            if (string.IsNullOrEmpty(trasa) || kontrahent != null)
            {

                var obroty = (from o in DataContext.ObrotyByKontrahent(przedstawiciel, kontrahent)
                               join f in DataContext.FeaturesByDef("Towary", grupa) on o.Towar.ID equals f.Parent
                               where o.Data >= dateFrom1 && o.Data <= dateTo1
                               group o by new { Grupa = f.Data, KontrahentID = o.RozchodKontrahent.ID } into g
                               select new ObrotyWgGrupTowarowych()
                               {
                                   GrupaTowarowa = g.Key.Grupa,
                                   IDKontrahenta = g.Key.KontrahentID,
                                   KodKontrahenta = g.FirstOrDefault().RozchodKontrahent.Kod,
                                   NazwaKontrahenta = g.FirstOrDefault().RozchodKontrahent.NazwaStr,
                                   ObrótNettoI = g.Sum(ob=>ob.RozchodWartosc),
                                   ObrótNettoII = 0
                               }).ToList();

                using (var lc = new DBWeb.WebContext())
                {
                    lc.CommandTimeout = int.MaxValue;
                    int? kontrahentID = kontrahent == null ? (int?)null : kontrahent.ID;
                    foreach (var f in DataContext.FeaturesByDef("Towary", grupa).ToList())
                    {
                        var towar = DataContext.Towary.Where(t => t.ID == f.Parent).FirstOrDefault();

                        var pozycje = (from pz in lc.PozycjeZamowien
                                       where
                                       pz.Zamowienie.DataDodania >= dateFrom2 && pz.Zamowienie.DataDodania <= dateTo2
                                       && pz.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete
                                       && pz.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.Notsaved
                                       && pz.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.Synchronizing
                                       && (pz.ProduktIndywidualny == null || pz.ProduktIndywidualny == false) && pz.Produkt.EnovaGuid == towar.Guid && pz.Ilosc > 0
                                       && (kontrahentID == null || pz.Zamowienie.Kontrahent.ID == kontrahentID)
                                       && (przedstawiciel == null || pz.Zamowienie.Kontrahent.Przedstawiciel.Kod == przedstawiciel)
                                       select pz).ToList();

                        foreach (var poz in pozycje)
                        {
                            var kontr = DataContext.Kontrahenci.Where(k => k.Guid == poz.Zamowienie.Kontrahent.Guid).FirstOrDefault();
                            decimal wartosc = (decimal)(poz.Ilosc == null ? 0 : poz.Ilosc.Value) * (poz.Cena == null ? 0 : poz.Cena.Value) * (1 - (poz.Rabat == null ? 0 : poz.Rabat.Value));

                            obroty.Add(new ObrotyWgGrupTowarowych()
                            {
                                GrupaTowarowa = f.Data,
                                IDKontrahenta = kontr.ID,
                                KodKontrahenta = kontr.Kod,
                                NazwaKontrahenta = kontr.NazwaStr,
                                ObrótNettoI = 0,
                                ObrótNettoII = decimal.Round(wartosc, 2)
                            });
                            
                        }
                    }
                }

                reportSource = (from o in obroty
                                group o by new { o.GrupaTowarowa, o.IDKontrahenta } into g
                                select new ObrotyWgGrupTowarowych()
                                {
                                    GrupaTowarowa = g.Key.GrupaTowarowa,
                                    IDKontrahenta = g.Key.IDKontrahenta,
                                    KodKontrahenta = g.FirstOrDefault().KodKontrahenta,
                                    NazwaKontrahenta = g.FirstOrDefault().NazwaKontrahenta,
                                    ObrótNettoI = g.Sum(o=>o.ObrótNettoI),
                                    ObrótNettoII = g.Sum(o=>o.ObrótNettoII)
                                }).ToList();

            }
            else
            {
            }

            reportViewer.LocalReport.ReportPath = "Reports\\ObrotyWgGrupTowarowychReport.rdlc";
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("ObrotyWgGrupTowarowych", reportSource));
            reportViewer.RefreshReport();


            Cursor = Cursors.Default;
            Enabled = true;
        }

    }
}
