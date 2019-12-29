using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Business;
using BAL.Forms;

//[assembly: MenuAction("Konfiguracja\\Statusy", MenuAction= MenuActionsType.OpenViewModal, ViewType = typeof(AbakTools.Core.Forms.StatusyDokumentowView), Priority = 1200)]
[assembly: MenuAction("Konfiguracja\\Statusy", MenuAction = MenuActionsType.OpenViewModal, ViewType = typeof(AbakTools.Core.Forms.StatusyDokumentowView), Priority = 1200, Options = ActionOptions.WithoutSession)]

namespace AbakTools.Core.Forms
{
    //public class StatusyDokumentowView : AbakTools.Forms.GridViewBase
    public class StatusyDokumentowView : AbakTools.Forms.GridViewBaseWithDbContext<Enova.Business.Old.DB.Web.StatusDokumentu>
    {
        #region Fields

        private ComboBox kategoriaComboBox;

        #endregion

        #region Properties

        /*
        public override ITable Table
        {
            get
            {
                if (kategoriaComboBox != null && kategoriaComboBox.SelectedItem != null)
                    return ((StatusyDokumentow)base.Table).WgKategorii[kategoriaComboBox.SelectedItem.ToString()];
                return base.Table;
            }
        }
         */

        public override string Key
        {
            get
            {
                return "StatusyDokumentow" + (SelectionMode ? "Select" : "") + "View";
            }
        }

        #endregion

        #region Methods

        /*
        public StatusyDokumentowView(Session session)
            : base(session.Tables["StatusyDokumentow"])
        {
        }
         */

        protected override Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.StatusDokumentu> CreateTable()
        {
            var kategoria = kategoriaComboBox != null ? kategoriaComboBox.SelectedItem.ToString() : null;
            return new Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.StatusDokumentu>(DbContext.StatusyDokumentow.Where(r => kategoria == null || r.Kategoria == kategoria));
        }

        public override string GetTitle()
        {
            return "Statusy";
        }

        public override string GetDefaultXmlDefinition()
        {
            return Properties.Resources.StatusyDokumentow_grid;
        }

        public override System.Collections.Generic.IEnumerable<BAL.Business.DataContextParam> GetParams()
        {
            return new DataContextParam[]
            {
                new DataContextParam("kategoriaParam","Kategoria:", PropertyPath.Create<Enova.Business.Old.DB.Web.StatusDokumentu>("Kategoria")){ ControlType = typeof(System.Windows.Forms.ComboBox) }
            };
        }

        protected override void OnInitParam(DataContextParamEventArgs e)
        {
            switch (e.Param.Name)
            {
                case "kategoriaParam":
                    kategoriaComboBox = (System.Windows.Forms.ComboBox)e.Control;
                    //kategoriaComboBox.DataSource = CoreModule.GetInstance(this).KategorieStatusowDokumentow;
                    kategoriaComboBox.DataSource = new AbakTools.Core.KategorieStatusowDokumentow();
                    kategoriaComboBox.SelectedIndex = 0;
                    kategoriaComboBox.SelectionChangeCommitted += (sender, args) => { this.Reload(); };
                    break;
            }
            base.OnInitParam(e);
        }

        #endregion
    }
}
