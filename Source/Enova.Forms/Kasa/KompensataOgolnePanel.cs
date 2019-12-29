using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Business;
using BAL.Forms;

//[assembly: DataPanel("Ogólne", typeof(Enova.API.Kasa.IKompensata), typeof(Enova.Forms.Kasa.KompensataOgolnePanel))]
[assembly: DataPanel("Ogólne", typeof(Enova.API.Kasa.Kompensata), typeof(Enova.Forms.Kasa.KompensataOgolnePanel))]

namespace Enova.Forms.Kasa
{
    public partial class KompensataOgolnePanel : BAL.Forms.DataPanel
    {
        public KompensataOgolnePanel()
        {
            InitializeComponent();
        }

        /*
        protected override void OnBindingComplete(EventArgs e)
        {
            base.OnBindingComplete(e);
            if (this.DataContext != null && this.DataContext.Current != null)
            {
                gridViewControl.DataContext = new PozycjeKompensatyView((API.Kasa.IKompensata)this.DataContext.Current);
            }
        }
         */

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.DataContext != null && this.DataContext.Current != null)
            {
                //gridViewControl.DataContext = new PozycjeKompensatyView((API.Kasa.IKompensata)this.DataContext.Current);
                gridViewControl.DataContext = new PozycjeKompensatyView((API.Kasa.Kompensata)this.DataContext.Current);
            }

        }
    }
}
