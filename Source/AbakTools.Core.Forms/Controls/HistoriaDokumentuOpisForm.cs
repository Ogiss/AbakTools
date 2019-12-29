using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Core.Forms.Controls
{
    public partial class HistoriaDokumentuOpisForm : Form
    {

        public string Opis
        {
            get { return opisTextBox.Text; }
            set { opisTextBox.Text = value; }
        }

        public HistoriaDokumentuOpisForm()
        {
            InitializeComponent();
        }

        private void HistoriaDokumentuOpisForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
                this.Close();
        }
    }
}
