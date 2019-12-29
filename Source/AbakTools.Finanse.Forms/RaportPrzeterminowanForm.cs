using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Microsoft.Reporting.WinForms;
using Enova.Business.Old.DB;
using Enova.Business.Old.Types;
//using Enova.Business.Old.DB.Services;
using Enova.Business.Old.Forms;

//[assembly: BAL.Forms.MenuAction("Finanse\\Raport przeterminowań", typeof(AbakTools.Finanse.Forms.RaportPrzeterminowanForm), Priority = 360)]

namespace AbakTools.Finanse.Forms
{
    public partial class RaportPrzeterminowanForm : Enova.Forms.FormWithEnovaAPI
    {
        private List<DokumentyProwizjeViewRow> dokPotrącenia = null;

        public RaportPrzeterminowanForm()
        {
            InitializeComponent();
        }

        private void RaportPrzeterminowanForm_Load(object sender, EventArgs e)
        {
            /*
            rokTextBox.Text = DateTime.Now.Year.ToString();
            miesiąceComboBox.SelectedIndex = DateTime.Now.Month - 1;
            this.przedstawicieleBindingSource.DataSource = DictionaryService.GetPrzedstawicieleQuery(Enova.Business.Old.Core.ContextManager.DataContext).OrderBy(p => p.Nazwa);
             */
        }

        private DateTime getDataOd()
        {
            int miesiąc = this.miesiąceComboBox.SelectedIndex + 1;
            int rok = int.Parse(this.rokTextBox.Text);

            return new DateTime(rok, miesiąc, 1);
        }

        private DateTime getDataDo()
        {
            DateTime data = new DateTime(int.Parse(this.rokTextBox.Text), this.miesiąceComboBox.SelectedIndex + 1, 1);
            return data.AddMonths(1).AddDays(-1);
        }


        private void dokumentyPrzeterminowaneLoad()
        {
            /*
            string przedstawiciel = (string)this.przedstawicieleComboBox.Text;
            DateTime dataOd = getDataOd();
            DateTime dataDo = getDataDo();
            int m = this.miesiąceComboBox.SelectedIndex + 1;
            if (m == 12) m = 0;
            int y = int.Parse(this.rokTextBox.Text);
            int pm = m < 4 ? 0 : (m > 3 && m < 8 ? 4 : 8);
            DateTime okresOd = new DateTime(pm == 0 && m != 0 ? y - 1 : y, pm == 0 ? 12 : pm, 1).AddMonths(-7);
            DateTime okresDo = okresOd.AddMonths(4).AddDays(-1);
            DateTime termin = okresOd.AddMonths(8).AddDays(-1);


            var dokumenty = DokHandloweService.GetDokSprzedazy(przedstawiciel, 0, okresOd, okresDo, dataDo.AddDays(1), null).ToList();

            if (stareCheckBox.Checked)
                dokumenty.AddRange(DokHandloweService.GetDokSprzedazy(przedstawiciel, 0, null, okresOd, new DateTime(2099, 12, 31, 0, 0, 0), null).ToList());

 
            dokPotrącenia = new List<DokumentyProwizjeViewRow>();


            foreach (var dh in dokumenty)
            {
                var podrzedny = dh.Podrzedny;
                if (podrzedny == null)
                    continue;
                var feature = dh.Features.Where(f => f.Name == "NIE LICZ POTRACEN").FirstOrDefault();
                bool rozliczana = feature == null || feature.Data.Trim() == "0" ? true : false;

                decimal? rozliczono = 0;

                foreach (var p in dh.Platnosci.ToList())
                {
                    if (p.Rozliczenia.Count() > 0)
                    {
                        decimal? zaplata = p.Rozliczenia.Where(r => r.Data <= dataDo).Sum(r => r.KwotaZaplatyValue);
                        rozliczono += zaplata == null ? 0 : zaplata;
                    }
                }

                decimal? pr = ((decimal)dh.SumaBrutto - rozliczono) / (decimal)dh.SumaBrutto;

                string kodKontrahenta = "";
                if (!dh.KontrahentReference.IsLoaded)
                {
                    kodKontrahenta = (string)dh.KontrahentReference.CreateSourceQuery().Select<Kontrahent, string>(k => k.Kod).FirstOrDefault();
                }
                else
                {
                    kodKontrahenta = dh.Kontrahent.Kod;
                }

                DokumentyProwizjeViewRow dok = new DokumentyProwizjeViewRow()
                {
                    KodKontrahenta = kodKontrahenta,
                    Data = dh.Data,
                    WartoscSprzedazyNetto = dh.SumaNetto,
                    WartoscSprzedazyBrutto = dh.SumaBrutto,
                    WartoscCenaAbak = podrzedny.WartośćWCenieAbak,
                    NumerPelny = dh.NumerPelny,
                    ID = dh.ID,
                    Korekta = (bool)dh.Korekta,
                    DoRozliczenia = dh.SumaBrutto - rozliczono,
                    TerminGraniczny = termin,
                    DataRozliczenia = dh.Platnosci.Max(pl => pl.DataRozliczenia),
                    PotrącenieProwizji = dh.Data< okresOd ? 0 : (decimal)podrzedny.Prowizja * pr * -0.25M,
                    Rozliczana = rozliczana
                };

                this.dokPotrącenia.Add(dok);


            }

            */
         }


        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            /*
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            dokumentyPrzeterminowaneLoad();

            reportViewer.LocalReport.ReportPath = "Reports\\DokumentyPotraceniaReport.rdlc";
            reportViewer.LocalReport.DataSources.Clear();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DokumentyProwizjeViewRow",
                wszystkieCheckBox.Checked ? this.dokPotrącenia : this.dokPotrącenia.Where(d => d.Rozliczana == true)
                ));
            reportViewer.LocalReport.SetParameters(new ReportParameter[]{
                    new ReportParameter("Przedstawiciel",this.przedstawicieleComboBox.SelectedValue.ToString()),
                    new ReportParameter("Okres",(this.miesiąceComboBox.SelectedIndex+1).ToString()+"/"+this.rokTextBox.Text)
                });

            this.reportViewer.RefreshReport();

            this.Cursor = Cursors.Default;
            this.Enabled = true;
             */
        }
    }
}
