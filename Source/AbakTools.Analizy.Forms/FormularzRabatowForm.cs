using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB;
//using Enova.Business.Old.DB.Services;
using Enova.Business.Old.Types;
using Microsoft.Reporting.WinForms;

//[assembly: BAL.Forms.MenuAction("Formularze\\Formularz rabatów", typeof(AbakTools.Analizy.Forms.FormularzRabatowForm), Priority = 150)]

namespace AbakTools.Analizy.Forms
{
    public partial class FormularzRabatowForm : Enova.Forms.FormWithEnovaAPI
    {
        private Enova.API.CRM.Kontrahent kontrahent;
        List<RabatGrupowy> reportSource;

        public FormularzRabatowForm()
        {
            InitializeComponent();
        }

        private void FormularzRabatowForm_Load(object sender, EventArgs e)
        {
            this.reportViewer.RefreshReport();
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            kontrahent = kontrahentSelect.Kontrahent;
            if (kontrahent != null)
            {
                reportSource = new List<RabatGrupowy>();
                Enova.Business.Old.Forms.ProgressForm.StartProgress(new ProgressHandler(this));
            }
        }

        private void createReport()
        {
            this.reportViewer.LocalReport.ReportPath = "Reports\\RabatyKontrahentaReport.rdlc";
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("RabatGrupowy", reportSource));

            this.reportViewer.LocalReport.SetParameters(
                new ReportParameter[] {
                    new ReportParameter("nazwa","("+kontrahent.Kod+") "+kontrahent.Nazwa)
                });

            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer.RefreshReport();

        }

        #region Nested Types

        public class ProgressHandler : Enova.Business.Old.Forms.ProgressFormHandler
        {
            private FormularzRabatowForm parent;

            public ProgressHandler(FormularzRabatowForm parent)
            {
                this.parent = parent;
            }

            public override void StartProcess()
            {
                //var grupyRabatowe = FeatureDef.GrupyRabatowe;
                var grupyRabatowe = parent.Session.GetModule<Enova.API.Towary.TowaryModule>().DefinicjeCen.GrupyTowarowe.ToList();
                //EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;
                this.ProgressArgs.Progress2Visible = false;
                this.ProgressArgs.MaxProgress1 = grupyRabatowe.Count;

                foreach (var grupa in grupyRabatowe)
                {
                    foreach (var r in new Enova.Forms.Towary.RabatyGrupowe() { Kontrahent = parent.kontrahent, FeatureDefinition = grupa })
                    {
                        parent.reportSource.Add(new RabatGrupowy()
                        {
                        });
                    }
                    /*
                    reportSource.AddRange((from d in dc.DictionarySet.Where(di => di.Category == "F." + grupa.Dictionary)
                                           join cg in kontrahent.CenyGrupowe.CreateSourceQuery() on d.ID equals cg.GrupaTowarowa.ID into cenag
                                           orderby d.Value
                                           select new Enova.Business.Old.Types.RabatGrupowy()
                                           {
                                               Grupa = grupa.Name,
                                               CenaGrupowa = cenag.FirstOrDefault(),
                                               GrupaTowarowaID = d.ID,
                                               GrupaTowarowaNazwa = d.Value,
                                               Rabat = cenag.FirstOrDefault() != null ? cenag.FirstOrDefault().Rabat : null,
                                               Zdefiniowany = cenag.FirstOrDefault() != null ? cenag.FirstOrDefault().RabatZdefiniowany : null
                                           }).ToList());
                     */
                }
            }

            public override void FinishProcess()
            {
                base.FinishProcess();
            }

        }

        #endregion

    }
}
