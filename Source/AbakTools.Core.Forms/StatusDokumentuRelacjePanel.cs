using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Forms;
using AbakTools.Core;

//[assembly: DataPanel("Relacje", typeof(StatusDokumentu), typeof(AbakTools.Core.Forms.StatusDokumentuRelacjePanel))]
[assembly: DataPanel("Relacje", typeof(Enova.Business.Old.DB.Web.StatusDokumentu), typeof(AbakTools.Core.Forms.StatusDokumentuRelacjePanel))]

namespace AbakTools.Core.Forms
{
    [Priority(20), ToolboxItem(false)]
    public partial class StatusDokumentuRelacjePanel : BAL.Forms.DataPanel
    {

        private StatusyDokumentowSelectView selectView;

        private Enova.Business.Old.DB.Web.StatusDokumentu Row
        {
            get { return (Enova.Business.Old.DB.Web.StatusDokumentu)DataContext.GetData(); }
        }

        public StatusDokumentuRelacjePanel()
        {
            InitializeComponent();
        }

        protected override void OnBindingComplete(EventArgs e)
        {
            base.OnBindingComplete(e);
            loadNadrzedne();
            loadPodrzedne();
        }

        private void loadNadrzedne()
        {
            this.nadrzedneBindingSource.DataSource = Row.NadrzedneRelacje.ToList();
        }

        private void loadPodrzedne()
        {
            this.podrzedneBindingSource.DataSource = Row.PodrzedneRelacje.ToList();
        }

        private Enova.Business.Old.DB.Web.StatusDokumentu selectStatus()
        {
            if (selectView == null)
                //selectView = new StatusyDokumentowSelectView(this.DataContext.Session, Row.Kategoria);
                selectView = new StatusyDokumentowSelectView(Row.Kategoria);

            if (FormManager.Instance.ShowGridFormDialog(selectView) == DialogResult.OK)
                return (Enova.Business.Old.DB.Web.StatusDokumentu)selectView.GetData();
            return null;
        }

        private void dodajNadrzednyButton_Click(object sender, EventArgs e)
        {
            var status = this.selectStatus();
            if(status != null)
            {
                nadrzedneBindingSource.Add(Row.DodajNadrzedny(status));
            }
        }

        private void dodajPodrzednyButton_Click(object sender, EventArgs e)
        {
            var status = this.selectStatus();
            if (status != null)
            {
                podrzedneBindingSource.Add(Row.DodajPodrzedny(status));
            }
        }

        private void usunNadrzednyButton_Click(object sender, EventArgs e)
        {
            nadrzedneGridView.Focus();
            var rel = (Enova.Business.Old.DB.Web.RelacjaStatDok)nadrzedneBindingSource.Current;
            if (rel != null)
            {
                if (!FormManager.Confirm("Czy napewno chcesz usunąć relacje?"))
                return;
                nadrzedneBindingSource.Remove(rel);
                ((BAL.Business.Old.IRow)rel).Delete();
            }
        }

        private void usunPodrzednyButton_Click(object sender, EventArgs e)
        {
            podrzedneGridView.Focus();
            var rel = (Enova.Business.Old.DB.Web.RelacjaStatDok)podrzedneBindingSource.Current;
            if (rel != null)
            {
                if (!FormManager.Confirm("Czy napewno chcesz usunąć relacje?"))
                    return;
                podrzedneBindingSource.Remove(rel);
                ((BAL.Business.Old.IRow)rel).Delete();
            }
        }

    }
}
