using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AbakTools.Core;

namespace AbakTools.Core.Controls
{
    public partial class HistoriaDokumentuListForm : Form
    {
        /*
        private DokumentZHistoria dokument;

        public DokumentZHistoria Dokument
        {
            get { return this.dokument; }
            set
            {
                this.dokument = value;
                this.bindingSource.DataSource = dokument.Historia.OrderBy(r=>r.Data).ToList();
            }
        }
         */

        private Enova.Business.Old.DB.Web.IDokumentZHistoria dokument;

        public Enova.Business.Old.DB.Web.IDokumentZHistoria Dokument
        {
            get { return this.dokument; }
            set
            {
                this.dokument = value;
                this.bindingSource.DataSource = dokument.Historia.OrderBy(r => r.Data).ToList();
            }
        }

        public HistoriaDokumentuListForm()
        {
            InitializeComponent();
        }
    }
}
