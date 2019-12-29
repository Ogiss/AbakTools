using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Zamowienia.Forms
{
    public partial class OpisPozycjiForm : Form
    {

        public string Opis
        {
            get { return opisTextBox.Text; }
            set { opisTextBox.Text = value; }
        }

        public OpisPozycjiForm()
        {
            InitializeComponent();
        }
    }
}
