using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Linq;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using Enova.Business.Old.DB;
using Enova.Business.Old.Types;
using Enova.Business.Old.Forms;
using Enova.Old.Handel;
using AbakTools.Printer;
using Enova.Forms.Services;
using Enova.API.Types;

[assembly: BAL.Forms.MenuAction("Prowizje\\Prowizje przedstawicieli", typeof(AbakTools.Kadry.Forms.ProwizjeForm), Priority = 15)]

namespace AbakTools.Kadry.Forms
{
    public partial class ProwizjeForm : DataBaseFormWithEnovaAPI
    {
        private SortedSet<DokumentProwizjaRow> dokSprzedazy = null;
        private SortedSet<DokumentProwizjaRow> dokKorekty = null;
        private SortedSet<DokumentProwizjaRow> dokPotrącenia = null;
        private List<DokumentProwizjaRow> dokObrotyFV = null;
        private List<DokumentProwizjaRow> dokObrotyFK = null;
        private IEnumerable<Enova.API.Kasa.RozliczenieSP> rozliczeniaSp = null;
        //private List<Enova.API.Kasa.RozliczenieSP> rozliczeniaFV = null;
        //private List<Enova.API.Kasa.RozliczenieSP> rozliczeniaFK = null;
        
        private decimal? prowizjaFV;
        private decimal? prowizjaFK;
        private decimal? prowizjaSuma;
        decimal sumaKorektNetto = 0;
        decimal sumaSprzedazyNetto = 0;
        private decimal? obrotFV = 0;
        private decimal? obrotFK = 0;
        private decimal? marzaFV = 0;
        private decimal? przeterminowane;
        private decimal? wysylki = 0;
        private int ilość_paczek = 0;
        private decimal? magazynowe;
        private decimal? dowyplaty;
        private decimal? rozliczeniaFVSuma;
        private decimal? rozliczeniaFVSumaRozliczone;
        private decimal? rozliczeniaFKSuma;
        private decimal? rozliczeniaFKSumaRozliczone;


        private decimal procentProwizji;

        private bool fireEvents = true;

        public ProwizjeForm()
        {
            InitializeComponent();
        }

        private void ProwizjeForm_Load(object sender, EventArgs e)
        {
            fireEvents = false;
            if (Enova.Business.Old.DB.Web.User.LoginedUser.IsAdmin.Value || string.IsNullOrEmpty(Enova.Business.Old.DB.Web.User.LoginedUser.AgentCode))
                this.przedstawicieleComboBox.Items.AddRange(BusinessService.Dictionary.GetListaPrzedstawicieli(Session).ToArray());
            else
                this.przedstawicieleComboBox.Items.Add(Enova.Business.Old.DB.Web.User.LoginedUser.AgentCode);

            this.setSumSprzedazBounds();
            this.setSumKorektyBounds();
            this.setSumPotraceniaBounds();

            this.miesiąceComboBox.SelectedIndex = DateTime.Now.Month - 1;
            this.rokTextBox.Text = DateTime.Now.Year.ToString();
            karyWgCechyCheckBox.Checked = false;
            fireEvents = true;
        }

        private void setSumSprzedazBounds()
        {
            Rectangle r;
            DataGridViewColumn c;
            c = this.dokSprzedazyDataGridView.Columns["cenaAbakSprzedaz"];
            if (c != null)
            {
                r = this.dokSprzedazyDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                this.sumaCenaAbakSprzedazTextBox.Location = new Point(r.X + this.dokSprzedazyDataGridView.Location.X, this.sumaCenaAbakSprzedazTextBox.Location.Y);
                this.sumaCenaAbakSprzedazTextBox.Size = new Size(r.Width, this.sumaCenaAbakSprzedazTextBox.Size.Height);
            }
            c = this.dokSprzedazyDataGridView.Columns["wartoscSprzedazyNetto"];
            if (c != null)
            {
                r = this.dokSprzedazyDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                this.sumaSprzedazTextBox.Location = new Point(r.X + this.dokSprzedazyDataGridView.Location.X, this.sumaSprzedazTextBox.Location.Y);
                this.sumaSprzedazTextBox.Size = new Size(r.Width, this.sumaSprzedazTextBox.Size.Height);
            }
            c = this.dokSprzedazyDataGridView.Columns["dochodSprzedaz"];
            if (c != null)
            {
                r = this.dokSprzedazyDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                this.sumaDochodSprzedazTextBox.Location = new Point(r.X + this.dokSprzedazyDataGridView.Location.X, this.sumaDochodSprzedazTextBox.Location.Y);
                this.sumaDochodSprzedazTextBox.Size = new Size(r.Width, this.sumaDochodSprzedazTextBox.Size.Height);
            }
            c = this.dokSprzedazyDataGridView.Columns["prowizjaSprzedaz"];
            if (c != null)
            {
                r = this.dokSprzedazyDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                this.sumaProwizjaSprzedazTextBox.Location = new Point(r.X + this.dokSprzedazyDataGridView.Location.X, this.sumaProwizjaSprzedazTextBox.Location.Y);
                this.sumaProwizjaSprzedazTextBox.Size = new Size(r.Width, this.sumaProwizjaSprzedazTextBox.Size.Height);
            }

        }

        private void setSumKorektyBounds()
        {
            Rectangle r;
            DataGridViewColumn c;
            c = this.dokKorektDataGridView.Columns["cenaAbakKorekty"];
            if (c != null)
            {
                r = this.dokKorektDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                this.sumaCenaAbakKorektyTextBox.Location = new Point(r.X + this.dokKorektDataGridView.Location.X, this.sumaCenaAbakKorektyTextBox.Location.Y);
                this.sumaCenaAbakKorektyTextBox.Size = new Size(r.Width, this.sumaCenaAbakKorektyTextBox.Size.Height);
            }
            c = this.dokKorektDataGridView.Columns["wartoscSprzedazyNettoKor"];
            if (c != null)
            {
                r = this.dokKorektDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                sumaSprzedazNettoKorTextBox.Location = new Point(r.X + dokKorektDataGridView.Location.X, sumaSprzedazNettoKorTextBox.Location.Y);
                sumaSprzedazNettoKorTextBox.Size = new Size(r.Width, sumaSprzedazNettoKorTextBox.Size.Height);
            }
            c = this.dokKorektDataGridView.Columns["dochodKorekty"];
            if (c != null)
            {
                r = this.dokKorektDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                sumaDochodKorektyTextBox.Location = new Point(r.X + dokKorektDataGridView.Location.X, sumaDochodKorektyTextBox.Location.Y);
                sumaDochodKorektyTextBox.Size = new Size(r.Width, sumaDochodKorektyTextBox.Size.Height);
            }
            c = this.dokKorektDataGridView.Columns["prowizjaKorektyColumn"];
            if (c != null)
            {
                r = this.dokKorektDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                sumaProwizjaKorektyTextBox.Location = new Point(r.X + dokKorektDataGridView.Location.X, sumaProwizjaKorektyTextBox.Location.Y);
                sumaProwizjaKorektyTextBox.Size = new Size(r.Width, sumaProwizjaKorektyTextBox.Size.Height);
            }
        }

        private void setSumPotraceniaBounds()
        {
            Rectangle r;
            DataGridViewColumn c;
            c = this.dokPrzeterminowaneDataGridView.Columns["wartośćBruttoPotrąceniaColumn"];
            if (c != null)
            {
                r = this.dokPrzeterminowaneDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                this.sumaWartoscBruttoPotraceniaTextBox.Location = new Point(r.X + this.dokKorektDataGridView.Location.X, this.sumaWartoscBruttoPotraceniaTextBox.Location.Y);
                this.sumaWartoscBruttoPotraceniaTextBox.Size = new Size(r.Width, this.sumaWartoscBruttoPotraceniaTextBox.Size.Height);
            }
            c = this.dokPrzeterminowaneDataGridView.Columns["doRozliczeniaPotrącenia"];
            if (c != null)
            {
                r = this.dokPrzeterminowaneDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                this.sumaDoRozliczeniaPotraceniaTextBox.Location = new Point(r.X + this.dokPrzeterminowaneDataGridView.Location.X, this.sumaDoRozliczeniaPotraceniaTextBox.Location.Y);
                this.sumaDoRozliczeniaPotraceniaTextBox.Size = new Size(r.Width, this.sumaDoRozliczeniaPotraceniaTextBox.Size.Height);
            }
            c = this.dokPrzeterminowaneDataGridView.Columns["prowizjaPotrąceniaColumn"];
            if (c != null)
            {
                r = this.dokPrzeterminowaneDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                this.sumaProwizjaPotraceniaTextBox.Location = new Point(r.X + this.dokPrzeterminowaneDataGridView.Location.X, this.sumaProwizjaPotraceniaTextBox.Location.Y);
                this.sumaProwizjaPotraceniaTextBox.Size = new Size(r.Width, this.sumaProwizjaPotraceniaTextBox.Size.Height);
            }
            c = this.dokPrzeterminowaneDataGridView.Columns["potrącenieProwizjiPotrąceniaColumn"];
            if (c != null)
            {
                r = this.dokPrzeterminowaneDataGridView.GetCellDisplayRectangle(c.Index, -1, false);
                this.sumaPotraceniaProwizjiPotraceniaTextBox.Location = new Point(r.X + this.dokPrzeterminowaneDataGridView.Location.X, this.sumaPotraceniaProwizjiPotraceniaTextBox.Location.Y);
                this.sumaPotraceniaProwizjiPotraceniaTextBox.Size = new Size(r.Width, this.sumaPotraceniaProwizjiPotraceniaTextBox.Size.Height);
            }

        }

        private void obliczButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            procentProwizji = decimal.Parse(procentProwizjiTextBox.Text);
            decimal współczynnik = decimal.Parse(współczynnikTextBox.Text);

            DokumentyProwizjeViewRow.ProcentProwizji = procentProwizji;
            DokumentyProwizjeViewRow.WspółczynnikKary = współczynnik;
            
            dokumentyLoad();
            this.prowizjaSuma = this.prowizjaFV + this.prowizjaFK;
            dokumentyPrzeterminowaneLoad();
            wysyłkiLoad();
            podsumowanieLoad();
            this.Cursor = Cursors.Default;
            this.Enabled = true;
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

        private void dokumentyLoad()
        {
            string przedstawiciel = (string)this.przedstawicieleComboBox.SelectedItem;
            DateTime dataOd = getDataOd();
            DateTime dataDo = getDataDo();
            bool karaWgCechy = karyWgCechyCheckBox.Checked;
            string cechaKary = string.IsNullOrEmpty(cechaKaryTextBox.Text) ? "KARY" : cechaKaryTextBox.Text;
            //decimal wspolczynnikKary = karaWgCechy ? 0.50M : decimal.Parse(współczynnikTextBox.Text) - 1M;
            decimal wspolczynnikKary = karaWgCechy ? 0.50M : decimal.Parse(współczynnikTextBox.Text);
            prowizjaFV = 0;
            prowizjaFK = 0;
            this.dokSprzedazy = new SortedSet<DokumentProwizjaRow>(new DokumentProwizjaNumerComparer());
            this.dokKorekty = new SortedSet<DokumentProwizjaRow>(new DokumentProwizjaNumerComparer());
            using(var dc = new EnovaContext())
            {
                var to = dataDo.Date.AddDays(1);
                var sql = string.Format(
                    "SELECT dh.ID,dh.Data,dh.NumerPelny,dh.Korekta,dh.Magazyn,k.Kod KontrahentKod,k.Nazwa KontrahentNazwa,dh.SumaNetto SprzedazNetto,"+
                    "dh.SumaBrutto SprzedazBrutto,r.DataRozliczenia FROM dbo.RozrachunkiIdx r INNER JOIN dbo.Platnosci p ON (p.ID = r.Dokument) "+
                    "INNER JOIN dbo.DokHandlowe dh ON (dh.ID = p.Dokument) INNER JOIN dbo.Kontrahenci k ON (k.ID = dh.Kontrahent) "+
                    "INNER JOIN dbo.Features f ON (f.Parent = dh.ID AND f.ParentType='DokHandlowe' AND f.Name='PRZEDSTAWICIEL') "+
                    "WHERE r.PodmiotType = 'Kontrahenci' AND r.DokumentType = 'Platnosci' AND p.DokumentType = 'DokHandlowe' AND p.Bufor = 0 "+
                    "AND f.Data='{0}' AND r.DataRozliczenia >= '{1}' AND r.DataRozliczenia<='{2}' "+
                    "ORDER BY dh.Data, dh.NumerPelny", przedstawiciel, dataOd.Date.ToShortDateString(), to.ToShortDateString());
                var dokInfos = dc.ExecuteStoreQuery<DokumentInfo>(sql).ToList();
                foreach(var dokInfo in dokInfos)
                {
                    dokInfo.Przelicz(dc);
                    var dok = new DokumentProwizjaRow(dokInfo, procentProwizji);
                    dok.KaryWgCechy = karaWgCechy;
                    dok.CechaKary = cechaKary;
                    dok.WspolczynnikKary = wspolczynnikKary;
                    if (dok.Korekta)
                        this.dokKorekty.Add(dok);
                    else
                        this.dokSprzedazy.Add(dok);
                }
            } //using
            sumaSprzedazyNetto = dokSprzedazy.Sum(d => d.WartoscSprzedazyNetto);
            prowizjaFV = dokSprzedazy.Sum(d => d.Prowizja);
            sumaCenaAbakSprzedazTextBox.Text = string.Format("{0:C2}", dokSprzedazy.Sum(d => d.WartoscCenaAbak));
            sumaSprzedazTextBox.Text = string.Format("{0:C2}", sumaSprzedazyNetto);
            sumaDochodSprzedazTextBox.Text = string.Format("{0:C2}", dokSprzedazy.Sum(d => d.Dochod));
            sumaProwizjaSprzedazTextBox.Text = string.Format("{0:C2}", prowizjaFV);
            this.dokSprzedazyDataGridView.DataSource = this.dokSprzedazy.ToList();

            prowizjaFK = dokKorekty.Sum(d => d.Prowizja);
            sumaKorektNetto = this.dokKorekty.Sum(d => d.WartoscSprzedazyNetto);
            sumaCenaAbakKorektyTextBox.Text = string.Format("{0:C}", this.dokKorekty.Sum(d => d.WartoscCenaAbak));
            sumaSprzedazNettoKorTextBox.Text = string.Format("{0:C}", sumaKorektNetto);
            sumaDochodKorektyTextBox.Text = string.Format("{0:C}", this.dokKorekty.Sum(d => d.Dochod));
            sumaProwizjaKorektyTextBox.Text = string.Format("{0:C}", this.prowizjaFK);
            this.dokKorektDataGridView.DataSource = this.dokKorekty.ToList();
        }

        private void dokumentyPrzeterminowaneLoad()
        {
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
            dokPotrącenia = new SortedSet<DokumentProwizjaRow>(new DokumentProwizjaNumerComparer(true));
            using (var dc = new EnovaContext())
            {
                //("Data >= '{0}' AND Data<='{1}' AND DataRozliczenia >= '{2}' AND (Typ = 10 OR Typ = 11)", okresOd, okresDo, dataDo.AddDays(1))
                var sql = string.Format(
                    "SELECT dh.ID,dh.Data,dh.NumerPelny,dh.Korekta,dh.Magazyn,k.Kod KontrahentKod,k.Nazwa KontrahentNazwa,dh.SumaNetto SprzedazNetto," +
                    "dh.SumaBrutto SprzedazBrutto,r.DataRozliczenia,CASE WHEN dh.Korekta=1 THEN -r.KwotaValue ELSE r.KwotaValue END AS Kwota,"+
	                "CASE WHEN dh.Korekta=1 THEN -r.KwotaRozliczonaValue ELSE r.KwotaRozliczonaValue END AS  KwotaRozliczona,r.Termin Termin, "+
                    "CONVERT(BIT,ISNULL(nlp.Data,'0')) NieLiczPotracen " +
                    "FROM dbo.RozrachunkiIdx r INNER JOIN dbo.Platnosci p ON (p.ID = r.Dokument) " +
                    "INNER JOIN dbo.DokHandlowe dh ON (dh.ID = p.Dokument) INNER JOIN dbo.Kontrahenci k ON (k.ID = dh.Kontrahent) " +
                    "INNER JOIN dbo.Features f ON (f.Parent = dh.ID AND f.ParentType='DokHandlowe' AND f.Name='PRZEDSTAWICIEL') " +
                    "LEFT JOIN dbo.Features nlp ON (nlp.Parent = dh.ID AND nlp.ParentType='DokHandlowe' AND nlp.Name='NIE LICZ POTRACEN') "+
                    "WHERE r.PodmiotType = 'Kontrahenci' AND r.DokumentType = 'Platnosci' AND p.DokumentType = 'DokHandlowe' AND p.Bufor = 0 " +
                    "AND f.Data='{0}' AND r.Data >= '{1}' AND r.Data <= '{2}' AND r.DataRozliczenia >= '{3}' " +
                    "ORDER BY dh.Data, dh.NumerPelny", przedstawiciel, okresOd.Date.ToShortDateString(), okresDo.Date.ToShortDateString(), dataDo.AddDays(1).Date.ToShortDateString());
                var dokInfos = dc.ExecuteStoreQuery<DokumentInfo>(sql).ToList();
                foreach(var dokInfo in dokInfos)
                {
                    dokInfo.Przelicz(dc);
                    var dok = new DokumentProwizjaRow(dokInfo, procentProwizji);
                    dok.Rozliczany = !dokInfo.NieLiczPotracen;
                    dok.TerminGraniczny = termin;
                    dok.PrzeliczRozliczenia(dc, dataDo);
                    dokPotrącenia.Add(dok);
                }
            }
            dokPrzeterminowaneDataGridView.DataSource = dokPotrącenia.ToList();
            przeterminiwanePrzelicz();
        }

        decimal sumaPotraceniaDoRozliczenia = 0;

        private void przeterminiwanePrzelicz()
        {
            decimal? sumBrutto = 0;
            decimal? sumDoRozlicz = 0;
            decimal? sumProwizja = 0;
            decimal? sumPotrac = 0;

            foreach (DataGridViewRow row in dokPrzeterminowaneDataGridView.Rows)
            {
                if (row.DataBoundItem != null)
                {
                    DokumentProwizjaRow dok = (DokumentProwizjaRow)row.DataBoundItem;
                    if (dok != null && dok.Rozliczany)
                    {
                        sumBrutto += (decimal)dok.WartoscSprzedazyBrutto;
                        sumDoRozlicz += (decimal)dok.DoRozliczenia;
                        sumPotrac += dok.PotracenieProwizji;
                        sumProwizja += dok.Prowizja;
                    }
                }
            }

            sumaPotraceniaDoRozliczenia = sumDoRozlicz.Value;
            sumaWartoscBruttoPotraceniaTextBox.Text = string.Format("{0:C}", sumBrutto);
            sumaDoRozliczeniaPotraceniaTextBox.Text = string.Format("{0:C}", sumDoRozlicz);
            sumaProwizjaPotraceniaTextBox.Text = string.Format("{0:C}", sumProwizja);
            sumaPotraceniaProwizjiPotraceniaTextBox.Text = string.Format("{0:C}", sumPotrac);

            this.przeterminowane = (decimal)sumPotrac;
        }

        private Dictionary<DateTime, decimal> cenyPaczki = new Dictionary<DateTime, decimal>()
        {
            { new DateTime(2000, 1, 1), 14.17M },
            { new DateTime(2012, 1, 1), 14.90M },
            { new DateTime(2012, 3, 1), 15.04M },
            { new DateTime(2013, 2, 1), 11.00M },
            { new DateTime(2013, 7, 1), 12.43M }
        };

        private decimal GetCenaPaczki()
        {
            DateTime datefrom = getDataOd().Date;
            decimal cena = 0;
            foreach (KeyValuePair<DateTime, decimal> kvp in cenyPaczki)
            {
                if (kvp.Key <= datefrom)
                    cena = kvp.Value;
            }
            return cena;
        }

        private void wysyłkiLoad()
        {
            string przedstawiciel = this.przedstawicieleComboBox.Text;
            DateTime dataOd = getDataOd();
            DateTime dataDo = getDataDo();
            wysylki = 0;
            ilość_paczek = 0;
            decimal cena = GetCenaPaczki();
            var hm = Session.GetModule<Enova.API.Handel.HandelModule>();

            var kontrahenci = CRMService.Kontrahenci.ByPrzedstawiciel(Session, przedstawiciel).ToList();
            foreach (var k in kontrahenci)
            {
                var rozrachunki = KasaService.RozrachunkiIdx.ByKontrahent(Session, k, Enova.API.Types.FromTo.Create(dataOd, dataDo), null).And("(Numer LIKE 'FV/%' OR Numer LIKE 'PAR/%')").ToList();
                foreach (var r in rozrachunki)
                {
                    var dh = r.Dokument.Dokument as Enova.API.Handel.DokumentHandlowy;
                    if (dh != null)
                    {
                        var przewoźnik = dh.Features["PRZEWOŻNIK"] as string;
                        if (przewoźnik != null && (przewoźnik.Trim().ToLower() == "dhl" || przewoźnik.Trim().ToLower() == "ups" || przewoźnik.Trim().ToLower() == "opek"
                            || przewoźnik.Trim().ToLower() == "tba"))
                        {
                            var placi = dh.Features["PŁACI WYSYŁKĘ"] as string;
                            var paczki = dh.Features["ILOSC PACZEK"] as string;
                            if (paczki != null && !string.IsNullOrEmpty(paczki.Trim()))
                            {
                                int ilosc;
                                if (int.TryParse(paczki.Trim(), out ilosc))
                                {
                                    if (ilosc > 0)
                                    {
                                        if (placi == null || string.IsNullOrEmpty(placi.Trim()) || placi != null && placi.Trim().Length >= 2 
                                            && placi.Trim().ToLower().Substring(0, 2) == przedstawiciel.ToLower())
                                        {
                                            if (paczki != null)
                                            {
                                                ilość_paczek += ilosc;
                                                decimal wartość = ilosc * cena;
                                                wysylki += wartość;
                                            }
                                        }
                                            /*
                                        else if (placi != null)
                                            MessageBox.Show("Wartość cechy \"PŁACI WYSYŁKĘ\" jest nieprawidłowa dla dokumentu: " + dh.NumerPelny + " wartość: " + placi.Trim());
                                             */
                                    }
                                }
                                else if (paczki.Trim().ToLower() == "paleta")
                                {

                                    if (placi != null && placi.Trim().ToLower().Substring(0, 2) == przedstawiciel.ToLower())
                                    {
                                        if (paczki != null)
                                        {
                                            ilość_paczek += 6;
                                            decimal wartość = 6 * cena;
                                            wysylki += wartość;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Wartość cechy \"ILOSC PACZEK\" jest nieprawidłowa dla dokumentu: " + dh.NumerPelny + " wartość: " + paczki.Trim(),
                                        "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Nie wypełniono cechy \"ILOSC PACZEK\" dla dokumentu: " + dh.NumerPelny, "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (przewoźnik != null && !string.IsNullOrEmpty(przewoźnik.Trim()) && przewoźnik.Trim().ToLower() != "poczta")
                        {
                            MessageBox.Show("Nieprawidłowa wartość cechy \"PRZEWOŹNIK\" dla dokumentu: " + dh.NumerPelny + " wartość: " + przewoźnik.Trim(),
                                "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            var paczki = dh.Features["ILOSC PACZEK"] as string;
                            if (paczki != null && !string.IsNullOrEmpty(paczki))
                            {
                                MessageBox.Show("Brak wypełnionej cechy \"PRZEWOŹNIK\" dla dokumentu: " + dh.NumerPelny, "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

            }

            wysylki = -1 * (wysylki - wysylki * (ilość_paczek > 50 ? 50 : ilość_paczek) / 100);
        }


        decimal? obrotFirmaFV = 0;
        decimal? obrotFirmaFK = 0;

        private void podsumowanieLoad()
        {
            string przedstawiciel = (string)this.przedstawicieleComboBox.Text;
            DateTime dataOd = this.getDataOd();
            DateTime dataDo = this.getDataDo();
            int magFirmaID = 1;
            obrotFirmaFV = 0;
            obrotFirmaFK = 0;
            obrotFV = 0;
            obrotFK = 0;
            dokObrotyFV = new List<DokumentProwizjaRow>();
            dokObrotyFK = new List<DokumentProwizjaRow>();
            //rozliczeniaFV = new List<Enova.API.Kasa.RozliczenieSP>();
            //rozliczeniaFK = new List<Enova.API.Kasa.RozliczenieSP>();

            /*
            var kontrahenci = CRMService.Kontrahenci.ByPrzedstawiciel(Session, przedstawiciel).ToList();
            var magFirma = Session.GetModule<Enova.API.Magazyny.MagazynyModule>().Magazyny.Firma;
            var fromTo = FromTo.Create(dataOd, dataDo);
            var km = Session.GetModule<Enova.API.Kasa.KasaModule>();
            foreach (var kh in kontrahenci)
            {
                var rozrachunki = KasaService.RozrachunkiIdx.ByKontrahent(Session, kh, fromTo, null).And("(Typ=10 OR Typ=11)");
                foreach (var r in rozrachunki)
                {
                    var dh = r.Dokument.Dokument as Enova.API.Handel.DokumentHandlowy;
                    if (dh != null)
                    {
                        var dok = new DokumentProwizjaRow(dh, r, procentProwizji);
                        dok.Przelicz();
                        if (dh.Korekta)
                            dokObrotyFK.Add(dok);
                        else
                            dokObrotyFV.Add(dok);
                    }
                }
                rozliczeniaSp = Session.GetModule<Enova.API.Kasa.KasaModule>().RozliczeniaSP[kh, fromTo].ToList();
                foreach (var rsp in rozliczeniaSp)
                {
                    if (rsp.Dokument.Kierunek == Enova.API.Core.KierunekPlatnosci.Przychod)
                        rozliczeniaFV.Add(rsp);
                    else
                        rozliczeniaFK.Add(rsp);
                }

            }
             */
            using (var dc = new EnovaContext())
            {

                var sql = string.Format(
                    "SELECT dh.ID,dh.Data,dh.NumerPelny,dh.Korekta,dh.Magazyn,k.Kod KontrahentKod,k.Nazwa KontrahentNazwa," +
                    "dh.SumaNetto SprzedazNetto,dh.SumaBrutto SprzedazBrutto,r.DataRozliczenia," +
                    "CASE WHEN dh.Korekta=1 THEN -r.KwotaValue ELSE r.KwotaValue END AS Kwota," +
                    "CASE WHEN dh.Korekta=1 THEN -r.KwotaRozliczonaValue ELSE r.KwotaRozliczonaValue END AS  KwotaRozliczona," +
                    "r.Termin Termin FROM dbo.RozrachunkiIdx r INNER JOIN dbo.Platnosci p ON (p.ID = r.Dokument) " +
                    "INNER JOIN dbo.DokHandlowe dh ON (dh.ID = p.Dokument) INNER JOIN dbo.Kontrahenci k ON (k.ID = dh.Kontrahent) " +
                    "INNER JOIN dbo.Features f ON (f.Parent = dh.ID AND f.ParentType='DokHandlowe' AND f.Name='PRZEDSTAWICIEL') " +
                    "WHERE r.PodmiotType = 'Kontrahenci' AND r.DokumentType = 'Platnosci' AND p.DokumentType = 'DokHandlowe' " +
                    "AND p.Bufor = 0 AND f.Data='{0}' AND r.Data >= '{1}' AND r.Data <= '{2}' ORDER BY dh.Data, dh.NumerPelny",
                    przedstawiciel, dataOd.ToShortDateString(), dataDo.ToShortDateString());
                var dokInfos = dc.ExecuteStoreQuery<DokumentInfo>(sql).ToList();
                foreach(var dokInfo in dokInfos)
                {
                    dokInfo.Przelicz(dc);
                    var dok = new DokumentProwizjaRow(dokInfo, procentProwizji);
                    if(dok.Korekta)
                        dokObrotyFK.Add(dok);
                    else
                        dokObrotyFV.Add(dok);
                }
            }

            if (dokObrotyFV.Count > 0)
            {
                obrotFV = dokObrotyFV.Sum(d => d.WartoscSprzedazyNetto);
                obrotFirmaFV = dokObrotyFV.Where(d => d.MagazynID == magFirmaID).Sum(d => d.WartoscSprzedazyNetto);
                decimal obrotDochod = dokObrotyFV.Sum(d => d.Dochod);
                marzaFV = obrotDochod / obrotFV * 100;
            }

            if (dokObrotyFK.Count > 0)
            {
                this.obrotFK = this.dokObrotyFK.Sum(d => d.WartoscSprzedazyNetto);
                obrotFirmaFK = this.dokObrotyFK.Where(d => d.MagazynID == magFirmaID).Sum(d => d.WartoscSprzedazyNetto);
            }

            //this.rozliczeniaFVSuma = this.rozliczeniaFV.Sum(r => r.KwotaZaplaty);
            //this.rozliczeniaFVSumaRozliczone = this.rozliczeniaFV.Where(r => r.Dokument.DataRozliczenia >= dataOd && r.Dokument.DataRozliczenia <= dataDo).Sum(r => r.KwotaZaplaty);
            //this.rozliczeniaFKSuma = this.rozliczeniaFK.Sum(r => r.KwotaZaplaty);
            //this.rozliczeniaFKSumaRozliczone = this.rozliczeniaFK.Where(r => r.Dokument.DataRozliczenia >= dataOd && r.Dokument.DataRozliczenia <= dataDo).Sum(r => r.KwotaZaplaty);

            this.magazynowe = (decimal)(obrotFirmaFV + obrotFirmaFK) * -0.01M;
            podsumowaniePrzelicz();
            podsumowanieLabel.Text = "Podsumowanie " + przedstawicieleComboBox.Text + " " + (miesiąceComboBox.SelectedIndex + 1).ToString() + "/" + rokTextBox.Text;
        }

        private void podsumowaniePrzelicz()
        {
            this.dowyplaty = this.prowizjaSuma + this.przeterminowane + this.wysylki + this.magazynowe;


            prowizjaFVTextBox.Text = string.Format("{0:C}", this.prowizjaFV);
            prowizjaFKTextBox.Text = string.Format("{0:C}", this.prowizjaFK);
            prowizjaSumaTextBox.Text = string.Format("{0:C}", this.prowizjaSuma);

            przeterminowaneTextBox.Text = string.Format("{0:C}", this.przeterminowane);
            wysyłkiTextBox.Text = string.Format("{0:C}", this.wysylki);
            ilośćPaczekTextBox.Text = ilość_paczek.ToString();
            magazynoweTextBox.Text = string.Format("{0:C}", this.magazynowe);
            prowizjaNettoTextBox.Text = string.Format("{0:C}", this.dowyplaty);
            podatekDochTextBox.Text = string.Format("{0:C}", this.dowyplaty * -0.19M);
            doWyplatyTextBox.Text = string.Format("{0:C}", this.dowyplaty - this.dowyplaty * 0.19M);
            podatekVatTextBox.Text = string.Format("{0:C}", this.dowyplaty * 0.23M);

            obrotyFVTextBox.Text = string.Format("{0:C}", this.obrotFV);
            obrotyFKTextBox.Text = string.Format("{0:C}", this.obrotFK);
            marzaFVTextBox.Text = string.Format("{0:P}", this.marzaFV / 100);
            obrotySumaTextBox.Text = string.Format("{0:C}", this.obrotFK + this.obrotFV);

            rozliczeniaFVTextBox.Text = string.Format("{0:C}", this.rozliczeniaFVSuma);
            rozliczeniaFVRozliczoneTextBox.Text = string.Format("{0:C}", this.rozliczeniaFVSumaRozliczone);
            rozliczeniaFKTextBox.Text = string.Format("{0:C}", this.rozliczeniaFKSuma);
            rozliczeniaFKRozliczoneTextBox.Text = string.Format("{0:C}", this.rozliczeniaFKSumaRozliczone);
        }

        private void dokSprzedazyDataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.setSumSprzedazBounds();
        }

        private void dokKorektDataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.setSumKorektyBounds();
        }

        private void dokSprzedazyDrukujButton_Click(object sender, EventArgs e)
        {
            ReportForm raport = new ReportForm(
                "Reports\\DokumentyProwizjeReport.rdlc", "DokumentyProwizjeViewRow", this.dokSprzedazy,
                new ReportParameter[]{
                    new ReportParameter("Tytul","Prowizje - Dokumenty sprzedaży",true),
                    new ReportParameter("Przedstawiciel",(string)this.przedstawicieleComboBox.SelectedItem),
                    new ReportParameter("Okres",(this.miesiąceComboBox.SelectedIndex+1).ToString()+"/"+this.rokTextBox.Text)
                });
            raport.Show();
        }

        

        private void drukujButton_Click(object sender, EventArgs e)
        {
            decimal podatekDochodowy = decimal.Round((decimal)this.dowyplaty * 0.19M, 2);
            ReportForm raport = new ReportForm("Reports\\ProwizjaPodsumowanieReport.rdlc", "RaportProwizjiData",
                new List<RaportProwizjiData>(){ new RaportProwizjiData()
            {
                Przedstawiciel = (string)this.przedstawicieleComboBox.SelectedItem,
                ZaOkres = (this.miesiąceComboBox.SelectedIndex+1).ToString().PadLeft(2,'0') + "/" + this.rokTextBox.Text,
                ProwizjaOdFV = string.Format("{0:C}", this.prowizjaFV),
                ProwizjaOdFVOpis = "(Zapłacone FV: " + string.Format("{0:C}",sumaSprzedazyNetto) + " -> " +(sumaSprzedazyNetto==0 ? 0 : decimal.Round((decimal)prowizjaFV/(decimal)sumaSprzedazyNetto*100,2))+"%)",
                ProwizjaOdFK = string.Format("{0:C}", this.prowizjaFK),
                ProwizjaOdFKOpis = "(Zapłacone FK: " + string.Format("{0:C}",sumaKorektNetto) + " -> " + (sumaKorektNetto == 0 ? 0 : decimal.Round((decimal)prowizjaFK/(decimal)sumaKorektNetto*100,2))+"%)",
                ProwizjaSuma = string.Format("{0:C}", this.prowizjaSuma),
                ProwizjaSumaOpis = "(Suma FV i FK: " + string.Format("{0:C}",sumaSprzedazyNetto+sumaKorektNetto)+" -> " + (sumaSprzedazyNetto == 0 && sumaKorektNetto==0 ? 0 : decimal.Round((decimal)prowizjaSuma/(decimal)(sumaSprzedazyNetto+sumaKorektNetto)*100,2)) + "%)",
                Przeterminowania = string.Format("{0:C}", this.przeterminowane),
                PrzeterminowaniaOpis = "(Od kwoty: " +sumaDoRozliczeniaPotraceniaTextBox.Text+")",
                Wysylki = string.Format("{0:C}", this.wysylki),
                WysylkiOpis = "("+ilość_paczek.ToString()+" p. * "+string.Format("{0:C}",GetCenaPaczki())+ " - "+(ilość_paczek>50?"50":ilość_paczek.ToString())+ ",00%)",
                Magazynowe = string.Format("{0:C}", this.magazynowe),
                MagazynoweOpis = "(1,00% od " + string.Format("{0:C}", decimal.Round((decimal)(obrotFirmaFV+obrotFirmaFK),2))+")",
                ProwizjaNetto = string.Format("{0:C}", this.dowyplaty),
                PodatekDochodowy = string.Format("{0:C}",podatekDochodowy),
                DoWyplaty = string.Format("{0:C}", this.dowyplaty - podatekDochodowy),
                PodatekVat = string.Format("{0:C}",decimal.Round((decimal)this.dowyplaty*0.23M,2))
           }}, null);
            raport.Show();
        }

        private void obrotFVDrukujButton_Click(object sender, EventArgs e)
        {
            string przedstawiciel = (string)this.przedstawicieleComboBox.SelectedItem;
            DateTime dataOd = this.getDataOd();
            DateTime dataDo = this.getDataDo();

            ReportForm raport = new ReportForm("Reports\\DokumentyObrotyReport.rdlc", "DokumentyProwizjeViewRow", dokObrotyFV, null);

            raport.Show();
        }

        private void dokPrzeterminowaneDataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.setSumPotraceniaBounds();
        }

        private void dokKorektDrykujButton_Click(object sender, EventArgs e)
        {
            ReportForm raport = new ReportForm(
                "Reports\\DokumentyProwizjeReport.rdlc", "DokumentyProwizjeViewRow", this.dokKorekty,
                new ReportParameter[]{
                    new ReportParameter("Tytul","Prowizje - Dokumenty korekt",true),
                    new ReportParameter("Przedstawiciel", (string)this.przedstawicieleComboBox.SelectedItem),
                    new ReportParameter("Okres",(this.miesiąceComboBox.SelectedIndex+1).ToString()+"/"+this.rokTextBox.Text)
                });
            raport.Show();
        }

        private void dokPotraceniaDrukujButton_Click(object sender, EventArgs e)
        {
            ReportForm raport = new ReportForm(
                "Reports\\DokumentyPotraceniaReport.rdlc", "DokumentyProwizjeViewRow", this.dokPotrącenia.Where(d=>d.Rozliczany == true),
                new ReportParameter[]{
                    new ReportParameter("Przedstawiciel", (string)this.przedstawicieleComboBox.SelectedItem),
                    new ReportParameter("Okres",(this.miesiąceComboBox.SelectedIndex+1).ToString()+"/"+this.rokTextBox.Text)
                });
            raport.Show();
        }

        private void obrotFKDrukujButton_Click(object sender, EventArgs e)
        {
            string przedstawiciel = (string)this.przedstawicieleComboBox.SelectedItem;
            ReportForm raport = new ReportForm("Reports\\DokumentyListaReport.rdlc", "DokumentyViewRow",dokObrotyFK, null);
            raport.Show();
        }

        private void rozliczeniaDrukujButton_Click(object sender, EventArgs e)
        {
            /*
            ReportForm raport = new ReportForm("Reports\\RozliczeniaSpReport.rdlc", "RozliczeniaSpViewRow", this.rozliczeniaFV, null);
            raport.Show();
             */
        }

        private void rozliczeniaFKDrukujButton_Click(object sender, EventArgs e)
        {
            /*
            ReportForm raport = new ReportForm("Reports\\RozliczeniaSpReport.rdlc", "RozliczeniaSpViewRow", this.rozliczeniaFK, null);
            raport.Show();
             */
        }

        private void dokPrzeterminowaneDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dokPrzeterminowaneDataGridView.Rows.Count)
            {
                DataGridViewCell cell = dokPrzeterminowaneDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (cell.OwningColumn.Name == "RozliczanaColumn")
                    dokPrzeterminowaneDataGridView.EndEdit();
            }

        }

        private void dokPrzeterminowaneDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (fireEvents && e.RowIndex>=0 && e.RowIndex < dokPrzeterminowaneDataGridView.Rows.Count)
            {
                DataGridViewRow row = dokPrzeterminowaneDataGridView.Rows[e.RowIndex];
                if (row.DataBoundItem != null)
                {
                    DokumentProwizjaRow dok = (DokumentProwizjaRow)row.DataBoundItem;
                    using(var dc = new EnovaContext())
                    {
                        var f = dc.Features.Where(r => r.Lp == 0 && r.ParentType == "DokHandlowe"
                            && r.Parent == dok.DokumentID && r.Name == "NIE LICZ POTRACEN").FirstOrDefault();
                        if(f == null)
                        {
                            f = new Feature()
                            {
                                Lp = 0,
                                ParentType = "DokHandlowe",
                                Parent = dok.DokumentID,
                                Name = "NIE LICZ POTRACEN"
                            };
                            dc.Features.AddObject(f);
                        }
                        f.Data = dok.Rozliczany ? "0" : "1";
                        f.DataKey = f.Data;
                        dc.SaveChanges();
                    }
                    przeterminiwanePrzelicz();
                    podsumowaniePrzelicz();
                }
            }

        }

        private void karyWgCechyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (karyWgCechyCheckBox.Checked)
            {
                label31.Enabled = true;
                cechaKaryTextBox.Enabled = true;
                label29.Enabled = false;
                współczynnikTextBox.Enabled = false;
            }
            else
            {
                label31.Enabled = false;
                cechaKaryTextBox.Enabled = false;
                label29.Enabled = true;
                współczynnikTextBox.Enabled = true;
            }
        }

        private void rozliczeniaProwButton_Click(object sender, EventArgs e)
        {
            var przedstawiciel = (string)przedstawicieleComboBox.SelectedItem;
            if (przedstawiciel != null)
            {
                Pracownik pracownik = DataContext.Pracownicy.Where(p => p.Kod == przedstawiciel).FirstOrDefault();
                if (pracownik != null)
                {
                    RozliczeniaProwizjiForm form = new RozliczeniaProwizjiForm();
                    form.Pracownik = pracownik;
                    form.DataOd = getDataOd();
                    form.DataDo = getDataDo();
                    form.ShowDialog();
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy napewno chcesz zapisać dane prowizji?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                string przedstawiciel = (string)przedstawicieleComboBox.SelectedItem;
                if (przedstawiciel != null)
                {
                    int miesiac = miesiąceComboBox.SelectedIndex + 1;
                    int rok = int.Parse(rokTextBox.Text);
                    Enova.Business.Old.DB.Web.Prowizja prowizja = Enova.Business.Old.Core.ContextManager.WebContext.Prowizje
                        .Where(p => p.Przedstawiciel == przedstawiciel && p.Miesiac == miesiac && p.Rok == rok).FirstOrDefault();
                    if (prowizja == null)
                    {
                        prowizja = new Enova.Business.Old.DB.Web.Prowizja()
                        {
                            Przedstawiciel = przedstawiciel,
                            Miesiac = miesiac,
                            Rok = rok
                        };
                        Enova.Business.Old.Core.ContextManager.WebContext.AddToProwizje(prowizja);
                    }

                    prowizja.ProwizjaOdFV = decimal.Round((decimal)this.prowizjaFV, 2);
                    prowizja.ProwizjaOdFK = decimal.Round((decimal)this.prowizjaFK, 2);
                    prowizja.ProwizjaSuma = decimal.Round((decimal)this.prowizjaSuma, 2);
                    prowizja.SumaSprzedazyNetto = decimal.Round((decimal)sumaSprzedazyNetto, 2);
                    prowizja.SumaKorektNetto = decimal.Round((decimal)sumaKorektNetto, 2);
                    prowizja.Przeterminowane = decimal.Round((decimal)przeterminowane, 2);
                    prowizja.PrzeterminowaneSumaDokumenty = decimal.Round((decimal)sumaPotraceniaDoRozliczenia, 2);
                    prowizja.Wysylki = decimal.Round((decimal)wysylki, 2);
                    prowizja.WysylkiIloscPaczek = ilość_paczek;
                    prowizja.Magazynowe = decimal.Round((decimal)magazynowe, 2);
                    prowizja.ObrotFirmaFV = decimal.Round((decimal)obrotFV, 2);
                    prowizja.ObrotFirmaFK = decimal.Round((decimal)obrotFK, 2);

                    Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();
                    loadSave();
                }
            }
        }

        private void raportOkresowyButton_Click(object sender, EventArgs e)
        {
            RaporOkresowyProwizjiForm form = new RaporOkresowyProwizjiForm();
            var pr = (string)przedstawicieleComboBox.SelectedItem;
            if (!string.IsNullOrWhiteSpace(pr))
                form.Przedstawiciel = pr;
            form.Rok = int.Parse(rokTextBox.Text);
            form.ShowDialog();
        }

        private void loadSave()
        {
            var przedstawiciel = (string)przedstawicieleComboBox.SelectedItem;
            if (przedstawiciel != null)
            {
                int miesiac = miesiąceComboBox.SelectedIndex + 1;
                int rok = int.Parse(rokTextBox.Text);
                var prowizja = Enova.Business.Old.Core.ContextManager.WebContext.Prowizje
                    .Where(p => p.Przedstawiciel == przedstawiciel && p.Miesiac == miesiac && p.Rok == rok).FirstOrDefault();
                if (prowizja != null)
                {
                    decimal? prowNetto = prowizja.ProwizjaSuma + prowizja.Przeterminowane + prowizja.Wysylki + prowizja.Magazynowe;
                    decimal? podDoch = prowNetto * -0.19M;

                    prowFVSaveTextBox.Text = string.Format("{0:C}", prowizja.ProwizjaOdFV);
                    prowFKSaveTextBox.Text = string.Format("{0:C}", prowizja.ProwizjaOdFK);
                    prowSumaSaveTextBox.Text = string.Format("{0:C}", prowizja.ProwizjaSuma);
                    przeterSaveTextBox.Text = string.Format("{0:C}", prowizja.Przeterminowane);
                    wysylkiSaveTextBox.Text = string.Format("{0:C}", prowizja.Wysylki);
                    paczkiSaveTextBox.Text = prowizja.WysylkiIloscPaczek.ToString();
                    magazynSaveTextBox.Text = string.Format("{0:C}", prowizja.Magazynowe);
                    prowNettoSaveTextBox.Text = string.Format("{0:C}", prowNetto);
                    podDochSaveTextBox.Text = string.Format("{0:C}", podDoch);
                    doWyplatySaveTextBox.Text = string.Format("{0:C}", prowNetto + podDoch);
                    vatSaveTextBox.Text = string.Format("{0:C}", prowNetto * 0.23M);
                    
                    return;
                }
            }
            prowFVSaveTextBox.Text = "";
            prowFKSaveTextBox.Text = "";
            prowSumaSaveTextBox.Text = "";
            przeterSaveTextBox.Text = "";
            wysylkiSaveTextBox.Text = "";
            paczkiSaveTextBox.Text  = "";
            magazynSaveTextBox.Text = "";
            prowNettoSaveTextBox.Text = "";
            podDochSaveTextBox.Text = "";
            doWyplatySaveTextBox.Text = "";
            vatSaveTextBox.Text = "";
        }

        private void przedstawicieleComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadSave();
        }

        private void miesiąceComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadSave();
        }

        private void rokTextBox_Leave(object sender, EventArgs e)
        {
            loadSave();
        }

        #region Nested Types


        public class DokumentyNumerPelnyComparer : Comparer<DokumentyProwizjeViewRow>
        {
            private bool grupujKontrahentami;

            public DokumentyNumerPelnyComparer(bool grupujKontrahentami) 
            {
                this.grupujKontrahentami = grupujKontrahentami;
            }

            public DokumentyNumerPelnyComparer() : this(false) { }

            public override int Compare(DokumentyProwizjeViewRow x, DokumentyProwizjeViewRow y)
            {
                int cmp = 0;
                if (grupujKontrahentami)
                    cmp = x.KodKontrahenta.CompareTo(y.KodKontrahenta);
                if (cmp == 0)
                    cmp = x.NumerPelny.CompareTo(y.NumerPelny);
                return cmp;
            }
        }

        #endregion
    }
}
