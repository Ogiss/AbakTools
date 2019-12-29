using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.Zwroty;

namespace AbakTools.Zwroty.Forms
{
    public partial class AnalizaZwrotuEditForm : Form
    {
        #region Fields

        private AnalizaZwrotu analizaZwrotu;

        #endregion

        #region Methods

        public AnalizaZwrotuEditForm()
        {
            InitializeComponent();
        }

        public AnalizaZwrotuEditForm(AnalizaZwrotu analizaZwrotu)
        {
            this.analizaZwrotu = analizaZwrotu;
            this.InitializeComponent();
            //this.pozycjeAnalizyZwrotuGrid.SetDokumenty(this.analizaZwrotu.Dokumenty);
            this.bindingSource.DataSource = this.analizaZwrotu;
            //this.pozycjeBindingSource.DataSource = this.analizaZwrotu.Pozycje;
            this.loadPozycje();
        }

        private void AnalizaZwrotuEditForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.pozycjeAnalizyZwrotuGrid;
        }

        private void analizujButton_Click(object sender, EventArgs e)
        {
            using (new BAL.Forms.WaitCursor(this))
            {
                this.pozycjeBindingSource.Clear();
                this.pozycjeAnalizyZwrotuGrid.ClearDokumenty();
                this.Refresh();
                this.analizaZwrotu.Analizuj();
                this.analizaZwrotu.Koryguj();
                this.loadPozycje();
            }
        }

        private void loadPozycje()
        {
            this.pozycjeBindingSource.Clear();
            this.pozycjeAnalizyZwrotuGrid.SetDokumenty(this.analizaZwrotu.Dokumenty);
            this.pozycjeBindingSource.DataSource = this.analizaZwrotu.Pozycje;
            this.pozycjeAnalizyZwrotuGrid.Refresh();
        }

        private void wystawButton_Click(object sender, EventArgs e)
        {
            this.Close();
            var form = new WystawKorektyForm()
            {
                AnalizaZwrotu = this.analizaZwrotu
            };

            var result = form.ShowDialog();
        }

        #endregion



    }
}
