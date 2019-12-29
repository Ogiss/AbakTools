using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Forms;

namespace AbakTools.Web
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
