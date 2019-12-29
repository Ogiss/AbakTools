using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API = Enova.API;

namespace Enova.Forms
{
    public abstract class GridViewWithEnovaApi<T> : BAL.Forms.GridViewContext
        where T : Enova.API.Business.Row
    {
        #region Fields
        #endregion

        #region Properties

        public API.EnovaService Service
        {
            get { return API.EnovaService.Instance; }
        }

        #endregion

        #region Methods

        public GridViewWithEnovaApi(string key = null) : base((BAL.Business.ITable)null, key) { }

        protected abstract Enova.API.Business.Table<T> CreateTable(Enova.API.Business.Session session);

        protected override System.Collections.IList GetRows()
        {
            /*
            if (ParentForm != null && ParentForm is API.Business.ISessionable)
                return CreateTable(((API.Business.ISessionable)ParentForm).Session).ToList();
             */

            using (var s = Service.CreateSession())
                return CreateTable(s).ToList();
        }

        public override Type GetDataType()
        {
            return typeof(T);
        }

        public override bool EditRow(object row)
        {
            bool ret = false;
            if (row is Enova.API.Business.Row)
            {
                using (var s = Service.CreateSession())
                {
                    var erow = this.CreateTable(s)[((Enova.API.Business.Row)row).ID];
                    using (var t = s.CreateTransaction())
                    {
                        if (base.EditRow(erow))
                        {
                            t.Commit();
                            ret = true;
                        }
                    }
                    if (ret)
                    {
                        s.Save();
                    }
                }
            }
            this.Reload();
            return ret;
        }

        #endregion
    }
}
