using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.Core;
using DBWeb = Enova.Business.Old.DB.Web;

/*
[assembly: DataContext(typeof(DBWeb.StanMagazynowyView), typeof(AbakTools.Towary.Forms.StanMagazynuViewContext))]
[assembly: DataContext(typeof(DBWeb.StanMagazynowy), typeof(AbakTools.Towary.Forms.StanMagazynuViewContext))]

namespace AbakTools.Towary.Forms
{
    public class StanMagazynuViewContext : DataContext
    {
        #region Properties

        new public DBWeb.WebContext DBContext
        {
            get
            {
                return ((StanyMagazynoweView)Parent).DBContext;
            }
        }

        #endregion

        #region Methods

        public StanMagazynuViewContext() : base(null, true) { }


        public override void SetData(Type dataType, object data)
        {
            var view = data as DBWeb.StanMagazynowyView;
            if (view != null)
            {
                var row = view.GetStanMagazynowy(DBContext);
                SetData(typeof(DBWeb.StanMagazynowy), row);
                AddData(data);
            }
            else if (data.GetType().IsAssignableFrom(typeof(DBWeb.StanMagazynowy)))
            {
                base.SetData(typeof(DBWeb.StanMagazynowy), data);
            }
            else
                base.SetData(dataType, data);
        }

        public override string GetTitle()
        {
            return "Edycja stanu magazynowego";
        }

        #endregion
    }
}
*/