using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.Types;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;

[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.ZamowioneTowaryAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{
    [Priority(100), Caption("Zamówione towary")]
    public class ZamowioneTowaryAction : ZamowieniaViewActionBase
    {
        #region Properties

        protected override bool ZawszeWidoczne
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        public void OnAction()
        {
            WybierzDateForm dataForm = new WybierzDateForm();
            DialogResult result = dataForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                DateTime data = dataForm.Data.AddDays(1);
                string pora = dataForm.PoraDnia;
                int dostawcaID = dataForm.Dostawca.ID;
                int przedstawicielID = dataForm.PrzedstawicielKontrahenta.ID;

                var pozycje = (from p in Enova.Business.Old.Core.ContextManager.WebContext.PozycjeZam(data, pora, dostawcaID, przedstawicielID,
                                   dataForm.NoweZamowienie, dataForm.DoMagazynu, dataForm.Blokada, dataForm.Wstrzymane, dataForm.Pakowane,
                                   dataForm.Spakowane, dataForm.Kurier, dataForm.Przedstawiciel, dataForm.Wyslane).ToList()
                               group p by new { Towar = p.Produkt.ID, Atrybut = p.AtrybutProduktu } into g
                               select new ReportRow()
                               {
                                   String1 = g.First().Kod,
                                   String2 = g.First().NazwaPełna,
                                   Decimal1 = (decimal)g.Sum(pz => pz.Ilosc)
                               });


                pozycje = (from p in pozycje
                           group p by new { s1 = p.String1, s2 = p.String2 } into g
                           select new ReportRow()
                           {
                               String1 = g.Key.s1,
                               String2 = g.Key.s2,
                               Decimal1 = (decimal)g.Sum(pz=>pz.Decimal1)
                           }).OrderBy(p => p.String1).ThenBy(p => p.String2);
                
                AbakTools.Printer.ReportForm form = new AbakTools.Printer.ReportForm();

                form.ReportViewer.LocalReport.ReportPath = "Reports\\ZamowioneTowaryReport.rdlc";
                form.ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportRow", pozycje));
                form.Show();
            }
        }

        #endregion
    }
}
