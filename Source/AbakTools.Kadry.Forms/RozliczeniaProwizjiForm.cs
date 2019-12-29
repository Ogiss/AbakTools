using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB;
using Enova.Business.Old.Types;

namespace AbakTools.Kadry.Forms
{
    public partial class RozliczeniaProwizjiForm : Form
    {
        public Pracownik Pracownik = null;
        public DateTime? DataOd = null;
        public DateTime? DataDo = null;

        public RozliczeniaProwizjiForm()
        {
            InitializeComponent();
        }

        private void RozliczeniaProwizjiForm_Load(object sender, EventArgs e)
        {
            if (Pracownik != null && DataOd != null && DataDo != null)
            {
                kodTextBox.Text = Pracownik.Kod;
                okresDateSpan.SetSpan(Enova.Business.Old.Controls.DateTimeSpanControl.SpanType.Month, DataOd.Value.Year, DataOd.Value.Month, 1);
            }
            loadData();
        }

        private void loadData()
        {
            if (Pracownik != null && DataOd != null && DataDo != null)
            {
                string tmp = "";
                if (kwCheckBox.Checked)
                    tmp += "KW;";
                if (kpCheckBox.Checked)
                    tmp += "KP;";
                if (kzTheckBox.Checked)
                    tmp += "KZ;";
                if (krCheckBox.Checked)
                    tmp += "KR;";
                if (prCheckBox.Checked)
                    tmp += "PR;";
                string[] symbols = tmp.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (symbols.Length > 0)
                {
                    EnovaContext ec = Enova.Business.Old.Core.ContextManager.DataContext;
                    DateTime dataOd = okresDateSpan.DateFrom.Date;
                    DateTime dataDo = okresDateSpan.DateTo.Date.AddDays(1).AddMilliseconds(-1);

                    var ds = (from z in ec.Zaplaty
                              join p in ec.Pracownicy on new { Podmiot = z.Podmiot.Value, PodmiotType = z.PodmiotType } equals new { Podmiot = p.ID, PodmiotType = "Pracownicy" }
                              join dk in ec.DokumentyKasowe on new { Dokument = z.Dokument.Value, DokumentType = z.DokumentType } equals new { Dokument = dk.ID, DokumentType = "DokumentyKasowe" }
                              where p.ID == Pracownik.ID && z.DataDokumentu >= dataOd && z.DataDokumentu <= dataDo && symbols.Contains(dk.DefDokumentow.Symbol)
                              select new ZaplatyView()
                              {
                                  KodPodmiotu = p.Kod,
                                  Data = z.DataDokumentu,
                                  Numer = z.NumerDokumentu,
                                  Opis = z.Opis,
                                  KwotaValue = z.KwotaValue.Value,
                                  Kierunek = z.Kierunek.Value
                              }).ToList();

                    bindingSource.DataSource = ds.OrderBy(d => d.Data);

                    sumaTextBox.Text = string.Format("{0:C}", ds.Sum(d => d.Kwota));
                    
                }
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void okresDateSpan_Changed(object sender, EventArgs e)
        {
            loadData();
        }

        #region Nested types

        public class ZaplatyView
        {
            public string KodPodmiotu { get; set; }
            public DateTime Data { get; set; }
            public string Numer { get; set; }
            public decimal KwotaValue { get; set; }
            public int Kierunek { get; set; }
            public string Opis { get; set; }
            public decimal? Kwota
            {
                get { return Kierunek == 1 ? KwotaValue : -KwotaValue; }
            }
        }

        #endregion

    }
}
