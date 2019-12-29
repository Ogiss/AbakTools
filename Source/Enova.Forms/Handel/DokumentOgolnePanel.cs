using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.DataPanel("Ogólne", typeof(Enova.API.Handel.DokumentHandlowy), typeof(Enova.Forms.Handel.DokumentOgolnePanel))]

namespace Enova.Forms.Handel
{
    [BAL.Types.Priority(10)]
    public partial class DokumentOgolnePanel : BAL.Forms.DataPanel
    {
        public DokumentOgolnePanel()
        {
            InitializeComponent();
        }

        protected override void OnBindingComplete(EventArgs e)
        {
            base.OnBindingComplete(e);
            if (this.DataContext != null)
            {
                var dok = this.DataContext.GetData() as Enova.API.Handel.DokumentHandlowy;
                if (dok != null && dok.Kontrahent != null)
                    this.kontrahentTextBox.Text = "(" + dok.Kontrahent.Kod + ") - " + dok.Kontrahent.Nazwa;
                this.pozycjeBindingSource.DataSource = dok.Pozycje;
                this.initPozycjeGrid();
            }
        }

        private void initPozycjeGrid()
        {
            Enova.API.Handel.DokumentHandlowy dokument = this.DataContext.GetData() as Enova.API.Handel.DokumentHandlowy;
            if (dokument != null)
            {
                if (!dokument.Korekta)
                {
                    IloscOrgColumn.Visible = false;
                    CenaOrgColumn.Visible = false;
                    RabatOrgColumn.Visible = false;
                }
            }
            if (typeof(DokHandlowyContext).IsAssignableFrom(this.DataContext.GetType()))
            {
                ((DokHandlowyContext)this.DataContext).InitPozycjeDokGrid(this.pozycjeDokHanGrid);
            }
        }
    }
}
