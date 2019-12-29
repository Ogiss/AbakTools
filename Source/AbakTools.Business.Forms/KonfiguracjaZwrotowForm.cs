using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Forms.Services;

[assembly: BAL.Forms.MenuAction("Konfiguracja\\Zwroty", typeof(AbakTools.Business.Forms.KonfiguracjaZwrotowForm), MenuAction = BAL.Forms.MenuActionsType.OpenFormModal, Priority = 1130)]

namespace AbakTools.Business.Forms
{
    public partial class KonfiguracjaZwrotowForm : Enova.Forms.FormWithEnovaAPI
    {
        public KonfiguracjaZwrotowForm()
        {
            InitializeComponent();
        }

        private void KonfiguracjaZwrotowForm_Load(object sender, EventArgs e)
        {
            grupyTowaroweBindingSource.DataSource = BusinessService.GetGrupyTowarowe(Session).OrderBy(r => r.Name).ToList();
            int? defaultGroup = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigInt("ZWROT_DEFAULT_PRODUCT_GROUP");
            int? defaultStatusDopPrzj = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigInt("ZWROT_STATUS_DOPIERO_PRZYJEDZIE");
            DateTime? defaultFrom = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("ZWROT_DEFAULT_SPAN_FROM");
            DateTime? defaultTo = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigDate("ZWROT_DEFAULT_SPAN_TO");



            if (defaultGroup != null)
                domyslnaGrupaComboBox.SelectedValue = defaultGroup.Value;

            if (defaultStatusDopPrzj != null)
                tbStatusDopieroPrzyjedzie.Text = defaultStatusDopPrzj.Value.ToString();

            if (defaultFrom != null)
                domyslnyOkrOddDatePicker.Value = defaultFrom.Value;

            if (defaultTo != null)
            {
                domyslnyOkrDoDatePicker.Value = defaultTo.Value;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (domyslnaGrupaComboBox.SelectedValue != null)
            {
                Enova.Business.Old.Core.ContextManager.WebContext.SetConfigString("ZWROT_DEFAULT_PRODUCT_GROUP", ((int)domyslnaGrupaComboBox.SelectedValue).ToString());
            }
            int i = 5;
            if (!string.IsNullOrEmpty(tbStatusDopieroPrzyjedzie.Text) && int.TryParse(tbStatusDopieroPrzyjedzie.Text, out i))
                Enova.Business.Old.Core.ContextManager.WebContext.SetConfigString("ZWROT_STATUS_DOPIERO_PRZYJEDZIE", i.ToString());

            Enova.Business.Old.Core.ContextManager.WebContext.SetConfigString("ZWROT_DEFAULT_SPAN_FROM", domyslnyOkrOddDatePicker.Value.ToShortDateString());
            Enova.Business.Old.Core.ContextManager.WebContext.SetConfigString("ZWROT_DEFAULT_SPAN_TO", domyslnyOkrDoDatePicker.Value.ToShortDateString());
            Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();

        }
    }
}
