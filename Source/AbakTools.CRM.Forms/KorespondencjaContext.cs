using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.Core;
using DBWeb = Enova.Business.Old.DB.Web;

[assembly: DataContext(typeof(DBWeb.Korespondencja), typeof(AbakTools.CRM.Forms.KorespondencjaContext))]

namespace AbakTools.CRM.Forms
{
    public class KorespondencjaContext : AbakTools.Forms.DataContextBase
    {
        #region Methods

        #endregion
    }
}
