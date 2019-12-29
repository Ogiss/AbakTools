using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Core.Forms
{
    public partial class WyborStatusuForm : Form
    {
        private Enova.Business.Old.DB.Web.StatusDokumentu wybranyStatus;

        public Enova.Business.Old.DB.Web.StatusDokumentu WybranyStatus
        {
            get { return this.wybranyStatus; }
        }

        public IList<Enova.Business.Old.DB.Web.StatusDokumentu> Statusy
        {
            get { return (IList<Enova.Business.Old.DB.Web.StatusDokumentu>)this.bindingSource.DataSource; }
            set { this.bindingSource.DataSource = value; }
        }

        public WyborStatusuForm()
        {
            InitializeComponent();
        }

        private void wybierzStatus()
        {
            if (this.gridView.CurrentRow != null)
            {
                this.wybranyStatus = (Enova.Business.Old.DB.Web.StatusDokumentu)this.gridView.CurrentRow.DataBoundItem;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void gridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.wybierzStatus();
        }
    }
}
