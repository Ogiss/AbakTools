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
using Enova.Business.Old.DB;
using Enova.Business.Old.Core;

namespace AbakTools.Zamowienia.Forms
{
    public partial class WyborFakturyForm : Form
    {
        public Enova.Business.Old.DB.Web.Zamowienie Zamowienie = null;
        public Enova.Forms.Handel.DokumentHandlowyView Faktura;

        public WyborFakturyForm()
        {


            InitializeComponent();
            

        }

        private void loadData()
        {
            if (Zamowienie != null)
            {
                List<Enova.Forms.Handel.DokumentHandlowyView> ds = new List<Enova.Forms.Handel.DokumentHandlowyView>();
                IEnumerable<Enova.API.CRM.Kontrahent> kontrahenci = null;
                DateTime to = okresDateSpan.DateTo.Date.AddDays(1);

                using (var s = Enova.API.EnovaService.Instance.CreateSession())
                {
                    if (Zamowienie.ZamPrzedstawiciela)
                        kontrahenci = Enova.Forms.Services.CRMService.Kontrahenci.ByPrzedstawiciel(s, Zamowienie.Kontrahent.Kod);
                    else
                        kontrahenci = new Enova.API.CRM.Kontrahent[] { s.GetModule<Enova.API.CRM.CRMModule>().Kontrahenci[Zamowienie.Kontrahent.Guid.Value] };
                    //var km = s.GetModule<Enova.API.Kasa.KasaModule>();
                    var hm = s.GetModule<Enova.API.Handel.HandelModule>();
                    foreach (var k in kontrahenci)
                    {
                        /*
                        var rozrachunki = km.RozrachunkiIdx.WgPodmiot(k).CreateView().SetFilter(
                            string.Format("Data >= '{0}' AND Data < '{1}' AND (Typ = 10 OR Typ = 11)", okresDateSpan.DateFrom.Date.ToShortDateString(), to.ToShortDateString())
                            ).Cast<Enova.API.Kasa.RozrachunekIdx>();
                        foreach (var r in rozrachunki)
                        {
                            var dh = r.Dokument.Dokument as Enova.API.Handel.DokumentHandlowy;
                            if (dh != null && !dh.Korekta)
                                ds.Add(new Enova.Forms.Handel.DokumentHandlowyView(dh));
                        }
                         */
                        var ft = Enova.API.Types.FromTo.Create(okresDateSpan.DateFrom.Date, okresDateSpan.DateTo.Date);
                        var defId = hm.DefDokHandlowych.FakturaSprzedazy.ID;
                        var dokumenty = hm.DokHandlowe.WgKontrahentDataDefinicja(k, null, ft, null, Enova.API.Handel.StanDokumentuHandlowego.Zatwierdzony);
                        foreach(var dh in dokumenty)
                        {
                            if (dh.Definicja.ID == defId)
                                ds.Add(new Enova.Forms.Handel.DokumentHandlowyView(dh));
                        }

                    }
                }

                bindingSource.DataSource = ds.OrderBy(d => d.Data).ThenBy(d => d.NumerPelny);
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentRow != null)
            {
                Faktura = (Enova.Forms.Handel.DokumentHandlowyView)dataGridView.CurrentRow.DataBoundItem;
            }
            this.Close();
        }

        private void okresDateSpan_Changed(object sender, EventArgs e)
        {
            loadData();
        }

        private void WyborFakturyForm_Load(object sender, EventArgs e)
        {
            okresDateSpan.SetSpan(Enova.Business.Old.Controls.DateTimeSpanControl.SpanType.Day);
            loadData();
        }
    }
}
