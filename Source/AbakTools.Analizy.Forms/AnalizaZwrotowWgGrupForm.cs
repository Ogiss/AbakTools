using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
/*
using Enova.Business.Old.DB;
using Enova.Business.Old.Types;
using Enova.Business.Old.DB.Services;
 */
using Enova.Forms.Services;

//[assembly: BAL.Forms.MenuAction("Formularze\\Analiza zwrotów wg grup", typeof(AbakTools.Analizy.Forms.AnalizaZwrotowWgGrupForm), Priority = 165)]

namespace AbakTools.Analizy.Forms
{
    public partial class AnalizaZwrotowWgGrupForm : Enova.Forms.FormWithEnovaAPI
    {
        private List<AnalizaZwrotowWgGrupRow> reportSource;
        List<Enova.API.CRM.Kontrahent> kontrahenci;
        private string przedstawiciel;
        private string trasa;
        //private GrupyTowaroweViewRow grupaTow;
        private Enova.API.Business.FeatureDefinition featureDef;
        private string grupa;
        private DateTime data1Od;
        private DateTime data1Do;
        private DateTime data2Od;
        private DateTime data2Do;
        private bool tylkoPoZwrotach;
        private bool tylkoZObrotemI;
        private int sortujWg;
        private bool sortujMalejaco;


        public AnalizaZwrotowWgGrupForm()
        {
            InitializeComponent();
        }

        private void AnalizaZwrotowWgGrup_Load(object sender, EventArgs e)
        {
            //grupyTowaroweBindingSource.DataSource = FeatureDefsService.GetGrupyTowaroweQuery().OrderBy(g => g.Nazwa).ToList();
            grupyTowaroweBindingSource.DataSource = BusinessService.GetGrupyTowarowe(Session);
            sortujWgComboBox.SelectedIndex = 0;
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            przedstawiciel = kontrahentSelect.Przedstawiciel;
            trasa = kontrahentSelect.Trasa;
            //grupaTow = (GrupyTowaroweViewRow)grupyTowaroweComboBox.SelectedItem;
            featureDef = (Enova.API.Business.FeatureDefinition)grupyTowaroweComboBox.SelectedItem;
            //grupa = grupaTow == null ? "" : grupaTow.Nazwa;
            grupa = featureDef == null ? null : featureDef.Name;
            data1Od = dtpData1Od.Value.Date;
            data1Do = dtpData1Do.Value.Date;
            data2Od = dtpData2Od.Value.Date;
            data2Do = dtpData2Do.Value.Date;
            tylkoPoZwrotach = tylkoPoZwotachCheckBox.Checked;
            tylkoZObrotemI = tylkoZObrotemICheckBox.Checked;
            sortujWg = sortujWgComboBox.SelectedIndex;
            sortujMalejaco = sortujMalejacoCheckBox.Checked;

            var dc = Enova.Business.Old.Core.ContextManager.DataContext;
            kontrahenci = new List<Enova.API.CRM.Kontrahent>();
            var crm = Session.GetModule<Enova.API.CRM.CRMModule>();

            if (kontrahentSelect.KontrahenciIDChecked != null && kontrahentSelect.KontrahenciIDChecked.Count > 0)
            {
                foreach (var id in kontrahentSelect.KontrahenciIDChecked)
                    kontrahenci.Add((Enova.API.CRM.Kontrahent)crm.Kontrahenci[(int)id]);
            }
            else
            {
                string filter = null;
                if (!string.IsNullOrEmpty(trasa) && !string.IsNullOrEmpty(przedstawiciel))
                {
                    var path = "\\" + przedstawiciel + "\\" + trasa + "\\";
                    filter = "Features.[TRASY] = '" + path + "'";
                }
                else if (!string.IsNullOrEmpty(przedstawiciel))
                    filter = "Features.[przedstawiciel] = '" + przedstawiciel + "'";
                var view = crm.Kontrahenci.CreateView();
                view.Filter = filter;
                foreach (Enova.API.CRM.Kontrahent k in view)
                    kontrahenci.Add(k);
            }

            reportSource = new List<AnalizaZwrotowWgGrupRow>();
            Enova.Business.Old.Forms.ProgressForm.StartProgress(new ProgressHandler(this));

        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            createReport();
        }

        private void setSortSettings()
        {
            if (this.InvokeRequired)
            {
                var d = new Action(setSortSettings);
                this.Invoke(d);
            }
            else
            {
                sortujWg = sortujWgComboBox.SelectedIndex;
                sortujMalejaco = sortujMalejacoCheckBox.Checked;
            }
        }

        private void enableRefesh()
        {
            if (this.InvokeRequired)
            {
                var d = new Action(setSortSettings);
                this.Invoke(d);
            }
            else
            {
                if (!refreshButton.Enabled)
                    refreshButton.Enabled = true;
            }
        }

        private void createReport()
        {
            setSortSettings();
            if (sortujWg > 0 || sortujMalejaco)
                this.reportSource = sortujMalejaco ? this.reportSource.OrderByDescending(this.keySelector).ToList() : this.reportSource.OrderBy(this.keySelector).ToList();

            //this.reportViewer.LocalReport.ReportPath = "Reports\\AnalizaZwrotowWgGrupReport.rdlc";
            this.reportViewer.LocalReport.ReportEmbeddedResource = "AbakTools.Analizy.Forms.Reports.AnalizaZwrotowWgGrupReport.rdlc";
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("AnalizaZwrotowWgGrupRow", this.reportSource));
            this.reportViewer.LocalReport.SetParameters(
              new ReportParameter[] {
                    new ReportParameter("grupa", grupa),
                    new ReportParameter("przedstawiciel", przedstawiciel),
                    new ReportParameter("okresI", data1Od.ToShortDateString() + " - " + data1Do.ToShortDateString()),
                    new ReportParameter("okresII", data2Od.ToShortDateString() + " - " + data2Do.ToShortDateString()),
                  });
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer.RefreshReport();
        }

        private object keySelector(AnalizaZwrotowWgGrupRow row)
        {
            if (row != null)
            {
                switch (sortujWg)
                {
                    case 1:
                        return row.WartoscObrotuI;
                    case 2:
                        return row.WartoscSprzedarzyII;
                    case 3:
                        return row.ProcentSprzedazy;
                    case 4:
                        return row.WartoscZwrotuII;
                    case 5:
                        return row.ProcentZwrotu;
                    case 6:
                        return row.TeoretycznyProcentZwrotu;
                    default:
                        return row.KontrahentKod;
                }
            }
            return "";
        }

        #region Nested types

        public class ProgressHandler : Enova.Business.Old.Forms.ProgressFormHandler
        {
            private AnalizaZwrotowWgGrupForm parent;


            public ProgressHandler(AnalizaZwrotowWgGrupForm parent)
            {
                this.parent = parent;
            }

            public override void StartProcess()
            {
                /*
                var dc = Enova.Business.Old.Core.ContextManager.DataContext;
                var wzGuid = new Guid("00000000-0011-0002-0009-000000000000");

                this.ProgressArgs.Progress2Visible = false;
                this.ProgressArgs.MaxProgress1 = parent.kontrahenci.Count;
                this.ProgressChanges();

                foreach (var kh in parent.kontrahenci)
                {
                    if (this.BackgroundWorker.CancellationPending)
                        break;
                        
                    decimal? obI = ObrotyService.GetObrotyByGrupaKontrahent(parent.grupa, kh.ID, parent.data1Od, parent.data1Do).Sum(o => o.RozchodWartosc);
                    decimal? obII = ObrotyService.GetObrotyByGrupaKontrahent(parent.grupa, kh.ID, parent.data2Od, parent.data2Do).Sum(o => o.RozchodWartosc);
                    decimal? spII = ObrotyService.GetObrotyByGrupaKontrahent(parent.grupa, kh.ID, parent.data2Od, parent.data2Do).
                        Where(o => (o.RozchodDokument.StanInt == 1 || o.RozchodDokument.StanInt == 2) && o.RozchodDokument.RelationDefinicja.Guid == wzGuid).Sum(o => o.RozchodWartosc);

                    if (obI != null || obII != null)
                    {
                        var row = new AnalizaZwrotowWgGrupRow()
                        {
                            KontrahentID = kh.ID,
                            KontrahentKod = kh.Kod,
                            KontrahentNazwa = kh.Nazwa,
                            WartoscObrotuI = obI,
                            WartoscObrotuII = obII,
                            WartoscSprzedarzyII = spII,
                        };
                        if ((parent.tylkoPoZwrotach == false || (row.WartoscZwrotuII != null && row.WartoscZwrotuII.Value != 0))
                            && (parent.tylkoZObrotemI == false || (row.WartoscObrotuI != null && row.WartoscObrotuI != 0)))
                        {
                            parent.reportSource.Add(row);
                        }
                    }
                    this.PerformStep1(kh.Kod);
                }
                 */
            }

            public override void FinishProcess()
            {
                parent.createReport();
                parent.enableRefesh();
            }

        }

        #endregion
    }
}
