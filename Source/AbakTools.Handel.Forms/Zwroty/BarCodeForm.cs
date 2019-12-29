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
    public partial class BarCodeForm : Form
    {
        public string BarCode
        {
            get { return this.barcodeTextBox.Text; }
        }

        public BarCodeForm()
        {
            InitializeComponent();
        }

        private void BarCodeForm_Load(object sender, EventArgs e)
        {
            this.barcodeTextBox.Text = "";
            this.ActiveControl = this.barcodeTextBox;
        }

        private void BarCodeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift && e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.barcodeTextBox.Text))
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                else
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

            }
        }
    }
}
