using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using DBWeb = Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

[assembly: MenuAction("Magazyn\\Proponowane dostępne/niedostępne",
    MenuAction = MenuActionsType.OpenView,
    ViewType = typeof(AbakTools.Towary.Forms.MagazynAVView),
    Options = ActionOptions.WithoutSession,
    Priority = 820,
    Data = "Title=Dostępność towarów;Key=MagazynAVView")
]
[assembly: MenuAction("Magazyn\\Lista towarów i atrybutów",
    MenuAction = MenuActionsType.OpenView,
    ViewType = typeof(AbakTools.Towary.Forms.MagazynAVView),
    Options = ActionOptions.WithoutSession,
    Priority = 810,
    Data = "ExtraPanelAvailable=true;ExtraPanelVisible=true;Title=Towary atrybuty;Key=TowaryAtrybutyListaView")
]

namespace AbakTools.Towary.Forms
{
    public class MagazynAVView : TowaryAtrybutyView
    {
        #region Fields


        #endregion

        #region Properties

        #endregion

        #region Methods

        public MagazynAVView(BAL.Forms.IMenuItem menuItem) : base(menuItem) { }

        public MagazynAVView() : this(null) { }

        /*
        public override string GetDefaultXmlDefinition()
        {
            return AbakTools.Handel.Forms.Properties.Resources.MagazynAVView_grid;
        }
         */

        #endregion
    }
}
