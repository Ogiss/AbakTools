using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.DataPanel("Rozliczenia", typeof(Enova.API.CRM.Kontrahent),typeof(Enova.Forms.CRM.KontrahentRozliczeniaPanel))]

namespace Enova.Forms.CRM
{
    [BAL.Types.Priority(50)]
    public partial class KontrahentRozliczeniaPanel : BAL.Forms.DataPanel
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

        public KontrahentRozliczeniaPanel()
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
            if (Kontrahent != null)
            {
                var km = Kontrahent.Session.GetModule<API.Kasa.KasaModule>();
                //var fromTo = new Enova.API.Types.FromToOld() { From = dateFromToControl.FromTo.From, To = dateFromToControl.FromTo.To };
                var fromTo = Enova.API.Types.FromTo.Create(dateFromToControl.FromTo.From, dateFromToControl.FromTo.To);
                rozliczeniaBindingSource.DataSource = km.RozliczeniaSP[Kontrahent, fromTo].ToList();
            }
        }

        private void dateFromToControl_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
