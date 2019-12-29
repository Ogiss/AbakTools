using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Security.Permissions;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

//[assembly: MenuAction("Zwroty", MenuAction = MenuActionsType.OpenView, ViewType = typeof(AbakTools.Forms.Zwroty.ZwrotyView), Options = ActionOptions.WithoutSession, Priority = 900)]

namespace AbakTools.Forms.Zwroty
{
    public class ZwrotyView : View
    {
        #region Fields
        #endregion

        #region Properties

        public override string Key
        {
            get
            {
                return "ZwrotyView";
            }
        }

        public override System.Collections.IList Rows
        {
            get
            {
                return new ArrayList();
            }
        }

        #endregion

        #region Methods

        public override string GetTitle()
        {
            return "Zwroty";
        }

        public override Type GetDataType()
        {
            return typeof(Zwrot);
        }

        #endregion
    }
}
