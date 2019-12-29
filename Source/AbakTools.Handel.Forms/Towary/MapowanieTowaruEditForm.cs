using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace AbakTools.Towary.Forms
{
    public partial class MapowanieTowaruEditForm : Enova.Business.Old.Forms.DataEditForm
    {
        public MapowanieTowaruEditForm()
        {
            InitializeComponent();

            srcSelectTextBox.SelectFormType = typeof(AbakTools.Towary.Forms.WyborTowaruEnovaForm);
            dstSelectTextBox.SelectFormType = typeof(AbakTools.Towary.Forms.WyborTowaruEnovaForm);

            srcSelectTextBox.SelectionChanging+=new EventHandler<Enova.Business.Old.Controls.SelectTextBox.SelectionChangingEventArgs>(srcSelectTextBox_SelectionChanging);
        }

        private void MapowanieTowaruEditForm_Load(object sender, EventArgs e)
        {
            if (this.DataSource != null)
            {
                Enova.Business.Old.Types.MapowanieTowaruRow mt = (Enova.Business.Old.Types.MapowanieTowaruRow)this.DataSource;

                if (mt.Src!= null && mt.Dst != null)
                {

                    srcSelectTextBox.SelectedItem = mt.Src;
                    dstSelectTextBox.SelectedItem = mt.Dst;

                    srcSelectTextBox.Enabled = false;
                }
            }
        }

        private void srcSelectTextBox_SelectionChanging(object sender, Enova.Business.Old.Controls.SelectTextBox.SelectionChangingEventArgs e)
        {
            Enova.Business.Old.Types.MapowanieTowaruRow mt = (Enova.Business.Old.Types.MapowanieTowaruRow)this.DataSource;
            var sel = (Enova.Business.Old.DB.Web.Produkt)e.Value;
            if (sel != null && Enova.Business.Old.Core.ContextManager.WebContext.GuidMaps.Any(r => r.Zrodlo == sel.EnovaGuid && r.Tabela == "Towary"))
            {
                e.Cancel = true;
                MessageBox.Show(string.Format("Istnieje już mapowanie dla towaru {0}", sel.FullName), "AbakTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnBeforeSaveChanges(EventArgs e)
        {
            base.OnBeforeSaveChanges(e);

            Enova.Business.Old.Types.MapowanieTowaruRow mt = (Enova.Business.Old.Types.MapowanieTowaruRow)this.DataSource;

            if (srcSelectTextBox.SelectedItem != null && dstSelectTextBox.SelectedItem != null)
            {
                /*
                var st = ((Enova.Business.Old.DB.TowarRow)srcSelectTextBox.SelectedItem).GetTowar();
                var dt = ((Enova.Business.Old.DB.TowarRow)dstSelectTextBox.SelectedItem).GetTowar();
                 */

                var st = (Enova.Business.Old.DB.Web.Produkt)srcSelectTextBox.SelectedItem;
                var dt = (Enova.Business.Old.DB.Web.Produkt)dstSelectTextBox.SelectedItem;

                if (mt.SrcGuid != st.EnovaGuid.Value)
                    mt.Src = st;

                if (mt.DstGuid != dt.EnovaGuid.Value)
                    mt.Dst = dt;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {

        }
    }
}
