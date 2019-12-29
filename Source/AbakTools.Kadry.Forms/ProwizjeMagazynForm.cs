using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.SqlClient;
using System.Windows.Forms;
using Enova.Business.Old.DB;
using DBWeb = Enova.Business.Old.DB.Web;
using Microsoft.Reporting.WinForms;
using Enova.Business.Old.Types;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using AbakTools.Printer;

[assembly: BAL.Forms.MenuAction("Prowizje\\Prowizje magazynu", typeof(AbakTools.Kadry.Forms.ProwizjeMagazynForm), Priority = 20)]

namespace AbakTools.Kadry.Forms
{
    public partial class ProwizjeMagazynForm : Form
    {
        DateTime dataOd;
        DateTime dataDo;

        decimal? obrot1 = 0;
        decimal? obrot2 = 0;
        decimal? obrot3 = 0;
        decimal? obrot4 = 0;
        decimal? obrot5 = 0;
        decimal dochod1 = 0;
        decimal dochod2 = 0;
        decimal dochod3 = 0;
        decimal dochod4 = 0;
        decimal dochod5 = 0;
        decimal procent1 = 0;
        decimal procent2 = 0;
        decimal procent3 = 0;
        decimal procent4 = 0;
        decimal procent5 = 0;
        decimal procentMG = 26.67M;
        decimal procentLD = 20M;
        decimal procentPD = 26.67M;
        decimal procentSD = 26.67M;
        decimal sumaMG = 0;
        decimal sumaLD = 0;
        decimal sumaPD = 0;
        decimal sumaSD = 0;
        decimal prowizjaMG = 0;
        decimal prowizjaLD = 0;
        decimal prowizjaPD = 0;
        decimal prowizjaSD = 0;
        decimal inneMG = 0;
        decimal inneLD = 0;
        decimal innePD = 0;
        decimal inneSD = 0;
        decimal prowizjaSuma = 0;
        decimal obrotDemo = 0;
        decimal dochodDemo = 0;
        decimal procentDemo = 3;
        decimal prowizjaDemo = 0;
        decimal wartoscKary = 0;
        decimal procentKary = 100M;
        decimal wspolczynnikKary = 1.25M;
        decimal prowizjaKary = 0;

        bool przeliczono = false;

        string[] magPBoxes = new string[] { "tbMGP", "tbLDP", "tbPDP", "tbSDP" };
        string[] magBoxes = new string[] { "tbMG", "tbLD", "tbPD", "tbSD" };

        public ProwizjeMagazynForm()
        {
            InitializeComponent();
        }


        private void ProwizjeMagazynForm_Load(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;

            for (int y = year - 3; y <= year; y++)
                comboBox2.Items.Add(y);

            comboBox1.SelectedIndex = DateTime.Now.Month - 1;
            comboBox2.SelectedIndex = 3;

            data_SelectionChangeCommitted(null, null);
            obliczSumaProcent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                comboBox1.Enabled = true;
                zapiszGodzinyButton.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                zapiszGodzinyButton.Enabled = false;
            }
            data_SelectionChangeCommitted(null, null);
        }

        private decimal karaWgStawki(EnovaContext ec, int magazyn, DateTime dataOd, DateTime dataDo, decimal prProwizja, decimal prKara)
        {
            decimal kara = 0;

            var sql = string.Format("SELECT SUM(t0.Przychod) AS Przychod, SUM(t0.Rozchod) Rozchod FROM (" +
                    "SELECT " +
                        "Przychod = CASE WHEN f.Data IS NULL THEN ob.PrzychodWartosc ELSE CONVERT(DECIMAL(12,2), f.Data) * ob.IloscValue END, " +
                        "ob.RozchodWartosc Rozchod " +
                    "FROM Obroty ob " +
                    "INNER JOIN Towary t ON t.ID = ob.Towar " +
                    "INNER JOIN PozycjeDokHan pdh ON pdh.Ident=ob.RozchodPozycjaIdent AND pdh.Dokument=ob.RozchodDokument " +
                    "INNER JOIN DokHandlowe dh ON ob.RozchodDokument=dh.id " +
                    "LEFT JOIN Features f ON f.ParentType='PozycjeDokHan' AND f.Parent = pdh.ID AND f.Name='prowizja' " +
                    "WHERE t.Typ!=2 AND ob.RozchodData>='{0}' AND ob.RozchodData<'{1}' AND ob.Magazyn = {2} AND dh.Definicja=5 AND dh.Stan=1) AS t0",
                    dataOd.ToShortDateString(), dataDo.ToShortDateString(), magazyn);

            SqlConnection con = (SqlConnection)((EntityConnection)ec.Connection).StoreConnection;
            con.Open();
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandTimeout = int.MaxValue;
                cmd.CommandText = sql;
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    dr.Read();
                    object ob0 = dr.GetValue(0);
                    object ob1 = dr.GetValue(1);
                    

                    double przychod = (ob0 == null || ob0.GetType() == typeof(DBNull)) ? 0 : (double)ob0;
                    decimal rozchod = (ob1 == null || ob1.GetType() == typeof(DBNull)) ? 0 : (decimal)ob1;
                    kara = decimal.Round(((decimal)rozchod - (decimal)przychod) * prProwizja * prKara / 10000M, 2);
                }
            }
            con.Close();
           

            return (decimal)-kara;
        }

        private decimal karaWgCechy(EnovaContext ec, int magazyn, DateTime dataOd, DateTime dataDo, decimal prProwizja, string cecha)
        {
            decimal kara = 0;

            //SUM(Round((Rozchod-Przychod)*{0}*PrKary/100,2)) Kara 

            string sql = string.Format("SELECT CONVERT(DECIMAL(12,2), SUM(t1.Kara)) Kara FROM (SELECT ((t0.Rozchod - t0.Przychod) *{0} * PrKary /100) Kara FROM (" +
                "SELECT " +
                    "Przychod = CASE WHEN f.Data IS NULL THEN ob.PrzychodWartosc ELSE CONVERT(DECIMAL(12,2), f.Data) * ob.IloscValue END, " +
                    "ob.RozchodWartosc Rozchod," +
                    "PrKary = CASE WHEN fk.Data IS NULL THEN 50 ELSE CONVERT(DECIMAL(12,2),fk.Data) END " +
                "FROM Obroty ob " +
                "INNER JOIN PozycjeDokHan pdh ON pdh.Ident=ob.RozchodPozycjaIdent AND pdh.Dokument=ob.RozchodDokument " +
                "INNER JOIN DokHandlowe dh ON ob.RozchodDokument=dh.id " +
                "INNER JOIN Towary t ON t.ID = ob.Towar " +
                "LEFT JOIN Features f ON f.ParentType='PozycjeDokHan' AND f.Parent = pdh.ID AND f.[Name]='prowizja' " +
                "LEFT JOIN Features fk ON fk.ParentType='Towary' AND fk.Parent=ob.Towar AND fk.[Name]='{1}' " +
                "WHERE ob.RozchodData>='{2}' AND ob.RozchodData<'{3}' AND ob.Magazyn = {4} AND dh.Definicja=5 AND t.Typ!=2 and dh.Stan=1)t0)t1",
                (prProwizja / 100).ToString().Replace(",","."), cecha, dataOd.ToShortDateString(), dataDo.ToShortDateString(), magazyn);


            SqlConnection con = (SqlConnection)((EntityConnection)ec.Connection).StoreConnection;
            con.Open();
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandTimeout = int.MaxValue;
                cmd.CommandText = sql;
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    dr.Read();
                    kara = dr.GetDecimal(0);
                }
            }
            con.Close();

            
            return (decimal)-kara;
        }


        private void obliczProwizjeZKar()
        {
            try
            {
                if (karyWgCechyCheckBox.Checked)
                {
                    wartoscKary = karaWgCechy(Enova.Business.Old.Core.ContextManager.DataContext, 1, this.dataOd, this.dataDo, 50, cechaKaryTextBox.Text);
                }
                else
                {
                    wspolczynnikKary = decimal.Parse(współczynnikTextBox.Text);
                    wartoscKary = karaWgStawki(Enova.Business.Old.Core.ContextManager.DataContext, 1, this.dataOd, this.dataDo, 50, (wspolczynnikKary - 1) * 100);
                }
                procentKary = decimal.Parse(tbProcentKary.Text);
                prowizjaKary = decimal.Round(wartoscKary * procentKary / 100, 2);

                tbWartoscKary.Text = string.Format("{0:C}", wartoscKary);
                tbProwizjaKary.Text = string.Format("{0:C}", prowizjaKary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Obsolete("Do przepisania")]
        private void obliczDemo()
        {
            try
            {
                var settings = Enova.Business.Old.Core.Configuration.GetDataBaseSettings("D_E_M_O");
                if (settings != null)
                {
                    using (EnovaContext ec = new EnovaContext(settings.ConnectionString))
                    {
                        var query = (from dh in ec.DokHandlowe
                                     join f in ec.Features on dh.Kontrahent.ID equals f.Parent
                                     where dh.Data >= dataOd && dh.Data < dataDo && f.ParentType == "Kontrahenci" && f.Name == "przedstawiciel"
                                     && (dh.RelationDefinicja.ID == 4 || dh.RelationDefinicja.ID == 5) && (dh.StanInt == 1 || dh.StanInt == 2)
                                     select new
                                     {
                                         Magazyn = dh.Magazyn.ID,
                                         Przedstawiciel = f.Data,
                                         Wartsosc = dh.SumaNetto,
                                         Przychod = 0
                                     });


                        decimal? d = query.Where(o => o.Magazyn == 1).Sum(o => o.Wartsosc);
                        obrotDemo = d == null ? 0 : d.Value;
                        //d = query.Where(o => o.Magazyn == 1).Sum(o => o.Przychod);
                        dochodDemo = 0;

                        d = decimal.Parse(tbDemoPr.Text);

                        procentDemo = d.Value;
                        prowizjaDemo = decimal.Round(obrotDemo * procentDemo / 100M, 2);

                        tbDemoOb.Text = string.Format("{0:C}", obrotDemo);
                        tbDemoDoch.Text = string.Format("{0:C}", dochodDemo);
                        tbDemoProw.Text = string.Format("{0:C}", prowizjaDemo);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void obliczButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                decimal?[] obroty = new decimal?[] { 0, 0, 0, 0, 0 };

                int month = comboBox1.SelectedIndex + 1;
                int year = (int)comboBox2.SelectedItem;

                if (radioButton1.Checked)
                {
                    dataOd = new DateTime(year, 1, 1, 0, 0, 0);
                    //dataDo = dataOd.AddYears(1).AddMilliseconds(-1);
                    dataDo = dataOd.AddYears(1);
                }
                else
                {
                    dataOd = new DateTime(year, month, 1, 0, 0, 0);
                    //dataDo = dataOd.AddMonths(1).AddMilliseconds(-1);
                    dataDo = dataOd.AddMonths(1);
                }

                //obliczDemo();
                obliczProwizjeZKar();
                EnovaContext ec = Enova.Business.Old.Core.ContextManager.DataContext;



                var query = (from dh in ec.DokHandlowe
                             join f in ec.Features on dh.Kontrahent.ID equals f.Parent
                             where dh.Data >= dataOd && dh.Data < dataDo && f.ParentType == "Kontrahenci" && f.Name == "przedstawiciel"
                             && (dh.RelationDefinicja.ID == 4 || dh.RelationDefinicja.ID == 5) && (dh.StanInt == 1 || dh.StanInt == 2)
                             && f.Data != "--"
                             select new
                             {
                                 Magazyn = dh.Magazyn.ID,
                                 Przedstawiciel = f.Data,
                                 Wartsosc = dh.SumaNetto,
                                 Przychod = 0
                             });

                obrot1 = query.Where(o => o.Magazyn == 1 && !o.Przedstawiciel.StartsWith("SZ")).Sum(o => o.Wartsosc);
                obrot2 = query.Where(o => o.Magazyn == 1 && o.Przedstawiciel.StartsWith("SZ")).Sum(o => o.Wartsosc);
                obrot3 = query.Where(o => o.Magazyn == 2).Sum(o => o.Wartsosc);
                obrot4 = query.Where(o => o.Magazyn == 3).Sum(o => o.Wartsosc);
                obrot5 = query.Where(o => o.Magazyn != 1 && o.Magazyn != 2 && o.Magazyn != 3).Sum(o => o.Wartsosc);

                decimal? przychod1 = 0;//query.Where(o => o.Magazyn == 1 && !o.Przedstawiciel.StartsWith("SZ")).Sum(o => o.Przychod);
                decimal? przychod2 = 0; // query.Where(o => o.Magazyn == 1 && o.Przedstawiciel.StartsWith("SZ")).Sum(o => o.Przychod);
                decimal? przychod3 = 0;// query.Where(o => o.Magazyn == 2).Sum(o => o.Przychod);
                decimal? przychod4 = 0;// query.Where(o => o.Magazyn == 3).Sum(o => o.Przychod);
                decimal? przychod5 = 0;// query.Where(o => o.Magazyn != 1 && o.Magazyn != 2 && o.Magazyn != 3).Sum(o => o.Przychod);

                obrot1 = obrot1 == null ? 0 : obrot1.Value;
                obrot2 = obrot2 == null ? 0 : obrot2.Value;
                obrot3 = obrot3 == null ? 0 : obrot3.Value;
                obrot4 = obrot4 == null ? 0 : obrot4.Value;
                obrot5 = obrot5 == null ? 0 : obrot5.Value;

                obroty[0] = obrot1;
                obroty[1] = obrot2;
                obroty[2] = obrot3;
                obroty[3] = obrot4;
                obroty[4] = obrot5;

                /*
                foreach (var mag in magBoxes)
                {
                    decimal suma = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        TextBox tbP = (TextBox)this.Controls[mag + "P" + (i + 1).ToString()];
                        TextBox tb = (TextBox)this.Controls[mag + (i + 1).ToString()];
                        decimal pr = decimal.Parse(tbP.Text);
                        decimal prow = decimal.Round(obroty[i].Value * pr / 100M, 2);
                        suma += prow;
                        tb.Text = string.Format("{0:C}", prow);
                    }

                    TextBox tbSuma = (TextBox)this.Controls[mag + "Suma"];
                    tbSuma.Text = string.Format("{0:C}", suma);
                }
                 */

                decimal obrotSuma = obrot1.Value + obrot2.Value + obrot3.Value + obrot4.Value + obrot5.Value + obrotDemo;

                przychod1 = przychod1 == null ? 0 : przychod1.Value;
                przychod2 = przychod2 == null ? 0 : przychod2.Value;
                przychod3 = przychod3 == null ? 0 : przychod3.Value;
                przychod4 = przychod4 == null ? 0 : przychod4.Value;
                przychod5 = przychod5 == null ? 0 : przychod5.Value;

                dochod1 = obrot1.Value - przychod1.Value;
                dochod2 = obrot2.Value - przychod2.Value;
                dochod3 = obrot3.Value - przychod3.Value;
                dochod4 = obrot4.Value - przychod4.Value;
                dochod5 = obrot5.Value - przychod5.Value;

                decimal dochodSuma = dochod1 + dochod2 + dochod3 + dochod4 + dochod5 + dochodDemo;


                procent1 = decimal.Parse(tbP1.Text);
                procent2 = decimal.Parse(tbP2.Text);
                procent3 = decimal.Parse(tbP3.Text);
                procent4 = decimal.Parse(tbP4.Text);
                procent5 = decimal.Parse(tbP5.Text);


                decimal prowizja1 = decimal.Round(obrot1.Value * procent1 / 100, 2);
                decimal prowizja2 = decimal.Round(obrot2.Value * procent2 / 100, 2);
                decimal prowizja3 = decimal.Round(obrot3.Value * procent3 / 100, 2);
                decimal prowizja4 = decimal.Round(obrot4.Value * procent4 / 100, 2);
                decimal prowizja5 = decimal.Round(obrot5.Value * procent5 / 100, 2);



                prowizjaSuma = prowizja1 + prowizja2 + prowizja3 + prowizja4 + prowizja5 + prowizjaDemo + prowizjaKary;

                decimal procentDoch = dochod1 + dochod2 == 0 ? 0 : decimal.Round(prowizjaSuma / (dochod1 + dochod2), 4);
                tbProcentDochodu.Text = string.Format("{0:P}", procentDoch);

                decimal czasMG = 0;
                decimal czasLD = 0;
                decimal czasPD = 0;
                decimal czasSD = 0;
                decimal czasSuma = 0;


                czasMG = decimal.Parse(tbMGC.Text);
                czasLD = decimal.Parse(tbŁDC.Text);
                czasPD = decimal.Parse(tbPDC.Text);
                czasSD = decimal.Parse(tbSDC.Text);
                czasSuma = czasMG + czasLD + czasPD + czasSD;

                decimal prowizjaFirma = prowizja1 + prowizja2;

                decimal prowizjaMGC = czasSuma == 0 ? 0M : czasMG / czasSuma * prowizjaFirma;
                decimal prowizjaLDC = czasSuma == 0 ? 0M : czasLD / czasSuma * prowizjaFirma;
                decimal prowizjaPDC = czasSuma == 0 ? 0M : czasPD / czasSuma * prowizjaFirma;
                decimal prowizjaSDC = czasSuma == 0 ? 0M : czasSD / czasSuma * prowizjaFirma;


                procentMG = decimal.Parse(tbMGP.Text.Replace("%", ""));
                procentLD = decimal.Parse(tbLDP.Text.Replace("%", ""));
                procentPD = decimal.Parse(tbPDP.Text.Replace("%", ""));
                procentSD = decimal.Parse(tbSDP.Text.Replace("%", ""));

                prowizjaMG = decimal.Round(prowizjaSuma * procentMG / 100M, 2);
                prowizjaLD = decimal.Round(prowizjaSuma * procentLD / 100M, 2);
                prowizjaPD = decimal.Round(prowizjaSuma * procentPD / 100M, 2);
                prowizjaSD = decimal.Round(prowizjaSuma * procentSD / 100M, 2);

                tbMG.Text = string.Format("{0:C}", prowizjaMG);
                tbLD.Text = string.Format("{0:C}", prowizjaLD);
                tbPD.Text = string.Format("{0:C}", prowizjaPD);
                tbSD.Text = string.Format("{0:C}", prowizjaSD);


                inneMG = decimal.Parse(tbMGI.Text);
                inneLD = decimal.Parse(tbLDI.Text);
                innePD = decimal.Parse(tbPDI.Text);
                inneSD = decimal.Parse(tbSDI.Text);

                sumaMG = prowizjaMG + inneMG;
                sumaLD = prowizjaLD + inneLD;
                sumaPD = prowizjaPD + innePD;
                sumaSD = prowizjaSD + inneSD;

                tbMGSuma.Text = string.Format("{0:C}", sumaMG);
                tbLDSuma.Text = string.Format("{0:C}", sumaLD);
                tbPDSuma.Text = string.Format("{0:C}", sumaPD);
                tbSDSuma.Text = string.Format("{0:C}", sumaSD);

                textBox1.Text = string.Format("{0:C}", obrot1);
                textBox4.Text = string.Format("{0:C}", obrot2);
                textBox7.Text = string.Format("{0:C}", obrot3);
                textBox10.Text = string.Format("{0:C}", obrot4);
                textBox13.Text = string.Format("{0:C}", obrot5);
                textBox16.Text = string.Format("{0:C}", obrotSuma);
                textBox3.Text = string.Format("{0:C}", prowizja1);
                textBox6.Text = string.Format("{0:C}", prowizja2);
                textBox9.Text = string.Format("{0:C}", prowizja3);
                textBox12.Text = string.Format("{0:C}", prowizja4);
                textBox15.Text = string.Format("{0:C}", prowizja5);
                textBox17.Text = string.Format("{0:C}", prowizjaSuma);
                textBox23.Text = string.Format("{0:C}", dochod1);
                textBox22.Text = string.Format("{0:C}", dochod2);
                textBox21.Text = string.Format("{0:C}", dochod3);
                textBox20.Text = string.Format("{0:C}", dochod4);
                textBox19.Text = string.Format("{0:C}", dochod5);
                textBox18.Text = string.Format("{0:C}", dochodSuma);

                tbMGCP.Text = string.Format("{0:C}", prowizjaMGC);
                tbLDCP.Text = string.Format("{0:C}", prowizjaLDC);
                tbPDCP.Text = string.Format("{0:C}", prowizjaPDC);
                tbSDCP.Text = string.Format("{0:C}", prowizjaSDC);

                tbSumaC.Text = decimal.Round(czasSuma, 2).ToString();
                tbSumaCP.Text = string.Format("{0:C}", prowizjaFirma);

                obliczSumaProcent();

                przeliczono = true;
                zapiszButton.Enabled = true;

                this.Enabled = true;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Enova.Business.Old.Core.ContextManager.DisposeDataContext();
                throw ex;
            }
            finally
            {
                this.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void tbMP_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void tbMP_Leave(object sender, EventArgs e)
        {
            try
            {
                

                TextBox tb = (TextBox)sender;
                string tmp = tb.Name.Substring(5);

                decimal sum = 0;
                decimal d = 0;

                foreach (var n in magPBoxes)
                {

                    tb = (TextBox)this.Controls[n + tmp];
                    d = decimal.Parse(tb.Text);
                    sum += d;
                }

                tb = (TextBox)this.Controls["tbP" + tmp];
                tb.Text = decimal.Round(sum, 2).ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbC_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal suma = 0;
                foreach (var b in magBoxes)
                {
                    decimal d;
                    TextBox tb = (TextBox)this.Controls[b.Replace("LD","ŁD") + "C"];
                    d = decimal.Parse(tb.Text);
                    suma += d;
                }
                tbSumaC.Text = suma.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void zapiszGodzinyButton_Click(object sender, EventArgs e)
        {
            int month = comboBox1.SelectedIndex + 1;
            int year = (int)comboBox2.SelectedItem;

            decimal czasMG = decimal.Parse(tbMGC.Text);
            decimal czasLD = decimal.Parse(tbŁDC.Text);
            decimal czasPD = decimal.Parse(tbPDC.Text);
            decimal czasSD = decimal.Parse(tbSDC.Text);

            Enova.Business.Old.DB.Web.CzasPracy czasPracy = null;

            czasPracy = Enova.Business.Old.Core.ContextManager.WebContext.CzasyPracy
                .Where(c => c.Kod == "MG" && c.Miesiac == month && c.Rok == year).FirstOrDefault();
            if (czasPracy == null)
            {
                czasPracy = new Enova.Business.Old.DB.Web.CzasPracy()
                {
                    Kod = "MG",
                    Miesiac = month,
                    Rok = year
                };
                Enova.Business.Old.Core.ContextManager.WebContext.AddToCzasyPracy(czasPracy);
            }
            czasPracy.Godziny = czasMG;

            czasPracy = Enova.Business.Old.Core.ContextManager.WebContext.CzasyPracy
                .Where(c => c.Kod == "ŁD" && c.Miesiac == month && c.Rok == year).FirstOrDefault();
            if (czasPracy == null)
            {
                czasPracy = new Enova.Business.Old.DB.Web.CzasPracy()
                {
                    Kod = "ŁD",
                    Miesiac = month,
                    Rok = year
                };
                Enova.Business.Old.Core.ContextManager.WebContext.AddToCzasyPracy(czasPracy);
            }
            czasPracy.Godziny = czasLD;

            czasPracy = Enova.Business.Old.Core.ContextManager.WebContext.CzasyPracy
                .Where(c => c.Kod == "PD" && c.Miesiac == month && c.Rok == year).FirstOrDefault();
            if (czasPracy == null)
            {
                czasPracy = new Enova.Business.Old.DB.Web.CzasPracy()
                {
                    Kod = "PD",
                    Miesiac = month,
                    Rok = year
                };
                Enova.Business.Old.Core.ContextManager.WebContext.AddToCzasyPracy(czasPracy);
            }
            czasPracy.Godziny = czasPD;

            czasPracy = Enova.Business.Old.Core.ContextManager.WebContext.CzasyPracy
                .Where(c => c.Kod == "SD" && c.Miesiac == month && c.Rok == year).FirstOrDefault();
            if (czasPracy == null)
            {
                czasPracy = new Enova.Business.Old.DB.Web.CzasPracy()
                {
                    Kod = "SD",
                    Miesiac = month,
                    Rok = year
                };
                Enova.Business.Old.Core.ContextManager.WebContext.AddToCzasyPracy(czasPracy);
            }
            czasPracy.Godziny = czasSD;

            Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();
        }

        private void data_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int month = comboBox1.SelectedIndex + 1;
            int year = (int)comboBox2.SelectedItem;

            tbMGC.Text = "0,00";
            tbŁDC.Text = "0,00";
            tbPDC.Text = "0,00";
            tbSDC.Text = "0,00";

            var dc = Enova.Business.Old.Core.ContextManager.WebContext;

            przeliczono = false;
            zapiszButton.Enabled = false;

            if (radioButton1.Checked)
            {
                var czasy = (from c in dc.CzasyPracy
                             where c.Rok == year
                             group c by c.Kod into g
                             select new
                             {
                                 Kod = g.Key,
                                 Godziny = g.Sum(gr=>gr.Godziny)
                             }).ToList();

                foreach (var c in czasy)
                {
                    TextBox tb = (TextBox)this.Controls["tb" + c.Kod + "C"];
                    tb.Text = c.Godziny==null ? "0,00" : c.Godziny.ToString();
                }
                loadProwizjeObroty(year, 0);
                loadProwizjePracownicy(year, 0);
                    
            }
            else
            {
                var czasy = dc.CzasyPracy.Where(c => c.Rok == year && c.Miesiac == month).ToList();
                foreach (var c in czasy)
                {
                    TextBox tb = (TextBox)this.Controls["tb" + c.Kod + "C"];
                    tb.Text = c.Godziny.ToString();
                }
                loadProwizjeObroty(year, month);
                loadProwizjePracownicy(year, month);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            data_SelectionChangeCommitted(null, null);
        }


        private void loadProwizjeObroty(int year, int month)
        {
            var obr = Enova.Business.Old.Core.ContextManager.WebContext.ProwizjeObroty.Where(p => p.Rok == year && p.Miesiac == month).FirstOrDefault();
            if (obr != null)
            {
                textBox1.Text = string.Format("{0:C}", obr.Obrot1);
                textBox4.Text = string.Format("{0:C}", obr.Obrot2);
                textBox7.Text = string.Format("{0:C}", obr.Obrot3);
                textBox10.Text = string.Format("{0:C}", obr.Obrot4);
                textBox13.Text = string.Format("{0:C}", obr.Obrot5);
                tbDemoOb.Text = string.Format("{0:C}", obr.Obrot6);

                obrot1 = obr.Obrot1;
                obrot2 = obr.Obrot2;
                obrot3 = obr.Obrot3;
                obrot4 = obr.Obrot4;
                obrot5 = obr.Obrot5;
                obrotDemo = obr.Obrot6;

                textBox23.Text = string.Format("{0:C}", obr.Dochod1);
                textBox22.Text = string.Format("{0:C}", obr.Dochod2);
                textBox21.Text = string.Format("{0:C}", obr.Dochod3);
                textBox20.Text = string.Format("{0:C}", obr.Dochod4);
                textBox19.Text = string.Format("{0:C}", obr.Dochod5);
                tbDemoDoch.Text = string.Format("{0:C}", obr.Dochod6);

                dochod1 = obr.Dochod1;
                dochod2 = obr.Dochod2;
                dochod3 = obr.Dochod3;
                dochod4 = obr.Dochod4;
                dochod5 = obr.Dochod5;
                dochodDemo = obr.Dochod6;

                tbP1.Text = obr.Procent1.ToString();
                tbP2.Text = obr.Procent2.ToString();
                tbP3.Text = obr.Procent3.ToString();
                tbP4.Text = obr.Procent4.ToString();
                tbP5.Text = obr.Procent5.ToString();
                tbDemoPr.Text = obr.Procent6.ToString();

                procent1 = obr.Procent1;
                procent2 = obr.Procent2;
                procent3 = obr.Procent3;
                procent4 = obr.Procent4;
                procent5 = obr.Procent5;
                procentDemo = obr.Procent6;

                decimal prowizja1 = decimal.Round(obrot1.Value * procent1 / 100M, 2);
                decimal prowizja2 = decimal.Round(obrot2.Value * procent2 / 100M, 2);
                decimal prowizja3 = decimal.Round(obrot3.Value * procent3 / 100M, 2);
                decimal prowizja4 = decimal.Round(obrot4.Value * procent4 / 100M, 2);
                decimal prowizja5 = decimal.Round(obrot5.Value * procent5 / 100M, 2);
                decimal prowizja6 = decimal.Round(obrotDemo * procentDemo / 100M, 2);

                prowizjaDemo = prowizja6;

                textBox3.Text = string.Format("{0:C}", prowizja1);
                textBox6.Text = string.Format("{0:C}", prowizja2);
                textBox9.Text = string.Format("{0:C}", prowizja3);
                textBox12.Text = string.Format("{0:C}", prowizja4);
                textBox15.Text = string.Format("{0:C}", prowizja5);
                tbDemoProw.Text = string.Format("{0:C}", prowizja6);

                karyWgCechyCheckBox.Checked = obr.KaryWgCechy;
                cechaKaryTextBox.Text = obr.CechaKary;
                współczynnikTextBox.Text = obr.WspolczynnikKary.ToString();
                wspolczynnikKary = obr.WspolczynnikKary;

                tbWartoscKary.Text = string.Format("{0:C}", obr.KaryWartosc);
                tbProcentKary.Text = obr.KaryProcent.ToString();
                tbProwizjaKary.Text = string.Format("{0:C}", obr.KaryProwizja);

                wartoscKary = obr.KaryWartosc;
                procentKary = obr.KaryProcent;
                prowizjaKary = obr.KaryProwizja;

                decimal obrotSuma = obrot1.Value + obrot2.Value + obrot3.Value + obrot4.Value + obrot5.Value + obrotDemo;
                textBox16.Text = string.Format("{0:C}", obrotSuma);

                decimal dochodSuma = dochod1 + dochod2 + dochod3 + dochod4 + dochod5 + dochodDemo;
                textBox18.Text = string.Format("{0:C}", dochodSuma);

                prowizjaSuma = prowizja1 + prowizja2 + prowizja3 + prowizja4 + prowizja5 + prowizjaDemo + prowizjaKary;
                textBox17.Text = string.Format("{0:C}", prowizjaSuma);

            }
            else
            {
                textBox1.Text = string.Format("{0:C}", 0);
                textBox4.Text = string.Format("{0:C}", 0);
                textBox7.Text = string.Format("{0:C}", 0);
                textBox10.Text = string.Format("{0:C}", 0);
                textBox13.Text = string.Format("{0:C}", 0);
                tbDemoOb.Text = string.Format("{0:C}", 0);

                obrot1 = 0;
                obrot2 = 0;
                obrot3 = 0;
                obrot4 = 0;
                obrot5 = 0;
                obrotDemo = 0;

                textBox23.Text = string.Format("{0:C}", 0);
                textBox22.Text = string.Format("{0:C}", 0);
                textBox21.Text = string.Format("{0:C}", 0);
                textBox20.Text = string.Format("{0:C}", 0);
                textBox19.Text = string.Format("{0:C}", 0);
                tbDemoDoch.Text = string.Format("{0:C}", 0);

                dochod1 = 0;
                dochod2 = 0;
                dochod3 = 0;
                dochod4 = 0;
                dochod5 = 0;
                dochodDemo = 0;

                tbP1.Text = "2,68";
                tbP2.Text = "2,68";
                tbP3.Text = "0,0";
                tbP4.Text = "1,0";
                tbP5.Text = "0,0";
                tbDemoPr.Text = "0,0";

                procent1 = 3;
                procent2 = 2;
                procent3 = 0;
                procent4 = 1;
                procent5 = 0;
                procentDemo = 3;

                decimal prowizja1 = 0;
                decimal prowizja2 = 0;
                decimal prowizja3 = 0;
                decimal prowizja4 = 0;
                decimal prowizja5 = 0;
                decimal prowizja6 = 0;

                textBox3.Text = string.Format("{0:C}", prowizja1);
                textBox6.Text = string.Format("{0:C}", prowizja2);
                textBox9.Text = string.Format("{0:C}", prowizja3);
                textBox12.Text = string.Format("{0:C}", prowizja4);
                textBox15.Text = string.Format("{0:C}", prowizja5);
                tbDemoProw.Text = string.Format("{0:C}", prowizja6);

                karyWgCechyCheckBox.Checked = false;
                cechaKaryTextBox.Text = "KARY";
                współczynnikTextBox.Text = "1,25";
                
                wspolczynnikKary = 1.25M;

                tbWartoscKary.Text = string.Format("{0:C}", 0);
                tbProcentKary.Text = "50";
                tbProwizjaKary.Text = string.Format("{0:C}", 0);

                wartoscKary = 0;
                procentKary = 0;
                prowizjaKary = 0;

                decimal obrotSuma = 0;
                textBox16.Text = string.Format("{0:C}", obrotSuma);

                decimal dochodSuma = 0;
                textBox18.Text = string.Format("{0:C}", dochodSuma);

                prowizjaSuma = 0;
                textBox17.Text = string.Format("{0:C}", prowizjaSuma);


            }
        }

        private void loadProwizjePracownicy(int year, int month)
        {
            var prowizje = Enova.Business.Old.Core.ContextManager.WebContext.ProwizjePracownikow.Where(p => p.Rok == year && p.Miesiac == month).ToList();
            Dictionary<string, string> pracownicy = new Dictionary<string, string>()
            {
                    { "MG" , "MG" },
                    { "ŁD" , "LD" },
                    { "PD" , "PD" },
                    { "SD" , "SD" }
            };

            if (prowizje.Count > 0)
            {
                foreach (var prowizja in prowizje)
                {
                    var tbSave = (TextBox)this.Controls["tb" + pracownicy[prowizja.PracownikKod] + "SumaSave"];
                    tbSave.Text = string.Format("{0:C}", prowizja.ProwizjaSuma);

                    var tbP = (TextBox)this.Controls["tb" + pracownicy[prowizja.PracownikKod] + "P"];
                    tbP.Text = prowizja.ProwizjaProcent.ToString();
                    var tb = (TextBox)this.Controls["tb" + pracownicy[prowizja.PracownikKod]];
                    tb.Text = string.Format("{0:C}", prowizja.Prowizja);
                    var tbI = (TextBox)this.Controls["tb" + pracownicy[prowizja.PracownikKod] + "I"];
                    tbI.Text = prowizja.Inne.ToString();
                    var tbSuma = (TextBox)this.Controls["tb" + pracownicy[prowizja.PracownikKod] + "Suma"];
                    tbSuma.Text = string.Format("{0:C}", prowizja.ProwizjaSuma);
                }
            }
            else
            {
                foreach (KeyValuePair<string,string> kvp in pracownicy)
                {
                    var tbSave = (TextBox)this.Controls["tb" + kvp.Value + "SumaSave"];
                    tbSave.Text = "";
                    //var tbP = (TextBox)this.Controls["tb" + kvp.Value + "P"];
                    //tbP.Text = "0";
                    var tb = (TextBox)this.Controls["tb" + kvp.Value];
                    tb.Text = string.Format("{0:C}", 0);
                    var tbI = (TextBox)this.Controls["tb" + kvp.Value + "I"];
                    tbI.Text = "0";
                    var tbSuma = (TextBox)this.Controls["tb" + kvp.Value + "Suma"];
                    tbSuma.Text = string.Format("{0:C}", 0);

                    switch (kvp.Value)
                    {
                        case "MG":
                            tbMGP.Text = "36,36";
                            break;
                        case "LD":
                            tbLDP.Text = "27,28";
                            break;
                        case "PD":
                            tbPDP.Text = "36,36";
                            break;
                        case "SD":
                            tbSDP.Text = "0,0";
                            break;
                    }

                }

            }
        }

        private void drukujButton_Click(object sender, EventArgs e)
        {
            List<Enova.Business.Old.Types.ColumnReportRow> rows = new List<Enova.Business.Old.Types.ColumnReportRow>();

            rows.Add(new Enova.Business.Old.Types.ColumnReportRow()
            {
                Column1 = "Magazyn Firma bez X",
                Column2 = textBox1.Text,
                Column3 = textBox23.Text,
                Column4 = tbMGP.Text,
                Column5 = tbMG.Text,
                Column6 = tbLDP.Text,
                Column7 = tbLD.Text,
                Column8 = tbPDP.Text,
                Column9 = tbPD.Text,
                Column10 = tbSDP.Text,
                Column11 = tbSD.Text,
                Column12 = tbP1.Text,
                Column13 = textBox3.Text
            });

            rows.Add(new Enova.Business.Old.Types.ColumnReportRow()
            {
                Column1 = "Magazyn Firma tylko X",
                Column2 = textBox4.Text,
                Column3 = textBox22.Text,
                /*
                Column4 = tbMGP2.Text,
                Column5 = tbMG2.Text,
                Column6 = tbLDP2.Text,
                Column7 = tbLD2.Text,
                Column8 = tbPDP2.Text,
                Column9 = tbPD2.Text,
                Column10 = tbSDP2.Text,
                Column11 = tbSD2.Text,
                Column12 = tbP2.Text,
                Column13 = textBox6.Text
                 */
            });

            rows.Add(new Enova.Business.Old.Types.ColumnReportRow()
            {
                Column1 = "Magazyn Radom",
                Column2 = textBox7.Text,
                Column3 = textBox21.Text,
                /*
                Column4 = tbMGP3.Text,
                Column5 = tbMG3.Text,
                Column6 = tbLDP3.Text,
                Column7 = tbLD3.Text,
                Column8 = tbPDP3.Text,
                Column9 = tbPD3.Text,
                Column10 = tbSDP3.Text,
                Column11 = tbSD3.Text,
                Column12 = tbP3.Text,
                Column13 = textBox9.Text
                 */
            });


            /*
            rows.Add(new Enova.Business.Types.ColumnReportRow()
            {
                Column1 = "Magazyn wysyłki",
                Column2 = textBox10.Text,
                Column3 = textBox20.Text,
                Column4 = tbMGP4.Text,
                Column5 = tbMG4.Text,
                Column6 = tbLDP4.Text,
                Column7 = tbLD4.Text,
                Column8 = tbPDP4.Text,
                Column9 = tbPD4.Text,
                Column10 = tbSDP4.Text,
                Column11 = tbSD4.Text,
                Column12 = tbP4.Text,
                Column13 = textBox12.Text
            });

            rows.Add(new Enova.Business.Types.ColumnReportRow()
            {
                Column1 = "Pozostałe magazyny",
                Column2 = textBox13.Text,
                Column3 = textBox19.Text,
                Column4 = tbMGP5.Text,
                Column5 = tbMG5.Text,
                Column6 = tbLDP5.Text,
                Column7 = tbLD5.Text,
                Column8 = tbPDP5.Text,
                Column9 = tbPD5.Text,
                Column10 = tbSDP5.Text,
                Column11 = tbSD5.Text,
                Column12 = tbP5.Text,
                Column13 = textBox15.Text
            });

            */

            decimal sumaObrot = decimal.Round(decimal.Parse(textBox1.Text.Replace("zł", "")) + decimal.Parse(textBox4.Text.Replace("zł", "")) + decimal.Parse(textBox7.Text.Replace("zł", "")), 2);
            decimal sumaDochod = decimal.Round(decimal.Parse(textBox23.Text.Replace("zł", "")) + decimal.Parse(textBox22.Text.Replace("zł", "")) + decimal.Parse(textBox21.Text.Replace("zł", "")), 2);
            /*
            decimal sumaMG = decimal.Round(decimal.Parse(tbMG1.Text.Replace("zł", "")) + decimal.Parse(tbMG2.Text.Replace("zł", "")) + decimal.Parse(tbMG3.Text.Replace("zł", "")), 2);
            decimal sumaLD = decimal.Round(decimal.Parse(tbLD1.Text.Replace("zł", "")) + decimal.Parse(tbLD2.Text.Replace("zł", "")) + decimal.Parse(tbLD3.Text.Replace("zł", "")), 2);
            decimal sumaPD = decimal.Round(decimal.Parse(tbPD1.Text.Replace("zł", "")) + decimal.Parse(tbPD2.Text.Replace("zł", "")) + decimal.Parse(tbPD3.Text.Replace("zł", "")), 2);
            decimal sumaSD = decimal.Round(decimal.Parse(tbSD1.Text.Replace("zł", "")) + decimal.Parse(tbSD2.Text.Replace("zł", "")) + decimal.Parse(tbSD3.Text.Replace("zł", "")), 2);
            decimal sumaProwizja = decimal.Round(decimal.Parse(textBox3.Text.Replace("zł", "")) + decimal.Parse(textBox6.Text.Replace("zł", "")) + decimal.Parse(textBox9.Text.Replace("zł", "")), 2);
            */

            ReportForm form = new ReportForm("ProwizjaMagazynuReport.rdlc", "Rows", rows, new List<ReportParameter>()
            {
                new ReportParameter("tytul","Raport "+(radioButton1.Checked ? "roczny" : "miesięczny")+ " prowizji za "+
                    (radioButton1.Checked ? "rok " : "miesiąc ") + (radioButton1.Checked ? comboBox2.Text : comboBox1.Text+"/"+comboBox2.Text)),
                new ReportParameter("sumaobrot", string.Format("{0:C}",sumaObrot)),
                new ReportParameter("sumadochod",string.Format("{0:C}",sumaDochod)),
        
                //new ReportParameter("sumamg",string.Format("{0:C}",sumaMG)),
                //new ReportParameter("sumald",string.Format("{0:C}",sumaLD)),
                //new ReportParameter("sumapd",string.Format("{0:C}",sumaPD)),
                //new ReportParameter("sumasd",string.Format("{0:C}",sumaSD)),
                //new ReportParameter("sumaprowizja",string.Format("{0:C}",sumaProwizja)),
                new ReportParameter("mgc",tbMGC.Text),
                new ReportParameter("mgcp",tbMGCP.Text),
                new ReportParameter("ldc",tbŁDC.Text),
                new ReportParameter("ldcp",tbLDCP.Text),
                new ReportParameter("pdc",tbPDC.Text),
                new ReportParameter("pdcp",tbPDCP.Text),
                new ReportParameter("sdc",tbSDC.Text),
                new ReportParameter("sdcp",tbSDCP.Text),
                new ReportParameter("sumac",tbSumaC.Text),
                new ReportParameter("sumacp",tbSumaCP.Text)
            });
            form.ShowDialog();
        }

        private void tbMGI_Leave(object sender, EventArgs e)
        {
            if (prowizjaMG != 0)
            {
                decimal inne = 0;
                if (decimal.TryParse(tbMGI.Text, out inne))
                {
                    if (inne != inneMG)
                    {
                        inneMG = inne;
                        sumaMG = prowizjaMG + inneMG;
                        tbMGSuma.Text = string.Format("{0:C}", sumaMG);
                    }
                }
            }
        }

        private void tbLDI_Leave(object sender, EventArgs e)
        {
            if (prowizjaLD != 0)
            {
                decimal inne = 0;
                if (decimal.TryParse(tbLDI.Text, out inne))
                {
                    if (inne != inneLD)
                    {
                        inneLD = inne;
                        sumaLD = prowizjaLD + inneLD;
                        tbLDSuma.Text = string.Format("{0:C}", sumaLD);
                    }
                }
            }

        }

        private void tbPDI_Leave(object sender, EventArgs e)
        {
            if (prowizjaPD != 0)
            {
                decimal inne = 0;
                if (decimal.TryParse(tbPDI.Text, out inne))
                {
                    if (inne != innePD)
                    {
                        innePD = inne;
                        sumaPD = prowizjaPD + innePD;
                        tbPDSuma.Text = string.Format("{0:C}", sumaPD);
                    }
                }
            }

        }

        private void tbSDI_Leave(object sender, EventArgs e)
        {
            if (prowizjaSD != 0)
            {
                decimal inne = 0;
                if (decimal.TryParse(tbSDI.Text, out inne))
                {
                    if (inne != inneSD)
                    {
                        inneSD = inne;
                        sumaSD = prowizjaSD + inneSD;
                        tbSDSuma.Text = string.Format("{0:C}", sumaSD);
                    }
                }
            }

        }

        private void obliczSumaProcent()
        {
            decimal suma = procentMG + procentLD + procentPD + procentSD;
            tbProcent.Text = suma.ToString() + "%";
        }

        private void tbMGP_Leave(object sender, EventArgs e)
        {
            if (prowizjaSuma != 0)
            {
                decimal procent = 0;
                if (decimal.TryParse(tbMGP.Text.Replace("%","").Trim(), out procent))
                {
                    if (procent != procentMG)
                    {
                        procentMG = procent;
                        prowizjaMG = decimal.Round(prowizjaSuma * procentMG / 100M, 2);
                        sumaMG = prowizjaMG + inneMG;
                        tbMG.Text = string.Format("{0:C}", prowizjaMG);
                        tbMGSuma.Text = string.Format("{0:C}", sumaMG);
                        obliczSumaProcent();
                    }
                }
            }

        }

        private void tbLDP_Leave(object sender, EventArgs e)
        {
            if (prowizjaSuma != 0)
            {
                decimal procent = 0;
                if (decimal.TryParse(tbLDP.Text.Replace("%", "").Trim(), out procent))
                {
                    if (procent != procentLD)
                    {
                        procentLD = procent;
                        prowizjaLD = decimal.Round(prowizjaSuma * procentLD / 100M, 2);
                        sumaLD = prowizjaLD + inneLD;
                        tbLD.Text = string.Format("{0:C}", prowizjaLD);
                        tbLDSuma.Text = string.Format("{0:C}", sumaLD);
                        obliczSumaProcent();
                    }
                }
            }

        }

        private void tbPDP_Leave(object sender, EventArgs e)
        {
            if (prowizjaSuma != 0)
            {
                decimal procent = 0;
                if (decimal.TryParse(tbPDP.Text.Replace("%", "").Trim(), out procent))
                {
                    if (procent != procentPD)
                    {
                        procentPD = procent;
                        prowizjaPD = decimal.Round(prowizjaSuma * procentPD / 100M, 2);
                        sumaPD = prowizjaPD + innePD;
                        tbPD.Text = string.Format("{0:C}", prowizjaPD);
                        tbPDSuma.Text = string.Format("{0:C}", sumaPD);
                        obliczSumaProcent();
                    }
                }
            }

        }

        private void tbSDP_Leave(object sender, EventArgs e)
        {
            if (prowizjaSuma != 0)
            {
                decimal procent = 0;
                if (decimal.TryParse(tbSDP.Text.Replace("%", "").Trim(), out procent))
                {
                    if (procent != procentSD)
                    {
                        procentSD = procent;
                        prowizjaSD = decimal.Round(prowizjaSuma * procentSD / 100M, 2);
                        sumaSD = prowizjaSD + inneSD;
                        tbSD.Text = string.Format("{0:C}", prowizjaSD);
                        tbSDSuma.Text = string.Format("{0:C}", sumaSD);
                        obliczSumaProcent();
                    }
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

        private void zapiszButton_Click(object sender, EventArgs e)
        {
            zapiszProwizjeObroty();
            zapiszProwizjePracownikow();
        }

        private void zapiszProwizjeObroty()
        {
            int month = radioButton1.Checked ? 0 : comboBox1.SelectedIndex + 1;
            int year = (int)comboBox2.SelectedItem;

            Enova.Business.Old.DB.Web.WebContext dc = Enova.Business.Old.Core.ContextManager.WebContext;

            var prowizjaObrot = dc.ProwizjeObroty.Where(po => po.Miesiac == month && po.Rok == year).FirstOrDefault();
            if (prowizjaObrot == null)
            {
                prowizjaObrot = new Enova.Business.Old.DB.Web.ProwizjaObrot()
                {
                    Miesiac = month,
                    Rok = year
                };
                dc.AddToProwizjeObroty(prowizjaObrot);
            }

            prowizjaObrot.Obrot1 = obrot1.Value;
            prowizjaObrot.Obrot2 = obrot2.Value;
            prowizjaObrot.Obrot3 = obrot3.Value;
            prowizjaObrot.Obrot4 = obrot4.Value;
            prowizjaObrot.Obrot5 = obrot5.Value;
            prowizjaObrot.Obrot6 = obrotDemo;
            prowizjaObrot.Dochod1 = dochod1;
            prowizjaObrot.Dochod2 = dochod2;
            prowizjaObrot.Dochod3 = dochod3;
            prowizjaObrot.Dochod4 = dochod4;
            prowizjaObrot.Dochod5 = dochod5;
            prowizjaObrot.Dochod6 = dochodDemo;
            prowizjaObrot.Procent1 = procent1;
            prowizjaObrot.Procent2 = procent2;
            prowizjaObrot.Procent3 = procent3;
            prowizjaObrot.Procent4 = procent4;
            prowizjaObrot.Procent5 = procent5;
            prowizjaObrot.Procent6 = procentDemo;
            prowizjaObrot.KaryWgCechy = karyWgCechyCheckBox.Checked;
            prowizjaObrot.CechaKary = cechaKaryTextBox.Text;
            prowizjaObrot.WspolczynnikKary = wspolczynnikKary;
            prowizjaObrot.KaryWartosc = wartoscKary;
            prowizjaObrot.KaryProcent = procentKary;
            prowizjaObrot.KaryProwizja = prowizjaKary;
            prowizjaObrot.ProwizjaSuma = prowizjaSuma;

            dc.SaveChanges();
        }

        private void zapiszProwizjePracownikow()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            if (radioButton2.Checked)
            {
                int month = comboBox1.SelectedIndex + 1;
                int year = (int)comboBox2.SelectedItem;
                DateTime data = new DateTime(year, month, 1).Date;

                var skladka = GetSkladki(data);

                Dictionary<string, string> pracownicy = new Dictionary<string, string>()
                {
                    {"MG","MG"},
                    {"LD","ŁD"},
                    {"PD","PD"},
                    {"SD","SD"},
                };

                var dc = Enova.Business.Old.Core.ContextManager.WebContext;

                foreach (KeyValuePair<string, string> kvp in pracownicy)
                {
                    DBWeb.ProwizjaPracownika prowizja = dc.ProwizjePracownikow.Where(pr => pr.Miesiac == month && pr.Rok == year && pr.PracownikKod == kvp.Value).FirstOrDefault();
                    if (prowizja == null)
                    {
                        prowizja = new DBWeb.ProwizjaPracownika()
                        {
                            PracownikKod = kvp.Value,
                            Miesiac = month,
                            Rok = year
                        };
                        dc.AddToProwizjePracownikow(prowizja);
                    }

                    var etat = GetEtat(kvp.Value, data);
                    //Zus placony przez firme
                    decimal funduszEmer = decimal.Round(etat.WynagrodzenieBrutto * skladka.FunduszEmerytalny / 100M, 2);
                    decimal funduszRentowy = decimal.Round(etat.WynagrodzenieBrutto * skladka.FunduszRentowy / 100M, 2);
                    decimal funduszWypadkowy = decimal.Round(etat.WynagrodzenieBrutto * skladka.FunduszWypadkowy / 100M, 2);
                    decimal funduszPracy = etat.WysokoscEtatu < 1 ? 0 : decimal.Round(etat.WynagrodzenieBrutto * skladka.FunduszPracy / 100M, 2);
                    decimal funduszGSP = decimal.Round(etat.WynagrodzenieBrutto * skladka.FunduszGSP / 100M, 2);

                    decimal skladkaEmerytalna = decimal.Round(etat.WynagrodzenieBrutto * skladka.SkladkaEmerytalna / 100M, 2);
                    decimal skladkaRentowa = decimal.Round(etat.WynagrodzenieBrutto * skladka.SkladkaRentowa / 100M, 2);
                    decimal skladkaChorobowa = decimal.Round(etat.WynagrodzenieBrutto * skladka.SkladkaChorobowa / 100M, 2);
                    decimal skladkiSpol = skladkaEmerytalna + skladkaRentowa + skladkaChorobowa;
                    decimal skladkaZdrowotnaI = decimal.Round((etat.WynagrodzenieBrutto - skladkiSpol) * skladka.SkladkaZdrowotna / 100M, 2);
                    decimal podatekDoch = etat.WysokoscEtatu == 0 ? 0 : decimal.Round((etat.WynagrodzenieBrutto - skladkiSpol - skladka.KosztyUsyskPrzych) * skladka.PodatekDochodowy / 100M, 2);


                    decimal podatekPracownika = etat.WysokoscEtatu == 0 ? 0 : -decimal.Round(podatekDoch - skladka.UlgaPodat - skladkaZdrowotnaI, 0);

                    decimal skladkaZdrowotnaII = decimal.Round((etat.WynagrodzenieBrutto - skladkiSpol) * skladka.SkladkaZdrowotnaPodatek / 100M, 2);

                    decimal zusFirma = -(funduszEmer + funduszRentowy + funduszWypadkowy + funduszPracy + funduszGSP);
                    decimal zusPracownik = -(skladkiSpol + skladkaZdrowotnaI + skladkaZdrowotnaII);


                    switch (kvp.Key)
                    {
                        case "MG":
                            prowizja.ProwizjaProcent = procentMG;
                            prowizja.Prowizja = prowizjaMG;
                            prowizja.Inne = inneMG;
                            prowizja.ProwizjaSuma = sumaMG;
                            break;
                        case "LD":
                            prowizja.ProwizjaProcent = procentLD;
                            prowizja.Prowizja = prowizjaLD;
                            prowizja.Inne = inneLD;
                            prowizja.ProwizjaSuma = sumaLD;
                            break;
                        case "PD":
                            prowizja.ProwizjaProcent = procentPD;
                            prowizja.Prowizja = prowizjaPD;
                            prowizja.Inne = innePD;
                            prowizja.ProwizjaSuma = sumaPD;
                            break;
                        case "SD":
                            prowizja.ProwizjaProcent = procentSD;
                            prowizja.Prowizja = prowizjaSD;
                            prowizja.Inne = inneSD;
                            prowizja.ProwizjaSuma = sumaSD;
                            break;
                    }

                    prowizja.Etat = etat.WynagrodzenieBrutto;
                    prowizja.KosztUsuskania = etat.WysokoscEtatu == 0 ? 0 : skladka.KosztyUsyskPrzych;
                    prowizja.UlgaPodatkowa = etat.WysokoscEtatu == 0 ? 0 : skladka.UlgaPodat;
                    prowizja.ZusPlaconyFirma = zusFirma;
                    prowizja.ProwizjaNetto = prowizja.ProwizjaSuma - etat.WynagrodzenieBrutto + zusFirma;
                    prowizja.Podatek19 = -decimal.Round(prowizja.ProwizjaNetto * 0.19M, 2);
                    prowizja.ProwizjaBezPodatki = decimal.Round(prowizja.ProwizjaNetto + prowizja.Podatek19, 2);
                    prowizja.PodatekPlaconyPracownik = podatekPracownika > 0 ? 0 : podatekPracownika;
                    prowizja.ZusPlaconyPracownik = zusPracownik;
                    prowizja.EtatNetto = prowizja.Etat + prowizja.PodatekPlaconyPracownik + prowizja.ZusPlaconyPracownik;
                    prowizja.WyplataNetto = prowizja.ProwizjaSuma + prowizja.Podatek19 + prowizja.PodatekPlaconyPracownik + prowizja.ZusPlaconyFirma + prowizja.ZusPlaconyPracownik;
                    dc.SaveChanges();
                }
            }

            this.Cursor = Cursors.Default;
            this.Enabled = true;

        }

        private DBWeb.Etat GetEtat(string pracownik, DateTime data)
        {
            var etaty = Enova.Business.Old.Core.ContextManager.WebContext.Etaty.Where(e => e.Pracownik == pracownik).OrderByDescending(e => e.Data).ToList();
            foreach (var etat in etaty)
            {
                if (etat.Data <= data)
                    return etat;
            }
            return null;
        }

        private DBWeb.SkladkaZUSUP GetSkladki(DateTime data)
        {
            var skladki = Enova.Business.Old.Core.ContextManager.WebContext.SkladkiZUSUP.OrderByDescending(s => s.Data).ToList();
            foreach (var skladka in skladki)
            {
                if (skladka.Data <= data)
                    return skladka;
            }
            return null;
        }

        private void raportButton_Click(object sender, EventArgs e)
        {
            new ProwizjeMagRocznyForm().ShowDialog();
        }

     }
}
