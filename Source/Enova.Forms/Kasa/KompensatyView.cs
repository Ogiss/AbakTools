using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Extensions;
using BAL.Forms;

[assembly: MenuAction("Finanse\\Kompensaty", MenuAction = MenuActionsType.OpenView, ViewType = typeof(Enova.Forms.Kasa.KompensatyView),
    Options = ActionOptions.WithoutSession, Priority = 380)]

namespace Enova.Forms.Kasa
{
    //public class KompensatyView : DokumentyViewBase<API.Kasa.DokRozlicz>
    public class KompensatyView : DokumentyViewBase<API.Kasa.DokRozliczBase>
    {
        #region Properties

        public override string Key
        {
            get
            {
                return "KompensatyView";
            }
        }

        public override bool ReadOnly
        {
            get
            {
                return true;
            }
        }
        

        #endregion

        #region Methods

        //protected override API.Business.Table<API.Kasa.DokRozlicz> CreateTable(API.Business.Session session)
        protected override API.Business.Table<API.Kasa.DokRozliczBase> CreateTable(API.Business.Session session)
        {
            return session.GetModule<API.Kasa.KasaModule>().DokRozliczeniowe;
        }

        
        public override Type GetDataType()
        {
            //return typeof(API.Kasa.IKompensata);
            return typeof(API.Kasa.Kompensata);
        }

        public override string GetTitle()
        {
            return "Kompensaty";
        }

        protected override System.Collections.IList GetRows()
        {
            using (var s = Service.CreateSession())
            {
                var table = s.GetModule<Enova.API.Kasa.KasaModule>().DokRozliczeniowe;
                var fromTo = API.Types.FromTo.Create(fromToControls.FromTo.From, fromToControls.FromTo.To);
                var list = table.WgTypDokumentu(API.Core.TypDokumentu.Kompensata, fromTo, base.kotrahentSelect.Value as Enova.API.CRM.Kontrahent);
                /*
                if (this.IsSorted && this.SortProperty != null)
                    list.Sort(new DokHandlowyComparer(this.SortProperty, this.SortDirection));
                 */
                return (System.Collections.IList)list;
            }

        }
        
        public override string GetDefaultXmlDefinition()
        {
            return Properties.Resources.KompensatyView_grid;
        }

        #endregion
    }
}
