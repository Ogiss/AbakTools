using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;

[assembly: BAL.Forms.MenuAction("Konfiguracja\\Operatorzy", 
    MenuAction = MenuActionsType.OpenViewModal,
    ViewType = typeof(AbakTools.Business.Forms.OperatorzyView),
    Priority = 1110,
    Options=ActionOptions.WithoutSession)]

namespace AbakTools.Business.Forms
{
    public class OperatorzyView : AbakTools.Forms.GridViewBaseWithDbContext<Enova.Business.Old.DB.Web.Operator>
    {
        #region Properties

        public override string Key
        {
            get
            {
                return "OperatorzyView";
            }
        }

        #endregion

        #region Methods

        protected override Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.Operator> CreateTable()
        {
            return new Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.Operator>(DbContext.Operatorzy);
        }
        

        public override string GetTitle()
        {
            return "Operatorzy";
        }

        public override string GetDefaultXmlDefinition()
        {
            return AbakTools.Business.Forms.Properties.Resources.OperatorzyView_grid;
        }

        #endregion
    }
}
