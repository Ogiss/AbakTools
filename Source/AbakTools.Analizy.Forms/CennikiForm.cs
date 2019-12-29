using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Enova.Business.Old.Types;
//using Enova.Business.Old.DB.Services;

[assembly: BAL.Forms.MenuAction("Formularze\\Cenniki", typeof(AbakTools.Analizy.Forms.CennikiForm), Priority = 160)]

namespace AbakTools.Analizy.Forms
{
    public partial class CennikiForm : Enova.Forms.FormWithEnovaAPI
    {
        public CennikiForm()
        {
            InitializeComponent();
        }

        private void CennikiForm_Load(object sender, EventArgs e)
        {
            grupyBindingSource.DataSource = Session.GetModule<Enova.API.Business.BusinessModule>().FeatureDefs.CreateView()
                .SetFilter("TableName = 'Towary' AND Group = true AND StrictDictionary = true").Cast<Enova.API.Business.FeatureDefinition>().ToList().OrderBy(r => r.Name);

            var grupa = (Enova.API.Business.FeatureDefinition)grupaComboBox.SelectedItem;
            podgrupyBindingSource.DataSource = grupa.DictionaryList.ToList().OrderBy(r=>r.Value);

        }

        private void grupaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var grupa = (Enova.API.Business.FeatureDefinition)grupaComboBox.SelectedItem;
                podgrupyBindingSource.DataSource = grupa.DictionaryList.ToList().OrderBy(r=>r.Value);

            }
            catch { }
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            this.createReport();
        }

        private void createReport()
        {
            try
            {
                this.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                string grupa = ((Enova.API.Business.FeatureDefinition)grupaComboBox.SelectedItem).Name;
                string podgrupa = ((Enova.API.Business.DictionaryItem)podgrupaComboBox.SelectedItem).Value;

                List<RaportCennikiViewRow> reportSource = new List<RaportCennikiViewRow>();
                var towary = Session.GetModule<Enova.API.Towary.TowaryModule>().Towary.CreateView().SetFilter("Features.[" + grupa + "] = '" + podgrupa + "'")
                    .Cast<Enova.API.Towary.Towar>().ToList().OrderBy(r => r.Kod);

                foreach (var towar in towary)
                {
                    var dostawca = (string)towar.Features["DOSTAWCY"];
                    var cena = (Enova.API.Towary.Cena)towar.Ceny["Hurtowa"];
                    var stawka = towar.FromEnova<Enova.API.Types.IObjectBase>("DefinicjaStawki").FromEnova<Enova.API.Types.IObjectBase>("Stawka").FromEnova<Enova.API.Types.Percent>("Procent");
                    reportSource.Add(new RaportCennikiViewRow()
                    {
                        Dostawca = dostawca,
                        Kod = towar.Kod,
                        Nazwa = towar.Nazwa,
                        StawkaVat = (decimal)stawka,
                        CenaNetto = cena.Netto.Value,
                        CenaBrutto = cena.Brutto.Value
                    });
                }

                reportViewer.LocalReport.ReportPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports\\CennikiReport.rdlc");
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("RaportCennikiViewRow", reportSource));
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.Enabled = true;
            }
        }

        #region Nested types

        public class RaportCennikiViewRow
        {
            public string Dostawca { get; set; }
            public string Kod { get; set; }
            public string Nazwa { get; set; }
            public double? CenaNetto { get; set; }
            public double? CenaBrutto { get; set; }
            public decimal? StawkaVat { get; set; }
        }

        #endregion

    }
}
