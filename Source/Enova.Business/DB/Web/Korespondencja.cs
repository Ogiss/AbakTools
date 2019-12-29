using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.Core;
using Microsoft.Reporting.WinForms;
using System.Data;

namespace Enova.Business.Old.DB.Web
{
    [DataEditForm("AbakTools.CRM.Forms.KorespondencjaEditForm, AbakTools.CRM.Forms")]
    public partial class Korespondencja : ISaveChanges, IDeleteRecord, IUndoChanges, IPrintable
    {
        public string AdresPelny
        {
            get
            {
                return Adres + ", " + KodPocztowy + " " + Miejscowosc;
            }
        }

        public string RodzajNazwa
        {
            get
            {
                if (RodzajKorespondencji != null)
                    return RodzajKorespondencji.Nazwa;
                return null;
            }
        }

        public decimal? RodzajKoszt
        {
            get
            {
                if (RodzajKorespondencji != null)
                    return RodzajKorespondencji.KosztZnaczkow;
                return null;
            }
        }


        #region Record Edit Implementation

        public bool SaveChanges()
        {
            if (EntityState == System.Data.EntityState.Detached)
                Enova.Business.Old.Core.ContextManager.WebContext.AddToRejestrKorespondencji(this);

            Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();
            return true;
        }

        public bool DeleteRecord()
        {
            if (this.EntityState != System.Data.EntityState.Detached && this.EntityState != System.Data.EntityState.Deleted)
            {
                Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(this);
                Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();
            }
            return true;
        }

        public bool UndoChanges()
        {
            if (EntityState == System.Data.EntityState.Added)
            {
                Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(this);
            }
            else if (EntityState == System.Data.EntityState.Modified)
            {
                Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
            }
            return true;
        }

        #endregion

        #region IPrintable Implementation

        string IPrintable.ReportTitle
        {
            get { return "Nadruk koperty"; }
        }

        string IPrintable.ReportPath
        {
            get { return "Reports\\NadrukKoperty.rdlc"; }
        }

        IEnumerable<ReportDataSource> IPrintable.ReportDataSources
        {
            get
            {
                return new List<ReportDataSource>()
                {
                    new ReportDataSource("Korespondencja", new List<Korespondencja>(){this})
                    
                }.AsEnumerable<ReportDataSource>();
            }
        }

        #endregion
    }
}
