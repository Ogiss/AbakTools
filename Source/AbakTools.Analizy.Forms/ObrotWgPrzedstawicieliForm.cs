using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
//using System.Data.Objects;
//using System.Data.Objects.DataClasses;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.Types;
using Enova.Business.Old.DB;
//using Enova.Business.Old.DB.Services;
using Microsoft.Reporting.WinForms;
using Enova.Old.Magazyny;

//[assembly: BAL.Forms.MenuAction("Finanse\\Obroty wg przedstawicieli", typeof(AbakTools.Analizy.Forms.ObrotWgPrzedstawicieliForm), Priority=340)]

namespace AbakTools.Analizy.Forms
{
    public partial class ObrotWgPrzedstawicieliForm : Form
    {
        public ObrotWgPrzedstawicieliForm()
        {
            InitializeComponent();
        }

        private void ObrotWgPrzedstawicieliForm_Load(object sender, EventArgs e)
        {
            /*
            grupyBindingSource.DataSource = FeatureDefsService.GetGrupyTowaroweQuery().OrderBy(g=>g.Nazwa).ToList();
            loadPodgrupy();
             */

        }

        private void loadPodgrupy()
        {
            /*
            GrupyTowaroweViewRow grupa = (GrupyTowaroweViewRow)grupaComboBox.SelectedItem;

            if (grupa != null)
            {
                podgrupyBindingSource.DataSource = DictionaryService.GetDictionaryQuery(grupa.Słownik).OrderBy(d=>d.Value).Select(d=>
                    new GrupyTowaroweViewRow()
                    {
                        ID = d.ID,
                        Nazwa = d.Value,
                        Słownik = null
                    });
            }
             */
        }

        private void grupaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPodgrupy();
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            /*

            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            string grupa = (string)grupaComboBox.SelectedValue;
            string podgrupa = (string)podgrupaComboBox.SelectedValue;
            DateTime dataOd = dataOdDateTimePicker.Value;
            DateTime dataDo = dataDoDateTimePicker.Value;

            

            var obrotySprzedaż = (from ob in ObrotyService.GetObrotyByData(dataOd, dataDo)
                                  join f in FeaturesService.GetFeaturesQuery("Towary", grupa) on ob.Towar.ID equals f.Parent
                                  where ob.RozchodData >= dataOd && ob.RozchodData <= dataDo && f.Data == podgrupa && ob.RozchodDokument.Korekta == false 
                                  && ob.Korekta != (int)KorektaObrotu.StornoZasobu
                                  select ob.PrzychodWartosc).Sum(w => w);

            var obrotyKorekty = (from ob in ObrotyService.GetObrotyByData(dataOd, dataDo)
                                 join f in FeaturesService.GetFeaturesQuery("Towary", grupa) on ob.Towar.ID equals f.Parent
                                 where ob.RozchodData >= dataOd && ob.RozchodData <= dataDo && f.Data == podgrupa && ob.RozchodDokument.Korekta == true 
                                 && ob.Korekta != (int)KorektaObrotu.StornoZasobu
                                 select ob.PrzychodWartosc).Sum(w => w);

            obrotySprzedaż = obrotySprzedaż == null ? 0 : obrotySprzedaż;
            obrotyKorekty = obrotyKorekty == null ? 0 : obrotyKorekty;

            var obroty = (from p in DictionaryService.GetPrzedstawicieleQuery()
                          orderby p.Nazwa
                          select new ObrotyWgPrzedstawicielViewRow()
                          {
                              Przedstawiciel = p.Nazwa
                          }).ToList();

            foreach (var o in obroty)
            {
                var op = (from ob in ObrotyService.GetObrotyByPrzedstawicielKontrahent(o.Przedstawiciel, 0, dataOd, dataDo)
                          join f in FeaturesService.GetFeaturesQuery("Towary", grupa) on ob.Towar.ID equals f.Parent
                          where f.Data == podgrupa
                          select new
                          {
                              ob.RozchodDokument.Korekta,
                              ob.PrzychodWartosc
                          }).ToList();

                o.Sprzedaż = op.Where(ob => ob.Korekta == false).Sum(ob => ob.PrzychodWartosc);
                o.Korekty = op.Where(ob => ob.Korekta == true).Sum(ob => ob.PrzychodWartosc);
                o.SprzedażProcent = obrotySprzedaż == 0 ? 0 : o.Sprzedaż / obrotySprzedaż;
                o.KorektyProcent = obrotyKorekty == 0 ? 0 : o.Korekty / obrotyKorekty;
                o.ObrotyProcent = (obrotySprzedaż == 0 && obrotyKorekty == 0) ? 0 : o.Obrót / (obrotySprzedaż + obrotyKorekty);
            }


            reportViewer.LocalReport.ReportPath = "Reports\\ObrotyWgPrzedstawicielReport.rdlc";
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { 
                new ReportParameter("grupa", grupa),
                new ReportParameter("podgrupa", podgrupa),
                new ReportParameter("okres",dataOd.ToString("yyyy-MM-dd")+" - "+dataDo.ToString("yyyy-MM-dd"))
            });
            reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("ObrotyWgPrzedstawicielViewRow", obroty));
            reportViewer.RefreshReport();

            this.Cursor = Cursors.Default;
            this.Enabled = true;
             */
        }
    }
}
