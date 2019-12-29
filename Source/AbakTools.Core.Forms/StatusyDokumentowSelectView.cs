using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace AbakTools.Core.Forms
{
    //public class StatusyDokumentowSelectView : AbakTools.Forms.GridViewBase
    public class StatusyDokumentowSelectView : AbakTools.Forms.GridViewBaseWithDbContext<Enova.Business.Old.DB.Web.StatusDokumentu>
    {
        #region Fields

        private string kategoria;

        #endregion

        #region Properties

        public override string Key
        {
            get
            {
                return "StatusyDokumentowSelectView";
            }
        }


        #endregion


        #region Methods

        public StatusyDokumentowSelectView(string kategoria)
        {
            this.SelectionMode = true;
            this.kategoria = kategoria;
        }

        /*
        public StatusyDokumentowSelectView(Session session, string kategoria)
            :base(CoreModule.GetInstance(session).StatusyDokumentow.WgKategorii[kategoria])
        {
            this.SelectionMode = true;
            this.kategoria = kategoria;
        }
         */

        protected override Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.StatusDokumentu> CreateTable()
        {
            return new Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.StatusDokumentu>(DbContext.StatusyDokumentow.Where(r => kategoria == null || kategoria == "" || r.Kategoria == kategoria));
        }
        

        public override string GetTitle()
        {
            return "Wybór statusu dokumentu - Kategoria: " + kategoria;
        }

        protected override void OnParentFormChanged(EventArgs e)
        {
            ParentForm.Width = 600;
            base.OnParentFormChanged(e);
        }

        public override string GetDefaultXmlDefinition()
        {
            return Properties.Resources.StatusyDokumentowSelectView_grid;
        }

        #endregion
    }
}
