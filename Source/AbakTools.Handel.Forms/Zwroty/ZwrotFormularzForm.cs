using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
/*
using Enova.Business.Old.DB;
using Enova.Business.Old.Types;
using Enova.Business.Old.DB.Services;
 */
using Enova.Forms.Services;
using System.Data.Objects;


namespace AbakTools.Zwroty.Forms
{
    public partial class ZwrotFormularzForm : Enova.Forms.FormWithEnovaAPI
    {
        public List<RaportFormularzWgGrupViewRow> DataSource = null;

        public int? KontrahentId = null;

        public ZwrotFormularzForm()
        {
            InitializeComponent();
        }

        private void ZwrotFormularzForm_Load(object sender, EventArgs e)
        {

            //grupyTowaroweBindingSource.DataSource = FeatureDefsService.GetGrupyTowaroweQuery().OrderBy(g => g.Nazwa).ToList();
            grupyTowaroweBindingSource.DataSource = BusinessService.GetGrupyTowarowe(Session);
            loadPrzedstawiciele();
            loadKontrahenci();
            if (KontrahentId != null)
            {
                kontrahenciComboBox.SelectedValue = KontrahentId;
            }
            int? grupa = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigInt("ZWROT_DEFAULT_PRODUCT_GROUP");
            DateTime? dateFrom = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("ZWROT_DEFAULT_SPAN_FROM");
            DateTime? dateTo = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("ZWROT_DEFAULT_SPAN_TO");
            if (grupa != null)
                grupyTowaroweComboBox.SelectedValue = grupa.Value;
            if (dateFrom != null)
                dataOdDateTimePicker.Value = dateFrom.Value;

            if (dateTo != null)
                dataDoDateTimePicker.Value = dateTo.Value;

            this.ActiveControl = obliczButton;
        }


        private void loadPrzedstawiciele()
        {
            /*
            string dictionary = FeatureDefsService.GetDictionary("Kontrahenci", "przedstawiciel");
            List<PrzedstawicieleViewRow> ds = new List<PrzedstawicieleViewRow>() { new PrzedstawicieleViewRow() { ID = 0, Nazwa = "(Wszyscy)" } };
            ds.AddRange(DictionaryService.GetDictionaryQuery(dictionary).OrderBy(d => d.Value).Select(d => new PrzedstawicieleViewRow() { ID = d.ID, Nazwa = d.Value }).ToList());
            przedstawicieleBindingSource.DataSource = ds;
             */
            przedstawicieleComboBox.Items.Add("(Wszyscy)");
            przedstawicieleComboBox.Items.Add(BusinessService.Dictionary.GetByFeatureName(Session, "Kontrahenci", "przedstawiciel").OrderBy(r => r.Value).Select(r => r.Value).ToArray());


        }


        private void loadKontrahenci()
        {
            /*
            string przedstawiciel = przedstawicieleComboBox.SelectedValue as string;
            if (!string.IsNullOrEmpty(przedstawiciel) && przedstawiciel != "(Wszyscy)")
            {
                kontrahenciBindingSource.DataSource = KontrahenciService.GetKontrahenciByPrzedstawiciel(przedstawiciel).OrderBy(k => k.Kod).ToList();
            }
            else
            {
                kontrahenciBindingSource.DataSource = KontrahenciService.GetKontrahenciQuery().OrderBy(k => k.Kod).ToList();
            }
             */
            string pr = przedstawicieleComboBox.SelectedIndex > 0 ? (string)przedstawicieleComboBox.SelectedItem : null;
            kontrahenciBindingSource.DataSource = CRMService.Kontrahenci.ByPrzedstawiciel(Session, pr);
        }

        private void obliczButton_Click(object sender, EventArgs e)
        {
            /*
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            string przedstawiciel = ((PrzedstawicieleViewRow)przedstawicieleComboBox.SelectedItem).ID == 0 ? null : przedstawicieleComboBox.SelectedValue as string;
            GrupyTowaroweViewRow grupaTow = (GrupyTowaroweViewRow)grupyTowaroweComboBox.SelectedItem;
            string grupa = grupaTow == null ? "" : grupaTow.Nazwa;
            int idKontrahenta = (int)kontrahenciComboBox.SelectedValue;
            DateTime dataOd = dataOdDateTimePicker.Value;
            DateTime dataDo = dataDoDateTimePicker.Value;

            if (idKontrahenta == 0)
            {
                MessageBox.Show("Nie wybrano kontrahenta");
                return;
            }

            string dictionary = FeatureDefsService.GetDictionary("Towary", grupa);
            var wzGuid = new Guid("00000000-0011-0002-0009-000000000000");

            DataSource = new List<RaportFormularzWgGrupViewRow>();

            var dc = Enova.Business.Old.Core.ContextManager.DataContext;

            foreach (var towar in dc.GetTowaryByGrupa(grupa, "RESZTA").OrderBy(t => t.Nazwa).ToList())
            {
                ObjectQuery<Obrot> obrotQuery = null;
                //var obliczObrotyZ = towar.GetTowarObliczObrotyZ(dc);

                //if (obliczObrotyZ == null)
                    obrotQuery = towar.GetObroty(dc, idKontrahenta, dataOd, dataDo);
                //else
                  //  obrotQuery = obliczObrotyZ.GetObroty(dc, idKontrahenta, dataOd, dataDo);

                string dostawca = towar.GetDostawca(dc);

                double? obrotFV = obrotQuery.Where(ob => ob.RozchodDokument.RelationDefinicja.Guid == wzGuid).Sum(ob => ob.IloscValue);
                double? obrotSuma = obrotQuery.Sum(ob => ob.IloscValue);
                int? iloscFV = obrotQuery.GroupBy(ob => ob.RozchodDokument.ID).Count();

                if (obrotFV != null && obrotFV.Value > 0)
                {
                    DataSource.Add(new RaportFormularzWgGrupViewRow()
                    {
                        TowarGuid = towar.Guid,
                        TowarID = towar.ID,
                        Grupa = dostawca,
                        Kod = towar.Kod,
                        Nazwa = towar.Nazwa,
                        IloscFV = iloscFV,
                        ObrotFV = obrotFV,
                        //ObrotSuma = obrotSuma,
                        StandardowaIlosc = towar.StandardowaIlosc
                    });
                }

            }

            pozycjeBindingSource.DataSource = DataSource;

            if (dataGrid.RowCount > 0)
            {
                dataGrid.CurrentCell = dataGrid.Rows[0].Cells[7];
                this.ActiveControl = dataGrid;
            }

            this.Cursor = Cursors.Default;
            this.Enabled = true;
            */

        }

        private void dataGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGrid.Rows.Count)
            {
                var row = dataGrid.Rows[e.RowIndex];
                var r = e.RowIndex % 2;
                if (r > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

        }

        #region Nested types

        [Obsolete("Do usunięcia")]
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
            public System.Collections.IEnumerable ObrotyWZ { get; set; }
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
