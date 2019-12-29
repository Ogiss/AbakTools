using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Towary.Forms
{
    public partial class WyborTowaruEnovaForm : AbakTools.Towary.Forms.TowaryEnovaForm
    {
        public WyborTowaruEnovaForm()
        {
            InitializeComponent();
        }

        public override bool SelectMode
        {
            get
            {
                return true;
            }
            set
            {
                base.SelectMode = value;
            }
        }

        public override bool HideOnSelect
        {
            get
            {
                return true;
            }
        }
    }
}
