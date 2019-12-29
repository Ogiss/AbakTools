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
//using Enova.Business.Old.DB.Services;
using Enova.Business.Old.Types;

[assembly: BAL.Forms.MenuAction("Konfiguracja\\Zamówienia", typeof(AbakTools.Business.Forms.KonfiguracjaZamowienForm), MenuAction = BAL.Forms.MenuActionsType.OpenFormModal, Priority = 1120)]

namespace AbakTools.Business.Forms
{
    public partial class KonfiguracjaZamowienForm : Enova.Forms.FormWithEnovaAPI
    {
        public KonfiguracjaZamowienForm()
        {
            InitializeComponent();
        }

        private void KonfiguracjaZamowienForm_Load(object sender, EventArgs e)
        {
            var dc = Enova.Business.Old.Core.ContextManager.WebContext;
            //grupyTowaroweBindingSource.DataSource = FeatureDefsService.GetGrupyTowaroweQuery().OrderBy(g => g.Nazwa).ToList();
            grupyTowaroweBindingSource.DataSource = Enova.Forms.Services.BusinessService.GetGrupyTowarowe(Session).ToList().OrderBy(r => r.Name);
            int? defaultGroup = dc.GetConfigInt("DEFAULT_PRODUCT_GROUP");
            DateTime? defaultFrom = dc.GetConfigDate("DEFAULT_SPAN_FROM");
            DateTime? defaultTo = dc.GetConfigDate("DEFAULT_SPAN_TO");
            DateTime? zwrotyFrom = dc.GetConfigDate("DEFAULT_RETURN_SPAN_FROM");
            DateTime? zwrotyTo = dc.GetConfigDate("DEFAULT_RETURN_SPAN_TO");
            bool? sp2aktywna = dc.GetConfigBool("SP2_ACTIVE");
            bool? sp2fromOrder = dc.GetConfigBool("SP2_FROM_ORDER");
            DateTime? sp2From = dc.GetConfigDate("DEFAULT_SP2_FROM");
            DateTime? sp2To = dc.GetConfigDate("DEFAULT_SP2_TO");

            bool? preview = dc.GetConfigBool("PREVIEW_ORDER", Enova.Business.Old.DB.Web.User.LoginedUser);
            

            if (defaultGroup != null)
                domyslnaGrupaComboBox.SelectedValue = defaultGroup.Value;

            if (defaultFrom != null)
                domyslnyOkrOddDatePicker.Value = defaultFrom.Value;

            if (defaultTo != null)
                domyslnyOkrDoDatePicker.Value = defaultTo.Value;

            if (zwrotyFrom != null)
                dtpZwrotyOd.Value = zwrotyFrom.Value;

            if (zwrotyTo != null)
                dtpZwrotyDo.Value = zwrotyTo.Value;

            sp2AktywnaCheckBox.Checked = sp2aktywna != null && sp2aktywna.Value ? true : false;
            sp2FromOrderCheckBox.Checked = sp2fromOrder != null && sp2fromOrder.Value ? true : false;
            sp2OdDtp.Value = sp2From != null ? sp2From.Value : (defaultFrom != null ? defaultFrom.Value.AddYears(1) : DateTime.Now.Date);
            sp2DoDtp.Value = sp2To != null ? sp2To.Value : (defaultTo != null ? defaultTo.Value.AddYears(1) : DateTime.Now.Date);

            if (preview != null)
                previewCheckBox.Checked = preview.Value;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var dc = Enova.Business.Old.Core.ContextManager.WebContext;
            if (domyslnaGrupaComboBox.SelectedValue != null)
            {
                //var grupa = (GrupyTowaroweViewRow)domyslnaGrupaComboBox.SelectedItem;
                var grupa = (Enova.API.Business.FeatureDefinition)domyslnaGrupaComboBox.SelectedItem;
                dc.SetConfigString("DEFAULT_PRODUCT_GROUP", ((int)domyslnaGrupaComboBox.SelectedValue).ToString());
                dc.SetConfigString("DEFAULT_PRODUCT_GROUP_GUID", grupa.Guid.ToString());
            }

            dc.SetConfigString("DEFAULT_SPAN_FROM", domyslnyOkrOddDatePicker.Value.ToShortDateString());
            dc.SetConfigString("DEFAULT_SPAN_TO", domyslnyOkrDoDatePicker.Value.ToShortDateString());
            dc.SetConfigString("DEFAULT_RETURN_SPAN_FROM", dtpZwrotyOd.Value.ToShortDateString());
            dc.SetConfigString("DEFAULT_RETURN_SPAN_TO", dtpZwrotyDo.Value.ToShortDateString());
            dc.SetConfigString("SP2_ACTIVE", sp2AktywnaCheckBox.Checked.ToString());
            dc.SetConfigString("SP2_FROM_ORDER", sp2FromOrderCheckBox.Checked.ToString());
            dc.SetConfigString("DEFAULT_SP2_FROM", sp2OdDtp.Value.ToShortDateString());
            dc.SetConfigString("DEFAULT_SP2_TO", sp2DoDtp.Value.ToShortDateString());
            dc.SetConfigString("PREVIEW_ORDER", previewCheckBox.Checked.ToString(),
                Enova.Business.Old.DB.Web.User.LoginedUser);
            dc.SaveChanges();
        }
    }
}
