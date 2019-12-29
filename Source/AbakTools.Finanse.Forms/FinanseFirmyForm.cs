using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data.Entity;
using Enova.Business.Old.DB;

[assembly: BAL.Forms.MenuAction("Finanse\\Finanse firmy", typeof(AbakTools.Finanse.Forms.FinanseFirmyForm), Priority = 350)]

namespace AbakTools.Finanse.Forms
{
    public partial class FinanseFirmyForm : Enova.Business.Old.Forms.DataBaseForm
    {
        const string kfSybbol = "FI11";
        const string bzwbkSymbol = "BZ WBK";
        const string multibankSymbol = "MultiBank";

        decimal kasaFirmowa;
        decimal pozostałeKasy;
        decimal bzwbk;
        decimal multibank;
        decimal naleznosci;
        decimal zobowiazania;
        decimal magazynFirmowy;
        decimal magazynRadom;
        decimal magazynWysyłki;
        decimal kredyty;
        decimal pożyczki;
        decimal wdrodze;
        decimal saldo;

        public FinanseFirmyForm()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FinanseFirmyForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            kasaFirmowa = 0;
            pozostałeKasy = 0;
            bzwbk = 0;
            multibank = 0;
            naleznosci = 0;
            zobowiazania = 0;
            magazynFirmowy = 0;
            magazynRadom = 0;
            magazynWysyłki = 0;
            kredyty = 0;
            pożyczki = 0;
            wdrodze = 0;
            saldo = 0;

            var kgESP = DataContext.KasaFirmowa;
            if (kgESP != null)
            {
                kasaFirmowa = kgESP.Saldo;
                kasaFirmowaTextBox.Text = string.Format("{0:C}", kasaFirmowa);
            }

            foreach (var kasa in DataContext.PozostałeKasy.ToList())
            {
                pozostałeKasy += kasa.Saldo;
            }

            pozostałekasyTextBox.Text = string.Format("{0:C}", pozostałeKasy);

            var bzwbkESP = DataContext.EwidencjeSP.Where(e => e.Symbol == bzwbkSymbol).FirstOrDefault();
            if (bzwbkESP != null)
            {
                bzwbk = bzwbkESP.Saldo;
                bzwbkTextBox.Text = string.Format("{0:C}", bzwbk);
            }

            var multibankESP = DataContext.EwidencjaSPBySymbol(multibankSymbol);
            if (multibankESP != null)
            {
                multibank = multibankESP.Saldo;
                multibankTextBox.Text = string.Format("{0:C}", multibank);
            }

            /*
            int n = (int)Enova.Business.Types.TypRozrachunku.Należność;
            int z = (int)Enova.Business.Types.TypRozrachunku.Zobowiązanie;

            DateTime max = new DateTime(2099, 12, 31, 0, 0, 0);

            decimal? na = DataContext.RozrachunkiIdx.Where(r => r.KwotaValue != r.KwotaRozliczonaValue && r.Typ == n).Sum(r => r.KwotaValue);
            decimal? zo = DataContext.RozrachunkiIdx.Where(r => r.KwotaValue != r.KwotaRozliczonaValue && r.Typ == z).Sum(r => r.KwotaValue);

            naleznosci = na == null ? 0 : na.Value;
            zobowiazania = zo == null ? 0 : -zo.Value;
             */

            naleznosci = DataContext.Należności;
            zobowiazania = -DataContext.Zobowiązania;

            należnosciTextBox.Text = string.Format("{0:C}", naleznosci);
            zobowiazaniaTextBox.Text = string.Format("{0:C}", zobowiazania);

            magazynFirmowy = DataContext.MagazynBySymbol("G").WartoscBrutto;
            magazynFirmowyTextBox.Text = string.Format("{0:C}", magazynFirmowy);

            magazynRadom = DataContext.MagazynBySymbol("LP").WartoscBrutto;
            magazynRadomTextBox.Text = string.Format("{0:C}", magazynRadom);

            magazynWysyłki = DataContext.MagazynBySymbol("NUL").WartoscBrutto;
            magazynWysyłkiTextBox.Text = string.Format("{0:C}", magazynWysyłki);

            var banki = (from b in DataContext.Banki where b.Kod.StartsWith(" KREDYT") select b).ToList();
            foreach (var b in banki)
            {
                kredyty += -b.Saldo;
            }

            kredytyTextBox.Text = string.Format("{0:C}", kredyty);

            var malin = DataContext.KontrahentByKod(" MALIN PRYWATNIE");
            if (malin != null)
            {
                decimal? wp = malin.RozrachunkiWpłaty.Sum(r => r.KwotaValue);
                decimal? wy = malin.RozrachunkiWypłaty.Sum(r => r.KwotaValue);
                pożyczki = (wy == 0 ? 0 : wy.Value) - (wp == null ? 0 : wp.Value);
            }

            var kwdrodze = DataContext.KontrahentByKod("  KASA (KASA->KASA)");
            if (kwdrodze != null)
            {
                decimal? wp = kwdrodze.RozrachunkiWpłaty.Sum(r => r.KwotaValue);
                decimal? wy = kwdrodze.RozrachunkiWypłaty.Sum(r => r.KwotaValue);
                wdrodze = (wy == null ? 0 : wy.Value) - (wp == null ? 0 : wp.Value);
            }



            pożyczkiTextBox.Text = string.Format("{0:C}", pożyczki);
            wdrodzeTextBox.Text = string.Format("{0:C}", wdrodze);

            saldo =
                (cbKasaFirm.Checked ? kasaFirmowa : 0) +
                (cbPozostałeKasy.Checked ? pozostałeKasy : 0) +
                (cbBzwbk.Checked ? bzwbk : 0) +
                (cbMultibank.Checked ? multibank : 0) +
                (cbNaleznosci.Checked ? naleznosci : 0) +
                (cbZobowiazania.Checked ? zobowiazania : 0) +
                (cbMagazynFirmowy.Checked ? magazynFirmowy : 0) +
                (cbMagazynRadom.Checked ? magazynRadom : 0) +
                (cbMagazynWysylki.Checked ? magazynWysyłki : 0) +
                (cbKredyty.Checked ? kredyty : 0) +
                (cbPozyczki.Checked ? pożyczki : 0) +
                (cbWdrodze.Checked ? wdrodze : 0);
            saldoTextBox.Text = string.Format("{0:C}", saldo);
                
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            this.loadData();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
