using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using BAL.Forms;

[assembly: DataContext(typeof(Enova.Business.Old.DB.Web.Magazyn_StanMagazynowy), typeof(AbakTools.Magazyn.Forms.StanMagazynowyContext))]

namespace AbakTools.Magazyn.Forms
{
    public class StanMagazynowyContext : AbakTools.Forms.DataContextBase
    {
        #region Properties

        public int? TowarID
        {
            get { return (int?)GetValue("TowarID"); }
            set { SetValue("TowarID", value); }
        }

        public Guid TowarGuid
        {
            get
            {
                var guid = (Guid?)GetValue("TowarGuid");
                return guid == null ? Guid.Empty : guid.Value;
            }
            set
            {
                SetValue("TowarGuid", value);
            }
        }

        public string TowarKod
        {
            get { return (string)GetValue("TowarKod"); }
            set { SetValue("TowarKod", value); }
        }

        public string TowarName
        {
            get { return (string)GetValue("TowarNazwa"); }
            set { SetValue("TowarNazwa", value); }
        }

        public int TypOpr
        {
            get { return (int)GetValue("TypOpr"); }
            set { SetValue("TypOpr", value); }
        }

        public double StanMag
        {
            get { return (double)GetValue("StanMag"); }
            set { SetValue("StanMag", value); }
        }

        #endregion

        #region Methods

        #endregion
    }
}
