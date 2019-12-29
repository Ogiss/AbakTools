using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Zwroty.Forms
{
    public partial class WyborTowaruForm : Form
    {
        private Enova.Business.Old.DB.Towar wybranyTowar;

        public List<Enova.Business.Old.DB.Towar> Towary
        {
            get
            {
                return (List<Enova.Business.Old.DB.Towar>)this.bindingSource.DataSource;
            }
            set
            {
                this.bindingSource.DataSource = value;
            }
        }

        public Enova.Business.Old.DB.Towar WybranyTowar
        {
            get { return this.wybranyTowar; }
        }

        public WyborTowaruForm()
        {
            InitializeComponent();
        }

        private void towaryGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.towaryGrid.CurrentRow != null)
            {
                this.wybranyTowar = (Enova.Business.Old.DB.Towar)this.towaryGrid.CurrentRow.DataBoundItem;
                this.Close();
            }
        }

        private void towaryGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = false;
                this.towaryGrid_CellDoubleClick(sender, null);
            }
        }
    }
}
