using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace AbakTools.Zwroty.Forms
{
    public partial class TowaryNieskorygowaneForm : Form
    {
        public TowaryNieskorygowaneForm()
        {
            InitializeComponent();
        }

        private void TowaryNieskorygowaneForm_Load(object sender, EventArgs e)
        {
            this.loadDostawcy();
            this.loadPrzedstawiciele();
        }

        private void loadDostawcy()
        {
            var ds = new List<Enova.Business.Old.DB.Web.Dostawca>() { new Enova.Business.Old.DB.Web.Dostawca() { ID = 0, Nazwa = "(Wszyscy)" } };
            ds.AddRange(Enova.Business.Old.Core.ContextManager.WebContext.Dostawcy.OrderBy(d => d.Nazwa).ToList());

            dostawcyBindingSource.DataSource = ds;
            dostawcyComboBox.SelectedIndex = 0;
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
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;

            var dostawca = (Dostawca)this.dostawcyComboBox.SelectedItem;
            var przedstawiciel = (Kontrahent)this.przedstawicielComboBox.SelectedItem;
            var dc = Enova.Business.Old.Core.ContextManager.WebContext;

            var query = dc.PozycjaZwrotow.Where(p => p.Zwrot.OstatniStatus.Typ == (int)TypStatusuZwrotu.Sprawdzony
                && p.Deleted == false && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete);

            if (dostawca != null && dostawca.ID > 0)
                query = query.Where(p => p.Towar.DostawcaID == dostawca.ID);

            if (przedstawiciel != null && przedstawiciel.ID > 0)
                query = query.Where(p => p.Zwrot.Kontrahent.Przedstawiciel.ID == przedstawiciel.ID);

            List<ReportRow> rows = null;

            if (this.agregujCheckBox.Checked)
            {
                rows = (from p in query
                        group p by p.TowarID into g
                        select new ReportRow()
                        {
                            String1 = g.FirstOrDefault().TowarNazwa,
                            Double1 = g.Sum(pg=>pg.Ilosc),
                            Decimal1 = g.FirstOrDefault().Cena
                        }).OrderBy(p => p.String1).ToList();
            }
            else
            {
                rows = query.Select(p => new ReportRow()
                {
                    String1 = p.TowarNazwa,
                    String2 = p.Zwrot.Kontrahent.Kod,
                    Int1 = p.ZwrotID,
                    Double1 = p.Ilosc,
                    Decimal1 = p.Cena
                }).OrderBy(p => p.String1).ToList();
            }

            this.Enabled = true;
            this.Cursor = Cursors.Default;

            if (rows != null)
            {
                var form = new AbakTools.Printer.ReportForm();
                form.SetReport("Towary do skorygowania",
                    this.agregujCheckBox.Checked ? "Reports\\TowaryDoSkorygowania.rdlc" : "Reports\\TowaryDoSkorygowaniaII.rdlc",
                    new ReportDataSource[]{
                        new ReportDataSource("Rows", rows)
                    }
                 );

                form.ShowDialog();
            }

        }


    }
}
