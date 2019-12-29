using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;

/*
[assembly: MenuAction("Kartoteki\\Towary NEW", MenuAction = MenuActionsType.OpenView, ViewType = typeof(Enova.Forms.Towary.TowaryView),
    Options = ActionOptions.WithoutSession, Priority = 530)]

namespace Enova.Forms.Towary
{
    public class TowaryView : Enova.Forms.GridViewWithEnovaApi<Enova.API.Towary.Towar>
    {
        #region Fields

        private string key;

        #endregion

        #region Properties

        public override string Key
        {
            get
            {
                return key + (SelectionMode ? "Select" : "");
            }
        }

        #endregion

        #region Methods

        public TowaryView(string key)
        {
            this.key = key;
        }

        public TowaryView() : this("TowaryEnovaView") { }

        public override string GetTitle()
        {
            return "Towary enova";
        }

        protected override API.Business.Table<API.Towary.Towar> CreateTable(API.Business.Session session)
        {
            return session.GetModule<Enova.API.Towary.TowaryModule>().Towary;
        }

        #endregion
    }
}
 */
