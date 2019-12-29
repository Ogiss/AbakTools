using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB;
using Enova.Business.Old.Types;

namespace AbakTools.Finanse.Forms
{
    public partial class RozrachunekEditForm : Enova.Business.Old.Forms.DataEditForm
    {
        private bool rozliczeniaLoaded = false;

        public new Enova.Business.Old.Types.RozrachunekRow DataSource
        {
            get { return (Enova.Business.Old.Types.RozrachunekRow)base.DataSource; }
            set { base.DataSource = value; }
        }

        private Enova.Business.Old.DB.Kontrahent kontrahent = null;

        public RozrachunekEditForm()
        {
            InitializeComponent();
        }

        private void RozrachunekEditForm_Load(object sender, EventArgs e)
        {
            terminComboBox.SelectedIndex = 0;
            terminGranicznyComboBox.SelectedIndex = 0;
            okresRozliczen.SetSpan(Enova.Business.Old.Controls.DateTimeSpanControl.SpanType.Year);

            if ((Enova.Business.Old.DB.Web.Operator.CurrentOperator.PrawaDostepu & AbakTools.Business.OperatorPrawaDostepu.Administrator) != Business.OperatorPrawaDostepu.Administrator)
            {
                blokadaCheckBox.Enabled = false;
                blokadaSprzCheckBox.Enabled = false;
            }
            else
            {
                blokadaCheckBox.Enabled = true;
                blokadaSprzCheckBox.Enabled = true;
            }

            if (DataSource != null)
            {
                if (DataSource.IDKontrahenta != null)
                {
                    kontrahent = DataContext.Kontrahenci.Where(k => k.ID == DataSource.IDKontrahenta).FirstOrDefault();
                    if (kontrahent != null)
                    {
                        DataContext.Refresh(RefreshMode.StoreWins, kontrahent);
                        blokadaSprzCheckBox.Refresh();
                        if (kontrahent.Windykacja != null) { }
                        kontrahentBindingSource.DataSource = kontrahent;
                        adresBindingSource.DataSource = kontrahent.Adres;
                        adresKorespBindingSource.DataSource = kontrahent.AdresKorespondencyjny;
                        loadRozrachunki();
                    }
                }
            }
        }

        private void loadRozrachunki()
        {
            if (kontrahent != null)
            {
                ObjectQuery<RozrachunekIdx> query = kontrahent.RozrachunkiQuery;
                DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                int m = now.Month;
                if (m == 12) m = 0;
                int y = now.Year;
                int pm = m < 4 ? 0 : (m > 3 && m < 8 ? 4 : 8);
                DateTime okresOd = new DateTime(pm == 0 && m != 0 ? y - 1 : y, pm == 0 ? 12 : pm, 1).AddMonths(-7);
                DateTime termin = okresOd.AddMonths(4).AddDays(-1);

                switch (terminComboBox.SelectedIndex)
                {
                    case 1:
                        query = (ObjectQuery<RozrachunekIdx>)query.Where(r => r.Termin < now);
                        break;
                    case 2:
                        query = (ObjectQuery<RozrachunekIdx>)query.Where(r => r.Termin >= now);
                        break;
                }

                switch (terminGranicznyComboBox.SelectedIndex)
                {
                    case 1:
                        query = (ObjectQuery<RozrachunekIdx>)query.Where(r => r.Data <= termin);
                        break;
                    case 2:
                        query = (ObjectQuery<RozrachunekIdx>)query.Where(r => r.Data > termin);
                        break;
                }

                DateTime max = new DateTime(2099, 12, 31, 0, 0, 0);

                rozrachunkiBindingSource.DataSource = query.ToList().Where(r => r.DataRozliczenia == max);

                decimal kwota = 0;
                decimal rozliczono = 0;

                foreach (DataGridViewRow row in rozrachunkiDataGrid.Rows)
                {
                    Enova.Business.Old.DB.RozrachunekIdx rozrach = row.DataBoundItem as Enova.Business.Old.DB.RozrachunekIdx;
                    if (rozrach != null)
                    {
                        kwota += rozrach.Kwota.Value;
                        rozliczono += rozrach.KwotaRozliczona.Value;
                    }
                }

                sumaKwotaTextBox.Text = string.Format("{0:C}", kwota);
                sumaKwotaRozliczTextBox.Text = string.Format("{0:C}", rozliczono);
                sumaPozostaloTextBox.Text = string.Format("{0:C}", kwota - rozliczono);
            }
        }

        private void rozliczeniaLoad()
        {
            DateTime dataOd = okresRozliczen.DateFrom;
            DateTime dataDo = okresRozliczen.DateTo;

            List<RozliczenieRow> ds = new List<RozliczenieRow>();

            var rozrachunki = ((Enova.Business.Old.Core.IRozrachunkiQuery)kontrahent).RozrachunkiQuery.Where(r => r.Data >= dataOd && r.Data <= dataDo).ToList();
            foreach (var r in rozrachunki)
            {
                if (r.Dokument != null)
                {
                    if (r.Dokument is Platnosc)
                    {
                        Platnosc platnosc = (Platnosc)r.Dokument;
                        string numer = "";

                        if (platnosc.Dokument is DokumentRozliczeniowy)
                        {
                            RozliczenieSP ro = platnosc.RozliczeniaSPZapłatyQuery.FirstOrDefault();
                            if (ro != null && ro.Dokument is Platnosc)
                            {
                                numer = ((Platnosc)ro.Dokument).NumerDokumentu;
                            }
                        }

                        ds.Add(new RozliczenieRow()
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
                    else if (r.Dokument is Zaplata)
                    {

                        ds.Add(new RozliczenieRow()
                        {
                            Numer = r.Numer,
                            Data = r.Data,
                            Termin = null,//r.Termin,
                            Należność = r.Należność,
                            Zobowiązanie = r.Zobowiązanie,
                            KwotaDokumenty = r.KwotaDokumenty,
                            KwotaZapłaty = r.KwotaZapłaty,
                            Opis = ((Zaplata)r.Dokument).Opis
                        });

                        var rozliczenia = ((Zaplata)r.Dokument).RozliczeniaSPQuery.ToList();
                        if (rozliczenia.Count == 1)
                        {
                            var rozliczenie = rozliczenia.First();
                            if (rozliczenie.Dokument is Zaplata)
                                ds.Last().NumerDokumentu = ((Zaplata)rozliczenie.Dokument).NumerDokumentu;
                            else if (rozliczenie.Dokument is Platnosc)
                                ds.Last().NumerDokumentu = ((Platnosc)rozliczenie.Dokument).NumerDokumentu;

                        }
                        else
                        {
                            foreach (var ro in rozliczenia)
                            {
                                if (ro.Dokument is Platnosc)
                                {
                                    string numer = "";

                                    if (ro.Dokument is Zaplata)
                                        numer = ((Zaplata)ro.Dokument).NumerDokumentu;
                                    else if (ro.Dokument is Platnosc)
                                        numer = ((Platnosc)ro.Dokument).NumerDokumentu;

                                    Platnosc platnosc = (Platnosc)ro.Dokument;
                                    ds.Add(new RozliczenieRow()
                                    {
                                        Data = null,
                                        Termin = null,
                                        Należność = null,
                                        Zobowiązanie = null,
                                        NumerDokumentu = numer + " - " + ro.KwotaZaplatyValue + " zł"
                                    });
                                }
                            }
                        }
                    }
                }

            }

            rozliczeniaBindingSource.DataSource = ds;
            decimal? sumaDokumenty = ds.Sum(r => r.KwotaDokumenty);
            decimal? sumaZaplaty = ds.Sum(r => r.KwotaZapłaty);
            decimal? saldo = sumaDokumenty - sumaZaplaty;

            sumaDokumentyTextBox.Text = string.Format("{0:C}", sumaDokumenty);
            sumaZapłatyTextBox.Text = string.Format("{0:C}", sumaZaplaty);
            saldoTextBox.Text = string.Format("{0:C}", saldo);

        }


        private void okButton_Click(object sender, EventArgs e)
        {
            if (kontrahent != null && (kontrahent.EntityState == EntityState.Modified || kontrahent.WindykacjaChanged || kontrahent.ZakończenieWindykacjiChanged ))
            {
                kontrahent.SaveChanges(DataContext);
                if (kontrahent.WindykacjaChanged)
                    DataSource.WindykacjaStr = kontrahent.CreateWindykacjaStr();
                if (kontrahent.ZakończenieWindykacjiChanged)
                    DataSource.ZakończenieWindykacji = kontrahent.ZakończenieWindykacji;
            }
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            if (kontrahent != null && kontrahent.EntityState == EntityState.Modified)
            {
                DataContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, kontrahent);
            }
        }

        private void terminComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadRozrachunki();
        }

        private void terminGranicznyComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadRozrachunki();
        }

        private void windykacjaTabPage_Click(object sender, EventArgs e)
        {

        }

        private void rozrachunkiDataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = rozrachunkiDataGrid.Rows[e.RowIndex];
            if (row != null)
            {
                RozrachunekIdx rozrachunek = (RozrachunekIdx)row.DataBoundItem;
                if (rozrachunek != null && rozrachunek.Dokument is Platnosc)
                {
                    /*
                    if(((Platnosc)rozrachunek.Dokument).Dokument is DokumentHandlowy)
                        new Enova.Forms.Handel.DokHandlowyEditForm() { DataSource = ((Platnosc)rozrachunek.Dokument).Dokument }.ShowDialog();
                     */
                }
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Name == "rozliczeniaTabPage" && !rozliczeniaLoaded )
            {

                rozliczeniaLoad();
                rozliczeniaLoaded = true;
            }
        }

        private void okresRozliczen_Changed(object sender, EventArgs e)
        {
            rozliczeniaLoad();
        }


    }
}
