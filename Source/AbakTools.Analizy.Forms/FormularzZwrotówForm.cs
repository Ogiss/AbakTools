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
using Enova.Business.Old.Types;
//using Enova.Business.Old.DB.Services;

//[assembly: BAL.Forms.MenuAction("Formularze\\Formularz zwrotów II", typeof(AbakTools.Analizy.Forms.FormularzZwrotówForm), Priority = 140)]

namespace AbakTools.Analizy.Forms
{
    /*
    public partial class FormularzZwrotówForm : Form
    {
        public FormularzZwrotówForm()
        {
            InitializeComponent();
        }

        private void FormularzZwrotówForm_Load(object sender, EventArgs e)
        {
            grupyTowaroweBindingSource.DataSource = FeatureDefsService.GetGrupyTowaroweQuery().OrderBy(g => g.Nazwa).ToList();
            //przedstawicieleBindingSource.DataSource = DictionaryService.GetPrzedstawicieleQuery().OrderBy(p => p.Nazwa).ToList();
            loadPrzedstawiciele();
            loadKontrahenci();

        }

        private void loadPrzedstawiciele()
        {
            string dictionary = FeatureDefsService.GetDictionary("Kontrahenci", "przedstawiciel");
            List<PrzedstawicieleViewRow> ds = new List<PrzedstawicieleViewRow>() { new PrzedstawicieleViewRow() { ID = 0, Nazwa = "(Wszyscy)" } };
            ds.AddRange(DictionaryService.GetDictionaryQuery(dictionary).OrderBy(d => d.Value).Select(d => new PrzedstawicieleViewRow() { ID = d.ID, Nazwa = d.Value }).ToList());
            przedstawicieleBindingSource.DataSource = ds;
        }


        private void loadKontrahenci()
        {
            string przedstawiciel = przedstawicieleComboBox.SelectedValue as string;
            if (!string.IsNullOrEmpty(przedstawiciel))
            {
                List<Kontrahent> ds = new List<Kontrahent>() { new Kontrahent() { ID = 0, Kod = "(Wszyscy)" } };

                ds.AddRange(KontrahenciService.GetKontrahenciByPrzedstawiciel(przedstawiciel).OrderBy(k => k.Kod).ToList());

                kontrahenciBindingSource.DataSource = ds;
            }
        }

        private void przedstawicieleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadKontrahenci();
        }

        private void createReport()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            string przedstawiciel = ((PrzedstawicieleViewRow)przedstawicieleComboBox.SelectedItem).ID == 0 ? null : przedstawicieleComboBox.SelectedValue as string;
            string grupa = (string)grupyTowaroweComboBox.SelectedValue;
            int idKontrahenta = (int)kontrahenciComboBox.SelectedValue;
            DateTime dataOd = dataOdDateTimePicker.Value;
            DateTime dataDo = dataDoDateTimePicker.Value;
            List<RaportFormularzWgGrupViewRow> raportDataSource;

            string dictionary = FeatureDefsService.GetDictionary("Towary", grupa);

            raportDataSource = (from d in DictionaryService.GetDictionaryQuery(dictionary).Where(d => d.Value != "RESZTA").OrderBy(d => d.Value).ToList()
                                join o in
                                    (from o in ObrotyService.GetObrotyByPrzedstawicielKontrahent(przedstawiciel, idKontrahenta, dataOd, dataDo)
                                     group o by o.Towar.ID into g
                                     select new
                                     {
                                         Towar = g.Key,
                                         Obroty = g,
                                         FV = g.Where(ob => ob.RozchodDokument.RelationDefinicja.Symbol == "WZ").Sum(ob => ob.IloscValue),
                                         Suma = g.Sum(ob => ob.IloscValue),
                                         WartoscFV = g.Where(ob => ob.RozchodDokument.RelationDefinicja.Symbol == "WZ").Sum(ob => ob.RozchodWartosc),
                                         WartoscSuma = g.Sum(ob => ob.RozchodWartosc)
                                     } into o
                                     join f in FeaturesService.GetFeaturesQuery("Towary", grupa) on o.Towar equals f.Parent
                                     where f.Data != "RESZTA"
                                     select new { Towar = o.Towar, FV = o.FV, Suma = o.Suma, WartoscFV = o.WartoscFV, WartoscSuma = o.WartoscSuma, Podgrupa = f.Data, Obroty = o.Obroty } into o
                                     group o by o.Podgrupa into g
                                     select new
                                     {
                                         Nazwa = g.Key,
                                         ObrotFV = g.Sum(ob => ob.FV),
                                         ObrotSuma = g.Sum(ob => ob.Suma),
                                         ObrotFVWartosc = g.Sum(ob => ob.WartoscFV),
                                         ObrotSumaWartosc = g.Sum(ob => ob.WartoscSuma)
                                     }).ToList() on d.Value equals o.Nazwa into obroty
                                from ob in obroty.DefaultIfEmpty()
                                where ob != null
                                select new RaportFormularzWgGrupViewRow()
                                {
                                    Grupa = "Zgrupowane",
                                    Kod = d.Value,
                                    Nazwa = d.Value,
                                    ObrotFV = ob == null ? null : ob.ObrotFV,
                                    //ObrotSuma = ob == null ? null : ob.ObrotSuma,
                                    ObrotFVWartosc = ob == null ? null : ob.ObrotFVWartosc,
                                    ObrotSumaWartosc = ob == null ? null : ob.ObrotSumaWartosc

                                }).ToList();

            raportDataSource.AddRange(
                (from t in
                     (from t in TowaryService.GetTowaryByGrupa(grupa, "RESZTA").OrderBy(t => t.Kod)
                      join f in FeaturesService.GetFeaturesQuery("Towary", "DOSTAWCY") on t.ID equals f.Parent
                      select new { ID = t.ID, Kod = t.Kod, Nazwa = t.Nazwa, Grupa = f.Data }).ToList()
                 join o in
                     (
                         from o in ObrotyService.GetObrotyByPrzedstawicielKontrahent(przedstawiciel, idKontrahenta, dataOd, dataDo)
                         group o by o.Towar.ID into g
                         select new
                         {
                             Towar = g.Key,
                             DokFV = g.Where(ob => ob.RozchodDokument.RelationDefinicja.Symbol == "WZ").Select(dok => dok.RozchodDokument),
                             FV = g.Where(ob => ob.RozchodDokument.RelationDefinicja.Symbol == "WZ").Sum(ob => ob.IloscValue),
                             Suma = g.Sum(ob => ob.IloscValue),
                             WartoscFV = g.Where(ob => ob.RozchodDokument.RelationDefinicja.Symbol == "WZ").Sum(ob => ob.RozchodWartosc),
                             WartoscSuma = g.Sum(ob=>ob.RozchodWartosc),
                             Obroty = g
                         }
                         ).ToList() on t.ID equals o.Towar into obroty
                 from ob in obroty.DefaultIfEmpty()
                 where ob != null
                 select new RaportFormularzWgGrupViewRow()
                 {
                     Grupa = t.Grupa,
                     Kod = t.Kod,
                     Nazwa = t.Nazwa,
                     IloscFV = (int?)(ob == null ? null : (int?)ob.DokFV.GroupBy(g => g).Count()),
                     ObrotFV = ob == null ? null : ob.FV,
                     //ObrotSuma = ob == null ? null : ob.Suma,
                     ObrotyWZ = ob == null ? null : ob.DokFV,
                     ObrotFVWartosc = ob == null ? null : ob.WartoscFV,
                     ObrotSumaWartosc = ob == null ? null : ob.WartoscSuma
                 }).ToList());


            this.reportViewer.LocalReport.ReportPath = "FormularzZwrotówReport.rdlc";
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("RaportFormularzWgGrupViewRow", raportDataSource));
       
            this.reportViewer.LocalReport.SetParameters(
                new ReportParameter[] {
                    new ReportParameter("grupa",grupa),
                    new ReportParameter("kontrahent",kontrahenciComboBox.Text)
                });
          
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            
            this.reportViewer.RefreshReport();

            this.Cursor = Cursors.Default;
            this.Enabled = true;

        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            createReport();
        }


    }
    */
}
