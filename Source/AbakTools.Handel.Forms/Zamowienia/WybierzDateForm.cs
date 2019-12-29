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
    public partial class WybierzDateForm : Form
    {

        public DateTime Data
        {
            get { return dateTimePicker.Value.Date; }
        }

        public string PoraDnia
        {
            get
            {
                if (radioButton1.Checked)
                    return "R";
                else if (radioButton2.Checked)
                    return "P";
                else
                    return "W";
            }
        }

        public bool NoweZamowienie
        {
            get { return checkBox1.Checked; }
        }

        public bool DoMagazynu
        {
            get { return checkBox2.Checked; }
        }

        public bool Blokada
        {
            get { return checkBox3.Checked; }
        }

        public bool Wstrzymane
        {
            get { return checkBox4.Checked; }
        }

        public bool Pakowane
        {
            get { return checkBox5.Checked; }
        }

        public bool Spakowane
        {
            get { return checkBox6.Checked; }
        }

        public bool Kurier
        {
            get { return checkBox7.Checked; }
        }

        public bool Przedstawiciel
        {
            get { return checkBox8.Checked; }
        }

        public bool Wyslane
        {
            get { return checkBox9.Checked; }
        }
        public Enova.Business.Old.DB.Web.Dostawca Dostawca
        {
            get { return (Enova.Business.Old.DB.Web.Dostawca)this.dostawcaComboBox.SelectedItem; }
        }

        public Enova.Business.Old.DB.Web.Kontrahent PrzedstawicielKontrahenta
        {
            get { return (Enova.Business.Old.DB.Web.Kontrahent)this.przedstawicielComboBox.SelectedItem; }
        }

        public WybierzDateForm()
        {
            InitializeComponent();
        }

        private void WybierzDateForm_Load(object sender, EventArgs e)
        {
            dateTimePicker.Value = DateTime.Now.Date;
            loadDostawcy();
            loadPrzedstawiciele();
        }

        private void loadDostawcy()
        {
            var ds = new List<Enova.Business.Old.DB.Web.Dostawca>() { new Enova.Business.Old.DB.Web.Dostawca() { ID = 0, Nazwa = "(Wszyscy)" } };
            ds.AddRange(Enova.Business.Old.Core.ContextManager.WebContext.Dostawcy.OrderBy(d => d.Nazwa).ToList());

            dostawcyBindingSource.DataSource = ds;
            dostawcaComboBox.SelectedIndex = 0;
        }

        private void loadPrzedstawiciele()
        {
            var ds = new List<Enova.Business.Old.DB.Web.Kontrahent>() { new Enova.Business.Old.DB.Web.Kontrahent() { ID = 0, Kod = "(Wszyscy)" } };
            ds.AddRange(Enova.Business.Old.Core.ContextManager.WebContext.Kontrahenci.Where(k => k.CzyAgent == true).OrderBy(k => k.Kod).ToList());

            przedstawicielBindingSource.DataSource = ds;
            przedstawicielComboBox.SelectedIndex = 0;
        }

        private void okButton_Click(object sender, EventArgs e)
        {

        }
    }
}
