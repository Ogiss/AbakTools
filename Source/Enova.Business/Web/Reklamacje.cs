using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Web
{
    public class Reklamacje : Enova.Business.Old.Core.TableBase<DB.Web.Reklamacja>
    {
        #region Fields

        public DateTime? DateFrom;
        public DateTime? DateTo;
        public string PrzedstawicielKod;
        public DB.Web.Kontrahent Kontrahent;

        #endregion

        #region Properies

        public override System.Data.Objects.ObjectQuery<DB.Web.Reklamacja> BaseQuery
        {
            get
            {
                int flag = (int)AbakTools.Core.OpcjeStatusuDokumentu.ZawszeWidoczny;
                var from = DateFrom == null ? (DateTime?)null : DateFrom.Value.Date;
                var to = DateTo == null ? (DateTime?)null : DateTo.Value.Date.AddDays(1);
                var dc = (DB.Web.WebContext)DataContext;
                IQueryable<DB.Web.Reklamacja> q = dc.Reklamacje.Where(r => r.Deleted == false && (((from == null || r.DataDodania >= from) && (to == null || r.DataDodania < to)) ||
                    (r.OstatniStatusDokumentu != null && (r.OstatniStatusDokumentu.OpcjeInt & flag) == flag)));
                if (Kontrahent != null)
                    q = q.Where(r => r.KontrahentID == Kontrahent.ID);
                else if (!string.IsNullOrEmpty(PrzedstawicielKod))
                    q = q.Where(r => r.Kontrahent.Przedstawiciel.Kod == PrzedstawicielKod);

                return (System.Data.Objects.ObjectQuery<DB.Web.Reklamacja>)q;
            }
            set
            {
                base.BaseQuery = value;
            }
        }

        #endregion

        #region Methods

        public Reklamacje(DB.Web.WebContext dbContext):base(dbContext,"Reklamacje")
        {
        }

        #endregion
    }
}
