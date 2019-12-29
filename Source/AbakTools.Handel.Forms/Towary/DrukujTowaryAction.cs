using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

[assembly: RowAction(typeof(AbakTools.Handel.Forms.Towary.DrukujTowaryAction), DataContextKey="TowaryAtrybutyListaView") ]

namespace AbakTools.Handel.Forms.Towary
{
    [Caption("Drukuj")]
    public class DrukujTowaryAction
    {
        #region Properties

        public ActionTarget Target
        {
            get { return ActionTarget.GridToolbar; }
        }

        public Image Image
        {
            get
            {
                return (Image)BAL.Forms.Properties.Resources.Print;
            }
        }

        public DataGridFormOld ParentForm { get; set; }

        #endregion

        #region Methods

        public void OnAction()
        {
            bool flag = false;
            if (BAL.Forms.FormManager.Confirm("Czy wydrukować tylko zaznaczone wiersze?"))
                flag = true;

            //TowaryAtrybutyListaReport.rdlc

            List<Enova.Business.Old.DB.Web.TowarAtrybut> ds = new List<Enova.Business.Old.DB.Web.TowarAtrybut>();

            if (flag)
                foreach (DataGridViewRow row in ParentForm.GridView.SelectedRows)
                    ds.Add((Enova.Business.Old.DB.Web.TowarAtrybut)row.DataBoundItem);
            else
                foreach (DataGridViewRow row in ParentForm.GridView.Rows)
                    ds.Add((Enova.Business.Old.DB.Web.TowarAtrybut)row.DataBoundItem);

            var asm = Assembly.LoadFrom("AbakTools.Handel.Forms.dll");
            System.IO.Stream stream = asm.GetManifestResourceStream("AbakTools.Handel.Forms.Reports.TowaryAtrybutyListaReport.rdlc");


            var form = new AbakTools.Printer.ReportForm("Towary lista", stream,
                new ReportDataSource[]{
                    new ReportDataSource("TowaryAtrybuty", ds)
                });

            form.Show();

        }

        #endregion
    }
}
