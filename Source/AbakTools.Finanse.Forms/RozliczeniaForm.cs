using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Reporting.WinForms;
using Enova.Business.Old.DB;
using Enova.Business.Old.Types;

[assembly: BAL.Forms.MenuAction("Finanse\\Rozliczenia", typeof(AbakTools.Finanse.Forms.RozliczeniaForm), Priority = 330)]

namespace AbakTools.Finanse.Forms
{
    public partial class RozliczeniaForm : Enova.Forms.FormWithEnovaAPI
    {
        public RozliczeniaForm()
        {
            InitializeComponent();
        }

        private void zatwierdźButton_Click(object sender, EventArgs e)
        {
            var podmiot = podmiotSelect.Podmiot;
            if (podmiot != null && podmiot is Enova.API.Kasa.IPodmiotRozrachunki)
            {
                try
                {
                    this.Enabled = false;
                    this.Cursor = Cursors.WaitCursor;

                    DateTime f = dateFromTo.FromTo.From;
                    DateTime t = dateFromTo.FromTo.To;

                    DateTime dataOd = new DateTime(f.Year, f.Month, f.Day, 0, 0, 0);
                    //DateTime dataDo = new DateTime(t.Year, t.Month, t.Day, 0, 0, 0).AddDays(1);
                    DateTime dataDo = new DateTime(t.Year, t.Month, t.Day, 0, 0, 0);

                    List<RozliczenieRow> reportSource = new List<RozliczenieRow>();

                    if (saldoCheckBox.Checked)
                    {
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
                            KwotaDokumenty = saldoDokumenty,
                            KwotaZapłaty = saldoZapłaty
                        });
                    }

                    var rozrachunki = (from r in ((Enova.API.Kasa.IPodmiotRozrachunki)podmiot).Rozrachunki.CreateView()
                                           .SetFilter("Data >= '" + dataOd.ToShortDateString() + "' AND Data<= '"+dataDo.ToShortDateString()+"'").Cast<Enova.API.Kasa.RozrachunekIdx>()
                                         select new Enova.Forms.Services.RozrachunekProxy() { RozrachunekIdx = r }).ToList();

                    foreach (var r in rozrachunki)
                    {
                        if (r.Dokument != null)
                        {
                            if (r.Dokument.Is<Enova.API.Kasa.Platnosc>())
                            {
                                var platnosc = r.Dokument.As<Enova.API.Kasa.Platnosc>();
                                string numer = "";

                                //if (platnosc.Dokument.Is<Enova.API.Kasa.DokRozlicz>())
                                if (platnosc.Dokument.Is<Enova.API.Kasa.DokRozliczBase>())
                                {
                                    var ro = Session.GetModule<Enova.API.Kasa.KasaModule>().RozliczeniaSP.WgZaplata(platnosc).Cast<Enova.API.Kasa.RozliczenieSP>().FirstOrDefault();
                                    if(ro!=null && ro.Dokument.Is<Enova.API.Kasa.Platnosc>())
                                    numer = ro.Dokument.As<Enova.API.Kasa.Platnosc>().NumerDokumentu;
                                }

                                reportSource.Add(new RozliczenieRow()
                                {
                                    Numer = r.Numer,
                                    Data = r.Data,
                                    Termin = r.Termin,
                                    Należność = r.Należność,
                                    Zobowiązanie = r.Zobowiązanie,
                                    NumerDokumentu = numer,
                                    KwotaDokumenty = r.KwotaDokumenty,
                                    KwotaZapłaty = r.KwotaZapłaty
                                });
                            }
                            else if (r.Dokument.Is<Enova.API.Kasa.Zaplata>())
                            {
                            
                                reportSource.Add(new RozliczenieRow()
                                {
                                    Numer = r.Numer,
                                    Data =r.Data,
                                    Termin = r.Termin,
                                    Należność = r.Należność,
                                    Zobowiązanie = r.Zobowiązanie,
                                    KwotaDokumenty = r.KwotaDokumenty,
                                    KwotaZapłaty = r.KwotaZapłaty,
                                    Opis = r.Dokument.As<Enova.API.Kasa.Zaplata>().Opis
                                });
                             
                                var rozliczenia = Session.GetModule<Enova.API.Kasa.KasaModule>().RozliczeniaSP.WgZaplata(r.Dokument.As<Enova.API.Kasa.Zaplata>())
                                    .Cast<Enova.API.Kasa.RozliczenieSP>().ToList(); ;
                                if (rozliczenia.Count == 1)
                                {
                                    var doc = rozliczenia.First().Dokument;
                                    if (doc.Is<Enova.API.Kasa.IRozliczalny>())
                                        reportSource.Last().NumerDokumentu = doc.As<Enova.API.Kasa.IRozliczalny>().NumerDokumentu;
                                }
                                else
                                {
                                    foreach (var ro in rozliczenia)
                                    {
                                        if (ro.Dokument.Is<Enova.API.Kasa.Platnosc>())
                                        {
                                            var platnosc = ro.Dokument.As<Enova.API.Kasa.Platnosc>();
                                            reportSource.Add(new RozliczenieRow()
                                            {
                                                Numer = r.Numer,
                                                Data = r.Data,
                                                Termin = new DateTime(1900, 1, 1, 0, 0, 0),
                                                Należność = null,
                                                Zobowiązanie = null,
                                                NumerDokumentu = platnosc.NumerDokumentu + " - " + ro.KwotaZaplaty.Value + " zł"
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
                            KwotaDokumenty = null,
                            KwotaZapłaty = null
                        });

                    }

                    this.reportViewer.Reset();

                    string reportPath = "";
                    
                    

                    switch (podmiotSelect.TypPodmiotu)
                    {
                        case Enova.API.Core.TypPodmiotu.Pracownik:
                            reportPath = "AbakTools.Finanse.Forms.Reports.RozliczeniaReportIII.rdlc";

                            break;
                        case Enova.API.Core.TypPodmiotu.Bank:
                        case Enova.API.Core.TypPodmiotu.UrzadSkarbowy:
                        case Enova.API.Core.TypPodmiotu.ZUS:
                            reportPath = "AbakTools.Finanse.Forms.Reports.RozliczeniaReportII.rdlc";
                            break;
                        default:
                            reportPath = "AbakTools.Finanse.Forms.Reports.RozliczeniaReport.rdlc";
                            break;
                    }

                    //reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, reportPath);

                    /*
                    using (StreamReader sr = new StreamReader(reportPath))
                    {
                        this.reportViewer.LocalReport.LoadReportDefinition(sr);
                    }
                     */

                    this.reportViewer.LocalReport.ReportEmbeddedResource = reportPath;

                    this.reportViewer.LocalReport.DataSources.Clear();
                    this.reportViewer.LocalReport.SetParameters(new ReportParameter[] { 
                        new ReportParameter("kod", podmiot.Kod),
                        new ReportParameter("nazwa", podmiot.Nazwa),
                        new ReportParameter("dataod", dataOd.ToShortDateString()),
                        new ReportParameter("datado",t.ToShortDateString())
                    });
                    this.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("RozliczenieRow", reportSource.OrderBy(r=>r.Data).ThenBy(r=>r.Numer)));
                    this.reportViewer.RefreshReport();
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
        }

        private void RozliczeniaForm_ResizeEnd(object sender, EventArgs e)
        {
        }

        #region Nested types


        #endregion

    }
}
