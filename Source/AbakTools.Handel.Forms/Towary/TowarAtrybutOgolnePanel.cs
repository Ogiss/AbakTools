using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.DataPanel("Ogólne", typeof(Enova.Business.Old.DB.Web.TowarAtrybut), typeof(AbakTools.Towary.Forms.TowarAtrybutOgolnePanel))]

namespace AbakTools.Towary.Forms
{
    public partial class TowarAtrybutOgolnePanel : BAL.Forms.DataPanel
    {
        public TowarAtrybutOgolnePanel()
        {
            InitializeComponent();
        }
    }
}
