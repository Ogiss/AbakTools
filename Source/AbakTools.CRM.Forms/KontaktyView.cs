using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Forms;

[assembly: AbakTools.Business.DefaultView(typeof(Enova.Business.Old.DB.Web.Kontakt), typeof(AbakTools.CRM.Forms.KontaktyView))]

[assembly: BAL.Forms.MenuAction(
    "Narzędzia\\Książka kontaktowa",
    MenuAction = MenuActionsType.OpenView,
    ViewType = typeof(AbakTools.CRM.Forms.KontaktyView),
    Priority = 630,
    Options=ActionOptions.WithoutSession
)]

namespace AbakTools.CRM.Forms
{
    public class KontaktyView : AbakTools.Forms.GridViewBaseWithDbContext<Enova.Business.Old.DB.Web.Kontakt>
    {
        public bool TylkoTelefoniczne;

        protected override Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.Kontakt> CreateTable()
        {
            return new Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.Kontakt>(DbContext.Kontakty);
        }

        public override string GetTitle()
        {
            return "Kontakty";
        }

        public override string GetDefaultXmlDefinition()
        {
            return Properties.Resources.KontaktyView_grid;
        }
    }
}
