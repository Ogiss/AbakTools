using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public partial class StatusDokumentu : Enova.Business.Old.IIsLive, Enova.Business.Old.IDbContext
    {
        #region Fields

        private IQueryable<RelacjaStatDok> nadrzedneRelacje;
        private IQueryable<RelacjaStatDok> podrzedneRelacje;

        #endregion

        #region Properties

        public bool IsLive { get; set; }

        public System.Data.Objects.ObjectContext DbContext { get; set; }

        public AbakTools.Core.OpcjeStatusuDokumentu Opcje
        {
            get { return (AbakTools.Core.OpcjeStatusuDokumentu)OpcjeInt; }
            set { OpcjeInt = (int)value; }
        }

        public AbakTools.Core.AlgorytmStatusuDokumentu Algorytm
        {
            get { return (AbakTools.Core.AlgorytmStatusuDokumentu)AlgorytmInt; }
            set { AlgorytmInt = (int)value; }
        }

        public IQueryable<RelacjaStatDok> NadrzedneRelacje
        {
            get
            {
                if (this.IsLive && this.nadrzedneRelacje == null)
                    nadrzedneRelacje = ((Enova.Business.Old.DB.Web.WebContext)DbContext).RelacjeStatDok.Where(r => r.Podrzedny.ID == this.ID);
                return this.nadrzedneRelacje;
            }
        }

        public IQueryable<RelacjaStatDok> PodrzedneRelacje
        {
            get
            {
                if (this.podrzedneRelacje == null && this.IsLive)
                    podrzedneRelacje = ((Enova.Business.Old.DB.Web.WebContext)DbContext).RelacjeStatDok.Where(r => r.Nadrzedny.ID == this.ID);
                return podrzedneRelacje;
            }
        }

        public IEnumerable<StatusDokumentu> Nadrzedne
        {
            get
            {
                return this.IsLive ? NadrzedneRelacje.Select(r => r.Nadrzedny) : null;
            }
        }

        public IEnumerable<StatusDokumentu> Podrzedne
        {
            get
            {
                return this.IsLive ? PodrzedneRelacje.Select(r => r.Podrzedny) : null;
            }
        }

        #endregion

        #region Methods

        public RelacjaStatDok DodajPodrzedny(StatusDokumentu podrzedny)
        {
            if (this.IsLive && !this.Podrzedne.Any(r => r.ID == podrzedny.ID))
            {
                var rel = new RelacjaStatDok()
                {
                    Nadrzedny = this,
                    Podrzedny = podrzedny
                };
                ((Enova.Business.Old.DB.Web.WebContext)DbContext).RelacjeStatDok.AddObject(rel);
                return rel;
            }
            return null;
        }

        public RelacjaStatDok DodajNadrzedny(StatusDokumentu nadrzedny)
        {
            if (this.IsLive && !this.Nadrzedne.Any(r => r.ID == nadrzedny.ID))
            {
                var rel = new RelacjaStatDok()
                {
                    Nadrzedny = nadrzedny,
                    Podrzedny = this
                };
                ((Enova.Business.Old.DB.Web.WebContext)DbContext).RelacjeStatDok.AddObject(rel);
                return rel;
            }
            return null;
        }

        public override string ToString()
        {
            return this.Nazwa;
        }


        #endregion

    }
}
