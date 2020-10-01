using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.Objects;
using Enova.Forms.Services;

namespace AbakTools.Zamowienia.Forms
{
    public partial class ZamowienieFormularzForm : Enova.Forms.FormWithEnovaAPI
    {
        public List<RaportFormularzWgGrupViewRow> DataSource = null;
        public int? KontrahentId;
        string przedstawiciel;
        Enova.API.Business.FeatureDefinition featureDef;
        Enova.API.CRM.Kontrahent kontrahent;
        DateTime dataOd;
        DateTime dataDo;
        DateTime zwrotyOd;
        DateTime zwrotyDo;


        public ZamowienieFormularzForm()
        {
            InitializeComponent();
        }

        private void ZamowienieFormularzForm_Load(object sender, EventArgs e)
        {
            var dc = Enova.Business.Old.Core.ContextManager.WebContext;
            grupyTowaroweBindingSource.DataSource = BusinessService.GetGrupyTowarowe(Session).ToList();
            loadPrzedstawiciele();
            loadKontrahenci();
            if (KontrahentId != null)
            {
                kontrahenciComboBox.SelectedValue = KontrahentId;
            } 
            int? grupa = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigInt("DEFAULT_PRODUCT_GROUP");
            DateTime? dateFrom = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("DEFAULT_SPAN_FROM");
            DateTime? dateTo = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("DEFAULT_SPAN_TO");
            DateTime? zwrotyFrom = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("DEFAULT_RETURN_SPAN_FROM");
            DateTime? zwrotyTo = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("DEFAULT_RETURN_SPAN_TO");
            bool? sp2aktywna = dc.GetConfigBool("SP2_ACTIVE");
            bool? sp2fromOrder = dc.GetConfigBool("SP2_FROM_ORDER");
            DateTime? sp2From = dc.GetConfigDate("DEFAULT_SP2_FROM");
            DateTime? sp2To = dc.GetConfigDate("DEFAULT_SP2_TO");


            if (grupa != null)
                grupyTowaroweComboBox.SelectedValue = grupa.Value;
            if (dateFrom != null)
                dataOdDateTimePicker.Value = dateFrom.Value;
            if (dateTo != null)
                dataDoDateTimePicker.Value = dateTo.Value;
            if (zwrotyFrom != null)
                dtpZwrotyOd.Value = zwrotyFrom.Value;
            else if (dateFrom != null)
                dtpZwrotyOd.Value = dateFrom.Value;
            if (zwrotyTo != null)
                dtpZwrotyDo.Value = zwrotyTo.Value;
            else if (dateTo != null)
                dtpZwrotyDo.Value = dateTo.Value;

            sp2ActiveCheckBox.Checked = sp2aktywna != null && sp2aktywna.Value ? true : false;
            sp2FromOrderCheckBox.Checked = sp2fromOrder != null && sp2fromOrder.Value ? true : false;
            sp2FromDtp.Value = sp2From != null ? sp2From.Value : (dateFrom != null ? dateFrom.Value.AddYears(1).Date : DateTime.Now.Date);
            sp2ToDtp.Value = sp2To != null ? sp2To.Value : (dateTo != null ? dateTo.Value.AddYears(1).Date : DateTime.Now.Date);
            setEnableSp2(sp2ActiveCheckBox.Checked);

            if (Enova.Business.Old.DB.Web.User.LoginedUser.Administrator || (Enova.Business.Old.DB.Web.User.LoginedUser.IsSuperAdmin != null && 
                Enova.Business.Old.DB.Web.User.LoginedUser.IsSuperAdmin.Value))
            {
                wypełnijButton.Visible = true;
            }
            else
            {
                wypełnijButton.Visible = false;
            }

            this.ActiveControl = obliczButton;
        }

        private void loadPrzedstawiciele()
        {
            przedstawicieleComboBox.Items.Add("(Wszyscy)");
            przedstawicieleComboBox.Items.AddRange(
                BusinessService.Dictionary.GetByFeatureName(Session, "Kontrahenci", "przedstawiciel").ToList().Select(r=>r.Value)
                .OrderBy(r => r).ToArray());
            przedstawicieleComboBox.SelectedIndex = 0;
        }


        private void loadKontrahenci()
        {
            string przedstawiciel = przedstawicieleComboBox.SelectedValue as string;
            kontrahenciBindingSource.DataSource = CRMService.Kontrahenci.ByPrzedstawiciel(Session, przedstawiciel).ToList().OrderBy(r => r.Kod);
        }

        private void przedstawicieleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadKontrahenci();
        }

        private void setEnableSp2(bool enable)
        {
            label8.Enabled = enable;
            label9.Enabled = enable;
            label11.Enabled = enable;
            sp2FromOrderCheckBox.Enabled = enable;
            sp2FromDtp.Enabled = enable;
            sp2ToDtp.Enabled = enable;
        }

        private void obliczButton_Click(object sender, EventArgs e)
        {
            if (kontrahenciComboBox.SelectedIndex < 0)
                throw new Exception("Nie wybrano kontrahenta");

            przedstawiciel = (string)(przedstawicieleComboBox.SelectedIndex <= 0 ? null : przedstawicieleComboBox.SelectedItem);
            featureDef = (Enova.API.Business.FeatureDefinition)grupyTowaroweComboBox.SelectedItem;
            kontrahent = (Enova.API.CRM.Kontrahent)kontrahenciComboBox.SelectedItem;
            dataOd = dataOdDateTimePicker.Value.Date;
            dataDo = dataDoDateTimePicker.Value.Date;
            zwrotyOd = dtpZwrotyOd.Value.Date;
            zwrotyDo = dtpZwrotyDo.Value.Date;


            Enova.Business.Old.Forms.ProgressForm.StartProgress(new ProgressHandler(this));
        }

        private void obliczZZamowienButton_Click(object sender, EventArgs e)
        {
            if (kontrahenciComboBox.SelectedIndex < 0)
                throw new Exception("Nie wybrano kontrahenta");

            przedstawiciel = (string)(przedstawicieleComboBox.SelectedIndex <= 0 ? null : przedstawicieleComboBox.SelectedItem);
            featureDef = (Enova.API.Business.FeatureDefinition)grupyTowaroweComboBox.SelectedItem;
            kontrahent = (Enova.API.CRM.Kontrahent)kontrahenciComboBox.SelectedItem;
            dataOd = dataOdDateTimePicker.Value.Date;
            dataDo = dataDoDateTimePicker.Value.Date;
            zwrotyOd = dtpZwrotyOd.Value.Date;
            zwrotyDo = dtpZwrotyDo.Value.Date;

            Enova.Business.Old.Forms.ProgressForm.StartProgress(new ZZamowienProgressHandler(this));

        }

        private void setDataSource()
        {
            if (this.InvokeRequired)
            {
                var d = new MethodInvoker(setDataSource);
                this.Invoke(d);
            }
            else
            {
                pozycjeBindingSource.DataSource = DataSource.OrderBy(d => d.Kolejnosc).ThenBy(d => d.Kod);

                if (dataGridView.RowCount > 0)
                {
                    dataGridView.CurrentCell = dataGridView.Rows[0].Cells[5];
                    ActiveControl = dataGridView;
                }
            }
        }

        private void setEnable(bool enable)
        {
            if (this.InvokeRequired)
            {
                var d = new Action<bool>(setEnable);
                this.Invoke(d, enable);
            }
            else
            {
                if (enable)
                {
                    this.Cursor = Cursors.Default;
                    this.Enabled = true;

                }
                else
                {
                    this.Enabled = false;
                    this.Cursor = Cursors.WaitCursor;
                }
            }
        }


        private void ZamowienieFormularzForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void wypełnijButton_Click(object sender, EventArgs e)
        {
            if (DataSource == null)
                obliczButton_Click(null, null);

            if (DataSource != null)
            {
                foreach (var r in DataSource)
                {
                    if (r.ObrotSuma != null && r.ObrotSuma.Value > 0)
                        r.IloscZam = (int)r.ObrotSuma.Value;
                }
                dataGridView.Refresh();
            }
        }

        private void wyczyscButton_Click(object sender, EventArgs e)
        {
            if (DataSource == null)
                obliczButton_Click(null, null);
            else
            {
                foreach (var r in DataSource)
                    r.IloscZam = null;
                dataGridView.Refresh();
            }
        }

        private void dataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < dataGridView.Rows.Count)
            {
                RaportFormularzWgGrupViewRow row = (RaportFormularzWgGrupViewRow)dataGridView.Rows[e.RowIndex].DataBoundItem;
                if (!string.IsNullOrEmpty(row.Kolor))
                {
                    Color color = ColorTranslator.FromHtml(row.Kolor);
                    dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = color;
                }
            }

        }

        private void sp2ActiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setEnableSp2(sp2ActiveCheckBox.Checked);
        }

        #region Nested Types

        public class ProgressHandler : Enova.Business.Old.Forms.ProgressFormHandler
        {
            private ZamowienieFormularzForm parent;

            public ProgressHandler(ZamowienieFormularzForm parent)
            {
                this.parent = parent;
            }

            public override void StartProcess()
            {
                try
                {
                    parent.setEnable(false);

                    string przedstawiciel = parent.przedstawiciel;
                    var featureDef = parent.featureDef;
                    DateTime dataOd = parent.dataOd;
                    DateTime dataDo = parent.dataDo;
                    DateTime zwrotyOd = parent.zwrotyOd;
                    DateTime zwrotyDo = parent.zwrotyDo;
                    parent.DataSource = new List<RaportFormularzWgGrupViewRow>();
                    var dbCfg = Enova.Business.Old.Core.Configuration.GetDataBaseSettings("EnovaContext");

                    if (dbCfg == null || string.IsNullOrEmpty(dbCfg.ProviderConnectionString))
                        throw new Exception("Brak skonfigurowanego połaczenia do bazy danych enova");

                    using (var conn = new SqlConnection(dbCfg.ProviderConnectionString))
                    {
                        conn.Open();
                        GetObrotyBase ob = new GetObrotyEnova(conn, parent.Session, parent.kontrahent);
                        GetObrotyBase ob2 = parent.sp2ActiveCheckBox.Checked ? (
                            parent.sp2FromOrderCheckBox.Checked ? (GetObrotyBase)new GetObrotyZamowienia(conn, parent.Session, parent.kontrahent) : 
                            (GetObrotyBase)new GetObrotyEnova(conn, parent.Session, parent.kontrahent)
                            ) : new GetObrotyBase(parent.Session, parent.kontrahent);
                        var dictionaryItems = featureDef.DictionaryList.ToList().Where(r => r.Value != "RESZTA").OrderBy(r => r.Value);
                        var towary = ob.TowaryModule.Towary.CreateView().SetFilter(string.Format("Features.[{0}] = '{1}'", featureDef.Name, "RESZTA"))
                            .Cast<Enova.API.Towary.Towar>().ToList();
                        var defCeny = ob.TowaryModule.DefinicjeCen["Hurtowa"];
                        this.ProgressArgs.MaxProgress1 = dictionaryItems.Count() + towary.Count;
                        this.ProgressArgs.Progress2Visible = false;
                        this.ProgressChanges();

                        foreach (var d in dictionaryItems)
                        {
                            int minusYear = 0;
                            int fvZmMonth = 0;
                            int fkZmMonth = 0;
                            int fvZmYear = 0;
                            int fkZmYear = 0;
                            int kolejnosc = -1;
                            string kolor = "LightGrey";
                            string kom = string.Empty;
                            string nazwa = d.Value;
                            DateTime dDataOd = dataOd;
                            DateTime dDataDo = dataDo;
                            DateTime dZwrotyOd = zwrotyOd;
                            DateTime dZwrotyDo = zwrotyDo;

                            var match = Regex.Match(nazwa, @"!([0-9]+)!");
                            if (match.Success)
                            {
                                int.TryParse(match.Groups[1].Value, out kolejnosc);
                                nazwa = nazwa.Replace("!" + match.Groups[1].Value + "!", "");
                            }

                            match = Regex.Match(nazwa, @"@([a-zA-Z0-9#]+)@");
                            if (match.Success)
                            {
                                kolor = match.Groups[1].Value;
                                nazwa = nazwa.Replace("@" + match.Groups[1].Value + "@", "");
                            }

                            match = Regex.Match(nazwa, @"#([0-9\-MmRrYySsZz]+)#");
                            if(match.Success)
                            {
                                var zmOkrStr = match.Groups[1].Value.ToLower();
                                nazwa = nazwa.Replace("#" + match.Groups[1].Value + "#", "");
                                bool zmS = false;
                                bool zmZ = false;
                                bool zmMinus = false;
                                string numS = "";
                                int num = 0;

                                foreach(var c in zmOkrStr)
                                {
                                    switch(c)
                                    {
                                        case 's':
                                            zmS = true;
                                            break;
                                        case 'z':
                                            zmZ = true;
                                            break;
                                        case '-':
                                            zmMinus = true;
                                            break;
                                        case 'm':
                                            num = int.Parse(numS);
                                            if (zmMinus)
                                                num *= -1;
                                            if (zmS || !zmZ)
                                                fvZmMonth = num;
                                            if (zmZ || !zmS)
                                                fkZmMonth = num;
                                            zmS = zmZ = zmMinus = false;
                                            numS = "";
                                            break;
                                        case 'y':
                                        case 'r':
                                            num = int.Parse(numS);
                                            if (zmMinus)
                                                num *= -1;
                                            if (zmS || !zmZ)
                                                fvZmYear = num;
                                            if (zmZ || !zmS)
                                                fkZmYear = num;
                                            zmS = zmZ = zmMinus = false;
                                            numS = "";

                                            break;
                                        default:
                                            if (c >= '0' && c <= '9')
                                                numS += c;
                                            break;
                                    }
                                }
                            }

                            if (d.Value.EndsWith("-1"))
                            {
                                minusYear = -1;
                                kom = " - dane z przed 2 lat";
                            }
                            else if (d.Value.EndsWith("-2"))
                            {
                                minusYear = -2;
                                kom = " - dane z przed 3 lat";
                            }
                            else if (d.Value.EndsWith("-3"))
                            {
                                minusYear = -3;
                                kom = " - dane z przed 4 lat";
                            }
                            else if (d.Value.EndsWith("-4"))
                            {
                                minusYear = -4;
                                kom = " - dane z przed 5 lat";
                            }

                            if (minusYear != 0)
                            {
                                dDataOd = dDataOd.AddYears(minusYear);
                                dDataDo = dDataDo.AddYears(minusYear);
                                dZwrotyOd = dZwrotyOd.AddYears(minusYear);
                                dZwrotyDo = dZwrotyDo.AddYears(minusYear);
                            }

                            if (fvZmYear != 0)
                            {
                                dDataOd = dDataOd.AddYears(fvZmYear);
                                dDataDo = dDataDo.AddYears(fvZmYear);
                            }
                            if (fvZmMonth != 0)
                            {
                                dDataOd = dDataOd.AddMonths(fvZmMonth);
                                dDataDo = dDataDo.AddMonths(fvZmMonth);
                            }

                            if(fkZmYear != 0)
                            {
                                dZwrotyOd = dZwrotyOd.AddYears(fkZmYear);
                                dZwrotyDo = dZwrotyDo.AddYears(fkZmYear);
                            }

                            if(fkZmMonth!=0)
                            {
                                dZwrotyOd = dZwrotyOd.AddMonths(fkZmMonth);
                                dZwrotyDo = dZwrotyDo.AddMonths(fkZmMonth);
                            }

                            this.ProgressArgs.TextProgress1 = "Grupa: " + nazwa;
                            this.ProgressChanges();

                            var obrWZ = ob.GetObrotyGrupaFV(featureDef, d.Value, dDataOd, dDataDo);
                            var obrKWZ = ob.GetObrotyGrupaFK(featureDef, d.Value, dZwrotyOd, dZwrotyDo);
                            var sp2obrWZ = ob2.GetObrotyGrupaFV(featureDef, d.Value, parent.sp2FromDtp.Value.Date, parent.sp2ToDtp.Value.Date);
                            var sp2obrKWZ = ob2.GetObrotyGrupaFK(featureDef, d.Value, parent.sp2FromDtp.Value.Date, parent.sp2ToDtp.Value.Date);



                            if (minusYear != 0)
                            {
                                nazwa = nazwa.Substring(0, nazwa.Length - 2) + kom;
                            }

                            parent.DataSource.Add(new RaportFormularzWgGrupViewRow()
                            {
                                Grupa = "Zgrupowane",
                                Nazwa = nazwa,
                                ObrotFV = (obrWZ == null || obrWZ.Value != 0) ? obrWZ : (double?)null,
                                ObrotFK = (obrKWZ == null || obrKWZ.Value != 0) ? obrKWZ : (double?)null,
                                ObrotFV2 = (sp2obrWZ == null || sp2obrWZ.Value != 0) ? sp2obrWZ : (double?)null,
                                ObrotFK2 = (sp2obrKWZ == null || sp2obrKWZ.Value != 0) ? sp2obrKWZ : (double?)null,
                                TowarIndywidualny = true,
                                StandardowaIlosc = null,
                                Kolejnosc = kolejnosc,
                                Kod = "",
                                Kolor = kolor
                            });
                            this.PerformStep1();

                        }

                        var stanMagazynuWorker = Enova.API.EnovaService.Instance.CreateObject<Enova.API.Magazyny.StanMagazynuWorker>();
                        stanMagazynuWorker.Magazyn = ob.MagazynyModule.Magazyny.Firma;

                        foreach (var towar in towary)
                        {
                            this.ProgressArgs.TextProgress1 = "Towar: " + towar.Kod;
                            this.ProgressChanges();

                            var obliczObrotyZ = (Enova.API.Towary.Towar)towar.Features["OBLICZ OBROTY Z"];
                            if (obliczObrotyZ == null)
                                obliczObrotyZ = towar;
                            var dodajObrotyZ = (Enova.API.Towary.Towar)towar.Features["DODAJ OBROTY Z"];

                            string dostawca = (string)towar.Features["DOSTAWCY"];
                            double? obrotFV = ob.GetObrotyTowarFV(obliczObrotyZ.Guid, dataOd, dataDo);
                            double? obrotKFV = ob.GetObrotyTowarFK(obliczObrotyZ.Guid, zwrotyOd, zwrotyDo);
                            double? obrotFV2 = ob2.GetObrotyTowarFV(towar.Guid, parent.sp2FromDtp.Value.Date, parent.sp2ToDtp.Value.Date);
                            double? obrotKFV2 = ob2.GetObrotyTowarFK(towar.Guid, parent.sp2FromDtp.Value.Date, parent.sp2ToDtp.Value.Date);
                            double? dodawanyObrotFV = 0;
                            double? dodawanyObrotKFV = 0;
                            if(dodajObrotyZ != null)
                            {
                                dodawanyObrotFV = ob.GetObrotyTowarFV(dodajObrotyZ.Guid, dataOd, dataDo);
                                dodawanyObrotKFV = ob.GetObrotyTowarFK(dodajObrotyZ.Guid, zwrotyOd, zwrotyDo);
                            }
                            
                            stanMagazynuWorker.Towar = towar;

                            int? iloscFV = null;
                            double? stanMag = stanMagazynuWorker.StanMagazynu.Value;

                            string nazwa = towar.Prefix() + towar.Nazwa + towar.Suffix() + (towar.Nowosc() ? " - NOWOŚĆ" : "");
                            double? mnoznik = towar.MnoznikObrotow();
                            double procent = towar.ProcentObrotow();
                            double procentDodawanych = towar.ProcentDodawanychObrotow();

                            var cena = towar.Ceny[defCeny];

                            parent.DataSource.Add(new RaportFormularzWgGrupViewRow()
                            {
                                TowarGuid = towar.Guid,
                                TowarID = towar.ID,
                                Grupa = dostawca,
                                Kod = towar.Kod,
                                Nazwa = nazwa,
                                IloscFV = iloscFV,
                                ObrotFV = roundDouble((obrotFV ?? 0) * mnoznik * procent + (dodawanyObrotFV ?? 0) * procentDodawanych),
                                ObrotFK = roundDouble((obrotKFV ?? 0) * mnoznik * procent + (dodawanyObrotKFV ?? 0) * procentDodawanych),
                                ObrotFV2 = obrotFV2,
                                ObrotFK2 = obrotKFV2,
                                StandardowaIlosc = cena.StandardowaIlosc,
                                CenaNetto = (decimal)cena.Netto.Value,
                                Kolejnosc = towar.KolejnoscNaFormularzu(),
                                Kolor = towar.KolorNaForm(),
                                Info = towar.Opis.ToString(),
                                StanMagazynu = stanMag
                            });
                            PerformStep1();
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    parent.setEnable(true);
                }
            }

            private double? roundDouble(double? d)
            {
                if (d != null)
                {
                    var r = (double)(d - (int)d);
                    if (r != 0)
                        return (double)decimal.Round((decimal)d, 2);
                }
                return d;
            }

            public override void FinishProcess()
            {
                parent.setDataSource();
                base.FinishProcess();
            }
        }

        public class ZZamowienProgressHandler : Enova.Business.Old.Forms.ProgressFormHandler
        {
            private ZamowienieFormularzForm parent;

            public ZZamowienProgressHandler(ZamowienieFormularzForm parent)
            {
                this.parent = parent;
            }

            public override void StartProcess()
            {
                try
                {
                    parent.setEnable(false);

                    string przedstawiciel = parent.przedstawiciel;
                    var featureDef = parent.featureDef;
                    var kontrahent = parent.kontrahent;
                    DateTime dataOd = parent.dataOd;
                    DateTime dataDo = parent.dataDo;
                    DateTime zwrotyOd = parent.zwrotyOd;
                    DateTime zwrotyDo = parent.zwrotyDo;
                    var session = parent.Session;
                    var bm = session.GetModule<Enova.API.Business.BusinessModule>();
                    var tm = session.GetModule<Enova.API.Towary.TowaryModule>();
                    var hm = session.GetModule<Enova.API.Handel.HandelModule>();
                    var mm = session.GetModule<Enova.API.Magazyny.MagazynyModule>();

                    var okres = mm.OkresyMag.Aktualny;
                    var wzDef = hm.DefDokHandlowych.WydanieMagazynowe;
                    var kwzDef = hm.DefDokHandlowych.KorektaWZ;
                    var defCeny = tm.DefinicjeCen["Hurtowa"];
                    parent.DataSource = new List<RaportFormularzWgGrupViewRow>();

                    var dictionaryItems = featureDef.DictionaryList.ToList().Where(r => r.Value != "RESZTA").OrderBy(r => r.Value);
                    var towary = tm.Towary.CreateView().SetFilter(string.Format("Features.[{0}] = '{1}'", featureDef.Name, "RESZTA"))
                        .Cast<Enova.API.Towary.Towar>().ToList();

                    this.ProgressArgs.MaxProgress1 = dictionaryItems.Count() + towary.Count;
                    this.ProgressArgs.Progress2Visible = false;
                    this.ProgressChanges();

                    var dbCfg = Enova.Business.Old.Core.Configuration.GetDataBaseSettings("EnovaContext");

                    if (dbCfg == null || string.IsNullOrEmpty(dbCfg.ProviderConnectionString))
                        throw new Exception("Brak skonfigurowanego połaczenia do bazy danych enova");

                    using (var conn = new SqlConnection(dbCfg.ProviderConnectionString))
                    using(var dc = new Enova.Business.Old.DB.Web.WebContext())
                    {
                        conn.Open();
                        foreach (var d in dictionaryItems)
                        {
                            int minusYear = 0;
                            int kolejnosc = -1;
                            string kolor = "LightGrey";
                            string kom = string.Empty;
                            string nazwa = d.Value;
                            DateTime dDataOd = dataOd;
                            DateTime dDataDo = dataDo;
                            DateTime dZwrotyOd = zwrotyOd;
                            DateTime dZwrotyDo = zwrotyDo;

                            var match = Regex.Match(nazwa, @"!([0-9]+)!");
                            if (match.Success)
                            {
                                int.TryParse(match.Groups[1].Value, out kolejnosc);
                                nazwa = nazwa.Replace("!" + match.Groups[1].Value + "!", "");
                            }

                            match = Regex.Match(nazwa, @"@([a-zA-Z0-9#]+)@");
                            if (match.Success)
                            {
                                kolor = match.Groups[1].Value;
                                nazwa = nazwa.Replace("@" + match.Groups[1].Value + "@", "");
                            }


                            if (d.Value.EndsWith("-1"))
                            {
                                minusYear = -1;
                                kom = " - dane z przed 2 lat";
                            }
                            else if (d.Value.EndsWith("-2"))
                            {
                                minusYear = -2;
                                kom = " - dane z przed 3 lat";
                            }
                            else if (d.Value.EndsWith("-3"))
                            {
                                minusYear = -3;
                                kom = " - dane z przed 4 lat";
                            }
                            else if (d.Value.EndsWith("-4"))
                            {
                                minusYear = -4;
                                kom = " - dane z przed 5 lat";
                            }

                            if (minusYear != 0)
                            {
                                dDataOd = dDataOd.AddYears(minusYear);
                                dDataDo = dDataDo.AddYears(minusYear);
                                dZwrotyOd = dZwrotyOd.AddYears(minusYear);
                                dZwrotyDo = dZwrotyDo.AddYears(minusYear);
                            }

                            this.ProgressArgs.TextProgress1 = "Grupa: " + nazwa;
                            this.ProgressChanges();

                            double obrFV = 0;

                            var sql = "SELECT t.Guid FROM dbo.Towary t INNER JOIN dbo.Features f ON (f.Parent=t.ID AND f.ParentType='Towary' AND f.Name='" 
                                + featureDef.Name + "') WHERE f.Data='" + d.Value + "'";
                            using(var cmd = new SqlCommand(sql, conn))
                            {
                                using(SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while(reader.Read())
                                    {
                                        var guid = reader.GetGuid(0);
                                        var obr = dc.PozycjeZamowien.Where(r =>
                                            r.Zamowienie.DataDodania >= dDataOd && r.Zamowienie.DataDodania <= dDataDo && 
                                            r.Zamowienie.Synchronizacja != (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedDelete &&
                                            r.Zamowienie.Kontrahent.Guid == kontrahent.Guid
                                            && r.Produkt != null && r.Produkt.EnovaGuid == guid && r.Ilosc > 0).Sum(r => r.Ilosc);
                                        if (obr != null)
                                            obrFV += obr.Value;
                                    }
                                }
                            }

                            if (minusYear != 0)
                            {
                                nazwa = nazwa.Substring(0, nazwa.Length - 2) + kom;
                            }

                            parent.DataSource.Add(new RaportFormularzWgGrupViewRow()
                            {
                                Grupa = "Zgrupowane",
                                Nazwa = nazwa,
                                ObrotFV = obrFV!= 0 ? obrFV : (double?)null,
                                ObrotFK = (double?)null,
                                TowarIndywidualny = true,
                                StandardowaIlosc = null,
                                Kolejnosc = kolejnosc,
                                Kod = "",
                                Kolor = kolor
                            });
                            this.PerformStep1();

                        }

                        var stanMagazynuWorker = Enova.API.EnovaService.Instance.CreateObject<Enova.API.Magazyny.StanMagazynuWorker>();
                        stanMagazynuWorker.Magazyn = mm.Magazyny.Firma;

                        foreach (var towar in towary)
                        {
                            this.ProgressArgs.TextProgress1 = "Towar: " + towar.Kod;
                            this.ProgressChanges();

                            string dostawca = (string)towar.Features["DOSTAWCY"];

                            double? obrotFV = dc.PozycjeZamowien.Where(r =>
                                            r.Zamowienie.DataDodania >= dataOd && r.Zamowienie.DataDodania <= dataDo &&
                                            r.Zamowienie.Synchronizacja != (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedDelete &&
                                            r.Zamowienie.Kontrahent.Guid == kontrahent.Guid
                                            && r.Produkt != null && r.Produkt.EnovaGuid == towar.Guid && r.Ilosc > 0).Sum(r => r.Ilosc);

                            stanMagazynuWorker.Towar = towar;

                            int? iloscFV = null;
                            double? stanMag = stanMagazynuWorker.StanMagazynu.Value;

                            string nazwa = towar.Prefix() + towar.Nazwa + towar.Suffix() + (towar.Nowosc() ? " - NOWOŚĆ" : "");

                            var cena = towar.Ceny[defCeny];

                            parent.DataSource.Add(new RaportFormularzWgGrupViewRow()
                            {
                                TowarGuid = towar.Guid,
                                TowarID = towar.ID,
                                Grupa = dostawca,
                                Kod = towar.Kod,
                                Nazwa = nazwa,
                                IloscFV = iloscFV,
                                ObrotFV = obrotFV ?? 0,
                                ObrotFK = null,
                                StandardowaIlosc = cena.StandardowaIlosc,
                                CenaNetto = (decimal)cena.Netto.Value,
                                Kolejnosc = towar.KolejnoscNaFormularzu(),
                                Kolor = towar.KolorNaForm(),
                                Info = towar.Opis.ToString(),
                                StanMagazynu = stanMag
                            });
                            PerformStep1();
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    parent.setEnable(true);
                }
            }

            private double? roundDouble(double? d)
            {
                if (d != null)
                {
                    var r = (double)(d - (int)d);
                    if (r != 0)
                        return (double)decimal.Round((decimal)d, 2);
                }
                return d;
            }

            public override void FinishProcess()
            {
                parent.setDataSource();
                base.FinishProcess();
            }
        }

        public class GetObrotyBase
        {
            private Enova.API.Business.Session session;
            private Enova.API.CRM.Kontrahent kontrahent;
            private Enova.API.Business.BusinessModule bm;
            private Enova.API.Towary.TowaryModule tm;
            private Enova.API.Magazyny.MagazynyModule mm;
            private Enova.API.Handel.HandelModule hm;

            public Enova.API.Business.Session Session
            {
                get { return session; }
            }

            public Enova.API.CRM.Kontrahent Kontrahent
            {
                get { return kontrahent; }
            }

            public Enova.API.Business.BusinessModule BusinessModule
            {
                get
                {
                    if (bm == null)
                        bm = Session.GetModule<Enova.API.Business.BusinessModule>();
                    return bm;
                }
            }

            public Enova.API.Towary.TowaryModule TowaryModule
            {
                get
                {
                    if (tm == null)
                        tm = Session.GetModule<Enova.API.Towary.TowaryModule>();
                    return tm;
                }
            }

            public Enova.API.Handel.HandelModule HandelModule
            {
                get
                {
                    if (hm == null)
                        hm = Session.GetModule<Enova.API.Handel.HandelModule>();
                    return hm;
                }
            }

            public Enova.API.Magazyny.MagazynyModule MagazynyModule
            {
                get
                {
                    if (mm == null)
                        mm = Session.GetModule<Enova.API.Magazyny.MagazynyModule>();
                    return mm;
                }
            }

            public GetObrotyBase(Enova.API.Business.Session session, Enova.API.CRM.Kontrahent kontrahent)
            {
                this.session = session;
                this.kontrahent = kontrahent;
            }

            public virtual double? GetObrotyGrupaFV(Enova.API.Business.FeatureDefinition fd, string name, DateTime dateFrom, DateTime dateTo)
            {
                return null;
            }

            public virtual double? GetObrotyGrupaFK(Enova.API.Business.FeatureDefinition fd, string name, DateTime dateFrom, DateTime dateTo)
            {
                return null;
            }

            public virtual double? GetObrotyTowarFV(Guid towarGuid, DateTime dateFrom, DateTime dateTo)
            {
                return null;
            }

            public virtual double? GetObrotyTowarFK(Guid towarGuid, DateTime dateFrom, DateTime dateTo)
            {
                return null;
            }
        }

        public class GetObrotyEnova : GetObrotyBase
        {
            private SqlConnection connection;
            private Enova.API.Magazyny.OkresMagazynowy okres;
            private Enova.API.Handel.DefDokHandlowego wzDef;
            private Enova.API.Handel.DefDokHandlowego kwzDef;

            public Enova.API.Magazyny.OkresMagazynowy Okres
            {
                get
                {
                    if(okres == null)
                        okres = MagazynyModule.OkresyMag.Aktualny;
                    return okres;
                }
            }

            public Enova.API.Handel.DefDokHandlowego DefinicjaWZ
            {
                get
                {
                    if(wzDef == null)
                        wzDef = HandelModule.DefDokHandlowych.WydanieMagazynowe;
                    return wzDef;
                }
            }

            public Enova.API.Handel.DefDokHandlowego DefinicjaKWZ
            {
                get
                {
                    if(kwzDef == null)
                        kwzDef = HandelModule.DefDokHandlowych.KorektaWZ;
                    return kwzDef;
                }
            }

            public GetObrotyEnova(SqlConnection connection, Enova.API.Business.Session session, Enova.API.CRM.Kontrahent kontrahent)
                : base(session, kontrahent)
            {
                this.connection = connection;
            }

            public override double? GetObrotyGrupaFV(Enova.API.Business.FeatureDefinition fd, string name, DateTime dateFrom, DateTime dateTo)
            {
                return MagazynyService.Obroty.Sumuj<double?>(connection, "IloscValue", Okres.ID, null, Kontrahent.ID, 
                    DefinicjaWZ.ID, dateFrom, dateTo, null, fd.Name, name);
            }

            public override double? GetObrotyGrupaFK(Enova.API.Business.FeatureDefinition fd, string name, DateTime dateFrom, DateTime dateTo)
            {
                return MagazynyService.Obroty.Sumuj<double?>(connection, "IloscValue", Okres.ID, null, Kontrahent.ID,
                    DefinicjaKWZ.ID, dateFrom, dateTo, null, fd.Name, name);
            }

            public override double? GetObrotyTowarFV(Guid towarGuid, DateTime dateFrom, DateTime dateTo)
            {
                return MagazynyService.Obroty.Sumuj<double?>(connection, "IloscValue", Okres.ID, null, Kontrahent.ID, 
                    DefinicjaWZ.ID, dateFrom, dateTo, towarGuid);
            }

            public override double? GetObrotyTowarFK(Guid towarGuid, DateTime dateFrom, DateTime dateTo)
            {
                return MagazynyService.Obroty.Sumuj<double?>(connection, "IloscValue", Okres.ID, null, Kontrahent.ID,
                    DefinicjaKWZ.ID, dateFrom, dateTo, towarGuid);
            }

        }

        public class GetObrotyZamowienia : GetObrotyBase
        {
            private SqlConnection connection;
            

            public GetObrotyZamowienia(SqlConnection connection, Enova.API.Business.Session session, Enova.API.CRM.Kontrahent kontrahent)
                : base(session, kontrahent)
            {
                this.connection = connection;
                
            }

            public override double? GetObrotyTowarFV(Guid towarGuid, DateTime dateFrom, DateTime dateTo)
            {
                using (var dc = new Enova.Business.Old.DB.Web.WebContext())
                {
                    var to = dateTo.Date.AddDays(1);
                    var kguid = Kontrahent.Guid;
                    var sql = string.Format(
                        "SELECT SUM(pz.Ilosc) Ilosc FROM dbo.PozycjeZamowien pz INNER JOIN dbo.Zamowienia z on z.ID = pz.Zamowienie " +
                        "INNER JOIN dbo.product p ON p.id=pz.Produkt INNER JOIN dbo.Kontrahenci k on k.ID=z.Kontrahent " +
                        "WHERE z.Synchronizacja != {0} and pz.Produkt is not null and p.enova_guid='{1}' and z.DataDodania>='{2}' and z.DataDodania<'{3}' " +
                        "AND pz.Ilosc IS NOT NULL AND pz.Ilosc > 0 AND k.Guid='{4}'",
                        (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedDelete, 
                        towarGuid, 
                        dateFrom.Date.ToString("s"), 
                        to.Date.ToString("s"),
                        kguid);
                    return dc.ExecuteStoreQuery<double?>(sql).FirstOrDefault();
                }
            }
        }

        public class RaportFormularzWgGrupViewRow
        {
            public int? TowarID { get; set; }
            public Guid? TowarGuid { get; set; }
            public string Kod { get; set; }
            public string Nazwa { get; set; }
            public string Info { get; set; }
            public double? ObrotFV { get; set; }
            public double? ObrotFK { get; set; }
            public double? ObrotSuma
            {
                get
                {
                    if (ObrotFV == null && ObrotFK == null)
                        return null;
                    return (ObrotFV == null ? 0 : ObrotFV.Value) + (ObrotFK == null ? 0 : ObrotFK.Value);
                }
            }
            public double? ObrotFV2 { get; set; }
            public double? ObrotFK2 { get; set; }
            public double? ObrotSuma2
            {
                get
                {
                    if (ObrotFV2 == null && ObrotFK2 == null)
                        return null;
                    return (ObrotFV2 == null ? 0 : ObrotFV2.Value) + (ObrotFK2 == null ? 0 : ObrotFK2.Value);
                }
            }

            public decimal? ObrotFVWartosc { get; set; }
            public decimal? ObrotSumaWartosc { get; set; }
            public int Kolejnosc { get; set; }
            public string Kolor { get; set; }

            private decimal? cennaNetto;
            public decimal? CenaNetto
            {
                get
                {
                    if (this.cennaNetto != null)
                        return this.cennaNetto;
                    if (this.ObrotFV != null && this.ObrotFV != 0 && this.ObrotFVWartosc != null)
                    {
                        return decimal.Round((decimal)(ObrotFVWartosc / (decimal?)ObrotFV), 2);
                    }
                    return null;
                }
                set
                {
                    this.cennaNetto = value;
                }
            }
            public double? ProcentZwrotu
            {
                get
                {
                    if (ObrotFV != null)
                    {
                        double obrotFK = ObrotFK == null ? 0 : (double)Math.Abs((decimal)ObrotFK);
                        return round(obrotFK / (double)ObrotFV);
                    }
                    return null;
                }
            }
            public int? IloscFV { get; set; }
            public string Grupa { get; set; }
            public string Kolumna1 { get; set; }
            public string Kolumna2 { get; set; }
            public string Kolumna3 { get; set; }
            public string Kolumna4 { get; set; }
            public string Kolumna5 { get; set; }
            public IEnumerable ObrotyWZ { get; set; }
            public float? IloscZam { get; set; }
            public string Opis { get; set; }
            public bool TowarIndywidualny { get; set; }
            public double? StandardowaIlosc { get; set; }
            public double? StanMagazynu { get; set; }

            private double? round(double? d)
            {
                try
                {
                    if (d != null)
                    {
                        var r = (double)(d - (int)d);
                        if (r != 0)
                            return (double)decimal.Round((decimal)d, 2);
                    }
                }
                catch { }
                return d;

            }

        }


        #endregion


    }
}
