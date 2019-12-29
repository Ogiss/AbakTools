using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;
using Enova.Business.Old.Types;
using Enova.Forms.Services;
using Enova.API.Types;


[assembly: BAL.Forms.MenuAction("Formularze\\Formularz zamówień wg grup", typeof(AbakTools.Analizy.Forms.FormularzWgGrupForm), Priority = 110)]

namespace AbakTools.Analizy.Forms
{


    public partial class FormularzWgGrupForm : Enova.Business.Old.Forms.DataBaseFormWithEnovaAPI
    {
        List<RaportFormularzWgGrupViewRow> DataSource;
        Enova.API.Business.FeatureDefinition featureDef;
        string przedstawiciel;
        Enova.API.CRM.Kontrahent kontrahent;
        DateTime dataOd;
        DateTime dataDo;
        DateTime zwrotyOd;
        DateTime zwrotyDo;

        public bool FormularzZwrotów = false;

        public FormularzWgGrupForm()
        {
            InitializeComponent();
        }

        private void FormularzWgGrupForm_Load(object sender, EventArgs e)
        {

            grupyTowaroweBindingSource.DataSource = BusinessService.GetGrupyTowarowe(Session).OrderBy(r => r.Name).ToList();
            if (FormularzZwrotów)
                this.Text = "Formularz Zwrotów";

            int? grupa = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigInt("DEFAULT_PRODUCT_GROUP");
            DateTime? dateFrom = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("DEFAULT_SPAN_FROM");
            DateTime? dateTo = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("DEFAULT_SPAN_TO");
            DateTime? zwrotyFrom = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("DEFAULT_RETURN_SPAN_FROM");
            DateTime? zwrotyTo = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("DEFAULT_RETURN_SPAN_TO");

            if (grupa != null)
                grupyTowaroweComboBox.SelectedValue = grupa.Value;
            if (dateFrom != null)
                dtpSprzedazOd.Value = dateFrom.Value;
            if (dateTo != null)
                dtpSprzedazDo.Value = dateTo.Value;
            if (zwrotyFrom != null)
                dtpZwrotOd.Value = zwrotyFrom.Value;
            else if (dateFrom != null)
                dtpZwrotOd.Value = dateFrom.Value;
            if (zwrotyTo != null)
                dtpZwrotDo.Value = zwrotyTo.Value;
            else if (dateTo != null)
                dtpZwrotDo.Value = dateTo.Value;
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            featureDef = (Enova.API.Business.FeatureDefinition)grupyTowaroweComboBox.SelectedItem;
            kontrahent = kontrahentSelect.Kontrahent;
            przedstawiciel = kontrahentSelect.Przedstawiciel;
            dataOd = dtpSprzedazOd.Value;
            dataDo = dtpSprzedazDo.Value;
            zwrotyOd = dtpZwrotOd.Value;
            zwrotyDo = dtpZwrotDo.Value;
            DataSource = new List<RaportFormularzWgGrupViewRow>();
            Enova.Business.Old.Forms.ProgressForm.StartProgress(new ProgressHandler(this));
        }

        private void createReport()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            string grupa = featureDef == null ? "" : featureDef.Name;
            this.reportViewer.LocalReport.LoadReportDefinition(new MemoryStream(AbakTools.Analizy.Forms.Properties.Resources.FormularzWgGrupReport));
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("RaportFormularzWgGrupViewRow", DataSource));
            this.reportViewer.LocalReport.SetParameters(
                new ReportParameter[] {
                    new ReportParameter("tytul",this.FormularzZwrotów ? "Formularz Zwrotów" : "Formularz Zamówień"),
                    new ReportParameter("grupa",grupa),
                    new ReportParameter("kontrahent",   kontrahent == null ? "(wszyscy)" : kontrahent.Kod)
                });
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer.RefreshReport();

            this.Cursor = Cursors.Default;
            this.Enabled = true;

        }

        #region Nested Types

        public class ProgressHandler : Enova.Business.Old.Forms.ProgressFormHandler
        {
            FormularzWgGrupForm parent;

            public ProgressHandler(FormularzWgGrupForm parent)
            {
                this.parent = parent;
            }

            private double? roundDouble(double? d)
            {
                if (d != null)
                {
                    var r = (double)(d - (int)d);
                    if (r != 0)
                        return (double)decimal.Round((decimal)d, 2);
                }
                return d;
            }

            public override void StartProcess()
            {
                if (parent.featureDef == null)
                    return;
                var session = parent.Session;
                var bm = session.GetModule<Enova.API.Business.BusinessModule>();
                var tm = session.GetModule<Enova.API.Towary.TowaryModule>();
                var hm = session.GetModule<Enova.API.Handel.HandelModule>();
                var mm = session.GetModule<Enova.API.Magazyny.MagazynyModule>();
                var kontrahent = parent.kontrahent;
                var przedstawiciel = parent.przedstawiciel;
                var fDef = (Enova.API.Business.FeatureDefinition)parent.featureDef;
                var okres = mm.OkresyMag.Aktualny;
                var wzDef = hm.DefDokHandlowych.WydanieMagazynowe;
                var kwzDef = hm.DefDokHandlowych.KorektaWZ;
                var defCeny = tm.DefinicjeCen["Hurtowa"];

                var dictionaryItems = fDef.DictionaryList.ToList().Where(r => r.Value != "RESZTA").OrderBy(r => r.Value);
                var towary = tm.Towary.CreateView().SetFilter(string.Format("Features.[{0}] = '{1}'", fDef.Name, "RESZTA")).Cast<Enova.API.Towary.Towar>().ToList();

                this.ProgressArgs.MaxProgress1 = dictionaryItems.Count() + towary.Count;
                this.ProgressArgs.Progress2Visible = false;
                this.ProgressChanges();

                var dbCfg = Enova.Business.Old.Core.Configuration.GetDataBaseSettings("EnovaContext");

                if (dbCfg == null || string.IsNullOrEmpty(dbCfg.ProviderConnectionString))
                    throw new Exception("Brak skonfigurowanego połaczenia do bazy danych enova");

                using (var conn = new SqlConnection(dbCfg.ProviderConnectionString))
                {
                    conn.Open();

                    foreach (var d in dictionaryItems)
                    {
                        int minusYear = 0;
                        int kolejnosc = -1;
                        string kolor = "LightGrey";
                        string kom = string.Empty;
                        string nazwa = d.Value;
                        DateTime dDataOd = parent.dataOd;
                        DateTime dDataDo = parent.dataDo;
                        DateTime dZwrotyOd = parent.zwrotyOd;
                        DateTime dZwrotyDo = parent.zwrotyDo;

                        var match = Regex.Match(nazwa, @"!([0-9]+)!");
                        if (match.Success)
                        {
                            int.TryParse(match.Groups[1].Value, out kolejnosc);
                            nazwa = nazwa.Replace("!" + match.Groups[1].Value + "!", "");
                        }

                        match = Regex.Match(nazwa, @"@([a-zA-Z0-9#]+)@");
                        if (match.Success)
                        {
                            kolor = match.Groups[1].Value;
                            nazwa = nazwa.Replace("@" + match.Groups[1].Value + "@", "");
                        }

                        if (d.Value.EndsWith("-1"))
                        {
                            minusYear = -1;
                            kom = " - dane z przed 2 lat";
                        }
                        else if (d.Value.EndsWith("-2"))
                        {
                            minusYear = -2;
                            kom = " - dane z przed 3 lat";
                        }
                        else if (d.Value.EndsWith("-3"))
                        {
                            minusYear = -3;
                            kom = " - dane z przed 4 lat";
                        }
                        else if (d.Value.EndsWith("-4"))
                        {
                            minusYear = -4;
                            kom = " - dane z przed 5 lat";
                        }

                        if (minusYear != 0)
                        {
                            dDataOd = dDataOd.AddYears(minusYear);
                            dDataDo = dDataDo.AddYears(minusYear);
                            dZwrotyOd = dZwrotyOd.AddYears(minusYear);
                            dZwrotyDo = dZwrotyDo.AddYears(minusYear);
                        }

                        this.ProgressArgs.TextProgress1 = "Grupa: " + nazwa;
                        this.ProgressChanges();

                        var obrWZ = MagazynyService.Obroty.Sumuj<double?>(conn, "IloscValue", okres.ID, null, kontrahent.ID, wzDef.ID, dDataOd, dDataDo, null, fDef.Name, d.Value);
                        var obrKWZ = MagazynyService.Obroty.Sumuj<double?>(conn, "IloscValue", okres.ID, null, kontrahent.ID, kwzDef.ID, dZwrotyOd, dZwrotyDo, null, fDef.Name, d.Value);

                        if (minusYear != 0)
                            nazwa = nazwa.Substring(0, nazwa.Length - 2) + kom;

                        parent.DataSource.Add(new RaportFormularzWgGrupViewRow()
                        {
                            Grupa = "Zgrupowane",
                            Nazwa = nazwa,
                            ObrotFV = obrWZ == null || obrWZ.Value == 0 ? (double?)null : obrWZ.Value,
                            ObrotFK = obrKWZ == null || obrKWZ.Value == 0 ? (double?)null : obrKWZ,
                            TowarIndywidualny = true,
                            StandardowaIlosc = null,
                            Kolejnosc = kolejnosc,
                            Kolor = kolor
                        });

                        this.PerformStep1();
                    }//end foreeach != RESZTA


                    foreach (var towar in towary)
                    {
                        this.ProgressArgs.TextProgress1 = "Towar: " + towar.Kod;
                        this.ProgressChanges();

                        var obliczObrotyZ = (Enova.API.Towary.Towar)towar.Features["OBLICZ OBROTY Z"];
                        if (obliczObrotyZ == null)
                            obliczObrotyZ = towar;

                        double? obrotFV = MagazynyService.Obroty.Sumuj<double?>(conn, "IloscValue", okres.ID, null, kontrahent.ID, wzDef.ID, parent.dataOd, parent.dataDo, obliczObrotyZ.ID);
                        double? obrotKFV = MagazynyService.Obroty.Sumuj<double?>(conn, "IloscValue", okres.ID, null, kontrahent.ID, kwzDef.ID, parent.zwrotyOd, parent.zwrotyDo, obliczObrotyZ.ID);
                        
                        string dostawca = (string)towar.Features["DOSTAWCY"];
                        int? iloscFV = null;

                        string nazwa = towar.Prefix() + towar.Nazwa + towar.Suffix() + (towar.Nowosc() ? " - NOWOŚĆ" : "");
                        double? mnoznik = towar.MnoznikObrotow();
                        double procent = towar.ProcentObrotow();

                        mnoznik = mnoznik == null ? 1 : mnoznik;

                        var cena = towar.Ceny[defCeny];

                        parent.DataSource.Add(new RaportFormularzWgGrupViewRow()
                        {
                            TowarGuid = towar.Guid,
                            TowarID = towar.ID,
                            Grupa = dostawca,
                            Kod = towar.Kod,
                            Nazwa = nazwa,
                            IloscFV = iloscFV,
                            ObrotFV = roundDouble(obrotFV * mnoznik.Value * procent),
                            ObrotFK = roundDouble(obrotKFV * mnoznik * procent),
                            StandardowaIlosc = cena.StandardowaIlosc,
                            CenaNetto = (decimal)cena.Netto.Value,
                            Kolejnosc = towar.KolejnoscNaFormularzu(),
                            Kolor = ""
                        });

                        PerformStep1();
                    }//END foreach towary
                }
            }

            public override void FinishProcess()
            {
                parent.createReport();
            }
        }

        #endregion
    }
}
