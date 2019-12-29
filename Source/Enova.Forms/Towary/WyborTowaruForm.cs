using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enova.Forms.Towary
{
    public partial class WyborTowaruForm : Enova.Forms.Towary.TowaryForm
    {
        public WyborTowaruForm()
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
