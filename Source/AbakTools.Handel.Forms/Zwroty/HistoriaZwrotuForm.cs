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
    public partial class HistoriaZwrotuForm : Form
    {
        public Enova.Business.Old.DB.Web.Zwrot Zwrot;

        public HistoriaZwrotuForm()
        {
            InitializeComponent();
        }

        private void HistoriaZwrotuForm_Load(object sender, EventArgs e)
        {
            if(this.Zwrot != null){
                this.bindingSource.DataSource = this.Zwrot.HistoriaZwrotu
                    .Where(h => h.Deleted == false && h.Synchronize != (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedDelete).OrderBy(h => h.Data).ToList();
            }
        }
    }
}
