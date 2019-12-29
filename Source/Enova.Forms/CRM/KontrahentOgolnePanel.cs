using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.DataPanel("Ogólne", typeof(Enova.API.CRM.Kontrahent), typeof(Enova.Forms.CRM.KontrahentOgolnePanel))]

namespace Enova.Forms.CRM
{
    [BAL.Types.Priority(10)]
    public partial class KontrahentOgolnePanel : BAL.Forms.DataPanel
    {
        public KontrahentOgolnePanel()
        {
            InitializeComponent();
        }

        protected override void OnBindingComplete(EventArgs e)
        {
            var kontrahent = this.DataContext.GetData<Enova.API.CRM.Kontrahent>();
            if (kontrahent != null)
            {
                this.adresBindingSource.DataSource = kontrahent.Adres;
                this.adresDoKorBindingSource.DataSource = kontrahent.AdresDoKorespondencji;
            }
            base.OnBindingComplete(e);
        }

        private void KontrahentOgolnePanel_Load(object sender, EventArgs e)
        {
            if ((Enova.Business.Old.DB.Web.Operator.CurrentOperator.PrawaDostepu & AbakTools.Business.OperatorPrawaDostepu.Administrator) != AbakTools.Business.OperatorPrawaDostepu.Administrator)
            {
                blokadaCheckBox.Enabled = false;
                blokadaSprzedażyCheckBox.Enabled = false;
            }
            else
            {
                blokadaCheckBox.Enabled = true;
                blokadaSprzedażyCheckBox.Enabled = true;
            }
        }

        private void blokadaSprzedażyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
