using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Forms;
using AbakTools.Handel;
using AbakTools.Towary;
using DBWeb = Enova.Business.Old.DB.Web;
/*
[assembly: DataPanel("Ogólne", typeof(DBWeb.StanMagazynowyView), typeof(AbakTools.Towary.Forms.StanMagazynuOgolnePanel))]
[assembly: DataPanel("Ogólne", typeof(DBWeb.StanMagazynowy), typeof(AbakTools.Towary.Forms.StanMagazynuOgolnePanel))]

namespace AbakTools.Towary.Forms
{
    public partial class StanMagazynuOgolnePanel : BAL.Forms.DataPanel
    {
        private bool saved;

        public StanMagazynuOgolnePanel()
        {
            InitializeComponent();
        }

        private void StanMagazynuOgolnePanel_Load(object sender, EventArgs e)
        {
            rodzajZmianyComboBox.SelectedIndex = 0;
            this.ActiveControl = zmianaTextBox;
            zmianaTextBox.Focus();

        }

        public override void EndEdit()
        {
            base.EndEdit();

            if (saved)
                return;

            DBWeb.StanMagazynowy stan = DataContext.Current as DBWeb.StanMagazynowy;

            ((DBWeb.WebContext)stan.DbContext).OptimisticSaveChanges();

            if (!string.IsNullOrEmpty(zmianaTextBox.Text))
            {
                double d = 0;
                DBWeb.RodzajZmianyStanu rodzaj = (DBWeb.RodzajZmianyStanu)rodzajZmianyComboBox.SelectedIndex;
                if (double.TryParse(zmianaTextBox.Text, out d) && (d != 0 || rodzaj == DBWeb.RodzajZmianyStanu.Ustaw) && rodzajZmianyComboBox.SelectedIndex >= 0)
                {
                    double stanMag = 0;
                    stan.ZmienStan(d, rodzaj, out stanMag);
                    var towar = stan.Towar;
                    if (towar != null)
                    {
                        using (var ec = new Enova.Business.Old.DB.EnovaContext("name=EnovaContext"))
                        {
                            if (towar.GetOgraniczenieSprzedazyWlaczone(ec))
                                towar.SetOgraniczenieSprzedazyStan(ec, (int)stanMag);
                        }
                        using (var lc = new Enova.Business.Old.DB.Web.WebContext("name=WebContext"))
                        {
                            if (stanMag > 0 && !towar.Dostepny)
                            {
                                towar.Dostepny = true;
                                lc.OptimisticSaveChanges();
                            }
                            else if (stanMag <= 0 && towar.Dostepny)
                            {
                                towar.Dostepny = false;
                                lc.OptimisticSaveChanges();
                            }
                        }
                    }
                }
                saved = true;
            }
        }
    }
}
*/