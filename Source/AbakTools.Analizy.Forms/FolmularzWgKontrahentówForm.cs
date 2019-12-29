using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using Microsoft.Reporting.WinForms;
using System.Data.Objects;
using Enova.Business.Old.Types;
using Enova.Business.Old.DB;
//using Enova.Business.Old.DB.Services;
using Enova.Old.Magazyny;
using System.IO;
using Services = Enova.Forms.Services;

[assembly: BAL.Forms.MenuAction("Formularze\\Formularz zamówień wg kontrahentów", typeof(AbakTools.Analizy.Forms.FolmularzWgKontrahentówForm), Priority = 120)]

namespace AbakTools.Analizy.Forms
{
    public partial class FolmularzWgKontrahentówForm : Enova.Forms.FormWithEnovaAPI
    {
        private bool firePrzedstawicielChanged;
        private List<Enova.API.CRM.Kontrahent> kontrahenci;
        private IEnumerable<Enova.API.Business.DictionaryItem> podgrupy;
        private IEnumerable<Enova.API.Towary.Towar> towary;
        private bool rokWstecz;
        bool bezZerowych;
        List<RaportFormularzWgKontrahViewRow> reportSource;
        Enova.API.Business.FeatureDefinition grupa;
        DateTime dataOd;
        DateTime dataDo;
        string przedstawiciel;


        public FolmularzWgKontrahentówForm()
        {
            InitializeComponent();
        }

        private void FolmularzWgKontrahentówForm_Load(object sender, EventArgs e)
        {
            przedstawicieleLoad();
            trasyLoad();
            grupyLoad();
        }

        private void przedstawicieleLoad()
        {
            firePrzedstawicielChanged = false;
            var bm = Session.GetModule<Enova.API.Business.BusinessModule>();
            przedstawicielComboBox.Items.Add("(Wszyscy)");
            przedstawicielComboBox.SelectedIndex = 0;
            var dictionary = bm.Dictionary.CreateView().SetFilter("Category = 'F.przedstawiciel'").Cast<Enova.API.Business.DictionaryItem>().ToList().OrderBy(r => r.Value).Select(r => r.Value);
            przedstawicielComboBox.Items.AddRange(dictionary.ToArray());
            firePrzedstawicielChanged = true;
        }
        
        private void trasyLoad()
        {
            trasyComboBox.Items.Clear();
            trasyComboBox.Items.Add("(Wszystkie)");
            trasyComboBox.SelectedIndex = 0;
            if (przedstawicielComboBox.SelectedIndex > 0)
            {
                var pr = (string)przedstawicielComboBox.SelectedItem;
                var bm = Session.GetModule<Enova.API.Business.BusinessModule>();
                var parent = bm.Dictionary["F.TRASY"].ToList().Where(r => r.Value == pr).FirstOrDefault();
                if (parent != null)
                    trasyComboBox.Items.AddRange(bm.Dictionary.WgParent(parent).ToList().OrderBy(r => r.Value).Select(r => r.Value).ToArray());
            }

        }

        private void grupyLoad()
        {
            grupyBindingSource.DataSource = Enova.Forms.Services.BusinessService.GetGrupyTowarowe(Session).ToList().OrderBy(r => r.Name);
        }

        private void przedstawicielComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (firePrzedstawicielChanged)
                trasyLoad();
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            przedstawiciel = przedstawicielComboBox.SelectedIndex > 0 ? (string)przedstawicielComboBox.SelectedItem : null;
            string trasa = trasyComboBox.SelectedIndex == 0 ? null : (string)trasyComboBox.SelectedItem;
            grupa = (Enova.API.Business.FeatureDefinition)grupaComboBox.SelectedItem;
            dataOd = dataOdDateTimePicker.Value;
            dataDo = dataDoDateTimePicker.Value;
            bezZerowych = bezZerowychCheckBox.Checked;
            rokWstecz = rokWsteczCheckBox.Checked;
            kontrahenci = new List<Enova.API.CRM.Kontrahent>();
            string filter = null;
            if (!string.IsNullOrEmpty(trasa) && przedstawiciel != null)
                filter = "Features.[TRASY] = '" + "\\" + przedstawiciel + "\\" + trasa + "\\'";
            else if (przedstawiciel != null)
                filter = "Features.[przedstawiciel] = '" + przedstawiciel + "'";
            kontrahenci.AddRange(Services.CRMService.Kontrahenci.Get(Session, filter).ToList().OrderBy(r => r.Kod));
            podgrupy = grupa.DictionaryList.ToList();
            towary = Services.TowaryService.Towary.GetByFeature(Session, grupa.Name, "RESZTA").ToList().OrderBy(r => r.Kod);
            reportSource = new List<RaportFormularzWgKontrahViewRow>();
            Enova.Business.Old.Forms.ProgressForm.StartProgress(new ProgressHandler(this));
        }

        private void createReport()
        {
            if (rokWstecz)
                this.reportViewer.LocalReport.LoadReportDefinition(new MemoryStream(AbakTools.Analizy.Forms.Properties.Resources.FormularzWgKontrahReport2));
            else
                this.reportViewer.LocalReport.LoadReportDefinition(new MemoryStream(AbakTools.Analizy.Forms.Properties.Resources.FormularzWgKontrahReport));
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("przedstawiciel", przedstawiciel) });
            this.reportViewer.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("grupa", grupa.Name) });
            this.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("RaportFormularzWgKontrahViewRow", reportSource));
            this.reportViewer.RefreshReport();
        }

        private void enableRefresh()
        {
        }

        #region Nested Types

        public class ProgressHandler : Enova.Business.Old.Forms.ProgressFormHandler
        {
            private FolmularzWgKontrahentówForm parent;

            public ProgressHandler(FolmularzWgKontrahentówForm parent)
            {
                this.parent = parent;
            }

            public override void StartProcess()
            {
                //this.ProgressArgs.Progress2Visible = false;
                this.ProgressArgs.MaxProgress1 = parent.kontrahenci.Count;
                this.ProgressChanges();

                var dbCfg = Enova.Business.Old.Core.Configuration.GetDataBaseSettings("EnovaContext");
                if (dbCfg == null || string.IsNullOrEmpty(dbCfg.ProviderConnectionString))
                    throw new Exception("Brak skonfigurowanego połaczenia do bazy danych enova");
                var hm = parent.Session.GetModule<Enova.API.Handel.HandelModule>();
                var mm = parent.Session.GetModule<Enova.API.Magazyny.MagazynyModule>();
                var okres = mm.OkresyMag.Aktualny;
                var wzDef = hm.DefDokHandlowych.WydanieMagazynowe;

                var from = parent.dataOd;
                var to = parent.dataDo;

                using (var con = new SqlConnection(dbCfg.ProviderConnectionString))
                {
                    con.Open();
                    foreach (var k in parent.kontrahenci)
                    {
                        if (this.BackgroundWorker.CancellationPending)
                            break;

                        this.ProgressArgs.MaxProgress2 = parent.podgrupy.Count() + parent.towary.Count();
                        this.ProgressArgs.ValueProgress2 = 0;
                        this.ProgressChanges();

                        foreach (var p in parent.podgrupy)
                        {
                            if (p.Value == "RESZTA") continue;

                            double? obrotySuma = Services.MagazynyService.Obroty.Sumuj<double?>(con, "IloscValue", okres.ID, null, k.ID, null, from, to,null, parent.grupa.Name, p.Value);
                            double? obrotySumaWstecz = null;
                            if (parent.rokWstecz)
                                obrotySumaWstecz = Services.MagazynyService.Obroty
                                    .Sumuj<double?>(con, "IloscValue", okres.ID, null, k.ID, null, from.AddYears(-1), to.AddYears(-1), null, parent.grupa.Name, p.Value);
                            double? obrotyFV = Services.MagazynyService.Obroty
                                .Sumuj<double?>(con, "IloscValue", okres.ID, null, k.ID, wzDef.ID, from, to, null, parent.grupa.Name, p.Value);
                            double? obrotyFVWstecz = null;
                            if (parent.rokWstecz)
                                obrotyFVWstecz = Services.MagazynyService.Obroty.Sumuj<double?>(con, "IloscValue", okres.ID, null, k.ID, wzDef.ID,
                                    from.AddYears(-1), to.AddYears(-1), null, parent.grupa.Name, p.Value);

                            double? obrotyFK = null;
                            double? obrotyFKWstecz = null;

                            if (obrotySuma != null && obrotyFV != null)
                                obrotyFK = obrotySuma - obrotyFV;

                            if (obrotySumaWstecz != null && obrotyFVWstecz != null)
                                obrotyFKWstecz = obrotySumaWstecz - obrotyFVWstecz;

                            if (!parent.bezZerowych || (obrotySuma != null && obrotyFV != null))
                            {

                                parent.reportSource.Add(new RaportFormularzWgKontrahViewRow()
                                {
                                    IDKontrahenta = k.ID,
                                    KodKontrahenta = k.Kod,
                                    NazwaKontrahenta = k.Nazwa,
                                    IDTowaru = 0,
                                    KodTowaru = string.Empty,
                                    NazwaTowaru = p.Value,
                                    obrotySuma = obrotySuma,
                                    obrotyFV = obrotyFV,
                                    obrotyFK = obrotyFK,
                                    obrotySumaWstecz = obrotySumaWstecz,
                                    obrotyFVWstecz = obrotyFVWstecz,
                                    obrotyFKWstecz = obrotyFKWstecz
                                });
                            }
                            this.PerformStep2();
                        }

                        foreach (var t in parent.towary)
                        {
                            double? obrotySuma = Services.MagazynyService.Obroty.Sumuj<double?>(con, "IloscValue", okres.ID, null, k.ID, null, from, to, t.ID);
                            double? obrotySumaWstecz = null;
                            if (parent.rokWstecz)
                                obrotySumaWstecz = Services.MagazynyService.Obroty.Sumuj<double?>(con, "IloscValue", okres.ID, null, k.ID, null, from.AddYears(-1), to.AddYears(-1), t.ID);
                            double? obrotyFV = Services.MagazynyService.Obroty.Sumuj<double?>(con, "IloscValue", okres.ID, null, k.ID, wzDef.ID, from, to, t.ID);
                            double? obrotyFVWstecz = null;
                            if(parent.rokWstecz)
                                obrotyFVWstecz = Services.MagazynyService.Obroty.Sumuj<double?>(con, "IloscValue", okres.ID, null, k.ID, wzDef.ID, from.AddYears(-1), to.AddYears(-1), t.ID);

                            double? obrotyFK = null;
                            double? obrotyFKWstecz = null;

                            if (obrotySuma != null && obrotyFV != null)
                            {
                                obrotyFK = obrotySuma - obrotyFV;
                            }

                            if (obrotySumaWstecz != null && obrotyFVWstecz != null)
                            {
                                obrotyFKWstecz = obrotySumaWstecz - obrotyFVWstecz;
                            }


                            if (!parent.bezZerowych || (obrotySuma != null && obrotyFV != null))
                            {
                                parent.reportSource.Add(new RaportFormularzWgKontrahViewRow()
                                {
                                    IDKontrahenta = k.ID,
                                    KodKontrahenta = k.Kod,
                                    NazwaKontrahenta = k.Nazwa,
                                    IDTowaru = t.ID,
                                    KodTowaru = t.Kod,
                                    NazwaTowaru = t.Nazwa,
                                    obrotySuma = obrotySuma,
                                    obrotyFV = obrotyFV,
                                    obrotyFK = obrotyFK,
                                    obrotySumaWstecz = obrotySumaWstecz,
                                    obrotyFVWstecz = obrotyFVWstecz,
                                    obrotyFKWstecz = obrotyFKWstecz

                                });
                            }
                            this.PerformStep2();
                        }
                        this.PerformStep1(k.Kod);
                    }//end foreach
                }
            }

            public override void FinishProcess()
            {
                parent.createReport();
                parent.enableRefresh();
            }
        }

        #endregion

    }
}
