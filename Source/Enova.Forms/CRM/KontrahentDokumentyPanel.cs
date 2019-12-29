using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.DataPanel("Dokumenty", typeof(Enova.API.CRM.Kontrahent), typeof(Enova.Forms.CRM.KontrahentDokumentyPanel))]

namespace Enova.Forms.CRM
{
    [BAL.Types.Priority(70)]
    public partial class KontrahentDokumentyPanel : BAL.Forms.DataPanel
    {
        #region Properties

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

        #endregion

        #region Methods

        public KontrahentDokumentyPanel()
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
                this.gridViewControl.DataContext = new Handel.DokumentyView("KontrahentEditDokumentyView", this.Kontrahent);
                
            }
        }

        #endregion 
    }
}
