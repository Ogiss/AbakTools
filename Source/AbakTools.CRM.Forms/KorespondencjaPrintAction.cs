using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Forms;
using DBWeb = Enova.Business.Old.DB.Web;

[assembly: Action(Path = "Drukuj", DataType = typeof(DBWeb.Korespondencja), ActionType = typeof(AbakTools.CRM.Forms.KorespondencjaPrintAction), Target = ActionTarget.FormMenu)]

namespace AbakTools.CRM.Forms
{
    public class KorespondencjaPrintAction : BAL.Business.IDataContexable
    {

        public BAL.Business.DataContext DataContext { get; set; }

        public void Action()
        {
            if (DataContext != null)
            {
                Enova.Business.Old.Core.IPrintable p = (Enova.Business.Old.Core.IPrintable)DataContext.Current;
                new AbakTools.Printer.ReportForm(p.ReportTitle, p.ReportPath, p.ReportDataSources).Show();
            }
        }

    }
}
