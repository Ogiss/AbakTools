using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.DataPanel("Rozrachunki", typeof(Enova.API.CRM.Kontrahent), typeof(Enova.Forms.CRM.KontrahentRozrachunkiPanel))]

namespace Enova.Forms.CRM
{
    [BAL.Types.Priority(40)]
    public partial class KontrahentRozrachunkiPanel : BAL.Forms.DataPanel
    {
        private bool isLoaded;

        public API.CRM.Kontrahent Kontrahent
        {
            get
            {
                if (DataContext != null)
                    return DataContext.Current as API.CRM.Kontrahent;
                return null;
            }
        }

        public KontrahentRozrachunkiPanel()
        {
            InitializeComponent();
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (!isLoaded)
            {
                isLoaded = true;
                loadData();
            }
        }

        private void loadData()
        {
            if (this.Kontrahent != null)
            {
                this.gridViewControl.DataContext = new Kasa.RozrachunkiView("KontrahentEditRozrachunkiView", this.Kontrahent);

            }
        }


        /*
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (!isLoaded)
            {
                filterIndexComboBox.SelectedIndex = 1;
                loadRozrachunki();
            }
        }

        private void loadRozrachunki()
        {
            if (Kontrahent != null)
            {
                using (new BAL.Forms.WaitCursor(this))
                {
                    isLoaded = true;
                    var km = Kontrahent.Session.GetModule<API.Kasa.IKasaModule>();
                    var fromTo = new API.Types.FromTo()
                    {
                        From = DateTime.MinValue,
                        To = DateTime.Now.Date
                    };

                    if (filterIndexComboBox.SelectedIndex == 1)
                    {
                        rozrachunkiBindingSource.DataSource = km.RozrachunkiIdx.Nierozliczone(Kontrahent, fromTo, DateTime.Now.Date);
                    }
                    else if (filterIndexComboBox.SelectedIndex == 2)
                    {
                        rozrachunkiBindingSource.DataSource = km.RozrachunkiIdx.Rozliczone(Kontrahent, fromTo, DateTime.Now.Date);
                    }
                    else
                    {
                        rozrachunkiBindingSource.DataSource = km.RozrachunkiIdx.Wszystkie(Kontrahent, fromTo);
                    }

                    decimal sumaKwota = 0;
                    decimal sumaKwotaRozliczona = 0;

                    foreach (DataGridViewRow row in rozrachunkiDataGrid.Rows)
                    {
                        var r = row.DataBoundItem as API.Kasa.IRozrachunekIdx;
                        if (r != null)
                        {
                            sumaKwota += r.Kwota.Value;
                            sumaKwotaRozliczona += r.KwotaRozliczona.Value;
                        }
                    }

                    sumaKwotaTextBox.Text = string.Format("{0:C}", sumaKwota);
                    sumaKwotaRozliczonaTextBox.Text = string.Format("{0:C}", sumaKwotaRozliczona);
                    sumaPozostałoTextBox.Text = string.Format("{0:C}", sumaKwota - sumaKwotaRozliczona);
                }
            }
        }

        private void filterIndexComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRozrachunki();
        }
         */

    }
}
