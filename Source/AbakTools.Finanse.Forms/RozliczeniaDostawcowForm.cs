using System;
using System.IO;
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

[assembly: BAL.Forms.MenuAction("Finanse\\Rozliczenia dostawcow", typeof(AbakTools.Finanse.Forms.RozliczeniaDostawcowForm), Priority = 330)]

namespace AbakTools.Finanse.Forms
{
    public partial class RozliczeniaDostawcowForm : Enova.Forms.FormWithEnovaAPI
    {
        public RozliczeniaDostawcowForm()
        {
            InitializeComponent();
        }

        private void zatwierdźButton_Click(object sender, EventArgs e)
        {
            
            var podmiot = podmiotSelect.Podmiot;
            if (podmiot != null && podmiot is Enova.API.Kasa.IPodmiotRozrachunki)
            {
                this.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                DateTime f = dateFromTo.FromTo.From;
                DateTime t = dateFromTo.FromTo.To;

                DateTime dataOd = new DateTime(f.Year, f.Month, f.Day, 0, 0, 0);
                DateTime dataDo = new DateTime(t.Year, t.Month, t.Day, 0, 0, 0).AddDays(1);

                List<RozliczenieRow> reportSource = new List<RozliczenieRow>();

                if (saldoCheckBox.Checked)
                {
                    //var rozrachBefore = ((Enova.Business.Old.Core.IRozrachunkiQuery)podmiot).RozrachunkiQuery.Where(r => r.Data < dataOd).ToList();
                    var rozrachBefore = (from r in ((Enova.API.Kasa.IPodmiotRozrachunki)podmiot).Rozrachunki.CreateView().SetFilter("Data < '" + dataOd.ToShortDateString() + "'")
                            .Cast<Enova.API.Kasa.RozrachunekIdx>()
                            select new Enova.Forms.Services.RozrachunekProxy() { RozrachunekIdx = r }).ToList();

                    decimal? saldoDokumenty = rozrachBefore.Sum(r => r.KwotaDokumenty);
                    decimal? saldoZapłaty = rozrachBefore.Sum(r => r.KwotaZapłaty);
                    decimal? saldoNaleznosci = rozrachBefore.Sum(r => r.Należność);
                    decimal? saldoZobowiazania = rozrachBefore.Sum(r => r.Zobowiązanie);
                    reportSource.Add(new RozliczenieRow()
                    {
                        Data = new DateTime(1900, 1, 1, 0, 0, 0),
                        Termin = new DateTime(1900, 1, 1, 0, 0, 0),
                        Numer = "SALDO POCZĄTKOWE",
                        Należność = saldoNaleznosci > saldoZobowiazania ? saldoNaleznosci - saldoZobowiazania : null,
                        Zobowiązanie = saldoZobowiazania > saldoNaleznosci ? saldoZobowiazania - saldoNaleznosci : null,
                        //Należność = saldoNaleznosci,
                        //Zobowiązanie = saldoZobowiazania,
                        KwotaDokumenty = saldoDokumenty,
                        KwotaZapłaty = saldoZapłaty
                    });
                }

                //var rozrachunki = ((Enova.Business.Old.Core.IRozrachunkiQuery)podmiot).RozrachunkiQuery.Where(r => r.Data >= dataOd && r.Data < dataDo).ToList();
                var rozrachunki = (from r in ((Enova.API.Kasa.IPodmiotRozrachunki)podmiot).Rozrachunki.CreateView()
                                           .SetFilter("Data >= '" + dataOd.ToShortDateString() + "' AND Data<= '" + dataDo.ToShortDateString() + "'").Cast<Enova.API.Kasa.RozrachunekIdx>()
                                   select new Enova.Forms.Services.RozrachunekProxy() { RozrachunekIdx = r }).ToList();

                foreach (var r in rozrachunki)
                {
                    if (r.Dokument != null)
                    {
                        //if (r.Dokument is Platnosc)
                        if (r.Dokument.Is<Enova.API.Kasa.Platnosc>())
                        {
                            var platnosc = r.Dokument.As<Enova.API.Kasa.Platnosc>();
                            string numer = "";

                            //if (platnosc.Dokument is DokumentRozliczeniowy)
                            if(platnosc.Dokument.Is<Enova.API.Kasa.DokRozliczBase>())
                            {
                                //RozliczenieSP ro = platnosc.RozliczeniaSPZapłatyQuery.FirstOrDefault();
                                var ro = Session.GetModule<Enova.API.Kasa.KasaModule>().RozliczeniaSP.WgZaplata(platnosc).Cast<Enova.API.Kasa.RozliczenieSP>().FirstOrDefault();
                                //if (ro!=null && ro.Dokument is Platnosc)
                                if (ro != null && ro.Dokument.Is<Enova.API.Kasa.Platnosc>())
                                {
                                    //numer = ((Platnosc)ro.Dokument).NumerDokumentu;
                                    numer = ro.Dokument.As<Enova.API.Kasa.Platnosc>().NumerDokumentu;
                                }
                            }

                            decimal? kwota = null;

                            if (!r.CzyKompensata)
                                //kwota = r.TypRozrachunku == TypRozrachunku.Należność ? r.KwotaValue : -r.KwotaValue;
                                kwota = r.Typ == Enova.API.Kasa.TypRozrachunku.Należność ? r.KwotaValue : -r.KwotaValue;


                            reportSource.Add(new RozliczenieRow()
                            {
                                Numer = r.Numer,
                                Data = r.Data,
                                Termin = r.Termin,
                                //Należność = r.Należność,
                                Należność = null,
                                //Zobowiązanie = r.Zobowiązanie,
                                Zobowiązanie = null,
                                NumerDokumentu = numer,
                                KwotaDokumenty = kwota,
                                KwotaZapłaty = null
                            });
                        }
                        //else if (r.Dokument is Zaplata)
                        else if (r.Dokument.Is<Enova.API.Kasa.Zaplata>())
                        {
                            
                            reportSource.Add(new RozliczenieRow()
                            {
                                Numer = r.Numer,
                                Data =r.Data,
                                Termin = r.Termin,
                                Należność = r.Należność,
                                Zobowiązanie = r.Zobowiązanie,
                                KwotaDokumenty = r.KwotaDokumentyDostawcy,
                                KwotaZapłaty = r.KwotaZapłatyDostawcy,
                                //Opis = ((Zaplata)r.Dokument).Opis
                                Opis = r.Dokument.As<Enova.API.Kasa.Zaplata>().Opis
                            });
                             
                            //var rozliczenia = ((Zaplata)r.Dokument).RozliczeniaSPQuery.ToList();
                            var rozliczenia = Session.GetModule<Enova.API.Kasa.KasaModule>().RozliczeniaSP.WgZaplata(r.Dokument).Cast<Enova.API.Kasa.RozliczenieSP>().ToList();
                            if (rozliczenia.Count == 1)
                            {
                                var doc = rozliczenia.First().Dokument;
                                /*
                                if (doc is Platnosc)
                                {
                                    Platnosc platnosc = (Platnosc)doc;
                                    reportSource.Last().NumerDokumentu = platnosc.NumerDokumentu;
                                    
                                }
                                else if (doc is Zaplata)
                                {
                                    Zaplata zapl = (Zaplata)doc;
                                    reportSource.Last().NumerDokumentu = zapl.NumerDokumentu;
                                }
                                 */
                                reportSource.Last().NumerDokumentu = doc.NumerDokumentu;
                            }
                            else
                            {
                                foreach (var ro in rozliczenia)
                                {
                                    //if (ro.Dokument is Platnosc)
                                    if (ro.Dokument.Is<Enova.API.Kasa.Platnosc>())
                                    {
                                        //Platnosc platnosc = (Platnosc)ro.Dokument;
                                        reportSource.Add(new RozliczenieRow()
                                        {
                                            Numer = r.Numer,
                                            Data = r.Data,
                                            Termin = new DateTime(1900, 1, 1, 0, 0, 0),
                                            Należność = null,
                                            Zobowiązanie = null,
                                            //NumerDokumentu = platnosc.NumerDokumentu + " - " + ro.KwotaZaplatyValue + " zł"
                                            NumerDokumentu = ro.Dokument.NumerDokumentu + " - " + ro.KwotaZaplaty.Value + " zł"
                                        });
                                    }
                                }
                            }
                        }
                    }
                    
                }

                if (saldoKoncoweCheckBox.Checked)
                {
                    decimal? saldoNaleznosci = reportSource.Sum(r => r.Należność);
                    decimal? saldoZobowiazania = reportSource.Sum(r => r.Zobowiązanie);

                    reportSource.Add(new RozliczenieRow()
                    {
                        Data = DateTime.MaxValue.Date,
                        Termin = DateTime.MaxValue.Date,
                        Numer = "SALDO KOŃCOWE",
                        Należność = saldoNaleznosci > saldoZobowiazania ? -(saldoNaleznosci - saldoZobowiazania) : null,
                        Zobowiązanie = saldoZobowiazania > saldoNaleznosci ? -(saldoZobowiazania - saldoNaleznosci) : null,
                        //Należność = saldoNaleznosci,
                        //Zobowiązanie = saldoZobowiazania,
                        KwotaDokumenty = null,
                        KwotaZapłaty = null
                    });

                }

                this.reportViewer.Reset();

                string reportPath = "";

                //switch (podmiotSelect.TypPodmiotu.Value)
                switch (podmiotSelect.TypPodmiotu)
                {
                    //case PodmiotType.Pracownik:
                    case Enova.API.Core.TypPodmiotu.Pracownik:
                        reportPath = "Reports\\RozliczeniaReportIII.rdlc";
                        break;
                        /*
                    case PodmiotType.Bank:
                    case PodmiotType.UrządSkarbowy:
                    case PodmiotType.ZUS:
                         */
                    case Enova.API.Core.TypPodmiotu.Bank:
                    case Enova.API.Core.TypPodmiotu.UrzadSkarbowy:
                    case Enova.API.Core.TypPodmiotu.ZUS:
                         reportPath = "Reports\\RozliczeniaReportII.rdlc";
                        break;
                    default:
                        reportPath ="Reports\\RozliczeniaReport.rdlc";
                        break;
                }

                reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, reportPath);

                using (StreamReader sr = new StreamReader(reportPath))
                {
                    this.reportViewer.LocalReport.LoadReportDefinition(sr);
                }

                this.reportViewer.LocalReport.DataSources.Clear();
                this.reportViewer.LocalReport.SetParameters(new ReportParameter[] { 
                    /*
                    new ReportParameter("kod", ((Enova.Business.Old.Core.IPodmiot)podmiot).Kod),
                    new ReportParameter("nazwa", ((Enova.Business.Old.Core.IPodmiot)podmiot).Nazwa),
                     */
                    new ReportParameter("kod", podmiot.Kod),
                    new ReportParameter("nazwa", podmiot.Nazwa),
                    new ReportParameter("dataod", dataOd.ToShortDateString()),
                    new ReportParameter("datado",t.ToShortDateString())
                });
                this.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("RozliczenieRow", reportSource.OrderBy(r=>r.Data).ThenBy(r=>r.Numer)));
                this.reportViewer.RefreshReport();

                this.Cursor = Cursors.Default;
                this.Enabled = true;

            }
        }

        private void RozliczeniaForm_ResizeEnd(object sender, EventArgs e)
        {
        }


    }
}
