using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

[assembly: BAL.Forms.MenuAction("Magazyn\\Mapowanie towarów", typeof(AbakTools.Towary.Forms.MapowanieTowarowForm), Priority = 830)]

namespace AbakTools.Towary.Forms
{
    public partial class MapowanieTowarowForm : Enova.Business.Old.Forms.DataGridForm
    {
        public MapowanieTowarowForm()
        {
            InitializeComponent();
        }

        protected override void LoadData()
        {
            this.DataSource = new Enova.Business.Old.Web.MapowanieTowarowTable();
        }
    }
}
