using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Objects;

namespace Enova.Forms.Controls
{
    public partial class PodmiotSelect : BALControlWithEnovaAPI
    {
        public PodmiotSelect()
        {
            InitializeComponent();
        }

        public Enova.API.Core.TypPodmiotu TypPodmiotu
        {
            get
            {
                if (podmiotTypeComboBox.SelectedIndex != -1)
                    return (Enova.API.Core.TypPodmiotu)podmiotTypeComboBox.SelectedIndex;
                return API.Core.TypPodmiotu.NieOkreślony;
            }
            set
            {
                if (podmiotComboBox.Items.Count > 0)
                    podmiotComboBox.SelectedIndex = (int)value;
            }
        }

        public API.Core.IPodmiot Podmiot
        {
            get
            {
                return (API.Core.IPodmiot)podmiotComboBox.SelectedItem;
            }
        }

        private void PodmiotSelectControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                podmiotTypeComboBox.Items.Add("Nie określony");
                podmiotTypeComboBox.Items.Add("Kontrahent");
                podmiotTypeComboBox.Items.Add("Bank");
                podmiotTypeComboBox.Items.Add("Urząd Skarbowy");
                podmiotTypeComboBox.Items.Add("ZUS");
                podmiotTypeComboBox.Items.Add("Pracownik");
                podmiotTypeComboBox.Items.Add("Urząd celny");
                podmiotTypeComboBox.Items.Add("PFROM");
                podmiotTypeComboBox.SelectedIndex = 0;
                loadData();
            }
        }

        private void loadData()
        {
            if (!this.DesignMode && Session!=null)
            {
                API.Business.View view = null;
                switch (TypPodmiotu)
                {
                    case API.Core.TypPodmiotu.Kontrahent:
                        view = Session.GetModule<API.CRM.CRMModule>().Kontrahenci.CreateView().SetFilter("Blokada = false");
                        break;
                    case API.Core.TypPodmiotu.Pracownik:
                        view = Session.GetModule<API.Kadry.KadryModule>().Pracownicy.CreateView();
                        break;
                    case API.Core.TypPodmiotu.Bank:
                        view = Session.GetModule<API.CRM.CRMModule>().Banki.CreateView();
                        break;
                    case API.Core.TypPodmiotu.UrzadSkarbowy:
                        view = Session.GetModule<API.CRM.CRMModule>().UrzedySkarbowe.CreateView();
                        break;
                    case API.Core.TypPodmiotu.ZUS:
                        view = Session.GetModule<API.CRM.CRMModule>().ZUSY.CreateView();
                        break;
                }
                if (view != null)
                {
                    var dc = view.Cast<API.Core.IPodmiot>().OrderBy(r=>r.Kod).ToList();
                    if (dc.Count > 0)
                        podmiotBindingSource.DataSource = dc;
                }
                else
                    podmiotBindingSource.Clear();

            }
        }

        private void podmiotTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!this.DesignMode)
                loadData();
        }
    }
}
