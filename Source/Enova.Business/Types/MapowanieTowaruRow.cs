using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Enova.Business.Old.Types
{
    [Core.DataEditForm("AbakTools.Towary.Forms.MapowanieTowaruEditForm, AbakTools.Handel.Forms")]
    public class MapowanieTowaruRow : Enova.Business.Old.Core.ISaveChanges, Enova.Business.Old.Core.IDeleteRecord
    {
        /*
        private Enova.Business.Old.DB.Towar src = null;
        private Enova.Business.Old.DB.Towar dst = null;

        public Enova.Business.Old.DB.Towar Src
        {
            get { return src; }
            set { this.src = value; }
        }
        public Enova.Business.Old.DB.Towar Dst
        {
            get { return this.dst; }
            set { this.dst = value; }
        }
         */

        public Enova.Business.Old.DB.Web.Produkt Src;
        public Enova.Business.Old.DB.Web.Produkt Dst;

        //public int SrcID { get { return this.Src != null ? this.Src.ID : 0; } }
        //public Guid SrcGuid { get { return this.Src != null ? this.Src.Guid : Guid.Empty; } }
        public Guid SrcGuid { get { return this.Src != null && this.Src.EnovaGuid!=null ? this.Src.EnovaGuid.Value : Guid.Empty; } }
        public string SrcKod { get { return this.Src != null ? this.Src.Kod : null; } }
        public string SrcNazwa { get { return this.Src != null ? this.Src.Nazwa : null; } }
        public string SrcFullName
        {
            get
            {
                return this.SrcKod + " - " + this.SrcNazwa;
            }
        }
        //public int DstID { get { return this.dst != null ? this.dst.ID : 0; } }
        public Guid DstGuid { get { return this.Dst != null && Dst.EnovaGuid!=null ? Dst.EnovaGuid.Value : Guid.Empty; } }
        public string DstKod { get { return this.Dst != null ? this.Dst.Kod : null; } }
        public string DstNazwa { get { return this.Dst != null ? this.Dst.Nazwa : null; } }
        public string DstFullName
        {
            get
            {
                return this.DstKod + " - " + this.DstNazwa;
            }
        }

        public bool SaveChanges()
        {
            var dc = Enova.Business.Old.Core.ContextManager.WebContext;
            var gm = dc.GuidMaps.Where(m => m.Zrodlo == this.SrcGuid && m.Tabela == "Towary").FirstOrDefault();

            if (gm == null)
            {
                gm = new DB.Web.GuidMap()
                {
                    Zrodlo = this.SrcGuid,
                    Cel = this.DstGuid,
                    Tabela = "Towary"
                };

                dc.AddToGuidMaps(gm);
                
            }
            else
            {
                if (gm.Cel != this.DstGuid)
                    gm.Cel = this.DstGuid;
            }

            if (gm != null && gm.EntityState == EntityState.Added || gm.EntityState == EntityState.Modified)
                dc.OptimisticSaveChanges();

            return true;
        }

        public bool DeleteRecord()
        {
            var dc = Enova.Business.Old.Core.ContextManager.WebContext;

            var gm = dc.GuidMaps.Where(m => m.Zrodlo == this.SrcGuid && m.Cel == this.DstGuid && m.Tabela == "Towary").FirstOrDefault();
            if (gm != null)
                dc.DeleteObject(gm);
            dc.OptimisticSaveChanges();
            return true;
        }
    }
}
