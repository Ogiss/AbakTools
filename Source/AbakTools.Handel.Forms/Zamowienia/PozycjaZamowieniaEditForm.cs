using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Enova.Business.Old.DB.Web;

namespace EnovaTools.Forms.Web
{
    public partial class PozycjaZamowieniaEditForm : Enova.Business.Old.Forms.DataEditForm
    {

        public PozycjaZamowienia PozycjaZamowienia
        {
            get { return (PozycjaZamowienia)this.DataSource; }
            set { this.DataSource = value; }
        }

        public PozycjaZamowieniaEditForm()
        {
            InitializeComponent();
        }

        private void PozycjaZamowieniaEditForm_Load(object sender, EventArgs e)
        {
            if (PozycjaZamowienia != null)
            {
                if (PozycjaZamowienia.Produkt != null)
                {
                    if (PozycjaZamowienia.Produkt.AtrybutyProduktu.Count > 0)
                    {
                        atrybutLabel.Enabled  = true;
                        atrybutComboBox.Enabled = true;

                        atrybutLabel.Text = PozycjaZamowienia.Produkt.GrupaAtrybutow.NazwaPubliczna;
                        atrybutyBindingSource.DataSource = PozycjaZamowienia.Produkt.AtrybutyProduktu;

                        if (PozycjaZamowienia.AtrybutProduktu != null)
                            atrybutComboBox.SelectedItem = PozycjaZamowienia.AtrybutProduktu;
                    }
                    else
                    {
                        atrybutLabel.Text = "Atrybut:";
                        atrybutLabel.Enabled = false;
                        atrybutComboBox.Enabled = false;

                        atrybutyBindingSource.DataSource = null;

                        
                    }
                }
            }
        }

        private void PozycjaZamowieniaEditForm_Shown(object sender, EventArgs e)
        {
            if (atrybutComboBox.Enabled)
                atrybutComboBox.Focus();
            else
                iloscTextBox.Focus();
        }

        private void atrybutComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PozycjaZamowienia.AtrybutProduktu = (AtrybutProduktu)atrybutComboBox.SelectedItem;
        }

        private void atrybutComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
                iloscTextBox.Focus();
        }

        private void iloscTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
            {
                this.okButton.Focus();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
