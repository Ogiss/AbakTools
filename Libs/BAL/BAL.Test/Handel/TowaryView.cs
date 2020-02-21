using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Business;
using BAL.Forms;

[assembly: BAL.Forms.MenuAction("Kartoteki\\Towary view", MenuAction = BAL.Forms.MenuActionsType.OpenViewModal, ViewType = typeof(BAL.Test.Handel.TowaryView))]

namespace BAL.Test.Handel
{
    public class TowaryView : GridViewContext
    {
        #region Fields

        private Kategoria kategoria;

        #endregion


        #region Properties

        public override string Key
        {
            get
            {
                return "TowaryView";
            }
        }

        public override bool ExtraPanelAvailable
        {
            get
            {
                return true;
            }
        }

        public override bool ExtraPanelVisible
        {
            get
            {
                return true;
            }
        }

        public override ITable Table
        {
            get
            {
                if (this.kategoria != null)
                    return ((Towary)base.Table).WgKategoria[this.kategoria];
                return base.Table;
            }
        }

        #endregion

        #region Methods

        public TowaryView(Session session)
            : base(session.Tables["Towary"])
        {
        }

        public override string GetDefaultXmlDefinition()
        {
            return Properties.Resources.TowaryView_grid;
        }

        public override void InitExtraPanel(System.Windows.Forms.Panel panel)
        {
            TreeView tree = new TreeView();
            tree.Nodes.Add("1");
            tree.Nodes.Add("2");
            tree.Nodes.Add("3");
            tree.Dock = DockStyle.Fill;
            panel.Controls.Add(tree);
        }

        /*
        public override System.Collections.Generic.IEnumerable<BAL.Business.DataContextParam> GetParams()
        {
            return new DataContextParam[]
            {
                new DataContextParam("kategoriaParam","Kategoria", Types.PropertyPath.Create<Towar>("Kategoria"))
            };
        }

        protected override void OnParamValueChanged(DataContextParamEventArgs e)
        {
            if (e.Param.Name == "kategoriaParam")
            {
                this.kategoria = (Kategoria)((IValue)e.Control).Value;
                this.Reload();
            }
        }
         */

        #endregion
    }
}
