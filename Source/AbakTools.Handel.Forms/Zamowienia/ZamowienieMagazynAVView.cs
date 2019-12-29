using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbWeb=Enova.Business.Old.DB.Web;

namespace AbakTools.Zamowienia.Forms
{
    public class ZamowienieMagazynAVView : AbakTools.Towary.Forms.TowaryAtrybutyView
    {
        #region Fields

        private DbWeb.Zamowienie zamowienie;

        #endregion

        #region Properties

        public DbWeb.Zamowienie Zamowienie
        {
            get
            {
                return this.zamowienie;
            }
        }

        public override DbWeb.WebContext DbContext
        {
            get
            {
                if (this.zamowienie != null && this.zamowienie.DbContext != null)
                    return this.zamowienie.DbContext;
                return base.DbContext;
            }
        }

        #endregion

        #region Methods

        public override string Key
        {
            get
            {
                return "ZamowienieMagazynAVView";
            }
        }

        #endregion

        #region Methods

        public ZamowienieMagazynAVView(DbWeb.Zamowienie zamowienie)
        {
            this.zamowienie = zamowienie;
        }

        public override string GetTitle()
        {
            return "Weryfikacja dostepności towarów";
        }

        protected override Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.TowarAtrybut> CreateTable()
        {
            return new Enova.Business.Old.Core.TableBase<DbWeb.TowarAtrybut>((IEnumerable<DbWeb.TowarAtrybut>)this.zamowienie.GetTowaryAtrybutyDlaZmienionychIlosci());
        }

        public override string GetDefaultXmlDefinition()
        {
            return AbakTools.Handel.Forms.Properties.Resources.ZamowienieMagazynAVView_grid;
        }

        #endregion
    }
}
